// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Gdi
{
    public enum PaletteEntryType : byte
    {
        /// <summary>
        ///  No special behavior.
        /// </summary>
        Default     = 0x00,

        /// <summary>
        ///  Specifies that the logical palette entry be used for palette animation. [PC_RESERVED]
        /// </summary>
        Reserved    = 0x01,

        /// <summary>
        ///  Specifies that the low-order word of the logical palette entry designates a hardware palette index.
        ///  [PC_EXPLICIT]
        /// </summary>
        Explicit    = 0x02,

        /// <summary>
        ///  Does not match the color to the system palette.
        /// </summary>
        NoCollapse  = 0x04
    }
}
