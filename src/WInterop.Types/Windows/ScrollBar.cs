// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Windows.Types
{
    // https://msdn.microsoft.com/en-us/library/windows/desktop/bb787599.aspx
    public enum ScrollBar : int
    {
        /// <summary>
        /// Window standard horizontal scroll bar. (SB_HORZ)
        /// </summary>
        Horizontal = 0,

        /// <summary>
        /// Window standard vertical scroll bar. (SB_VERT)
        /// </summary>
        Vertical = 1,

        /// <summary>
        /// Scroll bar control. (SB_CTL)
        /// </summary>
        Control = 2,

        /// <summary>
        /// Both window standard scroll bars. (SB_BOTH)
        /// </summary>
        Both = 3
    }
}
