// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using WInterop.Handles.Types;

namespace WInterop.SecurityManagement.Types
{
    public class SafeLsaMemoryHandle : SafeHandleZeroIsInvalid
    {
        public SafeLsaMemoryHandle() : base(ownsHandle: true) { }

        public SafeLsaMemoryHandle(IntPtr handle, bool ownsHandle = true) : base(ownsHandle)
        {
            SetHandle(handle);
        }

        protected override bool ReleaseHandle()
        {
            SecurityMethods.LsaFreeMemory(handle);
            return true;
        }
    }
}
