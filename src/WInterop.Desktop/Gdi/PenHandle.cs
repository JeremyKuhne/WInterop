// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Diagnostics;
using WInterop.Gdi.Native;
using WInterop.Windows;

namespace WInterop.Gdi
{
    public readonly struct PenHandle : IDisposable
    {
        public HPEN Handle { get; }
        private readonly bool _ownsHandle;

        public static FontHandle Null = new FontHandle(default);

        public PenHandle(HPEN handle, bool ownsHandle = true)
        {
            Debug.Assert(handle.IsInvalid || GdiImports.GetObjectType(handle) == ObjectType.Pen
                || GdiImports.GetObjectType(handle) == ObjectType.ExtendedPen);

            Handle = handle;
            _ownsHandle = ownsHandle;
        }

        public bool IsInvalid
        {
            get
            {
                if (Handle.IsInvalid)
                    return true;

                ObjectType type = GdiImports.GetObjectType(Handle);
                return !(type == ObjectType.Pen || type == ObjectType.ExtendedPen);
            }
        }

        public void Dispose()
        {
            if (_ownsHandle)
                GdiImports.DeleteObject(Handle);
        }

        public static implicit operator HGDIOBJ(PenHandle handle) => handle.Handle;
        public static implicit operator HPEN(PenHandle handle) => handle.Handle;
        public static implicit operator LResult(PenHandle handle) => handle.Handle.Handle;
        public static implicit operator GdiObjectHandle(PenHandle handle)
            => new GdiObjectHandle(handle.Handle, ownsHandle: false);
        public static implicit operator PenHandle(StockPen pen) => Gdi.GetStockPen(pen);
    }
}