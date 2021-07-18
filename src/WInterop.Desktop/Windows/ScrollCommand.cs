// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Windows
{
    public enum ScrollCommand
    {
        // https://msdn.microsoft.com/en-us/library/windows/desktop/bb787577.aspx

        /// <summary>
        ///  (SB_LINEUP)
        /// </summary>
        LineUp = 0,

        /// <summary>
        ///  (SB_LINELEFT)
        /// </summary>
        LineLeft = 0,

        /// <summary>
        ///  (SB_LINEDOWN)
        /// </summary>
        LineDown = 1,

        /// <summary>
        ///  (SB_LINERIGHT)
        /// </summary>
        LineRight = 1,

        /// <summary>
        ///  (SB_PAGEUP)
        /// </summary>
        PageUp = 2,

        /// <summary>
        ///  (SB_PAGELEFT)
        /// </summary>
        PageLeft = 2,

        /// <summary>
        ///  (SB_PAGEDOWN)
        /// </summary>
        PageDown = 3,

        /// <summary>
        ///  (SB_PAGERIGHT)
        /// </summary>
        PageRight = 3,

        /// <summary>
        ///  User has finished dragging the scroll box. (SB_THUMBPOSITION)
        /// </summary>
        ThumbPosition = 4,

        /// <summary>
        ///  User is dragging the scroll box. (SB_THUMBTRACK)
        /// </summary>
        ThumbTrack = 5,

        /// <summary>
        ///  Scroll to the top left. (SB_TOP)
        /// </summary>
        Top = 6,

        /// <summary>
        ///  Scroll to the top left. (SB_LEFT)
        /// </summary>
        Left = 6,

        /// <summary>
        ///  Scroll to the bottom right. (SB_BOTTOM)
        /// </summary>
        Bottom = 7,

        /// <summary>
        ///  Scroll to the bottom right. (SB_RIGHT)
        /// </summary>
        Right = 7,

        /// <summary>
        ///  Ends scrolling. (SB_ENDSCROLL)
        /// </summary>
        SB_ENDSCROLL = 8
    }
}