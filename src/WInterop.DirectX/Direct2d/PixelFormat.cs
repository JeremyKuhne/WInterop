// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using WInterop.GraphicsInfrastructure;

namespace WInterop.Direct2d
{
    /// <summary>
    /// Description of a pixel format. [D2D1_PIXEL_FORMAT]
    /// </summary>
    public readonly struct PixelFormat
    {
        public readonly Format Format;
        public readonly AlphaMode AlphaMode;

        public PixelFormat(Format format = Format.Unknown, AlphaMode alphaMode = AlphaMode.Unknown)
        {
            Format = format;
            AlphaMode = alphaMode;
        }
    }
}
