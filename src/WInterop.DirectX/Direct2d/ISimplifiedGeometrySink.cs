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
    /// Describes a geometric path that does not contain quadratic bezier curves or arcs.
    /// [ID2D1SimplifiedGeometrySink]
    /// </summary>
    [ComImport,
        Guid(InterfaceIds.IID_ID2D1SimplifiedGeometrySink),
        InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface ISimplifiedGeometrySink
    {
        [PreserveSig]
        void SetFillMode(
            FillMode fillMode);

        [PreserveSig]
        void SetSegmentFlags(
            PathSegment vertexFlags);

        [PreserveSig]
        void BeginFigure(
            PointF startPoint,
            FigureBegin figureBegin);

        [PreserveSig]
        unsafe void AddLines(
            PointF* points,
            uint pointsCount);

        [PreserveSig]
        unsafe void AddBeziers(
            BezierSegment* beziers,
            uint beziersCount);

        [PreserveSig]
        void EndFigure(
            FigureEnd figureEnd);

        [PreserveSig]
        void Close();
    }

    public static class SimplifiedGeometrySinkExtensions
    {
        public unsafe static void AddLines(this IGeometrySink sink, ReadOnlySpan<PointF> points)
        {
            fixed (PointF* p = &MemoryMarshal.GetReference(points))
            {
                sink.AddLines(p, (uint)points.Length);
            }
        }
    }
}
