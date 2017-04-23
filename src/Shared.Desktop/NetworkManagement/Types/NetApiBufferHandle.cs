// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Runtime.InteropServices;

namespace WInterop.NetworkManagement.Types
{
    public class NetApiBufferHandle : SafeBuffer
    {
        public NetApiBufferHandle() : base(ownsHandle: true)
        {
        }

        public override bool IsInvalid
        {
            get { return handle == IntPtr.Zero; }
        }

        protected override bool ReleaseHandle()
        {
            NetworkMethods.NetApiBufferFree(handle);
            handle = IntPtr.Zero;
            return true;
        }
    }
}
