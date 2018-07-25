// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using WInterop.Windows.Types;

namespace WInterop.Gdi.Types
{
    public readonly struct PenHandle : IDisposable
    {
        public HGDIOBJ Handle { get; }
        private readonly bool _ownsHandle;

        public static FontHandle Null = new FontHandle(default);

        public PenHandle(HGDIOBJ handle, bool ownsHandle = true)
        {
            Handle = handle;
            _ownsHandle = ownsHandle;
        }

        public bool IsInvalid
        {
            get
            {
                if (Handle.IsInvalid)
                    return true;

                ObjectType type = GdiMethods.Imports.GetObjectType(Handle);
                return !(type == ObjectType.Pen || type == ObjectType.ExtendedPen);
            }
        }


        public void Dispose()
        {
            if (_ownsHandle)
                GdiMethods.Imports.DeleteObject(Handle);
        }

        public static implicit operator PenHandle(StockPen pen) => GdiMethods.GetStockPen(pen);
        public static implicit operator HGDIOBJ(PenHandle handle) => handle.Handle;
        public static implicit operator LRESULT(PenHandle handle) => handle.Handle.Handle;
        public static implicit operator GdiObjectHandle(PenHandle handle) => new GdiObjectHandle(handle.Handle, ownsHandle: false);
    }
}
