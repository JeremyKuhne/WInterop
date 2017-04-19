// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using WInterop.Desktop.Registry.DataTypes;
using WInterop.ErrorHandling;
using WInterop.ErrorHandling.DataTypes;
using WInterop.Support;
using WInterop.Support.Buffers;

namespace WInterop.Desktop.Registry
{
    /// <summary>
    /// These methods are only available from Windows desktop apps. Windows store apps cannot access them.
    /// </summary>
    public static partial class RegistryMethods
    {
        /// <summary>
        /// Direct P/Invokes aren't recommended. Use the wrappers that do the heavy lifting for you.
        /// </summary>
        /// <remarks>
        /// By keeping the names exactly as they are defined we can reduce string count and make the initial P/Invoke call slightly faster.
        /// </remarks>
        public static class Direct
        {
            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms724837.aspx
            [DllImport(Libraries.Advapi32, ExactSpelling = true)]
            public static extern WindowsError RegCloseKey(IntPtr hKey);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms724897.aspx
            [DllImport(Libraries.Advapi32, CharSet = CharSet.Unicode, ExactSpelling = true)]
            public static extern WindowsError RegOpenKeyExW(
                RegistryKeyHandle hKey,
                string lpSubKey,
                uint ulOptions,
                RegistryAccessRights samDesired,
                out RegistryKeyHandle phkResult);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms724911.aspx
            [DllImport(Libraries.Advapi32, CharSet = CharSet.Unicode, ExactSpelling = true)]
            public unsafe static extern WindowsError RegQueryValueExW(
                RegistryKeyHandle hKey,
                string lpValueName,
                void* lpReserved,
                RegistryValueType* lpType,
                void* lpData,
                uint* lpcbData);

            // Performance for RegEnumValueW is suboptimal as it has to allocate an additional buffer
            // to call NtEnumerateValueKey. NtEnumerateValueKey is documented, but does not support
            // performance keys and automatic redirection of HKCR values to user overrides.

            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms724865.aspx
            [DllImport(Libraries.Advapi32, CharSet = CharSet.Unicode, ExactSpelling = true)]
            public unsafe static extern WindowsError RegEnumValueW(
                RegistryKeyHandle hKey,
                uint dwIndex,
                void* lpValueName,
                ref uint lpcchValueName,
                void* lpReserved,
                RegistryValueType* lpType,
                void* lpData,
                uint* lpcbData);

            [DllImport(Libraries.Ntdll, ExactSpelling = true)]
            public unsafe static extern NTSTATUS NtEnumerateValueKey(
                RegistryKeyHandle KeyHandle,
                uint Index,
                KEY_VALUE_INFORMATION_CLASS KeyValueInformationClass,
                void* KeyValueInformation,
                uint Length,
                out uint ResultLength);
        }

        /// <summary>
        /// Open the specified subkey.
        /// </summary>
        public static RegistryKeyHandle OpenKey(
            RegistryKeyHandle key,
            string subKeyName,
            RegistryAccessRights rights = RegistryAccessRights.KEY_READ)
        {
            WindowsError result = Direct.RegOpenKeyExW(key, subKeyName, 0, rights, out RegistryKeyHandle subKey);
            if (result != WindowsError.ERROR_SUCCESS)
                throw Errors.GetIoExceptionForError(result);

            return subKey;
        }

        /// <summary>
        /// Returns true if the given value exists.
        /// </summary>
        public unsafe static bool QueryValueExists(RegistryKeyHandle key, string valueName)
        {
            WindowsError result = Direct.RegQueryValueExW(key, valueName, null, null, null, null);
            switch (result)
            {
                case WindowsError.ERROR_SUCCESS:
                    return true;
                case WindowsError.ERROR_FILE_NOT_FOUND:
                    return false;
                default:
                    throw Errors.GetIoExceptionForError(result);
            }
        }

        /// <summary>
        /// Returns the type of the given value, or REG_NONE if it doesn't exist.
        /// </summary>
        public unsafe static RegistryValueType QueryValueType(RegistryKeyHandle key, string valueName)
        {
            RegistryValueType valueType = new RegistryValueType();
            WindowsError result = Direct.RegQueryValueExW(key, valueName, null, &valueType, null, null);
            switch (result)
            {
                case WindowsError.ERROR_SUCCESS:
                    return valueType;
                case WindowsError.ERROR_FILE_NOT_FOUND:
                    return RegistryValueType.REG_NONE;
                default:
                    throw Errors.GetIoExceptionForError(result);
            }
        }

        public unsafe static object QueryValue(RegistryKeyHandle key, string valueName)
        {
            return BufferHelper.CachedInvoke((HeapBuffer buffer) =>
            {
                RegistryValueType valueType = new RegistryValueType();

                WindowsError result;
                uint byteCapacity = (uint)buffer.ByteCapacity;
                while ((result = Direct.RegQueryValueExW(
                    key,
                    valueName,
                    null,
                    &valueType,
                    buffer.VoidPointer,
                    &byteCapacity)) != WindowsError.ERROR_SUCCESS)
                {
                    switch (result)
                    {
                        case WindowsError.ERROR_MORE_DATA:
                            // According to the docs, the byteCapacity given back will
                            // not be correct for HKEY_PERFORMANCE_DATA
                            byteCapacity = byteCapacity > buffer.ByteCapacity ? byteCapacity : checked(byteCapacity + 256);
                            buffer.EnsureByteCapacity(byteCapacity);
                            break;
                        case WindowsError.ERROR_FILE_NOT_FOUND:
                            return null;
                        default:
                            throw Errors.GetIoExceptionForError(result);
                    }
                }

                return ReadValue(buffer.VoidPointer, byteCapacity, valueType);
            });
        }

        /// <summary>
        /// Gets the key's value names directly from NtEnumerateValueKey. This is slightly faster
        /// and uses less memory, but only works for keys in the local machine registry and does
        /// not work with performance keys (such as HKEY_PERFORMANCE_DATA).
        /// 
        /// This also doesn't give the same results for "special" (HKCR) keys that are normally
        /// redirected to user specific settings by RegEnumValue.
        /// </summary>
        /// <param name="filterTo">
        /// Only return names that have the given type, or REG_NONE for all types.
        /// </param>
        /// <remarks>
        /// RegEnumValue doesn't map directly to NtEnumerateValueKey and requires allocating a
        /// temporary buffer for *each* invocation- making the direct call here avoids this extra
        /// buffer.
        /// </remarks>
        public unsafe static IEnumerable<string> GetValueNamesDirect(RegistryKeyHandle key, RegistryValueType filterTo = RegistryValueType.REG_NONE)
        {
            List<string> names = new List<string>();

            BufferHelper.CachedInvoke((HeapBuffer buffer) =>
            {
                NTSTATUS status;
                while((status = Direct.NtEnumerateValueKey(
                    key,
                    (uint)names.Count,
                    KEY_VALUE_INFORMATION_CLASS.KeyValueBasicInformation,
                    buffer.VoidPointer,
                    checked((uint)buffer.ByteCapacity),
                    out uint resultLength)) != NTSTATUS.STATUS_NO_MORE_ENTRIES)
                {
                    switch (status)
                    {
                        case NTSTATUS.STATUS_SUCCESS:
                            KEY_VALUE_BASIC_INFORMATION* info = (KEY_VALUE_BASIC_INFORMATION*)buffer.VoidPointer;
                            if (filterTo == RegistryValueType.REG_NONE || (*info).Type == filterTo)
                                names.Add(new string(&(*info).Name, 0, (int)(*info).NameLength / sizeof(char)));
                            break;
                        case NTSTATUS.STATUS_BUFFER_OVERFLOW:
                        case NTSTATUS.STATUS_BUFFER_TOO_SMALL:
                            buffer.EnsureByteCapacity(resultLength);
                            break;
                        default:
                            throw ErrorMethods.GetIoExceptionForNTStatus(status);
                    }
                }
            });

            return names;
        }

        /// <summary>
        /// Gets the key's value data directly from NtEnumerateValueKey. This is slightly faster
        /// and uses less memory, but only works for keys in the local machine registry and does
        /// not work with performance keys (such as HKEY_PERFORMANCE_DATA).
        /// 
        /// This also doesn't give the same results for "special" (HKCR) keys that are normally
        /// redirected to user specific settings by RegEnumValue.
        /// </summary>
        /// <param name="filterTo">
        /// Only return data of the given type, or REG_NONE for all types.
        /// </param>
        /// <remarks>
        /// RegEnumValue doesn't map directly to NtEnumerateValueKey and requires allocating a
        /// temporary buffer for *each* invocation- making the direct call here avoids this extra
        /// buffer.
        /// </remarks>
        public unsafe static IEnumerable<object> GetValueDataDirect(RegistryKeyHandle key, RegistryValueType filterTo = RegistryValueType.REG_NONE)
        {
            List<object> data = new List<object>();

            BufferHelper.CachedInvoke((HeapBuffer buffer) =>
            {
                NTSTATUS status;
                while ((status = Direct.NtEnumerateValueKey(
                    key,
                    (uint)data.Count,
                    KEY_VALUE_INFORMATION_CLASS.KeyValuePartialInformation,
                    buffer.VoidPointer,
                    checked((uint)buffer.ByteCapacity),
                    out uint resultLength)) != NTSTATUS.STATUS_NO_MORE_ENTRIES)
                {
                    switch (status)
                    {
                        case NTSTATUS.STATUS_SUCCESS:
                            KEY_VALUE_PARTIAL_INFORMATION* info = (KEY_VALUE_PARTIAL_INFORMATION*)buffer.VoidPointer;
                            if (filterTo == RegistryValueType.REG_NONE || (*info).Type == filterTo)
                                data.Add(ReadValue(&(*info).Data, (*info).DataLength, (*info).Type));
                            break;
                        case NTSTATUS.STATUS_BUFFER_OVERFLOW:
                        case NTSTATUS.STATUS_BUFFER_TOO_SMALL:
                            buffer.EnsureByteCapacity(resultLength);
                            break;
                        default:
                            throw ErrorMethods.GetIoExceptionForNTStatus(status);
                    }
                }
            });

            return data;
        }

        /// <summary>
        /// Gets all value names for the given registry key.
        /// </summary>
        public unsafe static IEnumerable<string> GetValueNames(RegistryKeyHandle key)
        {
            List<string> names = new List<string>();

            BufferHelper.CachedInvoke((StringBuffer buffer) =>
            {
                // Ensure we have enough space to hold the perf key values
                buffer.EnsureCharCapacity(10);
                uint bufferSize = buffer.CharCapacity;

                WindowsError result;
                while ((result = Direct.RegEnumValueW(key, (uint)names.Count, buffer.VoidPointer, ref bufferSize, null, null, null, null))
                    != WindowsError.ERROR_NO_MORE_ITEMS)
                {
                    switch (result)
                    {
                        case WindowsError.ERROR_SUCCESS:
                            buffer.Length = bufferSize;
                            names.Add(buffer.ToString());
                            break;
                        case WindowsError.ERROR_MORE_DATA:
                            if (key.IsPerfKey)
                            {
                                // Perf keys always report back ERROR_MORE_DATA,
                                // and also does not report the size of the string.
                                buffer.SetLengthToFirstNull();
                                names.Add(buffer.ToString());
                                bufferSize = buffer.CharCapacity;
                            }
                            else
                            {
                                // For some reason the name size isn't reported back,
                                // even though it is known. Why would they not do this?
                                buffer.EnsureCharCapacity(bufferSize + 100);
                                bufferSize = buffer.CharCapacity;
                            }
                            break;
                        default:
                            throw Errors.GetIoExceptionForError(result);
                    }
                }
            });

            return names;
        }

        private static unsafe object ReadValue(void* buffer, uint byteCount, RegistryValueType valueType)
        {
            switch (valueType)
            {
                case RegistryValueType.REG_SZ:
                case RegistryValueType.REG_EXPAND_SZ:
                case RegistryValueType.REG_LINK:
                    // Size includes the null
                    return new string((char*)buffer, 0, (int)(byteCount / sizeof(char)) - 1);
                case RegistryValueType.REG_MULTI_SZ:
                    return BufferHelper.SplitNullTerminatedStringList((IntPtr)buffer);
                case RegistryValueType.REG_DWORD:
                    return *(uint*)buffer;
                case RegistryValueType.REG_DWORD_BIG_ENDIAN:
                    byte* b = (byte*)buffer;
                    return (*b << 24) | (*(b + 1) << 16) | (*(b + 2) << 8) | (*(b + 3));
                case RegistryValueType.REG_QWORD:
                    return *(ulong*)buffer;
                case RegistryValueType.REG_BINARY:
                // The next three aren't defined yet, so we'll just return them as binary blobs
                case RegistryValueType.REG_RESOURCE_LIST:               // CM_RESOURCE_LIST
                case RegistryValueType.REG_FULL_RESOURCE_DESCRIPTOR:    // CM_FULL_RESOURCE_DESCRIPTOR
                case RegistryValueType.REG_RESOURCE_REQUIREMENTS_LIST:  // CM_RESOURCE_LIST??
                    byte[] outBuffer = new byte[byteCount];
                    fixed (void* outPointer = &outBuffer[0])
                    {
                        Buffer.MemoryCopy(buffer, outPointer, byteCount, byteCount);
                    }
                    return outBuffer;
                default:
                    throw new NotSupportedException($"No support for {valueType} value types.");
            }
        }
    }
}
