// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace WInterop.Direct2d
{
    /// <summary>
    /// Modifications made to the draw text call that influence how the text is
    /// rendered. [D2D1_DRAW_TEXT_OPTIONS]
    /// </summary>
    [Flags]
    public enum DrawTextOptions
    {
        /// <summary>
        /// Do not snap the baseline of the text vertically.
        /// </summary>
        NoSnap = 0x00000001,

        /// <summary>
        /// Clip the text to the content bounds.
        /// </summary>
        Clip = 0x00000002,

        /// <summary>
        /// Render color versions of glyphs if defined by the font.
        /// </summary>
        EnableColorFont = 0x00000004,

        /// <summary>
        /// Bitmap origins of color glyph bitmaps are not snapped.
        /// </summary>
        DisableColorBitmapSnapping = 0x00000008,

        None = 0x00000000,
    }
}
