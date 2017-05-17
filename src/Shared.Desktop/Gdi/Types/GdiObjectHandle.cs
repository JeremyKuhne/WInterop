// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Diagnostics;
using WInterop.Handles.Types;

namespace WInterop.Gdi.Types
{
    /// <summary>
    /// GDI object handle (HGDIOBJ)
    /// </summary>
    public class GdiObjectHandle : HandleZeroIsInvalid
    {
        public static GdiObjectHandle Null = new GdiObjectHandle(IntPtr.Zero);

        protected GdiObjectHandle() : base(ownsHandle: true) { }

        protected GdiObjectHandle(IntPtr handle, bool ownsHandle = true) : base(handle, ownsHandle) { }

        protected override bool ReleaseHandle()
        {
            return GdiMethods.Imports.DeleteObject(handle);
        }

        public ObjectType GetObjectType()
        {
            return GdiMethods.Imports.GetObjectType(handle);
        }

        public static implicit operator GdiObjectHandle(StockFont font) => (FontHandle)font;
        public static implicit operator GdiObjectHandle(StockPen pen) => (PenHandle)pen;
        public static implicit operator GdiObjectHandle(StockBrush brush) => (BrushHandle)brush;

        public static GdiObjectHandle Create(IntPtr handle, bool ownsHandle = false)
        {
            ObjectType type = GdiMethods.Imports.GetObjectType(handle);
            switch (type)
            {
                case ObjectType.OBJ_BRUSH:
                    return new BrushHandle(handle, ownsHandle);
                case ObjectType.OBJ_PEN:
                    return new PenHandle(handle, ownsHandle);
                case ObjectType.OBJ_BITMAP:
                    return new BitmapHandle(handle, ownsHandle);
                case ObjectType.OBJ_FONT:
                    return new FontHandle(handle, ownsHandle);
                default:
                    Debug.Fail($"Object type {type} not handled yet.");
                    return new GdiObjectHandle(handle, ownsHandle);
            }
        }
    }
}
