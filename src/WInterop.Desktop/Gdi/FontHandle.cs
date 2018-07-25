// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Diagnostics;

namespace WInterop.Gdi.Types
{
    public readonly struct FontHandle : IDisposable
    {
        public HGDIOBJ Handle { get; }
        private readonly bool _ownsHandle;

        public static RegionHandle Null = new RegionHandle(default);

        public FontHandle(HGDIOBJ handle, bool ownsHandle = true)
        {
            Debug.Assert(handle.IsInvalid || GdiMethods.Imports.GetObjectType(handle) == ObjectType.Font);

            Handle = handle;
            _ownsHandle = ownsHandle;
        }

        public bool IsInvalid => Handle.IsInvalid || GdiMethods.Imports.GetObjectType(Handle) != ObjectType.Font;

        public void Dispose()
        {
            if (_ownsHandle)
                GdiMethods.Imports.DeleteObject(Handle);
        }

        public static implicit operator FontHandle(StockFont font) => GdiMethods.GetStockFont(font);
        public static implicit operator HGDIOBJ(FontHandle handle) => handle.Handle;
        public static implicit operator GdiObjectHandle(FontHandle handle) => new GdiObjectHandle(handle.Handle, ownsHandle: false);
    }
}
