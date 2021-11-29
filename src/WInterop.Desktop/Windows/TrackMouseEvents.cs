// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Windows;

[Flags]
public enum TrackMouseEvents : uint
{
    /// <summary>
    ///  [TME_HOVER]
    /// </summary>
    Hover = 0x00000001,

    /// <summary>
    ///  [TME_LEAVE]
    /// </summary>
    Leave = 0x00000002,

    /// <summary>
    ///  [TME_NONCLIENT]
    /// </summary>
    NonClient = 0x00000010,

    /// <summary>
    ///  [TME_QUERY]
    /// </summary>
    Query = 0x40000000,

    /// <summary>
    ///  [TME_CANCEL]
    /// </summary>
    Cancel = 0x80000000
}