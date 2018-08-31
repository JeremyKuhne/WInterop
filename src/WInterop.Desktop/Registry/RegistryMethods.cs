// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using WInterop.Errors;
using WInterop.Support.Buffers;
using WInterop.Registry.Unsafe;

namespace WInterop.Registry
{
    public static partial class Registry
    {
        public unsafe static string QueryKeyName(RegistryKeyHandle key)
        {
            return BufferHelper.BufferInvoke((HeapBuffer buffer) =>
            {
                NTSTATUS status;
                uint resultLength;
                while ((status = Imports.NtQueryKey(key, KeyInformationClass.KeyNameInformation, buffer.VoidPointer, (uint)buffer.ByteCapacity, out resultLength))
                    == NTSTATUS.STATUS_BUFFER_TOO_SMALL || status == NTSTATUS.STATUS_BUFFER_OVERFLOW)
                {
                    buffer.EnsureByteCapacity(resultLength);
                }

                if (status != NTSTATUS.STATUS_SUCCESS)
                    Error.GetIoExceptionForNTStatus(status);

                return ((KEY_NAME_INFORMATION*)buffer.VoidPointer)->Name.CreateString();
            });
        }

        /// <summary>
        /// Open the specified subkey.
        /// </summary>
        public static RegistryKeyHandle OpenKey(
            RegistryKeyHandle key,
            string subKeyName,
            RegistryAccessRights rights = RegistryAccessRights.KEY_READ)
        {
            WindowsError result = Imports.RegOpenKeyExW(key, subKeyName, 0, rights, out RegistryKeyHandle subKey);
            if (result != WindowsError.ERROR_SUCCESS)
                throw Error.GetIoExceptionForError(result);

            return subKey;
        }

        /// <summary>
        /// Returns true if the given value exists.
        /// </summary>
        public unsafe static bool QueryValueExists(RegistryKeyHandle key, string valueName)
        {
            WindowsError result = Imports.RegQueryValueExW(key, valueName, null, null, null, null);
            switch (result)
            {
                case WindowsError.ERROR_SUCCESS:
                    return true;
                case WindowsError.ERROR_FILE_NOT_FOUND:
                    return false;
                default:
                    throw Error.GetIoExceptionForError(result);
            }
        }

        /// <summary>
        /// Returns the type of the given value, or REG_NONE if it doesn't exist.
        /// </summary>
        public unsafe static RegistryValueType QueryValueType(RegistryKeyHandle key, string valueName)
        {
            RegistryValueType valueType = new RegistryValueType();
            WindowsError result = Imports.RegQueryValueExW(key, valueName, null, &valueType, null, null);
            switch (result)
            {
                case WindowsError.ERROR_SUCCESS:
                    return valueType;
                case WindowsError.ERROR_FILE_NOT_FOUND:
                    return RegistryValueType.REG_NONE;
                default:
                    throw Error.GetIoExceptionForError(result);
            }
        }

        public unsafe static object QueryValue(RegistryKeyHandle key, string valueName)
        {
            return BufferHelper.BufferInvoke((HeapBuffer buffer) =>
            {
                RegistryValueType valueType = new RegistryValueType();

                WindowsError result;
                uint byteCapacity = (uint)buffer.ByteCapacity;
                while ((result = Imports.RegQueryValueExW(
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
                            throw Error.GetIoExceptionForError(result);
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

            BufferHelper.BufferInvoke((HeapBuffer buffer) =>
            {
                NTSTATUS status;
                while((status = Imports.NtEnumerateValueKey(
                    key,
                    (uint)names.Count,
                    KeyValueInformationClass.BasicInformation,
                    buffer.VoidPointer,
                    checked((uint)buffer.ByteCapacity),
                    out uint resultLength)) != NTSTATUS.STATUS_NO_MORE_ENTRIES)
                {
                    switch (status)
                    {
                        case NTSTATUS.STATUS_SUCCESS:
                            KEY_VALUE_BASIC_INFORMATION* info = (KEY_VALUE_BASIC_INFORMATION*)buffer.VoidPointer;
                            if (filterTo == RegistryValueType.REG_NONE || info->Type == filterTo)
                                names.Add(info->Name.CreateString());
                            break;
                        case NTSTATUS.STATUS_BUFFER_OVERFLOW:
                        case NTSTATUS.STATUS_BUFFER_TOO_SMALL:
                            buffer.EnsureByteCapacity(resultLength);
                            break;
                        default:
                            throw Error.GetIoExceptionForNTStatus(status);
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

            BufferHelper.BufferInvoke((HeapBuffer buffer) =>
            {
                NTSTATUS status;
                while ((status = Imports.NtEnumerateValueKey(
                    key,
                    (uint)data.Count,
                    KeyValueInformationClass.PartialInformation,
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
                            throw Error.GetIoExceptionForNTStatus(status);
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

            BufferHelper.BufferInvoke((StringBuffer buffer) =>
            {
                // Ensure we have enough space to hold the perf key values
                buffer.EnsureCharCapacity(10);
                uint bufferSize = buffer.CharCapacity;

                WindowsError result;
                while ((result = Imports.RegEnumValueW(key, (uint)names.Count, buffer.VoidPointer, ref bufferSize, null, null, null, null))
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
                            throw Error.GetIoExceptionForError(result);
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
