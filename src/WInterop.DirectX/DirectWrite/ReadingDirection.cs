// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.DirectWrite;

/// <summary>
///  Direction for how reading progresses. [DWRITE_READING_DIRECTION]
/// </summary>
public enum ReadingDirection : uint
{
    /// <summary>
    ///  Reading progresses from left to right.
    /// </summary>
    LeftToRight = 0,

    /// <summary>
    ///  Reading progresses from right to left.
    /// </summary>
    RightToLeft = 1,

    /// <summary>
    ///  Reading progresses from top to bottom.
    /// </summary>
    TopToBottom = 2,

    /// <summary>
    ///  Reading progresses from bottom to top.
    /// </summary>
    BottomToTop = 3,
}
