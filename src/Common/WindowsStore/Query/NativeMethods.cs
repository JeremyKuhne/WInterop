// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Runtime.InteropServices;
using System.Security;

namespace WInterop.WindowsStore.Query
{
    public static partial class NativeMethods
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
            // https://msdn.microsoft.com/en-us/library/windows/desktop/hh920918.aspx
            // Technically returns an int, but being compatable with error lookup
            [DllImport(Libraries.Kernel32, SetLastError = true, ExactSpelling = true)]
            public static extern uint GetCurrentApplicationUserModelId(
                ref uint applicationUserModelIdLength,
                SafeHandle applicationUserModelId);
        }
    }
}
