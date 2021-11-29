// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.DirectWrite;

/// <summary>
///  Direction for how lines of text are placed relative to one another. [DWRITE_FLOW_DIRECTION]
/// </summary>
public enum FlowDirection : uint
{
    /// <summary>
    ///  Text lines are placed from top to bottom.
    /// </summary>
    TopToBottom = DWRITE_FLOW_DIRECTION.DWRITE_FLOW_DIRECTION_TOP_TO_BOTTOM,

    /// <summary>
    ///  Text lines are placed from bottom to top.
    /// </summary>
    BottomToTop = DWRITE_FLOW_DIRECTION.DWRITE_FLOW_DIRECTION_BOTTOM_TO_TOP,

    /// <summary>
    ///  Text lines are placed from left to right.
    /// </summary>
    LeftToRight = DWRITE_FLOW_DIRECTION.DWRITE_FLOW_DIRECTION_LEFT_TO_RIGHT,

    /// <summary>
    ///  Text lines are placed from right to left.
    /// </summary>
    RightToLeft = DWRITE_FLOW_DIRECTION.DWRITE_FLOW_DIRECTION_RIGHT_TO_LEFT,
}
