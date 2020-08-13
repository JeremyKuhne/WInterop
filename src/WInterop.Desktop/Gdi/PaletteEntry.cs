// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Gdi
{
    /// <summary>
    ///  [PALETTEENTRY]
    /// </summary>
    public struct PaletteEntry
    {
        /// <summary>
        ///  [peRed]
        /// </summary>
        public byte Red;

        /// <summary>
        ///  [peGreen]
        /// </summary>
        public byte Green;

        /// <summary>
        ///  [peBlue]
        /// </summary>
        public byte Blue;

        /// <summary>
        ///  [peGreen]
        /// </summary>
        public PaletteEntryType Flags;

        public override string ToString() => $"RGB: 0x{Red:X2}, 0x{Green:X2}, 0x{Blue:X2} {Flags}";
    }
}
