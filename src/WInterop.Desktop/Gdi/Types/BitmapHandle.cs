// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace WInterop.Gdi.Types
{
    public class BitmapHandle : GdiObjectHandle
    {
        public new static BitmapHandle Null = new BitmapHandle(IntPtr.Zero);

        public BitmapHandle() : base() { }

        public BitmapHandle(IntPtr handle, bool ownsHandle = false) : base(handle, ownsHandle) { }

        static public implicit operator BitmapHandle(IntPtr handle)
        {
            return new BitmapHandle(handle);
        }
    }
}
