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
        void AddLines(
            ref PointF points,
            uint pointsCount);

        [PreserveSig]
        void AddBeziers(
            ref BezierSegment beziers,
            uint beziersCount);

        [PreserveSig]
        void EndFigure(
            FigureEnd figureEnd);

        [PreserveSig]
        void Close();
    }
}
