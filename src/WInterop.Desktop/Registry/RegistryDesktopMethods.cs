// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using WInterop.Desktop.Registry.DataTypes;
using WInterop.ErrorHandling;
using WInterop.ErrorHandling.DataTypes;
using WInterop.Support.Buffers;

namespace WInterop.Desktop.Registry
{
    /// <summary>
    /// These methods are only available from Windows desktop apps. Windows store apps cannot access them.
    /// </summary>
    public static class RegistryDesktopMethods
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
            // to call NtEnumerateValueKey. NtEnumerateValueKey is documented, 

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

        public static RegistryKeyHandle OpenKey(
            RegistryKeyHandle key,
            string subKeyName,
            RegistryAccessRights rights = RegistryAccessRights.KEY_READ)
        {
            WindowsError result = Direct.RegOpenKeyExW(key, subKeyName, 0, rights, out RegistryKeyHandle subKey);
            if (result != WindowsError.ERROR_SUCCESS)
                throw ErrorHelper.GetIoExceptionForError(result);

            return subKey;
        }

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
                    throw ErrorHelper.GetIoExceptionForError(result);
            }
        }

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
                    throw ErrorHelper.GetIoExceptionForError(result);
            }
        }

        public unsafe static IEnumerable<string> GetValueNamesDirect(RegistryKeyHandle key)
        {
            List<string> names = new List<string>();

            BufferHelper.CachedInvoke((HeapBuffer buffer) =>
            {
                uint index = 0;

                NTSTATUS status = NTSTATUS.STATUS_SUCCESS;
                do
                {
                    status = Direct.NtEnumerateValueKey(
                        key,
                        index,
                        KEY_VALUE_INFORMATION_CLASS.KeyValueBasicInformation,
                        buffer.VoidPointer,
                        checked((uint)buffer.ByteCapacity),
                        out uint resultLength);

                    switch (status)
                    {
                        case NTSTATUS.STATUS_SUCCESS:
                            KEY_VALUE_BASIC_INFORMATION* info = (KEY_VALUE_BASIC_INFORMATION*)buffer.VoidPointer;
                            names.Add(new string(&(*info).Name, 0, (int)(*info).NameLength / sizeof(char)));
                            index++;
                            break;
                        case NTSTATUS.STATUS_BUFFER_OVERFLOW:
                        case NTSTATUS.STATUS_BUFFER_TOO_SMALL:
                            buffer.EnsureByteCapacity(resultLength);
                            break;
                        case NTSTATUS.STATUS_NO_MORE_ENTRIES:
                            break;
                        default:
                            throw ErrorHelper.GetIoExceptionForNTStatus(status);
                    }
                } while (status != NTSTATUS.STATUS_NO_MORE_ENTRIES);
            });

            return names;
        }

        public unsafe static IEnumerable<string> GetValueNames(RegistryKeyHandle key)
        {
            List<string> names = new List<string>();

            BufferHelper.CachedInvoke((StringBuffer buffer) =>
            {
                uint index = 0;
                uint bufferSize = buffer.CharCapacity;
                WindowsError result = WindowsError.ERROR_SUCCESS;

                do
                {
                    result = Direct.RegEnumValueW(key, index, buffer.VoidPointer, ref bufferSize, null, null, null, null);
                    switch (result)
                    {
                        case WindowsError.ERROR_SUCCESS:
                            buffer.Length = bufferSize;
                            names.Add(buffer.ToString());
                            index++;
                            break;
                        case WindowsError.ERROR_MORE_DATA:
                            // For some reason the name size isn't reported back,
                            // even though it is known. Why would they not do this?
                            buffer.EnsureCharCapacity(bufferSize + 100);
                            bufferSize = buffer.CharCapacity;
                            break;
                        case WindowsError.ERROR_NO_MORE_ITEMS:
                            break;
                        default:
                            throw ErrorHelper.GetIoExceptionForError(result);
                    }
                } while (result != WindowsError.ERROR_NO_MORE_ITEMS);
            });

            return names;
        }
    }
}
