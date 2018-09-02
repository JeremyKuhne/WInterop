// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Windows
{
    // https://msdn.microsoft.com/en-us/library/windows/desktop/ms648381.aspx
    public enum CursorState : uint
    {
        /// <summary>
        /// [CURSOR_SHOWING]
        /// </summary>
        Showing = 0x00000001,

        /// <summary>
        /// [CURSOR_SUPPRESSED]
        /// </summary>
        Suppressed = 0x00000002
    }
}
