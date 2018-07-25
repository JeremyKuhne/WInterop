// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Drawing;
using WInterop.Gdi.Types;

namespace WInterop.GdiPlus
{
    public static class GdiPlus
    {
        public static GpGraphics CreateGraphics(in this DeviceContext deviceContext) => GdiPlusMethods.CreateGraphics(in deviceContext);
        public static GpPen CreatePen(ARGB argb) => GdiPlusMethods.CreatePen(argb);
        public static GpPen CreatePen(this GpGraphics graphics, ARGB argb) => GdiPlusMethods.CreatePen(argb);
        public static GpBrush CreateSolidBrush(ARGB argb) => GdiPlusMethods.CreateSolidBrush(argb);
        public static GpBrush CreateSolidBrush(this GpGraphics graphics, ARGB argb) => GdiPlusMethods.CreateSolidBrush(argb);
        public static void SetSmoothingMode(this GpGraphics graphics, SmoothingMode mode) => GdiPlusMethods.SetSmoothingMode(graphics, mode);
        public static void Clear(this GpGraphics graphics, ARGB argb) => GdiPlusMethods.GraphicsClear(graphics, argb);
        public static void FillEllipse(this GpGraphics graphics, GpBrush brush, Rectangle bounds)
            => GdiPlusMethods.FillEllipse(graphics, brush, bounds.X, bounds.Y, bounds.Width, bounds.Height);
        public static void FillEllipse(this GpGraphics graphics, GpBrush brush, int x, int y, int width, int height)
            => GdiPlusMethods.FillEllipse(graphics, brush, x, y, width, height);
        public static void DrawLines(this GpGraphics graphics, GpPen pen, ReadOnlySpan<Point> points)
            => GdiPlusMethods.DrawLines(graphics, pen, points);

    }
}
