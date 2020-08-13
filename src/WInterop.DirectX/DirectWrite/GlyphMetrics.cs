// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.DirectWrite
{
    /// <summary>
    ///  The <see cref="GlyphMetrics"/> structure specifies the metrics of an individual glyph.
    ///  The units depend on how the metrics are obtained.
    /// </summary>
    public readonly struct GlyphMetrics
    {
        /// <summary>
        ///  Specifies the X offset from the glyph origin to the left edge of the black box.
        ///  The glyph origin is the current horizontal writing position.
        ///  A negative value means the black box extends to the left of the origin (often true for lowercase italic 'f').
        /// </summary>
        public readonly int LeftSideBearing;

        /// <summary>
        ///  Specifies the X offset from the origin of the current glyph to the origin of the next glyph when writing horizontally.
        /// </summary>
        public readonly uint AdvanceWidth;

        /// <summary>
        ///  Specifies the X offset from the right edge of the black box to the origin of the next glyph when writing horizontally.
        ///  The value is negative when the right edge of the black box overhangs the layout box.
        /// </summary>
        public readonly int RightSideBearing;

        /// <summary>
        ///  Specifies the vertical offset from the vertical origin to the top of the black box.
        ///  Thus, a positive value adds whitespace whereas a negative value means the glyph overhangs the top of the layout box.
        /// </summary>
        public readonly int TopSideBearing;

        /// <summary>
        ///  Specifies the Y offset from the vertical origin of the current glyph to the vertical origin of the next glyph when writing vertically.
        ///  (Note that the term "origin" by itself denotes the horizontal origin. The vertical origin is different.
        ///  Its Y coordinate is specified by verticalOriginY value,
        ///  and its X coordinate is half the advanceWidth to the right of the horizontal origin).
        /// </summary>
        public readonly uint AdvanceHeight;

        /// <summary>
        ///  Specifies the vertical distance from the black box's bottom edge to the advance height.
        ///  Positive when the bottom edge of the black box is within the layout box.
        ///  Negative when the bottom edge of black box overhangs the layout box.
        /// </summary>
        public readonly int BottomSideBearing;

        /// <summary>
        ///  Specifies the Y coordinate of a glyph's vertical origin, in the font's design coordinate system.
        ///  The y coordinate of a glyph's vertical origin is the sum of the glyph's top side bearing
        ///  and the top (i.e. yMax) of the glyph's bounding box.
        /// </summary>
        public readonly int VerticalOriginY;
    }
}
