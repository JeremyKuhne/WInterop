// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Windows.Types
{
    public enum EditNotification : ushort
    {
        // https://msdn.microsoft.com/en-us/library/windows/desktop/ff485924.aspx

        /// <summary>
        /// Edit control has recieved focus. (EN_SETFOCUS)
        /// </summary>
        SetFocus = 0x0100,

        /// <summary>
        /// Edit control has lost focus. (EN_KILLFOCUS)
        /// </summary>
        KillFocus = 0x0200,

        /// <summary>
        /// User has altered text and the text has been displayd. (EN_CHANGE)
        /// </summary>
        Change = 0x0300,

        /// <summary>
        /// Control is about to redraw. (EN_UPDATE)
        /// </summary>
        Update = 0x0400,

        /// <summary>
        /// Control can't allocate enough memory. (EN_ERRSPACE)
        /// </summary>
        ErrorSpace = 0x0500,

        /// <summary>
        /// Control cannot insert any more text. (EN_MAXTEXT)
        /// </summary>
        MaxText = 0x0501,

        /// <summary>
        /// User has clicked in the horizontal scroll bar. (EN_HSCROLL)
        /// </summary>
        HorizontalScroll = 0x0601,

        /// <summary>
        /// User has clicked in the vertical scroll bar. (EN_VSCROLL)
        /// </summary>
        VerticalScroll = 0x0602,

        /// <summary>
        /// User has changed edit control direction to left-to-right. (EN_ALIGN_LTR_EC)
        /// </summary>
        AlignLeftToRight = 0x0700,

        /// <summary>
        /// User has changed edit control direction to right-to-left. (EN_ALIGN_RTL_EC)
        /// </summary>
        AlignRightToLeft = 0x0701,

        /// <summary>
        /// (EN_BEFORE_PASTE)
        /// </summary>
        BeforePaste = 0x0800,

        /// <summary>
        /// (EN_AFTER_PASTE)
        /// </summary>
        AfterPaste = 0x0801
    }
}
