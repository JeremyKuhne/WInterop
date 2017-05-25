// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using WInterop.ErrorHandling.Types;
using WInterop.Gdi.Types;
using WInterop.Support;

namespace WInterop.GdiPlus
{
    public static partial class GdiPlusMethods
    {

        public static Exception GetExceptionForStatus(GpStatus status)
        {
            switch (status)
            {
                case GpStatus.Win32Error:
                    WindowsError error = Errors.GetLastError();
                    if (error != WindowsError.ERROR_SUCCESS)
                        return Errors.GetIoExceptionForError(error);
                    goto default;
                default:
                    return new GdiPlusException(status);
            }
        }

        public static GpGraphics CreateGraphics(DeviceContext deviceContext)
        {
            GpStatus status = Imports.GdipCreateFromHDC(deviceContext, out GpGraphics graphics);
            if (status != GpStatus.Ok)
                throw GetExceptionForStatus(status);
            return graphics;
        }

        public static void SetSmoothingMode(GpGraphics graphics, SmoothingMode smoothingMode)
        {
            GpStatus status = Imports.GdipSetSmoothingMode(graphics, smoothingMode);
            if (status != GpStatus.Ok)
                throw GetExceptionForStatus(status);
        }

        public static UIntPtr Startup()
        {
            var input = new GdiplusStartupInput { GdiplusVersion = 1 };
            GpStatus status = Imports.GdiplusStartup(
                out UIntPtr token,
                ref input,
                out GdiplusStartupOutput _ );

            if (status != GpStatus.Ok)
                throw GetExceptionForStatus(status);

            return token;
        }

        public static void Shutdown(UIntPtr token)
        {
            Imports.GdiplusShutdown(token);
        }

        public static GpPen CreatePen(ARGB color, float width = 1.0f, GpUnit unit = GpUnit.UnitWorld)
        {
            GpStatus status = Imports.GdipCreatePen1(color, width, unit, out GpPen pen);
            if (status != GpStatus.Ok)
                throw GetExceptionForStatus(status);

            return pen;
        }

        public static GpSolidBrush CreateSolidBrush(ARGB color)
        {
            GpStatus status = Imports.GdipCreateSolidFill(color, out GpSolidBrush brush);
            if (status != GpStatus.Ok)
                throw GetExceptionForStatus(status);

            return brush;
        }

        public unsafe static void DrawLines(GpGraphics graphics, GpPen pen, POINT[] points)
        {
            fixed (POINT* p = points)
                DrawLines(graphics, pen, (GpPoint*)p, points.Length);
        }

        public unsafe static void DrawLines(GpGraphics graphics, GpPen pen, GpPoint[] points)
        {
            fixed (GpPoint* p = points)
                DrawLines(graphics, pen, p, points.Length);
        }

        private unsafe static void DrawLines(GpGraphics graphics, GpPen pen, GpPoint* points, int count)
        {
            GpStatus status = Imports.GdipDrawLinesI(graphics, pen, points, count);
            if (status != GpStatus.Ok)
                throw GetExceptionForStatus(status);
        }

        public static void FillEllipse(GpGraphics graphics, GpBrush brush, int x, int y, int width, int height)
        {
            GpStatus status = Imports.GdipFillEllipseI(graphics, brush, x, y, width, height);
            if (status != GpStatus.Ok)
                throw GetExceptionForStatus(status);
        }
    }
}
