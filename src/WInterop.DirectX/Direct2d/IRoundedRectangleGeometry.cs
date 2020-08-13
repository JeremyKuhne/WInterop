// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Drawing;
using System.Numerics;
using System.Runtime.InteropServices;

namespace WInterop.Direct2d
{
    /// <summary>
    ///  [ID2D1RoundedRectangleGeometry]
    /// </summary>
    [ComImport,
        Guid(InterfaceIds.IID_ID2D1RoundedRectangleGeometry),
        InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public unsafe interface IRoundedRectangleGeometry : IGeometry
    {
        #region ID2D1Resource
        [PreserveSig]
        new void GetFactory(
            out IFactory factory);
        #endregion

        #region ID2D1Geometry
        /// <summary>
        ///  Retrieve the bounds of the geometry, with an optional applied transform.
        /// </summary>
        new LtrbRectangleF GetBounds(
            Matrix3x2* worldTransform);

        /// <summary>
        ///  Get the bounds of the corresponding geometry after it has been widened or have
        ///  an optional pen style applied.
        /// </summary>
        new LtrbRectangleF GetWidenedBounds(
            float strokeWidth,
            IStrokeStyle strokeStyle,
            Matrix3x2* worldTransform,
            float flatteningTolerance);

        /// <summary>
        ///  Checks to see whether the corresponding penned and widened geometry contains the
        ///  given point.
        /// </summary>
        new IntBoolean StrokeContainsPoint(
            PointF point,
            float strokeWidth,
            IStrokeStyle strokeStyle,
            Matrix3x2* worldTransform,
            float flatteningTolerance);

        /// <summary>
        ///  Test whether the given fill of this geometry would contain this point.
        /// </summary>
        new IntBoolean FillContainsPoint(
            PointF point,
            Matrix3x2* worldTransform,
            float flatteningTolerance);

        /// <summary>
        ///  Compare how one geometry intersects or contains another geometry.
        /// </summary>
        new GeometryRelation CompareWithGeometry(
            IGeometry inputGeometry,
            Matrix3x2* inputGeometryTransform,
            float flatteningTolerance);

        /// <summary>
        ///  Converts a geometry to a simplified geometry that has arcs and quadratic beziers
        ///  removed.
        /// </summary>
        new void Simplify(
            GeometrySimplificationOption simplificationOption,
            Matrix3x2* worldTransform,
            float flatteningTolerance,
            ISimplifiedGeometrySink geometrySink);

        /// <summary>
        ///  Tessellates a geometry into triangles.
        /// </summary>
        new void Tessellate(
            Matrix3x2* worldTransform,
            float flatteningTolerance,
            ITesselationSink tessellationSink);

        /// <summary>
        ///  Performs a combine operation between the two geometries to produce a resulting
        ///  geometry.
        /// </summary>
        new void CombineWithGeometry(
            IGeometry inputGeometry,
            CombineMode combineMode,
            Matrix3x2* inputGeometryTransform,
            float flatteningTolerance,
            ISimplifiedGeometrySink geometrySink);

        /// <summary>
        ///  Computes the outline of the geometry. The result is written back into a
        ///  simplified geometry sink.
        /// </summary>
        new void Outline(
            Matrix3x2* worldTransform,
            float flatteningTolerance,
            ISimplifiedGeometrySink geometrySink);

        /// <summary>
        ///  Computes the area of the geometry.
        /// </summary>
        new float ComputeArea(
            Matrix3x2* worldTransform,
            float flatteningTolerance);

        /// <summary>
        ///  Computes the length of the geometry.
        /// </summary>
        new float ComputeLength(
            Matrix3x2* worldTransform,
            float flatteningTolerance);

        /// <summary>
        ///  Computes the point and tangent a given distance along the path.
        /// </summary>
        new void ComputePointAtLength(
            float length,
            Matrix3x2* worldTransform,
            float flatteningTolerance,
            PointF* point,
            PointF* unitTangentVector);

        /// <summary>
        ///  Get the geometry and widen it as well as apply an optional pen style.
        /// </summary>
        new void Widen(
            float strokeWidth,
            IStrokeStyle strokeStyle,
            Matrix3x2* worldTransform,
            float flatteningTolerance,
            ISimplifiedGeometrySink geometrySink);
        #endregion

        [PreserveSig]
        void GetRoundedRect(
            out RoundedRectangle roundedRect);
    }
}
