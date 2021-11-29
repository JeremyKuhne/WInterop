// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Runtime.InteropServices;

namespace WInterop.DirectWrite;

/// <summary>
///  The <see cref="Underline"/> structure contains information about the size and
///  placement of underlines. All coordinates are in device independent
///  pixels (DIPs). [DWRITE_UNDERLINE]
/// </summary>
[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
public readonly struct Underline
{
    /// <summary>
    ///  Width of the underline, measured parallel to the baseline.
    /// </summary>
    public readonly float Width;

    /// <summary>
    ///  Thickness of the underline, measured perpendicular to the
    ///  baseline.
    /// </summary>
    public readonly float Thickness;

    /// <summary>
    ///  Offset of the underline from the baseline.
    ///  A positive offset represents a position below the baseline and
    ///  a negative offset is above.
    /// </summary>
    public readonly float Offset;

    /// <summary>
    ///  Height of the tallest run where the underline applies.
    /// </summary>
    public readonly float RunHeight;

    /// <summary>
    ///  Reading direction of the text associated with the underline.  This 
    ///  value is used to interpret whether the width value runs horizontally 
    ///  or vertically.
    /// </summary>
    public readonly ReadingDirection ReadingDirection;

    /// <summary>
    ///  Flow direction of the text associated with the underline.  This value
    ///  is used to interpret whether the thickness value advances top to 
    ///  bottom, left to right, or right to left.
    /// </summary>
    public readonly FlowDirection FlowDirection;

    /// <summary>
    ///  Locale of the text the underline is being drawn under. Can be
    ///  pertinent where the locale affects how the underline is drawn.
    ///  For example, in vertical text, the underline belongs on the
    ///  left for Chinese but on the right for Japanese.
    ///  This choice is completely left up to higher levels.
    /// </summary>
    public readonly unsafe char* LocaleName;

    /// <summary>
    ///  The measuring mode can be useful to the renderer to determine how
    ///  underlines are rendered, e.g. rounding the thickness to a whole pixel
    ///  in GDI-compatible modes.
    /// </summary>
    public readonly MeasuringMode MeasuringMode;
};
