// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Gdi;

// https://msdn.microsoft.com/en-us/library/dd183499.aspx
// http://www.tech-archive.net/Archive/Development/microsoft.public.win32.programmer.gdi/2006-02/msg00027.html
public enum Quality : byte
{
    /// <summary>
    ///  Appearance doesn't matter. Will smooth if SPI_GETFONTSMOOTHING is true. (DEFAULT_QUALITY)
    /// </summary>
    Default = 0,

    /// <summary>
    ///  Can scale raster fonts to get desired font size. Bold, italic, etc. may be synthesized if needed. (DRAFT_QUALITY)
    /// </summary>
    Draft = 1,

    /// <summary>
    ///  Will not scale raster fonts, picks closes size. Bold, italic, etc. may be synthesized if needed. (PROOF_QUALITY)
    /// </summary>
    Proof = 2,

    /// <summary>
    ///  Do not antialias. (NONANTIALIASED_QUALITY)
    /// </summary>
    NonAntialiased = 3,

    /// <summary>
    ///  Antialiased if the font supports it and it isn't too small or large. (ANTIALIASED_QUALITY)
    /// </summary>
    Antialiased = 4,

    /// <summary>
    ///  Text is rendered using ClearType antialiasing if possible. (CLEARTYPE_QUALITY)
    /// </summary>
    ClearType = 5,

    /// <summary>
    ///  Text is rendered using ClearType if possible, glyph widths may vary from non-antialised
    ///  width to avoid distortion. (CLEARTYPE_NATURAL_QUALITY)
    /// </summary>
    ClearTypeNatural = 6
}