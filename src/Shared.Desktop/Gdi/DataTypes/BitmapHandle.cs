// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace WInterop.Gdi.DataTypes
{
    public class BitmapHandle : GdiObject
    {
        public BitmapHandle() : base() { }

        public BitmapHandle(IntPtr handle, bool ownsHandle = true) : base(handle, ownsHandle)
        {
        }
    }
}
