﻿// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.DirectWrite;

/// <summary>
///  The <see cref="FontMetrics"/> structure specifies the metrics of a font face that
///  are applicable to all glyphs within the font face. [DWRITE_FONT_METRICS]
/// </summary>
public readonly struct FontMetrics
{
    /// <summary>
    ///  The number of font design units per em unit.
    ///  Font files use their own coordinate system of font design units.
    ///  A font design unit is the smallest measurable unit in the em square,
    ///  an imaginary square that is used to size and align glyphs.
    ///  The concept of em square is used as a reference scale factor when defining font size and device transformation semantics.
    ///  The size of one em square is also commonly used to compute the paragraph indentation value.
    /// </summary>
    public readonly ushort DesignUnitsPerEm;

    /// <summary>
    ///  Ascent value of the font face in font design units.
    ///  Ascent is the distance from the top of font character alignment box to English baseline.
    /// </summary>
    public readonly ushort Ascent;

    /// <summary>
    ///  Descent value of the font face in font design units.
    ///  Descent is the distance from the bottom of font character alignment box to English baseline.
    /// </summary>
    public readonly ushort Descent;

    /// <summary>
    ///  Line gap in font design units.
    ///  Recommended additional white space to add between lines to improve legibility. The recommended line spacing 
    ///  (baseline-to-baseline distance) is thus the sum of ascent, descent, and lineGap. The line gap is usually 
    ///  positive or zero but can be negative, in which case the recommended line spacing is less than the height
    ///  of the character alignment box.
    /// </summary>
    public readonly short LineGap;

    /// <summary>
    ///  Cap height value of the font face in font design units.
    ///  Cap height is the distance from English baseline to the top of a typical English capital.
    ///  Capital "H" is often used as a reference character for the purpose of calculating the cap height value.
    /// </summary>
    public readonly ushort CapHeight;

    /// <summary>
    ///  x-height value of the font face in font design units.
    ///  x-height is the distance from English baseline to the top of lowercase letter "x", or a similar lowercase character.
    /// </summary>
    public readonly ushort XHeight;

    /// <summary>
    ///  The underline position value of the font face in font design units.
    ///  Underline position is the position of underline relative to the English baseline.
    ///  The value is usually made negative in order to place the underline below the baseline.
    /// </summary>
    public readonly short UnderlinePosition;

    /// <summary>
    ///  The suggested underline thickness value of the font face in font design units.
    /// </summary>
    public readonly ushort UnderlineThickness;

    /// <summary>
    ///  The strikethrough position value of the font face in font design units.
    ///  Strikethrough position is the position of strikethrough relative to the English baseline.
    ///  The value is usually made positive in order to place the strikethrough above the baseline.
    /// </summary>
    public readonly short StrikethroughPosition;

    /// <summary>
    ///  The suggested strikethrough thickness value of the font face in font design units.
    /// </summary>
    public readonly ushort StrikethroughThickness;
}
