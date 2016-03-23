// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Utility
{
    using System;
    using System.Runtime.InteropServices;

    public static class Environment
    {
        /// <summary>
        /// True if the current process is running in 64 bit.
        /// </summary>
        /// <remarks>
        /// This isn't defined in Portable so we need our own.
        /// </remarks>
        public static bool Is64BitProcess = Marshal.SizeOf<IntPtr>() == sizeof(ulong);
    }
}
