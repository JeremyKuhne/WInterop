// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

/// <summary>
/// Extended bitmap properties.
/// </summary>
namespace WInterop.Direct2d
{
    public readonly struct BitmapProperties1
    {
        public readonly PixelFormat PixelFormat;
        public readonly float DpiX;
        public readonly float DpiY;

        public readonly BitmapOptions BitmapOptions;

        /// <summary>
        /// This is an optional pointer to ID2D1ColorContext
        /// </summary>
        public readonly IntPtr ColorContext;

        public BitmapProperties1(PixelFormat format, float dpiX, float dpiY, BitmapOptions options)
        {
            PixelFormat = format;
            DpiX = dpiX;
            DpiY = dpiY;
            BitmapOptions = options;
            ColorContext = IntPtr.Zero;
        }

    }
}
