// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Drawing;
using System.Numerics;
using WInterop.Direct2d;
using WInterop.DirectWrite;

namespace DirectWriteDemo;

public class SampleTextRenderer : ManagedTextRenderer
{
    private readonly IRenderTarget _renderTarget;
    private readonly Brush _outlineBrush;
    private readonly Brush _fillBrush;
    private readonly Factory _factory;

    public SampleTextRenderer(
        Factory factory,
        IRenderTarget renderTarget,
        Brush outlineBrush,
        Brush fillBrush)
    {
        _renderTarget = renderTarget;
        _factory = factory;
        _outlineBrush = outlineBrush;
        _fillBrush = fillBrush;
    }

    public override Matrix3x2 GetCurrentTransform(IntPtr clientDrawingContext)
        => _renderTarget.Transform;

    public override float GetPixelsPerDip(IntPtr clientDrawingContext)
         => _renderTarget.Dpi.X / 96;

    public override void DrawGlyphRun(
        IntPtr clientDrawingContext,
        PointF baselineOrigin,
        MeasuringMode measuringMode,
        GlyphRun glyphRun,
        GlyphRunDescription glyphRunDescription,
        IntPtr clientDrawingEffect)
    {
        using PathGeometry geometry = _factory.CreatePathGeometry();
        using GeometrySink sink = geometry.Open();
        glyphRun.FontFace.GetGlyphRunOutline(
            glyphRun.FontEmSize,
            glyphRun.GlyphIndices,
            glyphRun.GlyphAdvances,
            glyphRun.GlyphOffsets,
            glyphRun.IsSideways,
            glyphRun.BidiLevel % 2 > 0,
            sink);
        sink.Close();

        Matrix3x2 matrix = new(1, 0, 0, 1, baselineOrigin.X, baselineOrigin.Y);
        using TransformedGeometry transformedGeometry = _factory.CreateTransformedGeometry(geometry, matrix);

        _renderTarget.DrawGeometry(transformedGeometry, _outlineBrush);
        _renderTarget.FillGeometry(transformedGeometry, _fillBrush);
    }

    public override void DrawUnderline(
        IntPtr clientDrawingContext,
        PointF baselineOrigin,
        Underline underline,
        IntPtr clientDrawingEffect)
    {
        using RectangleGeometry geometry = _factory.CreateRectangleGeometry(RectangleF.FromLTRB(
            0, underline.Offset, underline.Width, underline.Offset + underline.Thickness));

        Matrix3x2 matrix = new(1, 0, 0, 1, baselineOrigin.X, baselineOrigin.Y);
        TransformedGeometry transformedGeometry = _factory.CreateTransformedGeometry(geometry, matrix);

        _renderTarget.DrawGeometry(transformedGeometry, _outlineBrush);
        _renderTarget.FillGeometry(transformedGeometry, _fillBrush);
    }

    public override void DrawStrikethrough(
        IntPtr clientDrawingContext,
        PointF baselineOrigin,
        Strikethrough strikethrough,
        IntPtr clientDrawingEffect)
    {
        using RectangleGeometry geometry = _factory.CreateRectangleGeometry(RectangleF.FromLTRB(
            0, strikethrough.Offset, strikethrough.Width, strikethrough.Offset + strikethrough.Thickness));

        Matrix3x2 matrix = new(1, 0, 0, 1, baselineOrigin.X, baselineOrigin.Y);
        TransformedGeometry transformedGeometry = _factory.CreateTransformedGeometry(geometry, matrix);

        _renderTarget.DrawGeometry(transformedGeometry, _outlineBrush);
        _renderTarget.FillGeometry(transformedGeometry, _fillBrush);
    }

    public override void DrawInlineObject(
        IntPtr clientDrawingContext,
        PointF origin,
        InlineObject inlineObject,
        bool isSideways,
        bool isRightToLeft,
        IntPtr clientDrawingEffect)
    {
        throw new NotImplementedException();
    }
}
