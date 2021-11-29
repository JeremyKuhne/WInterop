// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Drawing;

namespace WInterop.Direct2d
{

    /// <summary>
    ///  Contains the center point, x-radius, and y-radius of an ellipse.
    ///  [D2D1_ELLIPSE]
    /// </summary>
    public readonly struct Ellipse
    {
        public PointF Point { get; }
        public float RadiusX { get; }
        public float RadiusY { get; }

        public Ellipse(PointF point, float radiusX, float radiusY)
        {
            Point = point;
            RadiusX = radiusX;
            RadiusY = radiusY;
        }

        public unsafe Ellipse((float X, float Y) point, float radiusX, float radiusY)
        {
            Point = *(PointF*)&point;
            RadiusX = radiusX;
            RadiusY = radiusY;
        }
    }
}
