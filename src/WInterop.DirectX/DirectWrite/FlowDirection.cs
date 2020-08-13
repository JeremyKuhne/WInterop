// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.DirectWrite
{
    /// <summary>
    ///  Direction for how lines of text are placed relative to one another. [DWRITE_FLOW_DIRECTION]
    /// </summary>
    public enum FlowDirection : uint
    {
        /// <summary>
        ///  Text lines are placed from top to bottom.
        /// </summary>
        TopToBottom = 0,

        /// <summary>
        ///  Text lines are placed from bottom to top.
        /// </summary>
        BottomToTop = 1,

        /// <summary>
        ///  Text lines are placed from left to right.
        /// </summary>
        LeftToRight = 2,

        /// <summary>
        ///  Text lines are placed from right to left.
        /// </summary>
        RightToLeft = 3,
    }
}
