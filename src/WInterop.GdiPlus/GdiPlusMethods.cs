// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Drawing;
using System.Runtime.InteropServices;
using WInterop.ErrorHandling;
using WInterop.Gdi;
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

        public static void ThrowIfFailed(GpStatus status)
        {
            if (status != GpStatus.Ok)
                throw GetExceptionForStatus(status);
        }

        public static GpGraphics CreateGraphics(in DeviceContext deviceContext)
        {
            ThrowIfFailed(Imports.GdipCreateFromHDC(deviceContext, out GpGraphics graphics));
            return graphics;
        }

        public static void SetSmoothingMode(GpGraphics graphics, SmoothingMode smoothingMode)
            => ThrowIfFailed(Imports.GdipSetSmoothingMode(graphics, smoothingMode));

        public static UIntPtr Startup()
        {
            var input = new GdiplusStartupInput { GdiplusVersion = 1 };
            ThrowIfFailed(Imports.GdiplusStartup(
                out UIntPtr token,
                in input,
                out GdiplusStartupOutput _ ));

            return token;
        }

        public static void Shutdown(UIntPtr token)
        {
            Imports.GdiplusShutdown(token);
        }

        public static GpPen CreatePen(Color color, float width = 1.0f, GpUnit unit = GpUnit.UnitWorld)
        {
            ThrowIfFailed(Imports.GdipCreatePen1(color, width, unit, out GpPen pen));
            return pen;
        }

        public static GpBrush CreateSolidBrush(Color color)
        {
            ThrowIfFailed(Imports.GdipCreateSolidFill(color, out GpBrush brush));
            return brush;
        }

        public unsafe static void DrawLines(GpGraphics graphics, GpPen pen, ReadOnlySpan<Point> points)
            => ThrowIfFailed(Imports.GdipDrawLinesI(graphics, pen, ref MemoryMarshal.GetReference(points), points.Length));

        public static void FillEllipse(GpGraphics graphics, GpBrush brush, int x, int y, int width, int height)
            => ThrowIfFailed(Imports.GdipFillEllipseI(graphics, brush, x, y, width, height));

        public static void GraphicsClear(GpGraphics graphics, Color color)
            => ThrowIfFailed(Imports.GdipGraphicsClear(graphics, color));
    }
}
