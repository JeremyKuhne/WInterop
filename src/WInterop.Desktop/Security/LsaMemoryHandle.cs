// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using WInterop.Handles;

namespace WInterop.Security
{
    public class LsaMemoryHandle : HandleZeroIsInvalid
    {
        public LsaMemoryHandle() : base(ownsHandle: true) { }

        public LsaMemoryHandle(IntPtr handle, bool ownsHandle = true) : base(ownsHandle)
        {
            SetHandle(handle);
        }

        protected override bool ReleaseHandle()
        {
            Security.LsaFreeMemory(handle);
            return true;
        }
    }
}
