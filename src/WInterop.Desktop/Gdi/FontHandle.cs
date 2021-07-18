// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Diagnostics;
using WInterop.Gdi.Native;
using WInterop.Windows;

namespace WInterop.Gdi
{
    public readonly struct FontHandle : IDisposable
    {
        public HFONT Handle { get; }
        private readonly bool _ownsHandle;

        public FontHandle(HFONT handle, bool ownsHandle = true)
        {
            Debug.Assert(handle.IsInvalid || GdiImports.GetObjectType(handle) == ObjectType.Font);

            Handle = handle;
            _ownsHandle = ownsHandle;
        }

        public bool IsNull => Handle.IsNull;

        public bool IsInvalid => Handle.IsInvalid || GdiImports.GetObjectType(Handle) != ObjectType.Font;

        public void Dispose()
        {
            if (_ownsHandle && !Handle.IsInvalid)
            {
                GdiImports.DeleteObject(Handle);
            }
        }

        public static implicit operator HGDIOBJ(FontHandle handle) => handle.Handle;
        public static implicit operator HFONT(FontHandle handle) => handle.Handle;
        public static implicit operator LResult(FontHandle handle) => handle.Handle.Value;
        public static implicit operator GdiObjectHandle(FontHandle handle) => new GdiObjectHandle(handle.Handle, ownsHandle: false);
        public static implicit operator FontHandle(StockFont font) => Gdi.GetStockFont(font);
    }
}