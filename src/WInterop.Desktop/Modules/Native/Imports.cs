// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Runtime.InteropServices;
using WInterop.ProcessAndThreads;

namespace WInterop.Modules.Native
{
    /// <summary>
    /// Direct usage of Imports isn't recommended. Use the wrappers that do the heavy lifting for you.
    /// </summary>
    public static partial class Imports
    {
        // https://msdn.microsoft.com/en-us/library/windows/desktop/ms684179.aspx
        [DllImport(Libraries.Kernel32, CharSet = CharSet.Unicode, SetLastError = true, ExactSpelling = true)]
        public static extern RefCountedModuleInstance LoadLibraryExW(
            string lpFileName,
            IntPtr hFile,
            LoadLibraryFlags dwFlags);

        // https://msdn.microsoft.com/en-us/library/windows/desktop/ms683152.aspx
        [DllImport(Libraries.Kernel32, SetLastError = true, ExactSpelling = true)]
        public static extern bool FreeLibrary(
            IntPtr hModule);

        // This API is only available in ANSI
        // https://msdn.microsoft.com/en-us/library/windows/desktop/ms683212.aspx
        [DllImport(Libraries.Kernel32, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true, BestFitMapping = false)]
        public static extern IntPtr GetProcAddress(
            ModuleInstance hModule,
            [MarshalAs(UnmanagedType.LPStr)] string methodName);

        // https://msdn.microsoft.com/en-us/library/windows/desktop/ms683200.aspx
        [DllImport(Libraries.Kernel32, SetLastError = true, ExactSpelling = true)]
        public unsafe static extern bool GetModuleHandleExW(
            GetModuleFlags dwFlags,
            IntPtr lpModuleName,
            out IntPtr moduleHandle);

        // The non-ex version is more performant for the current process.
        // https://msdn.microsoft.com/en-us/library/windows/desktop/ms683197.aspx
        [DllImport(Libraries.Kernel32, SetLastError = true, ExactSpelling = true)]
        public static extern uint GetModuleFileNameW(
            ModuleInstance hModule,
            SafeHandle lpFileName,
            uint nSize);

        // https://msdn.microsoft.com/en-us/library/windows/desktop/ms683198.aspx
        [DllImport(Libraries.Kernel32, SetLastError = true, ExactSpelling = true)]
        public static extern uint K32GetModuleFileNameExW(
            SafeProcessHandle hProcess,
            ModuleInstance hModule,
            SafeHandle lpFileName,
            uint nSize);

        // https://msdn.microsoft.com/en-us/library/windows/desktop/ms683201.aspx
        [DllImport(Libraries.Kernel32, SetLastError = true, ExactSpelling = true)]
        public static extern bool K32GetModuleInformation(
            SafeProcessHandle hProcess,
            ModuleInstance hModule,
            out ModuleInfo lpmodinfo,
            uint cb);

        // https://msdn.microsoft.com/en-us/library/windows/desktop/ms683195.aspx
        [DllImport(Libraries.Kernel32, SetLastError = true, ExactSpelling = true)]
        public static extern uint K32GetMappedFileNameW(
            SafeProcessHandle hProcess,
            IntPtr lpv,
            SafeHandle lpFilename,
            uint nSize);

        // https://msdn.microsoft.com/en-us/library/windows/desktop/ms682633.aspx
        [DllImport(Libraries.Kernel32, SetLastError = true, ExactSpelling = true)]
        public static extern bool K32EnumProcessModulesEx(
            SafeProcessHandle hProcess,
            SafeHandle lphModule,
            uint cb,
            out uint lpcbNeeded,
            ListModulesOptions dwFilterFlag);
    }
}
