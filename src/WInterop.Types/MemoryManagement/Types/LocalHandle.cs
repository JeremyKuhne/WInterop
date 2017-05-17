// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using WInterop.Handles.Types;

namespace WInterop.MemoryManagement.Types
{
    /// <summary>
    /// Handle for heap memory allocated with LocalAlloc- use SafeHeapHandle where possible instead.
    /// </summary>
    public class LocalHandle : HandleZeroIsInvalid
    {
        public LocalHandle() : base(ownsHandle: true) { }

        public LocalHandle(IntPtr handle, bool ownsHandle = true) : base(ownsHandle)
        {
            SetHandle(handle);
        }

        protected override bool ReleaseHandle()
        {
            Support.Internal.Imports.LocalFree(handle);
            return true;
        }
    }
}
