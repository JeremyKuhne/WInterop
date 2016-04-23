// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Handles
{
    using System;

    /// <summary>
    /// Safe handle for a block of memory returned by GetEnvironmentStrings.
    /// </summary>
    public class SafeEnvironmentStringsHandle : SafeHandleZeroIsInvalid
    {
        internal SafeEnvironmentStringsHandle() : base(ownsHandle: true)
        {
        }

        protected override bool ReleaseHandle()
        {
            NativeMethods.ProcessAndThreads.Direct.FreeEnvironmentStringsW(handle);
            handle = IntPtr.Zero;
            return true;
        }
    }
}
