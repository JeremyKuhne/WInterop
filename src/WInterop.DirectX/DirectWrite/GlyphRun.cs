﻿// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Runtime.InteropServices;

namespace WInterop.DirectWrite;

/// <summary>
///  The DWRITE_GLYPH_RUN structure contains the information needed by renderers
///  to draw glyph runs. All coordinates are in device independent pixels (DIPs).
///  [DWRITE_GLYPH_RUN]
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public readonly unsafe struct GlyphRun
{
    /// <summary>
    ///  The physical font face to draw with. (IFontFace)
    /// </summary>
    public readonly FontFace FontFace;

    /// <summary>
    ///  Logical size of the font in DIPs, not points (equals 1/96 inch).
    /// </summary>
    public readonly float FontEmSize;

    /// <summary>
    ///  The number of glyphs.
    /// </summary>
    public readonly uint GlyphCount;

    private readonly ushort* _glyphIndices;

    /// <summary>
    ///  The indices to render.
    /// </summary>
    public ReadOnlySpan<ushort> GlyphIndices => new(_glyphIndices, (int)GlyphCount);

    private readonly float* _glyphAdvances;

    /// <summary>
    ///  Glyph advance widths.
    /// </summary>
    public ReadOnlySpan<float> GlyphAdvances => new(_glyphAdvances, (int)GlyphCount);

    private readonly GlyphOffset* _glyphOffsets;

    /// <summary>
    ///  Glyph offsets.
    /// </summary>
    public ReadOnlySpan<GlyphOffset> GlyphOffsets => new(_glyphOffsets, (int)GlyphCount);

    /// <summary>
    ///  If true, specifies that glyphs are rotated 90 degrees to the left and
    ///  vertical metrics are used. Vertical writing is achieved by specifying
    ///  isSideways = true and rotating the entire run 90 degrees to the right
    ///  via a rotate transform.
    /// </summary>
    public readonly IntBoolean IsSideways;

    /// <summary>
    ///  The implicit resolved bidi level of the run. Odd levels indicate
    ///  right-to-left languages like Hebrew and Arabic, while even levels
    ///  indicate left-to-right languages like English and Japanese (when
    ///  written horizontally). For right-to-left languages, the text origin
    ///  is on the right, and text should be drawn to the left.
    /// </summary>
    public readonly uint BidiLevel;
};
