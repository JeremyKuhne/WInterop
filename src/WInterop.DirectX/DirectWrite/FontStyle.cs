// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.DirectWrite
{
    /// <summary>
    ///  The font style enumeration describes the slope style of a font face, such as Normal, Italic or Oblique.
    ///  [DWRITE_FONT_STYLE]
    /// </summary>
    public enum FontStyle : uint
    {
        /// <summary>
        ///  Font slope style : Normal.
        /// </summary>
        Normal,

        /// <summary>
        ///  Font slope style : Oblique.
        /// </summary>
        Oblique,

        /// <summary>
        ///  Font slope style : Italic.
        /// </summary>
        Italic
    }
}
