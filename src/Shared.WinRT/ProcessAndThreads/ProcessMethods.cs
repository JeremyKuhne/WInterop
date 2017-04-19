// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Runtime.InteropServices;
using WInterop.ProcessAndThreads.DataTypes;

namespace WInterop.ProcessAndThreads
{
    public static partial class ProcessMethods
    {
        /// <summary>
        /// Direct P/Invokes aren't recommended. Use the wrappers that do the heavy lifting for you.
        /// </summary>
        /// <remarks>
        /// By keeping the names exactly as they are defined we can reduce string count and make the initial P/Invoke call slightly faster.
        /// </remarks>
        public static partial class Direct
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
        }

        public static ProcessHandle GetCurrentProcess()
        {
            return Direct.GetCurrentProcess();
        }

        public static uint GetCurrentProcessId()
        {
            return Direct.GetCurrentProcessId();
        }
    }
}
