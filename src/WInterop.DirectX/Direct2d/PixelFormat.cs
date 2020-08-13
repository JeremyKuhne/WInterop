// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Direct2d
{
    /// <summary>
    ///  Description of a pixel format. [D2D1_PIXEL_FORMAT]
    /// </summary>
    public readonly struct PixelFormat
    {
        public readonly Dxgi.Format Format;
        public readonly AlphaMode AlphaMode;

        public PixelFormat(Dxgi.Format format = Dxgi.Format.DXGI_FORMAT_UNKNOWN, AlphaMode alphaMode = AlphaMode.Unknown)
        {
            Format = format;
            AlphaMode = alphaMode;
        }
    }
}
