// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Resources.Types
{
    public enum MenuObject : int
    {
        // https://msdn.microsoft.com/en-us/library/windows/desktop/dd373606.aspx

        /// <summary>
        /// Popup menu. (OBJID_CLIENT)
        /// </summary>
        Client = unchecked((int)0xFFFFFFFC),

        /// <summary>
        /// Menu bar. (OBJID_MENU)
        /// </summary>
        Menu = unchecked((int)0xFFFFFFFD),

        /// <summary>
        /// System menu. (OBJID_SYSMENU)
        /// </summary>
        SystemMenu = unchecked((int)0xFFFFFFFF)
    }
}
