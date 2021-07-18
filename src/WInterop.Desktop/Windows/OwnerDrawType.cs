// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Windows
{
    public enum OwnerDrawType : uint
    {
        // https://msdn.microsoft.com/en-us/library/windows/desktop/bb775802.aspx

        /// <summary>
        ///  (ODT_MENU)
        /// </summary>
        Menu = 1,

        /// <summary>
        ///  (ODT_LISTBOX)
        /// </summary>
        ListBox = 2,

        /// <summary>
        ///  (ODT_COMBOBOX)
        /// </summary>
        ComboBox = 3,

        /// <summary>
        ///  (ODT_BUTTON)
        /// </summary>
        Button = 4,

        /// <summary>
        ///  (ODT_STATIC)
        /// </summary>
        Static = 5
    }
}