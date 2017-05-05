// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace WInterop.Gdi.Types
{
    public class PenHandle : GdiObjectHandle
    {
        public new static FontHandle Null = new FontHandle(IntPtr.Zero);

        public PenHandle() : base() { }

        public PenHandle(IntPtr handle, bool ownsHandle = false) : base(handle, ownsHandle) { }

        public static implicit operator PenHandle(IntPtr handle) => new PenHandle(handle);

        public static implicit operator PenHandle(StockPen pen) => GdiMethods.GetStockPen(pen);
    }
}
