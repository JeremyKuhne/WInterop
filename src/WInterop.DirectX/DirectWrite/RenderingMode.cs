// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.DirectWrite;

/// <summary>
///  Represents a method of rendering glyphs. [DWRITE_RENDERING_MODE]
/// </summary>
public enum RenderingMode
{
    /// <summary>
    ///  Specifies that the rendering mode is determined automatically based on the font and size.
    ///  [DWRITE_RENDERING_MODE_DEFAULT]
    /// </summary>
    Default = DWRITE_RENDERING_MODE.DWRITE_RENDERING_MODE_DEFAULT,

    /// <summary>
    ///  Specifies that no antialiasing is performed. Each pixel is either set to the foreground 
    ///  color of the text or retains the color of the background. [DWRITE_RENDERING_MODE_ALIASED]
    /// </summary>
    Aliased = DWRITE_RENDERING_MODE.DWRITE_RENDERING_MODE_ALIASED,

    /// <summary>
    ///  Specifies that antialiasing is performed in the horizontal direction and the appearance
    ///  of glyphs is layout-compatible with GDI using CLEARTYPE_QUALITY. Use DWRITE_MEASURING_MODE_GDI_CLASSIC 
    ///  to get glyph advances. The antialiasing may be either ClearType or grayscale depending on
    ///  the text antialiasing mode. [DWRITE_RENDERING_MODE_GDI_CLASSIC]
    /// </summary>
    GdiClassic = DWRITE_RENDERING_MODE.DWRITE_RENDERING_MODE_CLEARTYPE_GDI_CLASSIC,

    /// <summary>
    ///  Specifies that antialiasing is performed in the horizontal direction and the appearance
    ///  of glyphs is layout-compatible with GDI using CLEARTYPE_NATURAL_QUALITY. Glyph advances
    ///  are close to the font design advances, but are still rounded to whole pixels. Use
    ///  DWRITE_MEASURING_MODE_GDI_NATURAL to get glyph advances. The antialiasing may be either
    ///  ClearType or grayscale depending on the text antialiasing mode. [DWRITE_RENDERING_MODE_GDI_NATURAL]
    /// </summary>
    GdiNatural = DWRITE_RENDERING_MODE.DWRITE_RENDERING_MODE_CLEARTYPE_GDI_NATURAL,

    /// <summary>
    ///  Specifies that antialiasing is performed in the horizontal direction. This rendering
    ///  mode allows glyphs to be positioned with subpixel precision and is therefore suitable
    ///  for natural (i.e., resolution-independent) layout. The antialiasing may be either
    ///  ClearType or grayscale depending on the text antialiasing mode. [DWRITE_RENDERING_MODE_NATURAL]
    /// </summary>
    Natural = DWRITE_RENDERING_MODE.DWRITE_RENDERING_MODE_NATURAL,

    /// <summary>
    ///  Similar to natural mode except that antialiasing is performed in both the horizontal
    ///  and vertical directions. This is typically used at larger sizes to make curves and
    ///  diagonal lines look smoother. The antialiasing may be either ClearType or grayscale
    ///  depending on the text antialiasing mode. [DWRITE_RENDERING_MODE_NATURAL_SYMMETRIC]
    /// </summary>
    NaturalSymmetric = DWRITE_RENDERING_MODE.DWRITE_RENDERING_MODE_CLEARTYPE_NATURAL_SYMMETRIC,

    /// <summary>
    ///  Specifies that rendering should bypass the rasterizer and use the outlines directly. 
    ///  This is typically used at very large sizes. [DWRITE_RENDERING_MODE_OUTLINE]
    /// </summary>
    Outline = DWRITE_RENDERING_MODE.DWRITE_RENDERING_MODE_OUTLINE,
};
