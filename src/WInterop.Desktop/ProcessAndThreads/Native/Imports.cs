// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Runtime.InteropServices;

namespace WInterop.ProcessAndThreads.Native
{
    /// <summary>
    /// Direct usage of Imports isn't recommended. Use the wrappers that do the heavy lifting for you.
    /// </summary>
    public static partial class Imports
    {
        // https://msdn.microsoft.com/en-us/library/windows/desktop/ms683188.aspx
        [DllImport(Libraries.Kernel32, CharSet = CharSet.Unicode, ExactSpelling = true)]
        public unsafe static extern uint GetEnvironmentVariableW(
            char* lpName,
            char* lpBuffer,
            uint nSize);

        // https://msdn.microsoft.com/en-us/library/windows/desktop/ms686206.aspx
        [DllImport(Libraries.Kernel32, CharSet = CharSet.Unicode, SetLastError = true, ExactSpelling = true)]
        public static extern bool SetEnvironmentVariableW(
            string lpName,
            string lpValue);

        // https://msdn.microsoft.com/en-us/library/windows/desktop/ms683187.aspx
        // Note that this API does not document that it sets GetLastError
        [DllImport(Libraries.Kernel32, CharSet = CharSet.Unicode, ExactSpelling = true)]
        public static extern EnvironmentStringsHandle GetEnvironmentStringsW();

        // https://msdn.microsoft.com/en-us/library/windows/desktop/ms683151.aspx
        [DllImport(Libraries.Kernel32, SetLastError = true, ExactSpelling = true)]
        public static extern bool FreeEnvironmentStringsW(
            IntPtr lpszEnvironmentBlock);

        // https://msdn.microsoft.com/en-us/library/windows/desktop/ms683219.aspx
        [DllImport(Libraries.Kernel32, SetLastError = true, ExactSpelling = true)]
        public static extern bool K32GetProcessMemoryInfo(
            SafeProcessHandle process,
            out ProcessMemoryCountersExtended ppsmemCounters,
            uint cb);

        // https://msdn.microsoft.com/en-us/library/windows/desktop/ms683215.aspx
        [DllImport(Libraries.Kernel32, ExactSpelling = true)]
        public static extern uint GetProcessId(
            SafeProcessHandle Process);

        // https://msdn.microsoft.com/en-us/library/windows/desktop/ms683179.aspx
        [DllImport(Libraries.Kernel32, ExactSpelling = true)]
        public static extern ProcessHandle GetCurrentProcess();

        // https://msdn.microsoft.com/en-us/library/windows/desktop/ms683180.aspx
        [DllImport(Libraries.Kernel32, ExactSpelling = true)]
        public static extern uint GetCurrentProcessId();

        // https://msdn.microsoft.com/en-us/library/windows/desktop/ms683156.aspx
        [DllImport(Libraries.Kernel32, ExactSpelling = true)]
        public static extern string GetCommandLineW();

        // https://msdn.microsoft.com/en-us/library/windows/desktop/ms684320.aspx
        [DllImport(Libraries.Kernel32, SetLastError = true, ExactSpelling = true)]
        public static extern SafeProcessHandle OpenProcess(
            ProcessAccessRights dwDesiredAccess,
            bool bInheritHandle,
            uint dwProcessId);

        // https://msdn.microsoft.com/en-us/library/windows/desktop/ms683182.aspx
        [DllImport(Libraries.Kernel32, ExactSpelling = true)]
        public static extern ThreadHandle GetCurrentThread();

        // https://msdn.microsoft.com/en-us/library/windows/desktop/ms683180.aspx
        [DllImport(Libraries.Kernel32, ExactSpelling = true)]
        public static extern uint GetCurrentThreadId();
    }
}