// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Drawing;
using WInterop.Gdi;
using WInterop.Gdi.Types;

namespace WInterop.Extensions.WindowExtensions
{
    public static partial class DeviceContextExtensions
    {
        public static int GetDeviceCapability(in this DeviceContext deviceContext, DeviceCapability capability)
            => GdiMethods.GetDeviceCapability(in deviceContext, capability);
        public static GdiObjectHandle SelectObject(in this DeviceContext deviceContext, GdiObjectHandle @object)
            => GdiMethods.SelectObject(in deviceContext, @object);
        public static COLORREF GetTextColor(in this DeviceContext deviceContext)
            => GdiMethods.GetTextColor(in deviceContext);
        public static COLORREF SetTextColor(in this DeviceContext deviceContext, COLORREF color)
            => GdiMethods.SetTextColor(in deviceContext, color);
        public static bool TextOut(in this DeviceContext deviceContext, int x, int y, string text)
            => GdiMethods.TextOut(in deviceContext, x, y, text);
        public static unsafe bool TextOut(in this DeviceContext deviceContext, int x, int y, char* text, int length)
            => GdiMethods.TextOut(in deviceContext, x, y, text, length);
        public static unsafe int DrawText(in this DeviceContext deviceContext, string text, RECT rect, TextFormat format)
            => GdiMethods.DrawText(in deviceContext, text, rect, format);
        public static TextAlignment SetTextAlignment(in this DeviceContext deviceContext, TextAlignment alignment)
            => GdiMethods.SetTextAlignment(in deviceContext, alignment);
        public static bool GetTextMetrics(in this DeviceContext deviceContext, out TEXTMETRIC metrics)
            => GdiMethods.GetTextMetrics(in deviceContext, out metrics);
        public static bool MoveTo(in this DeviceContext deviceContext, int x, int y)
            => GdiMethods.MoveTo(in deviceContext, x, y);
        public static bool LineTo(in this DeviceContext deviceContext, int x, int y)
            => GdiMethods.LineTo(in deviceContext, x, y);
        public static PenHandle GetCurrentPen(in this DeviceContext deviceContext)
            => GdiMethods.GetCurrentPen(in deviceContext);
        public static COLORREF GetBrushColor(in this DeviceContext deviceContext)
            => GdiMethods.GetBrushColor(in deviceContext);
        public static COLORREF GetBackgroundColor(in this DeviceContext deviceContext)
            => GdiMethods.GetBackgroundColor(in deviceContext);
        public static COLORREF SetBackgroundColor(in this DeviceContext deviceContext, COLORREF color)
            => GdiMethods.SetBackgroundColor(in deviceContext, color);
        public static BackgroundMode SetBackgroundMode(in this DeviceContext deviceContext, BackgroundMode mode)
            => GdiMethods.SetBackgroundMode(in deviceContext, mode);
        public static BackgroundMode GetBackgroundMode(in this DeviceContext deviceContext) => GdiMethods.GetBackgroundMode(in deviceContext);
        public static PenMixMode SetRasterOperation(in this DeviceContext deviceContext, PenMixMode foregroundMixMode)
            => GdiMethods.SetRasterOperation(in deviceContext, foregroundMixMode);
        public static PenMixMode GetRasterOperation(in this DeviceContext deviceContext) => GdiMethods.GetRasterOperation(in deviceContext);
        public static PolyFillMode GetPolyFillMode(in this DeviceContext deviceContext)
            => GdiMethods.GetPolyFillMode(in deviceContext);
        public static PolyFillMode SetPolyFillMode(in this DeviceContext deviceContext, PolyFillMode fillMode)
            => GdiMethods.SetPolyFillMode(in deviceContext, fillMode);
        public unsafe static bool Polygon(in this DeviceContext deviceContext, params Point[] points)
            => GdiMethods.Polygon(in deviceContext, points);
        public static bool Polyline(in this DeviceContext deviceContext, params Point[] points)
            => GdiMethods.Polyline(in deviceContext, points);
        public static bool Rectangle(in this DeviceContext deviceContext, int left, int top, int right, int bottom)
            => GdiMethods.Rectangle(in deviceContext, left, top, right, bottom);
        public static bool Rectangle(in this DeviceContext deviceContext, RECT rectangle) => GdiMethods.Rectangle(in deviceContext, rectangle);
        public static bool FillRectangle(in this DeviceContext deviceContext, RECT rectangle, BrushHandle brush)
            => GdiMethods.FillRectangle(in deviceContext, rectangle, brush);
        public static bool FrameRectangle(in this DeviceContext deviceContext, RECT rectangle, BrushHandle brush)
            => GdiMethods.FrameRectangle(in deviceContext, rectangle, brush);
        public static bool InvertRectangle(in this DeviceContext deviceContext, RECT rectangle)
            => GdiMethods.InvertRectangle(in deviceContext, rectangle);
        public static bool DrawFocusRectangle(in this DeviceContext deviceContext, RECT rectangle)
            => GdiMethods.DrawFocusRectangle(in deviceContext, rectangle);
        public static bool Ellipse(in this DeviceContext deviceContext, int left, int top, int right, int bottom)
            => GdiMethods.Ellipse(in deviceContext, left, top, right, bottom);
        public static bool Ellipse(in this DeviceContext deviceContext, Rectangle bounds)
            => GdiMethods.Ellipse(in deviceContext, bounds.Left, bounds.Top, bounds.Right, bounds.Bottom);
        public static bool RoundRectangle(in this DeviceContext deviceContext, int left, int top, int right, int bottom, int cornerWidth, int cornerHeight)
            => GdiMethods.RoundRectangle(in deviceContext, left, top, right, bottom, cornerWidth, cornerHeight);
        public static bool PolyBezier(in this DeviceContext deviceContext, params Point[] points)
            => GdiMethods.PolyBezier(in deviceContext, points);
        public static COLORREF GetPixel(in this DeviceContext deviceContext, int x, int y) => GdiMethods.GetPixel(in deviceContext, x, y);
        public static COLORREF GetPixel(in this DeviceContext deviceContext, Point point) => GdiMethods.GetPixel(in deviceContext, point.X, point.Y);
        public static bool SetPixel(in this DeviceContext deviceContext, Point point, COLORREF color) => GdiMethods.SetPixel(in deviceContext, point.X, point.Y, color);
        public static bool SetPixel(in this DeviceContext deviceContext, int x, int y, COLORREF color) => GdiMethods.SetPixel(in deviceContext, x, y, color);
        public static bool DeviceToLogical(in this DeviceContext deviceContext, params Point[] points) => GdiMethods.DeviceToLogical(in deviceContext, points);
        public static bool LogicalToDevice(in this DeviceContext deviceContext, params Point[] points) => GdiMethods.LogicalToDevice(in deviceContext, points);
        public static bool SetViewportOrigin(in this DeviceContext deviceContext, int x, int y) => GdiMethods.SetViewportOrigin(in deviceContext, x, y);
        public static bool SetWindowOrigin(in this DeviceContext deviceContext, int x, int y) => GdiMethods.SetWindowOrigin(in deviceContext, x, y);
        public static bool OffsetWindowOrigin(in this DeviceContext deviceContext, int x, int y) => GdiMethods.OffsetWindowOrigin(in deviceContext, x, y);
        public static bool OffsetViewportOrigin(in this DeviceContext deviceContext, int x, int y) => GdiMethods.OffsetViewportOrigin(in deviceContext, x, y);
        public static bool SetWindowExtents(in this DeviceContext deviceContext, int x, int y) => GdiMethods.SetWindowExtents(in deviceContext, x, y);
        public static bool SetViewportExtents(in this DeviceContext deviceContext, int x, int y) => GdiMethods.SetViewportExtents(in deviceContext, x, y);
        public static MapMode SetMapMode(in this DeviceContext deviceContext, MapMode mapMode) => GdiMethods.SetMapMode(in deviceContext, mapMode);

        public static RegionType SelectClippingRegion(in this DeviceContext deviceContext, RegionHandle region) => GdiMethods.SelectClippingRegion(in deviceContext, region);
        public static RegionType CombineRegion(this RegionHandle destination, RegionHandle sourceOne, RegionHandle sourceTwo, CombineRegionMode mode) =>
            GdiMethods.CombineRegion(destination, sourceOne, sourceTwo, mode);
    }
}
