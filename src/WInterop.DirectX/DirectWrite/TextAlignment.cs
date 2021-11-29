// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.DirectWrite
{
    /// <summary>
    ///  Alignment of paragraph text along the reading direction axis relative to 
    ///  the leading and trailing edge of the layout box. [DWRITE_TEXT_ALIGNMENT]
    /// </summary>
    public enum TextAlignment : uint
    {
        /// <summary>
        ///  The leading edge of the paragraph text is aligned to the layout box's leading edge.
        /// </summary>
        Leading = DWRITE_TEXT_ALIGNMENT.DWRITE_TEXT_ALIGNMENT_LEADING,

        /// <summary>
        ///  The trailing edge of the paragraph text is aligned to the layout box's trailing edge.
        /// </summary>
        Trailing = DWRITE_TEXT_ALIGNMENT.DWRITE_TEXT_ALIGNMENT_TRAILING,

        /// <summary>
        ///  The center of the paragraph text is aligned to the center of the layout box.
        /// </summary>
        Center = DWRITE_TEXT_ALIGNMENT.DWRITE_TEXT_ALIGNMENT_CENTER,

        /// <summary>
        ///  Align text to the leading side, and also justify text to fill the lines.
        /// </summary>
        Justified = DWRITE_TEXT_ALIGNMENT.DWRITE_TEXT_ALIGNMENT_JUSTIFIED
    }
}
