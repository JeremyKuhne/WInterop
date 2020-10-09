// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Runtime.InteropServices;
using WInterop.Errors;

namespace WInterop.SystemInformation.Native
{
    /// <summary>
    ///  Direct usage of Imports isn't recommended. Use the wrappers that do the heavy lifting for you.
    /// </summary>
    public static partial class SystemInformationImports
    {
        // https://msdn.microsoft.com/en-us/library/windows/desktop/ms724432.aspx
        [DllImport(Libraries.Advapi32, SetLastError = true, ExactSpelling = true)]
        public static extern bool GetUserNameW(
            SafeHandle lpBuffer,
            ref uint lpnSize);

        // https://msdn.microsoft.com/en-us/library/windows/desktop/ms724435.aspx
        [DllImport(Libraries.Secur32, SetLastError = true, ExactSpelling = true)]
        public static extern ByteBoolean GetUserNameExW(
            ExtendedNameFormat NameFormat,
            SafeHandle lpNameBuffer,
            ref uint lpnSize);

        // https://msdn.microsoft.com/en-us/library/windows/desktop/ms724301.aspx
        [DllImport(Libraries.Kernel32, SetLastError = true, ExactSpelling = true)]
        public static extern bool GetComputerNameExW(
            ComputerNameFormat NameType,
            SafeHandle lpBuffer,
            ref uint lpnSize);

        // https://msdn.microsoft.com/en-us/library/windows/desktop/mt668928.aspx
        [DllImport(Libraries.Ntdll, ExactSpelling = true)]
        public static extern SuiteMask RtlGetSuiteMask();

        // https://msdn.microsoft.com/en-us/library/windows/desktop/ms724509.aspx
        [DllImport(Libraries.Ntdll, ExactSpelling = true)]
        public static extern int NtQuerySystemInformation(
            SystemInformationClass SystemInformationClass,
            IntPtr SystemInformation,
            uint SystemInformationLength,
            out uint ReturnLength);

        // https://msdn.microsoft.com/en-us/library/windows/desktop/ms724482.aspx
        [DllImport(Libraries.Kernel32, ExactSpelling = true)]
        public static extern bool IsProcessorFeaturePresent(
            ProcessorFeature ProcessorFeature);

        // https://msdn.microsoft.com/en-us/library/windows/desktop/dn482415.aspx
        [DllImport(Libraries.Kernel32, ExactSpelling = true)]
        public static extern bool CeipIsOptedIn();

        // https://msdn.microsoft.com/en-us/library/windows/desktop/ms644905.aspx
        // This never returns false on XP or later
        [DllImport(Libraries.Kernel32, ExactSpelling = true)]
        public static extern bool QueryPerformanceFrequency(
            out long lpFrequency);

        // https://msdn.microsoft.com/en-us/library/windows/desktop/ms644904.aspx
        // This never returns false on XP or later
        [DllImport(Libraries.Kernel32, ExactSpelling = true)]
        public static extern bool QueryPerformanceCounter(
            out long lpPerformanceCount);

        // https://msdn.microsoft.com/en-us/library/windows/desktop/ms724338.aspx
        [DllImport(Libraries.Kernel32, ExactSpelling = true)]
        public static extern void GetLocalTime(
            out SystemTime lpSystemTime);

        // https://msdn.microsoft.com/en-us/library/windows/desktop/ms724295.aspx
        [DllImport(Libraries.Kernel32, SetLastError = true, ExactSpelling = true)]
        public static extern bool GetComputerNameW(
            SafeHandle lpBuffer,
            ref uint lpnSize);

        // https://msdn.microsoft.com/en-us/library/windows/desktop/ms724265.aspx
        [DllImport(Libraries.Kernel32, CharSet = CharSet.Unicode, SetLastError = true, ExactSpelling = true)]
        public static extern uint ExpandEnvironmentStringsW(
            string lpSrc,
            SafeHandle lpDst,
            uint nSize);

        // https://docs.microsoft.com/windows-hardware/drivers/ddi/content/wdm/nf-wdm-rtlverifyversioninfo
        [DllImport(Libraries.Ntdll, ExactSpelling = true)]
        public static extern NTStatus RtlVerifyVersionInfo(
            ref OsVersionInfo VersionInfo,
            VersionTypeMask TypeMask,
            ulong ConditionMask);

        // https://docs.microsoft.com/windows-hardware/drivers/ddi/content/wdm/nf-wdm-rtlverifyversioninfo
        [DllImport(Libraries.Ntdll, ExactSpelling = true)]
        public static extern NTStatus RtlGetVersion(
            ref OsVersionInfo VersionInfo);
    }
}
