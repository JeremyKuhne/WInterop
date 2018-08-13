// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Direct2d
{
    /// <summary>
    /// Describes the pixel format and dpi of a bitmap.
    /// </summary>
    public readonly struct BitmapProperties
    {
        public readonly PixelFormat PixelFormat;
        public readonly float DpiX;
        public readonly float DpiY;

        public BitmapProperties(PixelFormat format, float dpiX, float dpiY)
        {
            PixelFormat = format;
            DpiX = dpiX;
            DpiY = dpiY;
        }
    }
}
