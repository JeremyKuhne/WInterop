// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Runtime.InteropServices;
using System.Security;
using WInterop.Handles;

namespace WInterop
{
    public static partial class NativeMethods
    {
        public static partial class ProcessAndThreads
        {
            /// <summary>
            /// Direct P/Invokes aren't recommended. Use the wrappers that do the heavy lifting for you.
            /// </summary>
            /// <remarks>
            /// By keeping the names exactly as they are defined we can reduce string count and make the initial P/Invoke call slightly faster.
            /// </remarks>
#if DESKTOP
            [SuppressUnmanagedCodeSecurity] // We don't want a stack walk with every P/Invoke.
#endif
            public static partial class Direct
            {
                // https://msdn.microsoft.com/en-us/library/windows/desktop/ms683182.aspx
                [DllImport(Libraries.Kernel32, ExactSpelling = true)]
                public static extern SafeThreadHandle GetCurrentThread();

                // https://msdn.microsoft.com/en-us/library/windows/desktop/ms683179.aspx
                [DllImport(Libraries.Kernel32, ExactSpelling = true)]
                public static extern SafeProcessHandle GetCurrentProcess();

                // https://msdn.microsoft.com/en-us/library/windows/desktop/ms683180.aspx
                [DllImport(Libraries.Kernel32, ExactSpelling = true)]
                public static extern uint GetCurrentProcessId();

                // https://msdn.microsoft.com/en-us/library/windows/desktop/ms683180.aspx
                [DllImport(Libraries.Kernel32, ExactSpelling = true)]
                public static extern uint GetCurrentThreadId();

                // https://msdn.microsoft.com/en-us/library/windows/desktop/ms683156.aspx
                [DllImport(Libraries.Kernel32, ExactSpelling = true)]
                public static extern string GetCommandLineW();
            }
        }
    }
}
