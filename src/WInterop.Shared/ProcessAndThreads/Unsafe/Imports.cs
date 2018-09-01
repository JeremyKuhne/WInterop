// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Runtime.InteropServices;

namespace WInterop.ProcessAndThreads.Unsafe
{
    /// <summary>
    /// Direct usage of Imports isn't recommended. Use the wrappers that do the heavy lifting for you.
    /// </summary>
    public static partial class Imports
    {
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
