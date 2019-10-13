// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Diagnostics;
using WInterop.Gdi.Native;
using WInterop.Windows;

namespace WInterop.Gdi
{
    public readonly struct BrushHandle : IDisposable
    {
        public HBRUSH HBRUSH { get; }
        private readonly bool _ownsHandle;

        /// <summary>
        /// Used to specifiy that you don't want a default brush picked in WInterop method calls.
        /// </summary>
        public static BrushHandle NoBrush = new BrushHandle(new HBRUSH((IntPtr)(-1)));

        public BrushHandle(HBRUSH handle, bool ownsHandle = true)
        {
            Debug.Assert(handle.IsInvalid || handle.Value == ((IntPtr)(-1)) || Imports.GetObjectType(handle) == ObjectType.Brush
                || Imports.GetObjectType(handle) == 0);

            HBRUSH = handle;
            _ownsHandle = ownsHandle;
        }

        public bool IsInvalid => HBRUSH.IsInvalid || Imports.GetObjectType(HBRUSH) != ObjectType.Brush;

        public void Dispose()
        {
            if (_ownsHandle)
                Imports.DeleteObject(HBRUSH);
        }

        public static implicit operator HGDIOBJ(BrushHandle handle) => handle.HBRUSH;
        public static implicit operator HBRUSH(BrushHandle handle) => handle.HBRUSH;
        public static implicit operator LResult(BrushHandle handle) => handle.HBRUSH.Value;
        public static implicit operator GdiObjectHandle(BrushHandle handle) => new GdiObjectHandle(handle.HBRUSH, ownsHandle: false);
        public static implicit operator BrushHandle(StockBrush brush) => Gdi.GetStockBrush(brush);
        public static implicit operator BrushHandle(SystemColor color) => Gdi.GetSystemColorBrush(color);

        public override bool Equals(object? obj) => obj is BrushHandle other ? other.HBRUSH == HBRUSH : false;
        public bool Equals(BrushHandle other) => other.HBRUSH == HBRUSH;
        public static bool operator ==(BrushHandle a, BrushHandle b) => a.HBRUSH == b.HBRUSH;
        public static bool operator !=(BrushHandle a, BrushHandle b) => a.HBRUSH != b.HBRUSH;
        public override int GetHashCode() => HBRUSH.GetHashCode();
    }
}
