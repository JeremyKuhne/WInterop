// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace WInterop.Windows.Types
{
    // https://msdn.microsoft.com/en-us/library/windows/desktop/bb775802.aspx
    [Flags]
    public enum OwnerDrawStates : uint
    {
        /// <summary>
        /// Menu item is selected. (ODS_SELECTED)
        /// </summary>
        Selected = 0x0001,

        /// <summary>
        /// Item is grayed. Menu only. (ODS_GRAYED)
        /// </summary>
        Grayed = 0x0002,

        /// <summary>
        /// Item is disabled. (ODS_DISABLED)
        /// </summary>
        Disabled = 0x0004,

        /// <summary>
        /// Item is checked. Menu only. (ODS_CHECKED)
        /// </summary>
        Checked = 0x0008,

        /// <summary>
        /// Item has keyboard focus. (ODS_FOCUS)
        /// </summary>
        Focus = 0x0010,

        /// <summary>
        /// Item is the default item. (ODS_DEFAULT)
        /// </summary>
        Default = 0x0020,

        /// <summary>
        /// Item is hot-tracked, highlighted as the mouse is on it. (ODS_HOTLIGHT)
        /// </summary>
        HotLight = 0x0040,

        /// <summary>
        /// Item is inactive. (ODS_INACTIVE)
        /// </summary>
        Inactive = 0x0080,

        /// <summary>
        /// Item has no accelerator cues. (ODS_NOACCEL)
        /// </summary>
        NoAcceleratorCues = 0x0100,

        /// <summary>
        /// Item has no focus cues. (ODS_NOFOCUSRECT)
        /// </summary>
        NoFocusRectangle = 0x0200,

        /// <summary>
        /// The drawing takes place in the selection field (edit control) of an owner-drawn combo box. (ODS_COMBOBOXEDIT)
        /// </summary>
        ComboBoxEdit = 0x1000
    }
}
