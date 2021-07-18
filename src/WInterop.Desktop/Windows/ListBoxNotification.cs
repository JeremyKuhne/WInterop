// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
namespace WInterop.Windows
{
    // https://msdn.microsoft.com/en-us/library/ms907597.aspx
    public enum ListBoxNotification : ushort
    {
        /// <summary>
        ///  Not enough memory to process a request. (LBN_ERRSPACE)
        /// </summary>
        ErrorSpace = unchecked((ushort)-2),

        /// <summary>
        ///  Selection is about to change. (LBN_SELCHANGE)
        /// </summary>
        SelectionChange = 1,

        /// <summary>
        ///  String was double clicked. (LBN_DBLCLK)
        /// </summary>
        DoubleClick = 2,

        /// <summary>
        ///  Selection was canceled. (LBN_SELCANCEL)
        /// </summary>
        SelectionCancel = 3,

        /// <summary>
        ///  Recieved focus. (LBN_SETFOCUS)
        /// </summary>
        SetFocus = 4,

        /// <summary>
        ///  Lost focus. (LBN_KILLFOCUS)
        /// </summary>
        KillFocus = 5,
    }
}