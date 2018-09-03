// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Drawing;
using System.Numerics;
using System.Runtime.InteropServices;

namespace WInterop.Direct2d
{
    /// <summary>
    /// [ID2D1Geometry]
    /// </summary>
    [ComImport,
        Guid(InterfaceIds.IID_ID2D1Geometry),
        InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public unsafe interface IGeometry : IResource
    {
        // ID2D1Resource
        [PreserveSig]
        new void GetFactory(
            out IFactory factory);

        /// <summary>
        /// Retrieve the bounds of the geometry, with an optional applied transform.
        /// </summary>
        LtrbRectangleF GetBounds(
            Matrix3x2* worldTransform);

        /// <summary>
        /// Get the bounds of the corresponding geometry after it has been widened or have
        /// an optional pen style applied.
        /// </summary>
        LtrbRectangleF GetWidenedBounds(
            float strokeWidth,
            IStrokeStyle strokeStyle,
            Matrix3x2* worldTransform,
            float flatteningTolerance);

        /// <summary>
        /// Checks to see whether the corresponding penned and widened geometry contains the
        /// given point.
        /// </summary>
        Boolean32 StrokeContainsPoint(
            PointF point,
            float strokeWidth,
            IStrokeStyle strokeStyle,
            Matrix3x2* worldTransform,
            float flatteningTolerance);

        /// <summary>
        /// Test whether the given fill of this geometry would contain this point.
        /// </summary>
        Boolean32 FillContainsPoint(
            PointF point,
            Matrix3x2* worldTransform,
            float flatteningTolerance);

        /// <summary>
        /// Compare how one geometry intersects or contains another geometry.
        /// </summary>
        GeometryRelation CompareWithGeometry(
            IGeometry inputGeometry,
            Matrix3x2* inputGeometryTransform,
            float flatteningTolerance);

        /// <summary>
        /// Converts a geometry to a simplified geometry that has arcs and quadratic beziers
        /// removed.
        /// </summary>
        void Simplify(
            GeometrySimplificationOption simplificationOption,
            Matrix3x2* worldTransform,
            float flatteningTolerance,
            ISimplifiedGeometrySink geometrySink);

        /// <summary>
        /// Tessellates a geometry into triangles.
        /// </summary>
        void Tessellate(
            Matrix3x2* worldTransform,
            float flatteningTolerance,
            ITesselationSink tessellationSink);

        /// <summary>
        /// Performs a combine operation between the two geometries to produce a resulting
        /// geometry.
        /// </summary>
        void CombineWithGeometry(
            IGeometry inputGeometry,
            CombineMode combineMode,
            Matrix3x2* inputGeometryTransform,
            float flatteningTolerance,
            ISimplifiedGeometrySink geometrySink);

        /// <summary>
        /// Computes the outline of the geometry. The result is written back into a
        /// simplified geometry sink.
        /// </summary>
        void Outline(
            Matrix3x2* worldTransform,
            float flatteningTolerance,
            ISimplifiedGeometrySink geometrySink);

        /// <summary>
        /// Computes the area of the geometry.
        /// </summary>
        float ComputeArea(
            Matrix3x2* worldTransform,
            float flatteningTolerance);

        /// <summary>
        /// Computes the length of the geometry.
        /// </summary>
        float ComputeLength(
            Matrix3x2* worldTransform,
            float flatteningTolerance);

        /// <summary>
        /// Computes the point and tangent a given distance along the path.
        /// </summary>
        void ComputePointAtLength(
            float length,
            Matrix3x2* worldTransform,
            float flatteningTolerance,
            PointF* point,
            PointF* unitTangentVector);

        /// <summary>
        /// Get the geometry and widen it as well as apply an optional pen style.
        /// </summary>
        void Widen(
            float strokeWidth,
            IStrokeStyle strokeStyle,
            Matrix3x2* worldTransform,
            float flatteningTolerance,
            ISimplifiedGeometrySink geometrySink);
    }

    public static class GeometryExtensions
    {
        /// <summary>
        /// Retrieve the bounds of the geometry.
        /// </summary>
        public static unsafe RectangleF GetBounds(this IGeometry geometry) => geometry.GetBounds(null);

        /// <summary>
        /// Retrieve the bounds of the geometry with a transform.
        /// </summary>
        public static unsafe RectangleF GetBounds(this IGeometry geometry, Matrix3x2 worldTransform)
            => geometry.GetBounds(&worldTransform);
    }
}
