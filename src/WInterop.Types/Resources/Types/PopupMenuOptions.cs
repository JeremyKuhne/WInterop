// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace WInterop.Resources.Types
{
    [Flags]
    public enum PopupMenuOptions
    {
        /// <summary>
        /// Can only use left button to select (default). [TPM_LEFTBUTTON]
        /// </summary>
        LeftButton = 0x0000,

        /// <summary>
        /// Left or right button can select. [TPM_RIGHTBUTTON]
        /// </summary>
        RightButton = 0x0002,

        /// <summary>
        /// Position the menu's left side on the given position (default). [TPM_LEFTALIGN]
        /// </summary>
        LeftAlign = 0x0000,

        /// <summary>
        /// Position the menu horizontalally centered on the given position. [TPM_CENTERALIGN]
        /// </summary>
        CenterAlign = 0x0004,

        /// <summary>
        /// Position the menu's right side on the given position. [TPM_RIGHTALIGN]
        /// </summary>
        RightAlign = 0x0008,

        /// <summary>
        /// Position the menu's top side on the given postion (default). [TPM_TOPALIGN]
        /// </summary>
        TopAlign = 0x0000,

        /// <summary>
        /// Position the menu vertically centered on the given position. [TPM_VCENTERALIGN]
        /// </summary>
        VerticalCenterAlign = 0x0010,

        /// <summary>
        /// Position the menu's bottom side on the given postion. [TPM_BOTTOMALIGN]
        /// </summary>
        BottomAlign = 0x0020,

        /// <summary>
        /// If the menu cannot be shown at the specified location without overlapping the excluded rectangle,
        /// the system tries to accommodate the requested horizontal alignment before the requested vertical
        /// alignment. [TPM_HORIZONTAL]
        /// </summary>
        Horizontal = 0x0000,

        /// <summary>
        /// If the menu cannot be shown at the specified location without overlapping the excluded rectangle,
        /// the system tries to accommodate the requested vertical alignment before the requested horizontal
        /// alignment. [TPM_VERTICAL]
        /// </summary>
        Vertical = 0x0040,

        /// <summary>
        /// Don't send selection notification. [TPM_NONOTIFY]
        /// </summary>
        NoNotify = 0x0080,

        /// <summary>
        /// Returns the user selection in the result. [TPM_RETURNCMD]
        /// </summary>
        ReturnCommand = 0x0100,

        /// <summary>
        /// [TPM_RECURSE]
        /// </summary>
        Recurse = 0x0001,

        /// <summary>
        /// Animates the menu from left to right. [TPM_HORPOSANIMATION]
        /// </summary>
        HorizontalPositiveAnimation = 0x0400,

        /// <summary>
        /// Animates the menu from right to left. [TPM_HORNEGANIMATION]
        /// </summary>
        HorizontalNegativeAnimation = 0x0800,

        /// <summary>
        /// Animates the menu from top to bottom. [TPM_VERPOSANIMATION]
        /// </summary>
        VerticalPositiveAnimation = 0x1000,

        /// <summary>
        /// Animates the menu from bottom to top. [TPM_VERNEGANIMATION]
        /// </summary>
        VerticalNegativeAnimation = 0x2000,

        /// <summary>
        /// Do not animate. [TPM_NOANIMATION]
        /// </summary>
        NoAnimation = 0x4000,

        /// <summary>
        /// [TPM_LAYOUTRTL]
        /// </summary>
        LayoutRightToleft = 0x8000,

        /// <summary>
        /// [TPM_WORKAREA]
        /// </summary>
        WorkArea = 0x10000
    }
}
