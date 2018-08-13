// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.DirectWrite
{
    /// <summary>
    /// Represents the internal structure of a device pixel (i.e., the physical arrangement of red,
    /// green, and blue color components) that is assumed for purposes of rendering text.
    /// [DWRITE_PIXEL_GEOMETRY]
    /// </summary>
    public enum PixelGeometry
    {
        /// <summary>
        /// The red, green, and blue color components of each pixel are assumed to occupy the same point.
        /// [DWRITE_PIXEL_GEOMETRY_FLAT]
        /// </summary>
        Flat,

        /// <summary>
        /// Each pixel comprises three vertical stripes, with red on the left, green in the center, and 
        /// blue on the right. This is the most common pixel geometry for LCD monitors.
        /// [DWRITE_PIXEL_GEOMETRY_RGB]
        /// </summary>
        Rgb,

        /// <summary>
        /// Each pixel comprises three vertical stripes, with blue on the left, green in the center, and 
        /// red on the right. [DWRITE_PIXEL_GEOMETRY_BGR]
        /// </summary>
        Bgr
    };
}
