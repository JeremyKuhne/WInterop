// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop
{
    using System;
    using System.Runtime.InteropServices;
    using System.Security;

    public static partial class NativeMethods
    {
        public static class SecurityManagement
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
            public static class Direct
            {
                // https://msdn.microsoft.com/en-us/library/windows/desktop/ms721800.aspx
                [DllImport(Libraries.Advapi32, SetLastError = true, ExactSpelling = true)]
                public static extern uint LsaNtStatusToWinError(int Status);
            }

            /// <summary>
            /// Convert an NTSTATUS to a Windows error code
            /// </summary>
            public static uint NtStatusToWinError(int status)
            {
                return Direct.LsaNtStatusToWinError(status);
            }
        }
    }
}
