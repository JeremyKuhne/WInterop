// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.DirectWrite;

/// <summary>
///  The measuring method used for text layout. [DWRITE_MEASURING_MODE]
/// </summary>
public enum MeasuringMode : uint
{
    /// <summary>
    ///  Text is measured using glyph ideal metrics whose values are independent to the current display resolution.
    /// </summary>
    Natural,

    /// <summary>
    ///  Text is measured using glyph display compatible metrics whose values tuned for the current display resolution.
    /// </summary>
    GdiClassic,

    /// <summary>
    ///  Text is measured using the same glyph display metrics as text measured by GDI using a font
    ///  created with CLEARTYPE_NATURAL_QUALITY.
    /// </summary>
    GdiNatural
}
