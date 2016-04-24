// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Handles
{
    using System;
    using System.Runtime.InteropServices;

    /// <summary>
    /// Safe handle for a block of memory returned by GetEnvironmentStrings.
    /// </summary>
    public class SafeFindVolumeHandle : SafeHandleZeroIsInvalid
    {
        internal SafeFindVolumeHandle() : base(ownsHandle: true)
        {
        }

        protected override bool ReleaseHandle()
        {
            if (!NativeMethods.VolumeManagement.Direct.FindVolumeClose(this))
            {
                uint lastError = (uint)Marshal.GetLastWin32Error();
                throw ErrorHandling.get
            }

            handle = IntPtr.Zero;
            return true;
        }
    }
}
