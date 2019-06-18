// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Direct2d
{
    /// <summary>
    /// Creation properties for an image brush. [D2D1_IMAGE_BRUSH_PROPERTIES]
    /// </summary>
    public readonly struct ImageBrushProperties
    {
        public readonly LtrbRectangleF SourceRectangle;
        public readonly ExtendMode ExtendModeX;
        public readonly ExtendMode ExtendModeY;
        public readonly InterpolationMode InterpolationMode;
    }
}
