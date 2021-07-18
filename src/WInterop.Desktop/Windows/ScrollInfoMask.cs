// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace WInterop.Windows
{
    // https://msdn.microsoft.com/en-us/library/windows/desktop/bb787537.aspx
    [Flags]
    public enum ScrollInfoMask : uint
    {
        /// <summary>
        ///  Get/set the min and max values. (SIF_RANGE)
        /// </summary>
        Range = 0x0001,

        /// <summary>
        ///  Get/set the page size. (SIF_PAGE)
        /// </summary>
        Page = 0x0002,

        /// <summary>
        ///  Get/set the scroll box position. Does not update while dragging. (SIF_POS)
        /// </summary>
        Position = 0x0004,

        /// <summary>
        ///  Disable the scroll bar. (SIF_DISABLENOSCROLL)
        /// </summary>
        Disable = 0x0008,

        /// <summary>
        ///  Get/set the current scroll box position. Updates while dragging. (SIF_TRACKPOS)
        /// </summary>
        TrackPosition = 0x0010,

        /// <summary>
        ///  (SIF_ALL)
        /// </summary>
        All = Range | Page | Position | TrackPosition
    }
}