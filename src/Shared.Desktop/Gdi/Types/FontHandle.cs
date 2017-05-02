// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace WInterop.Gdi.Types
{
    public class FontHandle : GdiObjectHandle
    {
        public new static FontHandle Null = new FontHandle(IntPtr.Zero);

        public FontHandle() : base() { }

        public FontHandle(IntPtr handle, bool ownsHandle = false) : base(handle, ownsHandle) { }

        static public implicit operator FontHandle(IntPtr handle)
        {
            return new FontHandle(handle);
        }
    }
}
