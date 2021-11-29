// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Drawing;
using System.Numerics;
using WInterop.DirectWrite;

namespace WInterop.Direct2d
{
    public interface IRenderTarget : IResource, IDisposable
    {
        SolidColorBrush CreateSolidColorBrush(ColorF color);

        /// <summary>
        ///  Creates a bitmap brush. The bitmap is scaled, rotated, skewed or tiled to fill or pen a geometry.
        /// </summary>
        BitmapBrush CreateBitmapBrush(
            Bitmap bitmap,
            BitmapBrushProperties bitmapBrushProperties,
            BrushProperties brushProperties);

        SolidColorBrush CreateSolidColorBrush(
            ColorF color,
            BrushProperties brushProperties);

        /// <summary>
        ///  A gradient stop collection represents a set of stops in an ideal unit length.
        ///  This is the source resource for a linear gradient and radial gradient brush.
        /// </summary>
        /// <param name="colorInterpolationGamma">
        ///  Specifies which space the color interpolation occurs in.
        /// </param>
        /// <param name="extendMode">
        ///  Specifies how the gradient will be extended outside of the unit length.
        /// </param>
        GradientStopCollection CreateGradientStopCollection(
            ReadOnlySpan<GradientStop> gradientStops,
            Gamma colorInterpolationGamma = Gamma.ColorSpace_2_2,
            ExtendMode extendMode = ExtendMode.Clamp);

        RadialGradientBrush CreateRadialGradientBrush(
            RadialGradientBrushProperties radialGradientBrushProperties,
            BrushProperties brushProperties,
            GradientStopCollection gradientStopCollection);

        /// <summary>
        ///  Creates a bitmap render target whose bitmap can be used as a source for rendering in the API.
        /// </summary>
        /// <param name="desiredSize">
        ///  The requested size of the target in DIPs. If the pixel size is not specified, the DPI is inherited from
        ///  the parent target. However, the render target will never contain a fractional number of pixels.
        /// </param>
        /// <param name="desiredPixelSize">
        ///  The requested size of the render target in pixels. If the DIP size is also specified, the DPI is
        ///  calculated from these two values. If the desired size is not specified, the DPI is inherited from the
        ///  parent render target. If neither value is specified, the compatible render target will be the same size
        ///  and have the same DPI as the parent target.
        /// </param>
        /// <param name="desiredFormat">
        ///  The desired pixel format. The format must be compatible with the parent render target type. If the format
        ///  is not specified, it will be inherited from the parent render target.
        /// </param>
        /// <param name="options">
        ///  Allows the caller to retrieve a GDI compatible render target.
        /// </param>
        /// <returns>The bitmap render target.</returns>
        IBitmapRenderTarget CreateCompatibleRenderTarget(
            SizeF? desiredSize = default,
            SizeU? desiredPixelSize = default,
            PixelFormat? desiredFormat = default,
            CompatibleRenderTargetOptions options = CompatibleRenderTargetOptions.None);

        void DrawLine(
            PointF point0,
            PointF point1,
            Brush brush,
            float strokeWidth = 1.0f,
            StrokeStyle strokeStyle = default);

        void DrawRectangle(
            LtrbRectangleF rectangle,
            Brush brush,
            float strokeWidth = 1.0f,
            StrokeStyle strokeStyle = default);

        void FillRectangle(
            LtrbRectangleF rectangle,
            Brush brush);

        void DrawRoundedRectangle(
            RoundedRectangle roundedRectangle,
            Brush brush,
            float strokeWidth = 1.0f,
            StrokeStyle strokeStyle = default);

        void FillRoundedRectangle(
            RoundedRectangle roundedRectangle,
            Brush brush);

        void DrawEllipse(
            Ellipse ellipse,
            Brush brush,
            float strokeWidth = 1.0f,
            StrokeStyle strokeStyle = default);

        void FillEllipse(
            Ellipse ellipse,
            Brush brush);

        void DrawGeometry(
            Geometry geometry,
            Brush brush,
            float strokeWidth = 1.0f,
            StrokeStyle strokeStyle = default);

        /// <param name="opacityBrush">
        ///  An optionally specified opacity brush. Only the alpha channel of the corresponding brush will be sampled
        ///  and will be applied to the entire fill of the geometry. If this brush is specified, the fill brush must be
        ///  a bitmap brush with an extend mode of <see cref="ExtendMode.Clamp"/>.
        /// </param>
        void FillGeometry(
            Geometry geometry,
            Brush brush,
            Brush opacityBrush = default);

        /// <summary>
        ///  Draw a text layout object. If the layout is not subsequently changed, this can
        ///  be more efficient than DrawText when drawing the same layout repeatedly.
        /// </summary>
        /// <param name="options">
        ///  The specified text options. If <see cref="DrawTextOptions.Clip"/> is used, the text
        ///  is clipped to the layout bounds. These bounds are derived from the origin and the
        ///  layout bounds of the corresponding <see cref="TextLayout"/> object.
        /// </param>
        void DrawTextLayout(
            PointF origin,
            TextLayout textLayout,
            Brush defaultFillBrush,
            DrawTextOptions options = DrawTextOptions.None);

        /*
        void DrawGlyphRun(
            PointF baselineOrigin,
            GlyphRun glyphRun,
            Brush foregroundBrush,
            MeasuringMode measuringMode = MeasuringMode.Natural);
        */

        Matrix3x2 Transform { get; set; }

        AntialiasMode AntialiasMode { get; set; }

        TextAntialiasMode TextAntialiasMode { get; set; }

        /// <summary>
        ///  Set a tag to correspond to the succeeding primitives. If an error occurs
        ///  rendering a primitive, the tags can be returned from the Flush or EndDraw call.
        ///
        ///  This does not retrieve the tags corresponding to any primitive that is in error.
        /// </summary>
        public Tags Tags { get; set; }

        /// <summary>
        ///  Ends a layer that was defined with particular layer resources.
        /// </summary>
        void PopLayer();

        Tags Flush();

        /// <summary>
        ///  Pushes a clip. The clip can be antialiased. The clip must be axis aligned. If
        ///  the current world transform is not axis preserving, then the bounding box of the
        ///  transformed clip rect will be used. The clip will remain in effect until a
        ///  PopAxisAligned clip call is made.
        /// </summary>
        void PushAxisAlignedClip(
            LtrbRectangleF clipRect,
            AntialiasMode antialiasMode);

        void PopAxisAlignedClip();

        void Clear();

        void Clear(ColorF clearColor);

        /// <summary>
        ///  Start drawing on this render target. Draw calls can only be issued between a
        ///  BeginDraw and EndDraw call.
        /// </summary>
        void BeginDraw();

        /// <summary>
        ///  Ends drawing on the render target, error results can be retrieved at this time,
        ///  or when calling flush.
        /// </summary>
        Tags EndDraw(out bool recreateTarget);

        PixelFormat PixelFormat { get; }

        /// <summary>
        ///  The DPI on the render target. This results in the render target being
        ///  interpreted to a different scale. Neither DPI can be negative. If zero is
        ///  specified for both, the system DPI is chosen. If one is zero and the other
        ///  unspecified, the DPI is not changed.
        /// </summary>
        PointF Dpi { get; set; }

        /// <summary>
        ///  Returns the size of the render target in DIPs.
        /// </summary>
        SizeF Size { get; }

        /// <summary>
        ///  Returns the size of the render target in pixels.
        /// </summary>
        SizeU PixelSize { get; }

        /// <summary>
        ///  Returns the maximum bitmap and render target size that is guaranteed to be
        ///  supported by the render target.
        /// </summary>
        uint MaximumBitmapSize { get; }

        /// <summary>
        ///  Returns true if the given properties are supported by this render target. The
        ///  DPI is ignored. NOTE: If the render target type is software, then neither
        ///  D2D1_FEATURE_LEVEL_9 nor D2D1_FEATURE_LEVEL_10 will be considered to be
        ///  supported.
        /// </summary>
        bool IsSupported(RenderTargetProperties renderTargetProperties);
    }
}
