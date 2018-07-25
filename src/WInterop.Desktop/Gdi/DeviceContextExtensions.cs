// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Drawing;
using WInterop.Gdi;
using WInterop.Gdi.Types;

namespace WInterop.Gdi
{
    public static partial class DeviceContextExtensions
    {
        public static int GetDeviceCapability(in this DeviceContext context, DeviceCapability capability)
            => GdiMethods.GetDeviceCapability(in context, capability);
        public static GdiObjectHandle SelectObject(in this DeviceContext context, GdiObjectHandle @object)
            => GdiMethods.SelectObject(in context, @object);
        public static COLORREF GetTextColor(in this DeviceContext context)
            => GdiMethods.GetTextColor(in context);
        public static COLORREF SetTextColor(in this DeviceContext context, COLORREF color)
            => GdiMethods.SetTextColor(in context, color);
        public static bool TextOut(in this DeviceContext context, int x, int y, string text)
            => GdiMethods.TextOut(in context, x, y, text);
        public static unsafe bool TextOut(in this DeviceContext context, int x, int y, char* text, int length)
            => GdiMethods.TextOut(in context, x, y, text, length);
        public static unsafe int DrawText(in this DeviceContext context, string text, RECT rect, TextFormat format)
            => GdiMethods.DrawText(in context, text, rect, format);
        public static TextAlignment SetTextAlignment(in this DeviceContext context, TextAlignment alignment)
            => GdiMethods.SetTextAlignment(in context, alignment);
        public static bool GetTextMetrics(in this DeviceContext context, out TEXTMETRIC metrics)
            => GdiMethods.GetTextMetrics(in context, out metrics);
        public static bool MoveTo(in this DeviceContext context, int x, int y)
            => GdiMethods.MoveTo(in context, x, y);
        public static bool LineTo(in this DeviceContext context, int x, int y)
            => GdiMethods.LineTo(in context, x, y);
        public static PenHandle GetCurrentPen(in this DeviceContext context)
            => GdiMethods.GetCurrentPen(in context);
        public static COLORREF GetBrushColor(in this DeviceContext context)
            => GdiMethods.GetBrushColor(in context);
        public static COLORREF GetBackgroundColor(in this DeviceContext context)
            => GdiMethods.GetBackgroundColor(in context);
        public static COLORREF SetBackgroundColor(in this DeviceContext context, COLORREF color)
            => GdiMethods.SetBackgroundColor(in context, color);
        public static BackgroundMode SetBackgroundMode(in this DeviceContext context, BackgroundMode mode)
            => GdiMethods.SetBackgroundMode(in context, mode);
        public static BackgroundMode GetBackgroundMode(in this DeviceContext context) => GdiMethods.GetBackgroundMode(in context);
        public static PenMixMode SetRasterOperation(in this DeviceContext context, PenMixMode foregroundMixMode)
            => GdiMethods.SetRasterOperation(in context, foregroundMixMode);
        public static PenMixMode GetRasterOperation(in this DeviceContext context) => GdiMethods.GetRasterOperation(in context);
        public static PolyFillMode GetPolyFillMode(in this DeviceContext context)
            => GdiMethods.GetPolyFillMode(in context);
        public static PolyFillMode SetPolyFillMode(in this DeviceContext context, PolyFillMode fillMode)
            => GdiMethods.SetPolyFillMode(in context, fillMode);
        public unsafe static bool Polygon(in this DeviceContext context, params Point[] points)
            => GdiMethods.Polygon(in context, points);
        public static bool Polyline(in this DeviceContext context, params Point[] points)
            => GdiMethods.Polyline(in context, points);
        public static bool Rectangle(in this DeviceContext context, int left, int top, int right, int bottom)
            => GdiMethods.Rectangle(in context, left, top, right, bottom);
        public static bool Rectangle(in this DeviceContext context, RECT rectangle) => GdiMethods.Rectangle(in context, rectangle);
        public static bool FillRectangle(in this DeviceContext context, RECT rectangle, BrushHandle brush)
            => GdiMethods.FillRectangle(in context, rectangle, brush);
        public static bool FrameRectangle(in this DeviceContext context, RECT rectangle, BrushHandle brush)
            => GdiMethods.FrameRectangle(in context, rectangle, brush);
        public static bool InvertRectangle(in this DeviceContext context, RECT rectangle)
            => GdiMethods.InvertRectangle(in context, rectangle);
        public static bool DrawFocusRectangle(in this DeviceContext context, RECT rectangle)
            => GdiMethods.DrawFocusRectangle(in context, rectangle);
        public static bool Ellipse(in this DeviceContext context, int left, int top, int right, int bottom)
            => GdiMethods.Ellipse(in context, left, top, right, bottom);
        public static bool Ellipse(in this DeviceContext context, Rectangle bounds)
            => GdiMethods.Ellipse(in context, bounds.Left, bounds.Top, bounds.Right, bounds.Bottom);
        public static bool RoundRectangle(in this DeviceContext context, int left, int top, int right, int bottom, int cornerWidth, int cornerHeight)
            => GdiMethods.RoundRectangle(in context, left, top, right, bottom, cornerWidth, cornerHeight);
        public static bool PolyBezier(in this DeviceContext context, params Point[] points)
            => GdiMethods.PolyBezier(in context, points);
        public static COLORREF GetPixel(in this DeviceContext context, int x, int y) => GdiMethods.GetPixel(in context, x, y);
        public static COLORREF GetPixel(in this DeviceContext context, Point point) => GdiMethods.GetPixel(in context, point.X, point.Y);
        public static bool SetPixel(in this DeviceContext context, Point point, COLORREF color) => GdiMethods.SetPixel(in context, point.X, point.Y, color);
        public static bool SetPixel(in this DeviceContext context, int x, int y, COLORREF color) => GdiMethods.SetPixel(in context, x, y, color);
        public static bool DeviceToLogical(in this DeviceContext context, params Point[] points) => GdiMethods.DeviceToLogical(in context, points);
        public static bool LogicalToDevice(in this DeviceContext context, params Point[] points) => GdiMethods.LogicalToDevice(in context, points);
        public static bool SetViewportOrigin(in this DeviceContext context, int x, int y) => GdiMethods.SetViewportOrigin(in context, x, y);
        public static bool SetWindowOrigin(in this DeviceContext context, int x, int y) => GdiMethods.SetWindowOrigin(in context, x, y);
        public static bool OffsetWindowOrigin(in this DeviceContext context, int x, int y) => GdiMethods.OffsetWindowOrigin(in context, x, y);
        public static bool OffsetViewportOrigin(in this DeviceContext context, int x, int y) => GdiMethods.OffsetViewportOrigin(in context, x, y);
        public static bool SetWindowExtents(in this DeviceContext context, int x, int y) => GdiMethods.SetWindowExtents(in context, x, y);
        public static bool SetViewportExtents(in this DeviceContext context, int x, int y) => GdiMethods.SetViewportExtents(in context, x, y);
        public static MapMode SetMapMode(in this DeviceContext context, MapMode mapMode) => GdiMethods.SetMapMode(in context, mapMode);

        public static RegionType SelectClippingRegion(in this DeviceContext context, RegionHandle region)
            => GdiMethods.SelectClippingRegion(in context, region);
        public static RegionType CombineRegion(this RegionHandle destination, RegionHandle sourceOne, RegionHandle sourceTwo, CombineRegionMode mode)
            => GdiMethods.CombineRegion(destination, sourceOne, sourceTwo, mode);
        public static Rectangle GetClipBox(in this DeviceContext context, out RegionType complexity)
            => GdiMethods.GetClipBox(in context, out complexity);

        public static DeviceContext CreateCompatibleDeviceContext(in this DeviceContext context)
            => GdiMethods.CreateCompatibleDeviceContext(in context);
        public static BitmapHandle CreateCompatibleBitmap(in this DeviceContext context, Size size)
            => GdiMethods.CreateCompatibleBitmap(in context, size);
        public static void BitBlt(
            this in DeviceContext source,
            in DeviceContext destination,
            Point sourceOrigin,
            Rectangle destinationRect,
            RasterOperation operation)
            => GdiMethods.BitBlt(in source, in destination, sourceOrigin, destinationRect, operation);


    }
}
