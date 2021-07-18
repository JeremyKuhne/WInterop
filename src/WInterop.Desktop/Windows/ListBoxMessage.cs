// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Windows
{
    public enum ListBoxMessage : uint
    {
        // https://msdn.microsoft.com/en-us/library/windows/desktop/ff485967.aspx

        /// <summary>
        ///  (LB_ADDSTRING)
        /// </summary>
        AddString = 0x0180,

        /// <summary>
        ///  (LB_INSERTSTRING)
        /// </summary>
        InsertString = 0x0181,

        /// <summary>
        ///  (LB_DELETESTRING)
        /// </summary>
        DeleteString = 0x0182,

        /// <summary>
        ///  (LB_SELITEMRANGEEX)
        /// </summary>
        SelectItemRangeExtended = 0x0183,

        /// <summary>
        ///  (LB_RESETCONTENT)
        /// </summary>
        ResetContent = 0x0184,

        /// <summary>
        ///  Set the selection state of a specified item. (LB_SETSEL)
        /// </summary>
        SetSelection = 0x0185,

        /// <summary>
        ///  Set the currently selected item for a single selection listbox. (LB_SETCURSEL)
        /// </summary>
        SetCurrentSelection = 0x0186,

        /// <summary>
        ///  Get the selection state of a specified item. (LB_GETSEL)
        /// </summary>
        GetSelection = 0x0187,

        /// <summary>
        ///  Get the currently selected item for a single selection listbox. (LB_GETCURSEL)
        /// </summary>
        GetCurrentSelection = 0x0188,

        /// <summary>
        ///  Get the text of an item. (LB_GETTEXT)
        ///  LRESULT is the length without the terminating null, or -1 for an error such
        ///  as bad index (LB_ERR).
        /// </summary>
        GetText = 0x0189,

        /// <summary>
        ///  Get the length, in characters, of an item. (LB_GETTEXTLEN)
        /// </summary>
        GetTextLength = 0x018A,

        /// <summary>
        ///  Get the number of items. (LB_GETCOUNT)
        /// </summary>
        GetCount = 0x018B,

        /// <summary>
        ///  Select an item that begins with the given string. (LB_SELECTSTRING)
        /// </summary>
        SelectString = 0x018C,

        /// <summary>
        ///  Adds files or drives to the listbox. (LB_DIR)
        /// </summary>
        Directory = 0x018D,

        /// <summary>
        ///  Get the index of the first visible item. (LB_GETTOPINDEX)
        /// </summary>
        GetTopIndex = 0x018E,

        /// <summary>
        ///  Finds the first item that begins with the given string. (LB_FINDSTRING)
        /// </summary>
        FindString = 0x018F,

        /// <summary>
        ///  Gets the count of selected items in a multiple-selection list box. (LB_GETSELCOUNT)
        /// </summary>
        GetSelectionCount = 0x0190,

        /// <summary>
        ///  Gets selected items. (LB_GETSELITEMS)
        /// </summary>
        GetSelectedItems = 0x0191,

        /// <summary>
        ///  Sets tab stops for expanding tabs. (LB_SETTABSTOPS)
        /// </summary>
        SetTabStops = 0x0192,

        /// <summary>
        ///  Gets the scrollable pixel width. (LB_GETHORIZONTALEXTENT)
        /// </summary>
        GetHorizontalExtent = 0x0193,

        /// <summary>
        ///  (LB_SETHORIZONTALEXTENT)
        /// </summary>
        SetHorizontalExtent = 0x0194,

        /// <summary>
        ///  (LB_SETCOLUMNWIDTH)
        /// </summary>
        SetColumnWidth = 0x0195,

        /// <summary>
        ///  Add a file to a list box that contains a directory listing. (LB_ADDFILE)
        /// </summary>
        AddFile = 0x0196,

        /// <summary>
        ///  Ensures that the given item is visible. (LB_SETTOPINDEX)
        /// </summary>
        SetTopIndex = 0x0197,

        /// <summary>
        ///  Gets the bounds of the item as it is displayed. (LB_GETITEMRECT)
        /// </summary>
        GetItemRectangle = 0x0198,

        /// <summary>
        ///  Get the value associated with the given item. (LB_GETITEMDATA)
        /// </summary>
        GetItemData = 0x0199,

        /// <summary>
        ///  Set the value associated with the given item. (LB_SETITEMDATA)
        /// </summary>
        SetItemData = 0x019A,

        /// <summary>
        ///  Selects/deselects consecutive items. (LB_SELITEMRANGE)
        /// </summary>
        SelectItemRange = 0x019B,

        /// <summary>
        ///  Sets the anchor item for a multiple selection. (LB_SETANCHORINDEX)
        /// </summary>
        SetAnchorIndex = 0x019C,

        /// <summary>
        ///  (LB_GETANCHORINDEX)
        /// </summary>
        GetAnchorIndex = 0x019D,

        /// <summary>
        ///  Sets focus to the given item. (LB_SETCARETINDEX)
        /// </summary>
        SetCaretIndex = 0x019E,

        /// <summary>
        ///  Gets the item that has focus. (LB_GETCARETINDEX)
        /// </summary>
        GetCaretIndex = 0x019F,

        /// <summary>
        ///  (LB_SETITEMHEIGHT)
        /// </summary>
        SetItemHeight = 0x01A0,

        /// <summary>
        ///  (LB_GETITEMHEIGHT)
        /// </summary>
        GetItemHeight = 0x01A1,

        /// <summary>
        ///  Find the full string, case-insensitive. (LB_FINDSTRINGEXACT)
        /// </summary>
        FindStringExact = 0x01A2,

        /// <summary>
        ///  (LB_SETLOCALE)
        /// </summary>
        SetLocale = 0x01A5,

        /// <summary>
        ///  (LB_GETLOCALE)
        /// </summary>
        GetLocale = 0x01A6,

        /// <summary>
        ///  Sets the count of items when using the NoData style. (LB_SETCOUNT)
        /// </summary>
        SetCount = 0x01A7,

        /// <summary>
        ///  Pre-initialize space to improve perf on adding items. (LB_INITSTORAGE)
        /// </summary>
        InitStorage = 0x01A8,

        /// <summary>
        ///  Gets the item nearest the given point. (LB_ITEMFROMPOINT)
        /// </summary>
        ItemFromPoint = 0x01A9,

        /// <summary>
        ///  Get the number of items per column. (LB_GETLISTBOXINFO)
        /// </summary>
        GetListBoxInfo = 0x01B2
    }
}