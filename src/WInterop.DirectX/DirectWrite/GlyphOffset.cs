// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.DirectWrite
{
    /// <summary>
    ///  Optional adjustment to a glyph's position. A glyph offset changes the position of a glyph without affecting
    ///  the pen position. Offsets are in logical, pre-transform units.
    /// </summary>
    public readonly struct GlyphOffset
    {
        /// <summary>
        ///  Offset in the advance direction of the run. A positive advance offset moves the glyph to the right
        ///  (in pre-transform coordinates) if the run is left-to-right or to the left if the run is right-to-left.
        /// </summary>
        public readonly float AdvanceOffset;

        /// <summary>
        ///  Offset in the ascent direction, i.e., the direction ascenders point. A positive ascender offset moves
        ///  the glyph up (in pre-transform coordinates).
        /// </summary>
        public readonly float AscenderOffset;
    }
}
