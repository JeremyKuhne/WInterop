// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.DirectWrite;

/// <summary>
///  Overall metrics associated with text after layout.
///  All coordinates are in device independent pixels (DIPs).
///  [DWRITE_TEXT_METRICS]
/// </summary>
public readonly struct TextMetrics
{
    /// <summary>
    ///  Left-most point of formatted text relative to layout box
    ///  (excluding any glyph overhang).
    /// </summary>
    public readonly float Left;

    /// <summary>
    ///  Top-most point of formatted text relative to layout box
    ///  (excluding any glyph overhang).
    /// </summary>
    public readonly float Top;

    /// <summary>
    ///  The width of the formatted text ignoring trailing whitespace
    ///  at the end of each line.
    /// </summary>
    public readonly float Width;

    /// <summary>
    ///  The width of the formatted text taking into account the
    ///  trailing whitespace at the end of each line.
    /// </summary>
    public readonly float WidthIncludingTrailingWhitespace;

    /// <summary>
    ///  The height of the formatted text. The height of an empty string
    ///  is determined by the size of the default font's line height.
    /// </summary>
    public readonly float Height;

    /// <summary>
    ///  Initial width given to the layout. Depending on whether the text
    ///  was wrapped or not, it can be either larger or smaller than the
    ///  text content width.
    /// </summary>
    public readonly float LayoutWidth;

    /// <summary>
    ///  Initial height given to the layout. Depending on the length of the
    ///  text, it may be larger or smaller than the text content height.
    /// </summary>
    public readonly float LayoutHeight;

    /// <summary>
    ///  The maximum reordering count of any line of text, used
    ///  to calculate the most number of hit-testing boxes needed.
    ///  If the layout has no bidirectional text or no text at all,
    ///  the minimum level is 1.
    /// </summary>
    public readonly uint MaxBidiReorderingDepth;

    /// <summary>
    ///  Total number of lines.
    /// </summary>
    public readonly uint LineCount;
};
