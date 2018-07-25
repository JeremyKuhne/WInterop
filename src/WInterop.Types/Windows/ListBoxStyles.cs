// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace WInterop.Windows.Types
{
    [Flags]
    public enum ListBoxStyles : uint
    {
        // https://msdn.microsoft.com/en-us/library/windows/desktop/bb775149.aspx

        /// <summary>
        /// Sends notifcation codes to the parent window when
        /// item selections are made. (LBS_NOTIFY)
        /// </summary>
        Notify = 0x0001,

        /// <summary>
        /// Sort strings alphabetically. (LBS_SORT)
        /// </summary>
        Sort = 0x0002,

        /// <summary>
        /// Don't update appearance when changes are made. (LBS_NOREDRAW)
        /// </summary>
        NoRedraw = 0x0004,

        /// <summary>
        /// Toggles selections. (LBS_MULTIPLESEL)
        /// </summary>
        MultipleSelect = 0x0008,

        /// <summary>
        /// Owner drawn and items are same in height. (LBS_OWNERDRAWFIXED)
        /// </summary>
        OwnerDrawFixed = 0x0010,

        /// <summary>
        /// Owner drawn and items are variable in height. (LBS_OWNERDRAWVARIABLE)
        /// </summary>
        OwnerDrawVariable = 0x0020,

        /// <summary>
        /// Items consist of strings. (LBS_HASSTRINGS)
        /// </summary>
        HasStrings = 0x0040,

        /// <summary>
        /// Expands tab characters. (LBS_USETABSTOPS)
        /// </summary>
        UseTabStops = 0x0080,

        /// <summary>
        /// Prevents sizing of the listbox to avoid showing partial items.
        /// (LBS_NOINTEGRALHEIGHT)
        /// </summary>
        NoIntegralHeight = 0x0100,

        /// <summary>
        /// Multi-column listbox. Scrolls horizontally. (LBS_MULTICOLUMN)
        /// </summary>
        MultiColumn = 0x0200,

        /// <summary>
        /// Sends VirtualKeyToItem messages (WM_VKEYTOITEM) when the list box recieves key
        /// presses. (LBS_WANTKEYBOARDINPUT)
        /// </summary>
        WantKeyboardInput = 0x0400,

        /// <summary>
        /// Allows multiple items to be selected. (LBS_EXTENDEDSEL)
        /// </summary>
        ExtendedSelection = 0x0800,

        /// <summary>
        /// Disables, rather than hides the scroll bar when there are not
        /// enough items to scroll. (LBS_DISABLENOSCROLL)
        /// </summary>
        DisableNoScroll = 0x1000,

        /// <summary>
        /// Owner drawn list box with no data for the item. (LBS_NODATA)
        /// </summary>
        NoData = 0x2000,

        /// <summary>
        /// Items can be viewed, but not selected. (LBS_NOSEL)
        /// </summary>
        NoSelect = 0x4000,

        /// <summary>
        /// Used to make a linked child of a combo box. (LBS_COMBOBOX)
        /// </summary>
        ComboBox = 0x8000,

        /// <summary>
        /// Standard listbox style. (LBS_STANDARD)
        /// </summary>
        Standard = (Notify | Sort | WindowStyles.VerticalScroll | WindowStyles.Border)
    }
}
