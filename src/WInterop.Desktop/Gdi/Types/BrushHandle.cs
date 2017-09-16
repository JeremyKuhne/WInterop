// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using WInterop.Windows.Types;

namespace WInterop.Gdi.Types
{
    public class BrushHandle : GdiObjectHandle
    {
        public new static BrushHandle Null = new BrushHandle(IntPtr.Zero);

        public BrushHandle() : base() { }

        public BrushHandle(IntPtr handle, bool ownsHandle = false) : base(handle, ownsHandle) { }

        public static implicit operator BrushHandle(IntPtr handle) => new BrushHandle(handle);

        public static implicit operator BrushHandle(StockBrush brush) => GdiMethods.GetStockBrush(brush);

        public static implicit operator BrushHandle(SystemColor color) => GdiMethods.GetSystemColorBrush(color);
    }
}
