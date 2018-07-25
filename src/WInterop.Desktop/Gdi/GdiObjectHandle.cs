// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Diagnostics;
using WInterop.Handles.Types;
using WInterop.Windows;

namespace WInterop.Gdi
{
    /// <summary>
    /// GDI object handle (HGDIOBJ)
    /// </summary>
    public readonly struct GdiObjectHandle : IDisposable
    {
        public HGDIOBJ Handle { get; }
        private readonly bool _ownsHandle;

        public static GdiObjectHandle Null = new GdiObjectHandle(default);

        public GdiObjectHandle(HGDIOBJ handle, bool ownsHandle = true)
        {
            Handle = handle;
            _ownsHandle = ownsHandle;
        }

        public ObjectType GetObjectType()
        {
            return GdiMethods.Imports.GetObjectType(Handle);
        }

        public static implicit operator GdiObjectHandle(StockFont font) => new GdiObjectHandle(GdiMethods.GetStockFont(font).Handle, false);
        public static implicit operator GdiObjectHandle(StockBrush brush) => new GdiObjectHandle(GdiMethods.GetStockBrush(brush), false);
        public static implicit operator GdiObjectHandle(SystemColor color) => new GdiObjectHandle(GdiMethods.GetSystemColorBrush(color), false);
        public static implicit operator GdiObjectHandle(StockPen pen) => new GdiObjectHandle(GdiMethods.GetStockPen(pen), false);

        public static implicit operator HGDIOBJ(GdiObjectHandle handle) => handle.Handle;

        public bool IsInvalid => Handle.IsInvalid;

        public void Dispose()
        {
            if (_ownsHandle)
                GdiMethods.Imports.DeleteObject(Handle);
        }
    }
}
