// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.DirectWrite
{
    /// <summary>
    /// Word wrapping in multiline paragraph. [DWRITE_WORD_WRAPPING]
    /// </summary>
    public enum WordWrapping : uint
    {
        /// <summary>
        /// Words are broken across lines to avoid text overflowing the layout box.
        /// </summary>
        Wrap = 0,

        /// <summary>
        /// Words are kept within the same line even when it overflows the layout box.
        /// This option is often used with scrolling to reveal overflow text. 
        /// </summary>
        NoWrap = 1,

        /// <summary>
        /// Words are broken across lines to avoid text overflowing the layout box.
        /// Emergency wrapping occurs if the word is larger than the maximum width.
        /// </summary>
        EmergencyBreak = 2,

        /// <summary>
        /// Only wrap whole words, never breaking words (emergency wrapping) when the
        /// layout width is too small for even a single word.
        /// </summary>
        WholeWord = 3,

        /// <summary>
        /// Wrap between any valid characters clusters.
        /// </summary>
        Character = 4,
    }
}
