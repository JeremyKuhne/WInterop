// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Runtime.CompilerServices;

namespace WInterop.Direct2d
{
    /// <summary>
    ///  Describes the extend modes and the interpolation mode of an <see cref="BitmapBrush"/>.
    ///  [D2D1_BITMAP_BRUSH_PROPERTIES]
    /// </summary>
    public struct BitmapBrushProperties
    {
        public ExtendMode ExtendModeX;
        public ExtendMode ExtendModeY;
        public BitmapInterpolationMode InterpolationMode;

        public BitmapBrushProperties(
            ExtendMode extendModeX = ExtendMode.Clamp,
            ExtendMode extendModeY = ExtendMode.Clamp,
            BitmapInterpolationMode interpolationMode = BitmapInterpolationMode.Linear)
        {
            ExtendModeX = extendModeX;
            ExtendModeY = extendModeY;
            InterpolationMode = interpolationMode;
        }

        public static implicit operator BitmapBrushProperties(in D2D1_BITMAP_BRUSH_PROPERTIES properties)
            => Unsafe.As<D2D1_BITMAP_BRUSH_PROPERTIES, BitmapBrushProperties>(ref Unsafe.AsRef(properties));

        public static implicit operator D2D1_BITMAP_BRUSH_PROPERTIES(in BitmapBrushProperties properties)
            => Unsafe.As<BitmapBrushProperties, D2D1_BITMAP_BRUSH_PROPERTIES>(ref Unsafe.AsRef(properties));
    }
}
