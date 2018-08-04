// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Runtime.InteropServices;
using WInterop.SystemInformation;

namespace WInterop.SystemInformation
{
    public static partial class SystemInformationMethods
    {
        /// <summary>
        /// Direct usage of Imports isn't recommended. Use the wrappers that do the heavy lifting for you.
        /// </summary>
        public static partial class Imports
        {
            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms724432.aspx
            [DllImport(Libraries.Advapi32, SetLastError = true, ExactSpelling = true)]
            public static extern bool GetUserNameW(
                SafeHandle lpBuffer,
                ref uint lpnSize);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms724435.aspx
            [DllImport(Libraries.Secur32, SetLastError = true, ExactSpelling = true)]
            public static extern BOOLEAN GetUserNameExW(
                EXTENDED_NAME_FORMAT NameFormat,
                SafeHandle lpNameBuffer,
                ref uint lpnSize);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms724301.aspx
            [DllImport(Libraries.Kernel32, SetLastError = true, ExactSpelling = true)]
            public static extern bool GetComputerNameExW(
                COMPUTER_NAME_FORMAT NameType,
                SafeHandle lpBuffer,
                ref uint lpnSize);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/mt668928.aspx
            [DllImport(Libraries.Ntdll, ExactSpelling = true)]
            public static extern SuiteMask RtlGetSuiteMask();

            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms724509.aspx
            [DllImport(Libraries.Ntdll, ExactSpelling = true)]
            public static extern int NtQuerySystemInformation(
                SYSTEM_INFORMATION_CLASS SystemInformationClass,
                IntPtr SystemInformation,
                uint SystemInformationLength,
                out uint ReturnLength);
        }
    }
}
