// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Drawing;
using System.Runtime.InteropServices;

namespace WInterop.Direct2d
{
    /// <summary>
    /// Describes a geometric path that can contain lines, arcs, cubic Bezier curves,
    /// and quadratic Bezier curves. [ID2D1GeometrySink]
    /// </summary>
    [ComImport,
        Guid(InterfaceIds.IID_ID2D1GeometrySink),
        InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IGeometrySink : ISimplifiedGeometrySink
    {
        #region ID2D1SimplifiedGeometrySink
        [PreserveSig]
        new void SetFillMode(
            FillMode fillMode);

        [PreserveSig]
        new void SetSegmentFlags(
            PathSegment vertexFlags);

        [PreserveSig]
        new void BeginFigure(
            PointF startPoint,
            FigureBegin figureBegin);

        [PreserveSig]
        unsafe new void AddLines(
            PointF* points,
            uint pointsCount);

        [PreserveSig]
        unsafe new void AddBeziers(
            BezierSegment* beziers,
            uint beziersCount);

        [PreserveSig]
        new void EndFigure(
            FigureEnd figureEnd);

        [PreserveSig]
        new void Close();
        #endregion

        [PreserveSig]
        void AddLine(
            PointF point);

        [PreserveSig]
        void AddBezier(
            in BezierSegment bezier);

        [PreserveSig]
        void AddQuadraticBezier(
            in QuadraticBezierSegment bezier);

        [PreserveSig]
        void AddQuadraticBeziers(
            in QuadraticBezierSegment bezier,
            uint beziersCount);

        [PreserveSig]
        void AddArc(
            in ArcSegment arc);
    }

    public static class GeometrySinkExtensions
    {
        public unsafe static void AddBezier(this IGeometrySink sink, (float X, float Y) point1, (float X, float Y) point2, (float X, float Y) point3)
            => sink.AddBezier(new BezierSegment(point1, point2, point3));
    }
}
