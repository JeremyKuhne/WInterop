// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Drawing;

namespace WInterop.Direct2d;

public static class RenderTargetExtensions
{
    public static SolidColorBrush CreateSolidColorBrush(
        this IRenderTarget renderTarget,
        float r,
        float g,
        float b,
        float a = 1.0f)
        => renderTarget.CreateSolidColorBrush(new(r, g, b, a));

    public static IBitmapRenderTarget CreateCompatibleRenderTarget(
        this IRenderTarget renderTarget,
        (float X, float Y) desiredSize)
        => renderTarget.CreateCompatibleRenderTarget(new(desiredSize.X, desiredSize.Y));

    public static BitmapBrush CreateBitmapBrush(
        this IRenderTarget renderTarget,
        Bitmap bitmap,
        BitmapBrushProperties bitmapBrushProperties)
        => renderTarget.CreateBitmapBrush(bitmap, bitmapBrushProperties, BrushProperties.Default);

    public static RadialGradientBrush CreateRadialGradientBrush(
        this IRenderTarget renderTarget,
        RadialGradientBrushProperties radialGradientBrushProperties,
        GradientStopCollection gradientStopCollection)
        => renderTarget.CreateRadialGradientBrush(radialGradientBrushProperties, BrushProperties.Default, gradientStopCollection);

    public static Tags EndDraw(
        this IRenderTarget renderTarget)
        => renderTarget.EndDraw(out _);

    public unsafe static void DrawLine(
        this IRenderTarget renderTarget,
        (float X, float Y) point0,
        (float X, float Y) point1,
        Brush brush,
        float strokeWidth = 1.0f,
        StrokeStyle strokeStyle = default)
        => renderTarget.DrawLine(
            *(PointF*)&point0,
            *(PointF*)&point1,
            brush,
            strokeWidth,
            strokeStyle);

    //public static void FillRectangle(
    //    this IRenderTarget renderTarget,
    //    (float Left, float Top, float Right, float Bottom) rectangle,
    //    Brush brush)
    //    => renderTarget.FillRectangle(LtrbRectangleF.)
}
