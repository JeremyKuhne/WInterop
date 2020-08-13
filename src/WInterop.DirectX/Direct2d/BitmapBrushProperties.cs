// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Direct2d
{
    /// <summary>
    ///  Describes the extend modes and the interpolation mode of an <see cref="IBitmapBrush"/>.
    ///  [D2D1_BITMAP_BRUSH_PROPERTIES]
    /// </summary>
    public readonly struct BitmapBrushProperties
    {
        public readonly ExtendMode ExtendModeX;
        public readonly ExtendMode ExtendModeY;
        public readonly BitmapInterpolationMode InterpolationMode;

        public BitmapBrushProperties(
            ExtendMode extendModeX = ExtendMode.Clamp,
            ExtendMode extendModeY = ExtendMode.Clamp,
            BitmapInterpolationMode interpolationMode = BitmapInterpolationMode.Linear)
        {
            ExtendModeX = extendModeX;
            ExtendModeY = extendModeY;
            InterpolationMode = interpolationMode;
        }
    }
}
