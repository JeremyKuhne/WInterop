// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Runtime.InteropServices;

namespace WInterop.DirectWrite
{
    /// <summary>
    ///  The <see cref="Strikethrough"/> structure contains information about the size and
    ///  placement of strikethroughs. All coordinates are in device independent
    ///  pixels (DIPs). [DWRITE_STRIKETHROUGH]
    /// </summary>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public readonly struct Strikethrough
    {
        /// <summary>
        ///  Width of the strikethrough, measured parallel to the baseline.
        /// </summary>
        public readonly float Width;

        /// <summary>
        ///  Thickness of the strikethrough, measured perpendicular to the
        ///  baseline.
        /// </summary>
        public readonly float Thickness;

        /// <summary>
        ///  Offset of the strikethrough from the baseline.
        ///  A positive offset represents a position below the baseline and
        ///  a negative offset is above.
        /// </summary>
        public readonly float Offset;

        /// <summary>
        ///  Reading direction of the text associated with the strikethrough.  This
        ///  value is used to interpret whether the width value runs horizontally 
        ///  or vertically.
        /// </summary>
        public readonly ReadingDirection ReadingDirection;

        /// <summary>
        ///  Flow direction of the text associated with the strikethrough.  This 
        ///  value is used to interpret whether the thickness value advances top to
        ///  bottom, left to right, or right to left.
        /// </summary>
        public readonly FlowDirection FlowDirection;

        /// <summary>
        ///  Locale of the range. Can be pertinent where the locale affects the style.
        /// </summary>
        public unsafe readonly char* LocaleName;

        /// <summary>
        ///  The measuring mode can be useful to the renderer to determine how
        ///  underlines are rendered, e.g. rounding the thickness to a whole pixel
        ///  in GDI-compatible modes.
        /// </summary>
        public readonly MeasuringMode MeasuringMode;
    };
}
