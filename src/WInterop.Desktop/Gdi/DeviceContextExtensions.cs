// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Drawing;

namespace WInterop.Gdi
{
    public static partial class DeviceContextExtensions
    {
        public static int GetDeviceCapability(in this DeviceContext context, DeviceCapability capability)
            => Gdi.GetDeviceCapability(in context, capability);
        public static GdiObjectHandle SelectObject(in this DeviceContext context, GdiObjectHandle @object)
            => Gdi.SelectObject(in context, @object);
        public static Color GetTextColor(in this DeviceContext context)
            => Gdi.GetTextColor(in context);
        public static Color SetTextColor(in this DeviceContext context, Color color)
            => Gdi.SetTextColor(in context, color);
        public static bool TextOut(in this DeviceContext context, Point position, ReadOnlySpan<char> text)
            => Gdi.TextOut(in context, position, text);
        public static unsafe int DrawText(in this DeviceContext context, ReadOnlySpan<char> text, Rectangle rect, TextFormat format)
            => Gdi.DrawText(in context, text, rect, format);
        public static TextAlignment SetTextAlignment(in this DeviceContext context, TextAlignment alignment)
            => Gdi.SetTextAlignment(in context, alignment);
        public static bool GetTextMetrics(in this DeviceContext context, out TEXTMETRIC metrics)
            => Gdi.GetTextMetrics(in context, out metrics);
        public static bool MoveTo(in this DeviceContext context, int x, int y)
            => Gdi.MoveTo(in context, x, y);
        public static bool LineTo(in this DeviceContext context, int x, int y)
            => Gdi.LineTo(in context, x, y);
        public static PenHandle GetCurrentPen(in this DeviceContext context)
            => Gdi.GetCurrentPen(in context);
        public static Color GetBrushColor(in this DeviceContext context)
            => Gdi.GetBrushColor(in context);
        public static Color GetBackgroundColor(in this DeviceContext context)
            => Gdi.GetBackgroundColor(in context);
        public static Color SetBackgroundColor(in this DeviceContext context, Color color)
            => Gdi.SetBackgroundColor(in context, color);
        public static BackgroundMode SetBackgroundMode(in this DeviceContext context, BackgroundMode mode)
            => Gdi.SetBackgroundMode(in context, mode);
        public static BackgroundMode GetBackgroundMode(in this DeviceContext context) => Gdi.GetBackgroundMode(in context);
        public static PenMixMode SetRasterOperation(in this DeviceContext context, PenMixMode foregroundMixMode)
            => Gdi.SetRasterOperation(in context, foregroundMixMode);
        public static PenMixMode GetRasterOperation(in this DeviceContext context) => Gdi.GetRasterOperation(in context);
        public static PolyFillMode GetPolyFillMode(in this DeviceContext context)
            => Gdi.GetPolyFillMode(in context);
        public static PolyFillMode SetPolyFillMode(in this DeviceContext context, PolyFillMode fillMode)
            => Gdi.SetPolyFillMode(in context, fillMode);
        public unsafe static bool Polygon(in this DeviceContext context, params Point[] points)
            => Gdi.Polygon(in context, points);
        public unsafe static bool Polygon(in this DeviceContext context, ReadOnlySpan<Point> points)
            => Gdi.Polygon(in context, points);
        public static bool Polyline(in this DeviceContext context, params Point[] points)
            => Gdi.Polyline(in context, points);
        public static bool Polyline(in this DeviceContext context, ReadOnlySpan<Point> points)
            => Gdi.Polyline(in context, points);
        public static bool Rectangle(in this DeviceContext context, Rectangle rectangle) => Gdi.Rectangle(in context, rectangle);
        public static bool FillRectangle(in this DeviceContext context, Rectangle rectangle, BrushHandle brush)
            => Gdi.FillRectangle(in context, rectangle, brush);
        public static bool FrameRectangle(in this DeviceContext context, Rectangle rectangle, BrushHandle brush)
            => Gdi.FrameRectangle(in context, rectangle, brush);
        public static bool InvertRectangle(in this DeviceContext context, Rectangle rectangle)
            => Gdi.InvertRectangle(in context, rectangle);
        public static bool DrawFocusRectangle(in this DeviceContext context, Rectangle rectangle)
            => Gdi.DrawFocusRectangle(in context, rectangle);
        public static bool Ellipse(in this DeviceContext context, Rectangle bounds)
            => Gdi.Ellipse(in context, bounds);
        public static bool RoundRectangle(in this DeviceContext context, Rectangle bounds, Size corner)
            => Gdi.RoundRectangle(in context, bounds, corner);
        public static bool PolyBezier(in this DeviceContext context, params Point[] points)
            => Gdi.PolyBezier(in context, points);
        public static Color GetPixel(in this DeviceContext context, int x, int y) => Gdi.GetPixel(in context, x, y);
        public static Color GetPixel(in this DeviceContext context, Point point) => Gdi.GetPixel(in context, point.X, point.Y);
        public static bool SetPixel(in this DeviceContext context, Point point, Color color) => Gdi.SetPixel(in context, point.X, point.Y, color);
        public static bool SetPixel(in this DeviceContext context, int x, int y, Color color) => Gdi.SetPixel(in context, x, y, color);
        public static bool DeviceToLogical(in this DeviceContext context, params Point[] points) => Gdi.DeviceToLogical(in context, points);
        public static bool LogicalToDevice(in this DeviceContext context, params Point[] points) => Gdi.LogicalToDevice(in context, points);
        public static bool SetViewportOrigin(in this DeviceContext context, int x, int y) => Gdi.SetViewportOrigin(in context, x, y);
        public static bool SetWindowOrigin(in this DeviceContext context, int x, int y) => Gdi.SetWindowOrigin(in context, x, y);
        public static bool OffsetWindowOrigin(in this DeviceContext context, int x, int y) => Gdi.OffsetWindowOrigin(in context, x, y);
        public static bool OffsetViewportOrigin(in this DeviceContext context, int x, int y) => Gdi.OffsetViewportOrigin(in context, x, y);
        public static bool SetWindowExtents(in this DeviceContext context, int x, int y) => Gdi.SetWindowExtents(in context, x, y);
        public static bool SetViewportExtents(in this DeviceContext context, int x, int y) => Gdi.SetViewportExtents(in context, x, y);
        public static MapMode SetMapMode(in this DeviceContext context, MapMode mapMode) => Gdi.SetMapMode(in context, mapMode);

        public static RegionType SelectClippingRegion(in this DeviceContext context, RegionHandle region)
            => Gdi.SelectClippingRegion(in context, region);
        public static RegionType CombineRegion(this RegionHandle destination, RegionHandle sourceOne, RegionHandle sourceTwo, CombineRegionMode mode)
            => Gdi.CombineRegion(destination, sourceOne, sourceTwo, mode);
        public static Rectangle GetClipBox(in this DeviceContext context, out RegionType complexity)
            => Gdi.GetClipBox(in context, out complexity);

        public static DeviceContext CreateCompatibleDeviceContext(in this DeviceContext context)
            => Gdi.CreateCompatibleDeviceContext(in context);
        public static BitmapHandle CreateCompatibleBitmap(in this DeviceContext context, Size size)
            => Gdi.CreateCompatibleBitmap(in context, size);
        public static void BitBlt(
            this in DeviceContext source,
            in DeviceContext destination,
            Point sourceOrigin,
            Rectangle destinationRect,
            RasterOperation operation)
            => Gdi.BitBlt(in source, in destination, sourceOrigin, destinationRect, operation);
    }
}
