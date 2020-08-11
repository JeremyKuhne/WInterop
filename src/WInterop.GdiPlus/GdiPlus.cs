// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Drawing;
using WInterop.Errors;
using WInterop.Gdi;
using WInterop.GdiPlus.Native;

namespace WInterop.GdiPlus
{
    public static class GdiPlus
    {
        internal static GdiPlusSession Init() => s_session;
        private static readonly GdiPlusSession s_session = new GdiPlusSession();

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
            => ThrowIfFailed(Imports.GdipSetSmoothingMode(graphics, smoothingMode));

        public static unsafe UIntPtr Startup(uint version = 2)
        {
            var input = new GdiPlusStartupInput { GdiplusVersion = version };
            GdiPlusStartupOutput* output;
            nuint* token;
            ThrowIfFailed(Imports.GdiplusStartup(
                &token,
                &input,
                &output));

            return (UIntPtr)token;
        }

        public static unsafe void Shutdown(UIntPtr token)
        {
            Imports.GdiplusShutdown(&token);
        }

        public static unsafe void DrawLines(this Graphics graphics, Pen pen, ReadOnlySpan<Point> points)
        {
            fixed (Point* p = points)
            {
                Imports.GdipDrawLinesI(graphics, pen, p, points.Length).ThrowIfFailed();
            }
        }

        public static void FillEllipse(this Graphics graphics, GpBrush brush, Rectangle bounds)
            => FillEllipse(graphics, brush, bounds.X, bounds.Y, bounds.Width, bounds.Height);

        public static void FillEllipse(this Graphics graphics, GpBrush brush, int x, int y, int width, int height)
            => ThrowIfFailed(Imports.GdipFillEllipseI(graphics, brush, x, y, width, height));

        public static void Clear(this Graphics graphics, ARGB argb)
            => ThrowIfFailed(Imports.GdipGraphicsClear(graphics, argb));

        public static void Clear(this Graphics graphics, Color color)
            => Clear(graphics, (ARGB)color);

        public static PaletteHandle GetHalftonePalette()
            => new PaletteHandle(Imports.GdipCreateHalftonePalette());
    }
}
