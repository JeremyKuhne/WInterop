// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace WInterop.Direct2d
{
    /// <summary>
    ///  Describes a geometric path that does not contain quadratic bezier curves or arcs.
    ///  [ID2D1SimplifiedGeometrySink]
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    [Guid(InterfaceIds.IID_ID2D1SimplifiedGeometrySink)]
    public unsafe struct SimplifiedGeometrySink : SimplifiedGeometrySink.Interface, IDisposable
    {
        internal ID2D1SimplifiedGeometrySink* Handle { get; }

        internal SimplifiedGeometrySink(ID2D1SimplifiedGeometrySink* handle) => Handle = handle;

        public void AddBeziers(ReadOnlySpan<BezierSegment> beziers)
        {
            fixed(BezierSegment* b = beziers)
            {
                Handle->AddBeziers((D2D1_BEZIER_SEGMENT*)b, (uint)beziers.Length);
            }
        }

        public void AddLines(ReadOnlySpan<PointF> points)
        {
            fixed(PointF* p = points)
            {
                Handle->AddLines((D2D_POINT_2F*)p, (uint)points.Length);
            }
        }

        public void BeginFigure(PointF startPoint, FigureBegin figureBegin)
            => Handle->BeginFigure(startPoint.ToD2D(), (D2D1_FIGURE_BEGIN)figureBegin);

        public void BeginFigure((float X, float Y) startPoint, FigureBegin figureBegin)
            => Handle->BeginFigure(new D2D_POINT_2F(startPoint.X, startPoint.Y), (D2D1_FIGURE_BEGIN)figureBegin);

        public void Close() => Handle->Close().ThrowIfFailed();

        public void EndFigure(FigureEnd figureEnd) => Handle->EndFigure((D2D1_FIGURE_END)figureEnd);

        public void SetFillMode(FillMode fillMode) => Handle->SetFillMode((D2D1_FILL_MODE)fillMode);

        public void SetSegmentFlags(PathSegment vertexFlags) => Handle->SetSegmentFlags((D2D1_PATH_SEGMENT)vertexFlags);

        internal static ref SimplifiedGeometrySink From<TFrom>(in TFrom from)
            where TFrom : unmanaged, Interface
            => ref Unsafe.AsRef<SimplifiedGeometrySink>(Unsafe.AsPointer(ref Unsafe.AsRef(from)));

        public void Dispose()
        {
            Handle->Close();
            Handle->Release();
        }

        internal interface Interface : IDisposable
        {
            void SetFillMode(FillMode fillMode);

            void SetSegmentFlags(PathSegment vertexFlags);

            void BeginFigure(
                PointF startPoint,
                FigureBegin figureBegin);

            void AddLines(ReadOnlySpan<PointF> points);

            void AddBeziers(ReadOnlySpan<BezierSegment> beziers);

            void EndFigure(FigureEnd figureEnd);

            void Close();
        }
    }
}
