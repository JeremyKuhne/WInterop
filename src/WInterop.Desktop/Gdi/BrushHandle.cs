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
        public HBRUSH Handle { get; }
        private readonly bool _ownsHandle;

        public static BrushHandle Null = new BrushHandle(default);

        public BrushHandle(HBRUSH handle, bool ownsHandle = true)
        {
            Debug.Assert(handle.IsInvalid || Imports.GetObjectType(handle) == ObjectType.Brush);

            Handle = handle;
            _ownsHandle = ownsHandle;
        }

        public bool IsInvalid => Handle.IsInvalid || Imports.GetObjectType(Handle) != ObjectType.Brush;

        public void Dispose()
        {
            if (_ownsHandle)
                Imports.DeleteObject(Handle);
        }

        public static implicit operator HGDIOBJ(BrushHandle handle) => handle.Handle;
        public static implicit operator HBRUSH(BrushHandle handle) => handle.Handle;
        public static implicit operator LRESULT(BrushHandle handle) => handle.Handle.Handle;
        public static implicit operator GdiObjectHandle(BrushHandle handle) => new GdiObjectHandle(handle.Handle, ownsHandle: false);
        public static implicit operator BrushHandle(StockBrush brush) => Gdi.GetStockBrush(brush);
        public static implicit operator BrushHandle(SystemColor color) => Gdi.GetSystemColorBrush(color);
    }
}
