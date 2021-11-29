// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Drawing;
using System.Numerics;
using System.Runtime.InteropServices;

namespace WInterop.Direct2d
{
    [StructLayout(LayoutKind.Sequential)]
    [Guid(InterfaceIds.IID_ID2D1RectangleGeometry)]
    public readonly unsafe struct RectangleGeometry : RectangleGeometry.Interface, IDisposable
    {
        internal readonly ID2D1RectangleGeometry* _handle;

        internal RectangleGeometry(ID2D1RectangleGeometry* handle) => _handle = handle;

        public RectangleF GetBounds() => Geometry.From(this).GetBounds();

        public RectangleF GetBounds(Matrix3x2 worldTransform)
            => Geometry.From(this).GetBounds(worldTransform);

        public void CombineWithGeometry(Geometry inputGeometry, CombineMode combineMode, SimplifiedGeometrySink geometrySink)
            => Geometry.From(this).CombineWithGeometry(inputGeometry, combineMode, geometrySink);

        public Factory GetFactory() => Resource.From(this).GetFactory();

        public RectangleF GetRect()
        {
            D2D_RECT_F rect;
            _handle->GetRect(&rect);
            return rect.ToRectangleF();
        }

        public void Dispose() => _handle->Release();

        public static implicit operator Geometry(RectangleGeometry brush) => new((ID2D1Geometry*)brush._handle);

        internal unsafe interface Interface : Geometry.Interface
        {
            RectangleF GetRect();
        }
    }
}
