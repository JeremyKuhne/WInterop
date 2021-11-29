// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Drawing;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace WInterop.Direct2d;

[Guid(InterfaceIds.IID_ID2D1Geometry)]
[StructLayout(LayoutKind.Sequential)]
public readonly unsafe struct Geometry : Geometry.Interface, IDisposable
{
    internal readonly ID2D1Geometry* _handle;

    internal Geometry(ID2D1Geometry* handle) => _handle = handle;

    public Factory GetFactory() => Resource.From(this).GetFactory();

    public RectangleF GetBounds()
    {
        D2D_RECT_F rect;
        _handle->GetBounds(null, &rect).ThrowIfFailed();
        return rect.ToRectangleF();
    }

    public RectangleF GetBounds(Matrix3x2 worldTransform)
    {
        D2D_RECT_F rect;
        _handle->GetBounds((D2D_MATRIX_3X2_F*)&worldTransform, &rect).ThrowIfFailed();
        return rect.ToRectangleF();
    }

    public unsafe void CombineWithGeometry(
        Geometry inputGeometry,
        CombineMode combineMode,
        SimplifiedGeometrySink geometrySink)
    {
        _handle->CombineWithGeometry(
            inputGeometry._handle,
            (D2D1_COMBINE_MODE)combineMode,
            null,
            geometrySink.Handle);
    }

    internal static ref Geometry From<TFrom>(in TFrom from)
        where TFrom : unmanaged, Interface
        => ref Unsafe.AsRef<Geometry>(Unsafe.AsPointer(ref Unsafe.AsRef(from)));

    public void Dispose() => _handle->Release();

    /// <summary>
    ///  [ID2D1Geometry]
    /// </summary>
    internal unsafe interface Interface : Resource.Interface
    {
        /// <summary>
        ///  Retrieve the bounds of the geometry.
        /// </summary>
        RectangleF GetBounds();

        /// <summary>
        ///  Retrieve the bounds of the geometry with the applied <paramref name="worldTransform"/>.
        /// </summary>
        RectangleF GetBounds(Matrix3x2 worldTransform);

        /*
        /// <summary>
        ///  Get the bounds of the corresponding geometry after it has been widened or have
        ///  an optional pen style applied.
        /// </summary>
        RectangleF GetWidenedBounds(
            float strokeWidth,
            StrokeStyle* strokeStyle,
            Matrix3x2* worldTransform,
            float flatteningTolerance);

        /// <summary>
        ///  Checks to see whether the corresponding penned and widened geometry contains the
        ///  given point.
        /// </summary>
        bool StrokeContainsPoint(
            PointF point,
            float strokeWidth,
            StrokeStyle* strokeStyle,
            Matrix3x2* worldTransform,
            float flatteningTolerance);

        /// <summary>
        ///  Test whether the given fill of this geometry would contain this point.
        /// </summary>
        IntBoolean FillContainsPoint(
            PointF point,
            Matrix3x2* worldTransform,
            float flatteningTolerance);

        /// <summary>
        ///  Compare how one geometry intersects or contains another geometry.
        /// </summary>
        GeometryRelation CompareWithGeometry(
            Geometry* inputGeometry,
            Matrix3x2* inputGeometryTransform,
            float flatteningTolerance);

        /// <summary>
        ///  Converts a geometry to a simplified geometry that has arcs and quadratic beziers
        ///  removed.
        /// </summary>
        void Simplify(
            GeometrySimplificationOption simplificationOption,
            Matrix3x2* worldTransform,
            float flatteningTolerance,
            ISimplifiedGeometrySink geometrySink);

        /// <summary>
        ///  Tessellates a geometry into triangles.
        /// </summary>
        void Tessellate(
            Matrix3x2* worldTransform,
            float flatteningTolerance,
            ITesselationSink tessellationSink);
        */

        /// <summary>
        ///  Performs a combine operation between the two geometries to produce a resulting geometry.
        /// </summary>
        void CombineWithGeometry(
            Geometry inputGeometry,
            CombineMode combineMode,
            //Matrix3x2? inputGeometryTransform,
            //float? flatteningTolerance,
            SimplifiedGeometrySink geometrySink);

        /*
        /// <summary>
        ///  Computes the outline of the geometry. The result is written back into a
        ///  simplified geometry sink.
        /// </summary>
        void Outline(
            Matrix3x2* worldTransform,
            float flatteningTolerance,
            ISimplifiedGeometrySink geometrySink);

        /// <summary>
        ///  Computes the area of the geometry.
        /// </summary>
        float ComputeArea(
            Matrix3x2* worldTransform,
            float flatteningTolerance);

        /// <summary>
        ///  Computes the length of the geometry.
        /// </summary>
        float ComputeLength(
            Matrix3x2* worldTransform,
            float flatteningTolerance);

        /// <summary>
        ///  Computes the point and tangent a given distance along the path.
        /// </summary>
        void ComputePointAtLength(
            float length,
            Matrix3x2* worldTransform,
            float flatteningTolerance,
            PointF* point,
            PointF* unitTangentVector);

        /// <summary>
        ///  Get the geometry and widen it as well as apply an optional pen style.
        /// </summary>
        void Widen(
            float strokeWidth,
            IStrokeStyle strokeStyle,
            Matrix3x2* worldTransform,
            float flatteningTolerance,
            ISimplifiedGeometrySink geometrySink);
        */
    }
}
