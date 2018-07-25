// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace WInterop.Windows
{
    [Flags]
    public enum ScrollBarStyles : uint
    {
        // https://msdn.microsoft.com/en-us/library/windows/desktop/bb787533.aspx
        // https://msdn.microsoft.com/en-us/library/ms997557.aspx

        /// <summary>
        /// Horizontal scroll bar. (SBS_HORZ)
        /// </summary>
        Horizontal = 0x0000,

        /// <summary>
        /// Vertical scroll bar. (SBS_VERT)
        /// </summary>
        Veritcal = 0x0001,

        /// <summary>
        /// Aligns a default height horizontal scrollbar with the top of the control. (SBS_TOPALIGN)
        /// </summary>
        TopAlign = 0x0002,

        /// <summary>
        /// Aligns a default width vertical scrollbar with the left of the control. (SBS_LEFTALIGN)
        /// </summary>
        LeftAlign = 0x0002,

        /// <summary>
        /// Aligns a default height horizontal scrollbar with the bottom of the control. (SBS_BOTTOMALIGN)
        /// </summary>
        BottomAlign = 0x0004,

        /// <summary>
        /// Aligns a default width vertical scrollbar with the right of the control. (SBS_RIGHTALIGN)
        /// </summary>
        RightAlign = 0x0004,

        /// <summary>
        /// Size box is top left aligned and standard size. (SBS_SIZEBOXTOPLEFTALIGN)
        /// </summary>
        SizeBoxTopLeftAlign = 0x0002,

        /// <summary>
        /// Size box is bottom right aligned and standard size. (SBS_SIZEBOXBOTTOMRIGHTALIGN)
        /// </summary>
        SizeBoxBottomRightAlign = 0x0004,

        /// <summary>
        /// Designates a size box. (SBS_SIZEBOX)
        /// </summary>
        SizeBox = 0x0008,

        /// <summary>
        /// Designates a size box with a raised grip. (SBS_SIZEGRIP)
        /// </summary>
        SizeGrip = 0x0010
    }
}
