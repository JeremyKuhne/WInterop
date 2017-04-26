// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace WInterop.Gdi.Types
{
    public class RegionHandle : GdiObjectHandle
    {
        public RegionHandle() : base() { }

        public RegionHandle(IntPtr handle, bool ownsHandle = false) : base(handle, ownsHandle) { }

        static public implicit operator RegionHandle(IntPtr handle)
        {
            return new RegionHandle(handle);
        }
    }
}
