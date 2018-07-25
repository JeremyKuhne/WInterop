// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Diagnostics;
using WInterop.Windows;

namespace WInterop.Gdi
{
    public readonly struct BrushHandle : IDisposable
    {
        public HBRUSH Handle { get; }
        private readonly bool _ownsHandle;

        public static BrushHandle Null = new BrushHandle(default);

        public BrushHandle(HGDIOBJ handle, bool ownsHandle = true)
        {
            Debug.Assert(handle.IsInvalid || GdiMethods.Imports.GetObjectType(handle) == ObjectType.Brush);

            Handle = new HBRUSH(handle.Handle);
            _ownsHandle = ownsHandle;
        }

        public bool IsInvalid => Handle.IsInvalid || GdiMethods.Imports.GetObjectType(Handle) != ObjectType.Brush;

        public void Dispose()
        {
            if (_ownsHandle)
                GdiMethods.Imports.DeleteObject(Handle);
        }

        public static implicit operator BrushHandle(StockBrush brush) => GdiMethods.GetStockBrush(brush);
        public static implicit operator BrushHandle(SystemColor color) => GdiMethods.GetSystemColorBrush(color);
        public static implicit operator HGDIOBJ(BrushHandle handle) => handle.Handle;
        public static implicit operator HBRUSH(BrushHandle handle) => handle.Handle;
        public static implicit operator LRESULT(BrushHandle handle) => handle.Handle.Handle;
        public static implicit operator GdiObjectHandle(BrushHandle handle) => new GdiObjectHandle(handle.Handle, ownsHandle: false);
    }
}
