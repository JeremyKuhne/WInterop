// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Drawing;
using System.Numerics;
using System.Runtime.InteropServices;
using WInterop;
using WInterop.Direct2d;
using WInterop.DirectWrite;

namespace DirectWriteDemo
{
    public class CustomTextRenderer : ITextRenderer
    {
        private readonly IRenderTarget _renderTarget;
        private readonly IBrush _outlineBrush;
        private readonly IBrush _fillBrush;
        private readonly WInterop.Direct2d.IFactory _factory;

        public CustomTextRenderer(
            WInterop.Direct2d.IFactory factory,
            IRenderTarget renderTarget,
            IBrush outlineBrush,
            IBrush fillBrush)
        {
            _renderTarget = renderTarget;
            _factory = factory;
            _outlineBrush = outlineBrush;
            _fillBrush = fillBrush;
        }

        public IntBoolean IsPixelSnappingDisabled(IntPtr clientDrawingContext) => false;

        public Matrix3x2 GetCurrentTransform(IntPtr clientDrawingContext)
        {
            _renderTarget.GetTransform(out Matrix3x2 transform);
            return transform;
        }

        public float GetPixelsPerDip(IntPtr clientDrawingContext)
        {
            _renderTarget.GetDpi(out float x, out _);
            return x / 96;
        }

        public unsafe void DrawGlyphRun(
            IntPtr clientDrawingContext,
            float baselineOriginX,
            float baselineOriginY,
            MeasuringMode measuringMode,
            in GlyphRun glyphRun,
            in GlyphRunDescription glyphRunDescription,
            [MarshalAs(UnmanagedType.IUnknown)] object clientDrawingEffect)
        {
            IPathGeometry geometry = _factory.CreatePathGeometry();
            IGeometrySink sink = geometry.Open();
            glyphRun.FontFace.GetGlyphRunOutline(
                glyphRun.FontEmSize,
                glyphRun.GlyphIndices,
                glyphRun.GlyphAdvances,
                glyphRun.GlyphOffsets,
                glyphRun.GlyphCount,
                glyphRun.IsSideways,
                glyphRun.BidiLevel % 2 > 0,
                sink);
            sink.Close();

            Matrix3x2 matrix = new Matrix3x2(1, 0, 0, 1, baselineOriginX, baselineOriginY);
            ITransformedGeometry transformedGeometry = _factory.CreateTransformedGeometry(geometry, ref matrix);

            _renderTarget.DrawGeometry(transformedGeometry, _outlineBrush);
            _renderTarget.FillGeometry(transformedGeometry, _fillBrush);
        }

        public void DrawUnderline(
            IntPtr clientDrawingContext,
            float baselineOriginX,
            float baselineOriginY,
            in Underline underline,
            [MarshalAs(UnmanagedType.IUnknown)] object clientDrawingEffect)
        {
            IRectangleGeometry geometry = _factory.CreateRectangleGeometry(RectangleF.FromLTRB(
                0, underline.Offset, underline.Width, underline.Offset + underline.Thickness));

            Matrix3x2 matrix = new Matrix3x2(1, 0, 0, 1, baselineOriginX, baselineOriginY);
            ITransformedGeometry transformedGeometry = _factory.CreateTransformedGeometry(geometry, ref matrix);

            _renderTarget.DrawGeometry(transformedGeometry, _outlineBrush);
            _renderTarget.FillGeometry(transformedGeometry, _fillBrush);
        }

        public void DrawStrikethrough(
            IntPtr clientDrawingContext,
            float baselineOriginX,
            float baselineOriginY,
            in Strikethrough strikethrough,
            [MarshalAs(UnmanagedType.IUnknown)] object clientDrawingEffect)
        {
            IRectangleGeometry geometry = _factory.CreateRectangleGeometry(RectangleF.FromLTRB(
                0, strikethrough.Offset, strikethrough.Width, strikethrough.Offset + strikethrough.Thickness));

            Matrix3x2 matrix = new Matrix3x2(1, 0, 0, 1, baselineOriginX, baselineOriginY);
            ITransformedGeometry transformedGeometry = _factory.CreateTransformedGeometry(geometry, ref matrix);

            _renderTarget.DrawGeometry(transformedGeometry, _outlineBrush);
            _renderTarget.FillGeometry(transformedGeometry, _fillBrush);
        }

        public void DrawInlineObject(
            IntPtr clientDrawingContext,
            float originX,
            float originY,
            IInlineObject inlineObject,
            IntBoolean isSideways,
            IntBoolean isRightToLeft,
            [MarshalAs(UnmanagedType.IUnknown)] object clientDrawingEffect)
        {
            throw new NotImplementedException();
        }
    }
}
