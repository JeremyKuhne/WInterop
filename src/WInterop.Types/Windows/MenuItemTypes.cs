// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace WInterop.Windows
{
    [Flags]
    public enum MenuItemTypes : uint
    {
        // https://msdn.microsoft.com/en-us/library/windows/desktop/ms647578.aspx

        /// <summary>
        /// [MFT_STRING]
        /// </summary>
        String = MenuFlags.String,

        /// <summary>
        /// [MFT_BITMAP]
        /// </summary>
        Bitmap = MenuFlags.Bitmap,

        /// <summary>
        /// [MFT_MENUBARBREAK]
        /// </summary>
        MenuBarBreak = MenuFlags.MenuBarBreak,

        /// <summary>
        /// [MFT_MENUBREAK]
        /// </summary>
        MenuBreak = MenuFlags.MenuBreak,

        /// <summary>
        /// [MFT_OWNERDRAW]
        /// </summary>
        OwnerDraw = MenuFlags.OwnerDraw,

        /// <summary>
        /// MFT_RADIOCHECK
        /// </summary>
        RadioCheck = 0x00000200,

        /// <summary>
        /// [MFT_SEPARATOR]
        /// </summary>
        Separator = MenuFlags.Separator,

        /// <summary>
        /// [MFT_RIGHTORDER]
        /// </summary>
        RightOrder = 0x00002000,

        /// <summary>
        /// [MFT_RIGHTJUSTIFY]
        /// </summary>
        RightJustify = MenuFlags.RightJustify
    }
}
