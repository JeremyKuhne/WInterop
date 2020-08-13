// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Gdi
{
    public enum BitmapCompression : uint
    {
        /// <summary>
        ///  Uncrompressed. [BI_RGB]
        /// </summary>
        Rgb = 0,

        /// <summary>
        ///  Run-length encoded for 8 bpp bitmaps. [BI_RLE8]
        /// </summary>
        Rle8 = 1,

        /// <summary>
        ///  Run-length encoded for 4 bpp bitmaps. [BI_RLE4]
        /// </summary>
        Rle4 = 2,

        /// <summary>
        ///  Uncompressed, color table has 3 uint color masks for RGB.
        ///  For 16/32 bpp bitmaps. [BI_BITFIELDS]
        /// </summary>
        Bitfields = 3,

        /// <summary>
        ///  Image is Jpeg [BI_JPEG]
        /// </summary>
        Jpeg = 4,

        /// <summary>
        ///  Image is Png [BI_PNG]
        /// </summary>
        Png = 5
    }
}
