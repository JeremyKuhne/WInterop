// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Drawing;
using System.Numerics;
using System.Runtime.InteropServices;

namespace WInterop.Direct2d
{
    /// <summary>
    ///  Represents a complex shape that may be composed of arcs, curves, and lines. [ID2D1PathGeometry]
    /// </summary>
    [Guid(InterfaceIds.IID_ID2D1PathGeometry)]
    [StructLayout(LayoutKind.Sequential)]
    public readonly unsafe struct PathGeometry : PathGeometry.Interface, IDisposable
    {
        internal readonly ID2D1PathGeometry* _handle;

        internal PathGeometry(ID2D1PathGeometry* handle) => _handle = handle;

        public RectangleF GetBounds() => Geometry.From(this).GetBounds();

        public RectangleF GetBounds(Matrix3x2 worldTransform)
            => Geometry.From(this).GetBounds(worldTransform);

        public void CombineWithGeometry(Geometry inputGeometry, CombineMode combineMode, SimplifiedGeometrySink geometrySink)
            => Geometry.From(this).CombineWithGeometry(inputGeometry, combineMode, geometrySink);

        public Factory GetFactory() => Geometry.From(this).GetFactory();

        public uint GetFigureCount()
        {
            uint count;
            _handle->GetFigureCount(&count).ThrowIfFailed();
            return count;
        }

        public uint GetSegmentCount()
        {
            uint count;
            _handle->GetSegmentCount(&count).ThrowIfFailed();
            return count;
        }

        public GeometrySink Open()
        {
            GeometrySink sink;
            _handle->Open((ID2D1GeometrySink**)&sink).ThrowIfFailed();
            return sink;
        }

        public void Dispose() => _handle->Release();

        public static implicit operator Geometry(PathGeometry brush) => new((ID2D1Geometry*)brush._handle);

        internal unsafe interface Interface : Geometry.Interface
        {
            /// <summary>
            ///  Opens a geometry sink that will be used to create this path geometry.
            /// </summary>
            GeometrySink Open();

            uint GetSegmentCount();

            uint GetFigureCount();
        }
    }
}
