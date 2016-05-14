// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Runtime.InteropServices;

namespace WInterop.Handles.Desktop
{
    public class SafeNetApiBufferHandle : SafeBuffer
    {
        public SafeNetApiBufferHandle() : base(ownsHandle: true)
        {
        }

        public override bool IsInvalid
        {
            get { return handle == IntPtr.Zero; }
        }

        protected override bool ReleaseHandle()
        {
            NetworkManagement.Desktop.NativeMethods.NetApiBufferFree(handle);
            handle = IntPtr.Zero;
            return true;
        }
    }
}
