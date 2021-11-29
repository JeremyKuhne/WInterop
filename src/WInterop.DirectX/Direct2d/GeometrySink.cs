// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace WInterop.Direct2d;

/// <summary>
///  Describes a geometric path that can contain lines, arcs, cubic Bezier curves,
///  and quadratic Bezier curves. [ID2D1GeometrySink]
/// </summary>
[StructLayout(LayoutKind.Sequential)]
[Guid(InterfaceIds.IID_ID2D1GeometrySink)]
public readonly unsafe struct GeometrySink : GeometrySink.Interface, IDisposable
{
    internal ID2D1GeometrySink* Handle { get; }

    public void AddArc(ArcSegment arc) => Handle->AddArc((D2D1_ARC_SEGMENT*)&arc);

    public void AddBezier(BezierSegment bezier) => Handle->AddBezier((D2D1_BEZIER_SEGMENT*)&bezier);

    public void AddBeziers(ReadOnlySpan<BezierSegment> beziers)
    {
        fixed (BezierSegment* b = beziers)
        {
            Handle->AddBeziers((D2D1_BEZIER_SEGMENT*)b, (uint)beziers.Length);
        }
    }

    public void AddLine(PointF point) => Handle->AddLine(point.ToD2D());

    public void AddLine((float X, float Y) point) => Handle->AddLine(new(point.X, point.Y));

    public void AddLines(ReadOnlySpan<PointF> points)
        => SimplifiedGeometrySink.From(this).AddLines(points);

    public void AddQuadraticBezier(QuadraticBezierSegment bezier)
        => Handle->AddQuadraticBezier((D2D1_QUADRATIC_BEZIER_SEGMENT*)&bezier);

    public void AddQuadraticBeziers(ReadOnlySpan<QuadraticBezierSegment> beziers)
    {
        fixed (QuadraticBezierSegment* b = beziers)
        {
            Handle->AddQuadraticBeziers((D2D1_QUADRATIC_BEZIER_SEGMENT*)b, (uint)beziers.Length);
        }
    }
    public void BeginFigure(PointF startPoint, FigureBegin figureBegin)
        => SimplifiedGeometrySink.From(this).BeginFigure(startPoint, figureBegin);

    public void BeginFigure((float X, float Y) startPoint, FigureBegin figureBegin)
        => SimplifiedGeometrySink.From(this).BeginFigure(startPoint, figureBegin);

    public void Close() => SimplifiedGeometrySink.From(this).Close();

    public void EndFigure(FigureEnd figureEnd)
        => SimplifiedGeometrySink.From(this).EndFigure(figureEnd);

    public void SetFillMode(FillMode fillMode)
        => SimplifiedGeometrySink.From(this).SetFillMode(fillMode);

    public void SetSegmentFlags(PathSegment vertexFlags)
        => SimplifiedGeometrySink.From(this).SetSegmentFlags(vertexFlags);

    public static implicit operator SimplifiedGeometrySink(GeometrySink sink)
        => new((ID2D1SimplifiedGeometrySink*)sink.Handle);

    internal static ref GeometrySink From<TFrom>(in TFrom from)
        where TFrom : unmanaged, Interface
        => ref Unsafe.AsRef<GeometrySink>(Unsafe.AsPointer(ref Unsafe.AsRef(from)));

    public void Dispose()
    {
        Handle->Close();
        Handle->Release();
    }

    internal interface Interface : SimplifiedGeometrySink.Interface
    {
        void AddLine(PointF point);

        void AddBezier(BezierSegment bezier);

        void AddQuadraticBezier(QuadraticBezierSegment bezier);

        void AddQuadraticBeziers(ReadOnlySpan<QuadraticBezierSegment> beziers);

        void AddArc(ArcSegment arc);
    }
}
