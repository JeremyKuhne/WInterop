// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace WInterop.Handles
{
    /// <summary>
    /// Safe handle for a block of memory returned by GetEnvironmentStrings.
    /// </summary>
    public class SafeEnvironmentStringsHandle : SafeHandleZeroIsInvalid
    {
        public SafeEnvironmentStringsHandle() : base(ownsHandle: true)
        {
        }

        protected override bool ReleaseHandle()
        {
            NativeMethods.ProcessAndThreads.Desktop.Direct.FreeEnvironmentStringsW(this);
            handle = IntPtr.Zero;
            return true;
        }
    }
}
