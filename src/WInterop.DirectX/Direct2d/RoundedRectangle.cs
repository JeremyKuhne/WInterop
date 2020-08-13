// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Drawing;

namespace WInterop.Direct2d
{
    /// <summary>
    ///  [D2D1_ROUNDED_RECT]
    /// </summary>
    public readonly struct RoundedRectangle
    {
        private readonly LtrbRectangleF rect;
        public readonly float RadiusX;
        public readonly float RadiusY;

        public RoundedRectangle(RectangleF rectangle, float radiusX, float radiusY)
        {
            rect = new LtrbRectangleF(rectangle);
            RadiusX = radiusX;
            RadiusY = radiusY;
        }

        public RectangleF Rectangle => rect;
    }
}
