// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Gdi;

/// <docs>
///  https://docs.microsoft.com/windows/win32/api/wingdi/nf-wingdi-createfontw
///  https://groups.google.com/g/microsoft.public.win32.programmer.gdi/c/i8qW4Wh81RM/m/ha7C5wasGpAJ
/// </docs>
public enum FontQuality : byte
{
    /// <summary>
    ///  Appearance doesn't matter. Will smooth if SPI_GETFONTSMOOTHING is true. (DEFAULT_QUALITY)
    /// </summary>
    Default = DEFAULT.DEFAULT_QUALITY,

    /// <summary>
    ///  Can scale raster fonts to get desired font size. Bold, italic, etc. may be synthesized if needed. (DRAFT_QUALITY)
    /// </summary>
    Draft = TerraFXWindows.DRAFT_QUALITY,

    /// <summary>
    ///  Will not scale raster fonts, picks closes size. Bold, italic, etc. may be synthesized if needed. (PROOF_QUALITY)
    /// </summary>
    Proof = TerraFXWindows.PROOF_QUALITY,

    /// <summary>
    ///  Do not antialias. (NONANTIALIASED_QUALITY)
    /// </summary>
    NonAntialiased = TerraFXWindows.NONANTIALIASED_QUALITY,

    /// <summary>
    ///  Antialiased if the font supports it and it isn't too small or large. (ANTIALIASED_QUALITY)
    /// </summary>
    Antialiased = TerraFXWindows.ANTIALIASED_QUALITY,

    /// <summary>
    ///  Text is rendered using ClearType antialiasing if possible. (CLEARTYPE_QUALITY)
    /// </summary>
    ClearType = TerraFXWindows.CLEARTYPE_QUALITY,

    /// <summary>
    ///  Text is rendered using ClearType if possible, glyph widths may vary from non-antialised
    ///  width to avoid distortion. (CLEARTYPE_NATURAL_QUALITY)
    /// </summary>
    ClearTypeNatural = TerraFXWindows.CLEARTYPE_NATURAL_QUALITY
}