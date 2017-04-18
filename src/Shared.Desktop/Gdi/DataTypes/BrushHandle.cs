// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace WInterop.Gdi.DataTypes
{
    public class BrushHandle : GdiObject
    {
        public BrushHandle() : base() { }

        public BrushHandle(IntPtr handle, bool ownsHandle = true) : base(handle, ownsHandle)
        {
        }
    }
}
