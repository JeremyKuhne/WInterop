// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Drawing;
using WInterop.Gdi.Types;

namespace WInterop.GdiPlus
{
    public static class GdiPlus
    {
        public static GpGraphics CreateGraphics(this DeviceContext deviceContext) => GdiPlusMethods.CreateGraphics(deviceContext);
        public static GpBrush CreateSolidBrush(this GpGraphics graphics, ARGB argb) => GdiPlusMethods.CreateSolidBrush(argb);
        public static void SetSmoothingMode(this GpGraphics graphics, SmoothingMode mode) => GdiPlusMethods.SetSmoothingMode(graphics, mode);
        public static void Clear(this GpGraphics graphics, ARGB argb) => GdiPlusMethods.GraphicsClear(graphics, argb);
        public static void FillEllipse(this GpGraphics graphics, GpBrush brush, Rectangle bounds) => GdiPlusMethods.FillEllipse(graphics, brush, bounds.X, bounds.Y, bounds.Width, bounds.Height);
    }
}
