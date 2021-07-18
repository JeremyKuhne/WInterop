// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Runtime.InteropServices;
using WInterop.Errors;
using WInterop.Storage;

namespace WInterop.Registry.Native
{
    /// <summary>
    ///  Direct usage of Imports isn't recommended. Use the wrappers that do the heavy lifting for you.
    /// </summary>
    public static class Imports
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
        public static unsafe extern WindowsError RegQueryValueExW(
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
        public static unsafe extern WindowsError RegEnumValueW(
            RegistryKeyHandle hKey,
            uint dwIndex,
            void* lpValueName,
            ref uint lpcchValueName,
            void* lpReserved,
            RegistryValueType* lpType,
            void* lpData,
            uint* lpcbData);

        // https://msdn.microsoft.com/en-us/library/windows/desktop/ms724902.aspx
        [DllImport(Libraries.Advapi32, CharSet = CharSet.Unicode, ExactSpelling = true)]
        public static extern WindowsError RegQueryInfoKeyW(
            RegistryKeyHandle hKey,
            SafeHandle lpClass,
            ref uint lpcClass,
            IntPtr lpReserved,
            out uint lpcSubKeys,
            out uint lpcMaxSubKeyLen,
            out uint lpcMaxClassLen,
            out uint lpcValues,
            out uint lpcMaxValueNameLen,
            out uint lpcMaxValueLen,
            out uint lpcbSecurityDescriptor,
            out FileTime lpftLastWriteTime);

        // https://msdn.microsoft.com/en-us/library/windows/hardware/ff556680.aspx
        // https://msdn.microsoft.com/en-us/library/windows/hardware/ff567060.aspx
        [DllImport(Libraries.Ntdll, ExactSpelling = true)]
        public static unsafe extern NTStatus NtQueryKey(
            RegistryKeyHandle KeyHandle,
            KeyInformationClass KeyInformationClass,
            void* KeyInformation,
            uint Length,
            out uint ResultLength);

        // https://msdn.microsoft.com/en-us/library/windows/hardware/ff556514.aspx
        // https://msdn.microsoft.com/en-us/library/windows/hardware/ff566453.aspx
        [DllImport(Libraries.Ntdll, ExactSpelling = true)]
        public static unsafe extern NTStatus NtEnumerateValueKey(
            RegistryKeyHandle KeyHandle,
            uint Index,
            KeyValueInformationClass KeyValueInformationClass,
            void* KeyValueInformation,
            uint Length,
            out uint ResultLength);
    }
}