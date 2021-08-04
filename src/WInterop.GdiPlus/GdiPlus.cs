// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Drawing;
using System.Runtime.CompilerServices;
using WInterop.Errors;
using WInterop.Gdi;
using WInterop.GdiPlus.Native;

namespace WInterop.GdiPlus
{
    public static class GdiPlus
    {
        internal static Session Init() => s_session;
        private static readonly Session s_session = new();

        [MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
        public static void Initialize() => Init();

        public static Exception GetExceptionForStatus(GpStatus status)
        {
            switch (status)
            {
                case GpStatus.Win32Error:
                    WindowsError error = Error.GetLastError();
                    if (error != WindowsError.ERROR_SUCCESS)
                        return error.GetException();
                    goto default;
                default:
                    return new GdiPlusException(status);
            }
        }

        public static void ThrowIfFailed(this GpStatus status)
        {
            if (status != GpStatus.Ok)
                throw GetExceptionForStatus(status);
        }

        public static void SetSmoothingMode(this Graphics graphics, SmoothingMode smoothingMode)
            => ThrowIfFailed(GdiPlusImports.GdipSetSmoothingMode(graphics, smoothingMode));

        public static unsafe UIntPtr Startup(uint version = 2)
        {
            var input = new StartupInput { GdiplusVersion = version };
            StartupOutput* output;
            nuint* token;
            ThrowIfFailed(GdiPlusImports.GdiplusStartup(
                &token,
                &input,
                &output));

            return (UIntPtr)token;
        }

        public static unsafe void Shutdown(UIntPtr token) => GdiPlusImports.GdiplusShutdown(&token);

        public static unsafe void DrawLine(this Graphics graphics, Pen pen, Point from, Point to)
        {
            Span<Point> points = stackalloc Point[]
            {
                from,
                to
            };

            DrawLines(graphics, pen, points);
        }

        public static unsafe void DrawLines(this Graphics graphics, Pen pen, ReadOnlySpan<Point> points)
        {
            fixed (Point* p = points)
            {
                GdiPlusImports.GdipDrawLinesI(graphics, pen, p, points.Length).ThrowIfFailed();
            }
        }

        public static void DrawRectangle(this Graphics graphics, Pen pen, Rectangle rectangle)
            => GdiPlusImports.GdipDrawRectangleI(
                graphics,
                pen,
                rectangle.X,
                rectangle.Y,
                rectangle.Width,
                rectangle.Height).ThrowIfFailed();

        public static void FillRectangle(this Graphics graphics, Brush brush, Rectangle rectangle)
            => GdiPlusImports.GdipFillRectangleI(
                graphics,
                brush,
                rectangle.X,
                rectangle.Y,
                rectangle.Width,
                rectangle.Height).ThrowIfFailed();

        public static void FillEllipse(this Graphics graphics, GpBrush brush, Rectangle bounds)
            => FillEllipse(graphics, brush, bounds.X, bounds.Y, bounds.Width, bounds.Height);

        public static void FillEllipse(this Graphics graphics, GpBrush brush, int x, int y, int width, int height)
            => ThrowIfFailed(GdiPlusImports.GdipFillEllipseI(graphics, brush, x, y, width, height));

        public static void Clear(this Graphics graphics, ARGB argb)
            => ThrowIfFailed(GdiPlusImports.GdipGraphicsClear(graphics, argb));

        public static void Clear(this Graphics graphics, Color color)
            => Clear(graphics, (ARGB)color);

        public static PaletteHandle GetHalftonePalette()
            => new(GdiPlusImports.GdipCreateHalftonePalette());

        public static Region GetClip(this Graphics graphics)
        {
            GdiPlusImports.GdipCreateRegion(out GpRegion gpRegion).ThrowIfFailed();
            GdiPlusImports.GdipGetClip(graphics, gpRegion).ThrowIfFailed();
            return new(gpRegion);
        }

        public static bool IsVisibleClipEmpty(this Graphics graphics)
        {
            GdiPlusImports.GdipIsVisibleClipEmpty(graphics, out BOOL result).ThrowIfFailed();
            return result.IsTrue();
        }

        public static bool IsEmpty(this Region region, Graphics graphics)
        {
            GdiPlusImports.GdipIsEmptyRegion(region, graphics, out BOOL result).ThrowIfFailed();
            return result.IsTrue();
        }

        public static bool IsInfinite(this Region region, Graphics graphics)
        {
            GdiPlusImports.GdipIsInfiniteRegion(region, graphics, out BOOL result).ThrowIfFailed();
            return result.IsTrue();
        }
    }
}
