// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Drawing;

namespace WInterop.Direct2d
{
    /// <summary>
    ///  Contains the gradient origin offset and the size and position of the gradient
    ///  ellipse for an <see cref="RadialGradientBrush"/>. [D2D1_RADIAL_GRADIENT_BRUSH_PROPERTIES]
    /// </summary>
    public readonly struct RadialGradientBrushProperties
    {
        public readonly PointF Center;
        public readonly PointF GradientOriginOffset;
        public readonly float RadiusX;
        public readonly float RadiusY;

        public RadialGradientBrushProperties(PointF center, PointF gradientOriginOffset, float radiusX, float radiusY)
        {
            Center = center;
            GradientOriginOffset = gradientOriginOffset;
            RadiusX = radiusX;
            RadiusY = radiusY;
        }

        public unsafe RadialGradientBrushProperties((float X, float Y) center, (float X, float Y) gradientOriginOffset, float radiusX, float radiusY)
        {
            Center = *(PointF*)&center;
            GradientOriginOffset = *(PointF*)&gradientOriginOffset;
            RadiusX = radiusX;
            RadiusY = radiusY;
        }
    }
}
