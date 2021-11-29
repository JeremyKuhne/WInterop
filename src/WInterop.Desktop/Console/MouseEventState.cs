// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Console;

public enum MouseEventState : uint
{
    /// <summary>
    ///  [MOUSE_MOVED]
    /// </summary>
    Moved = 0x0001,

    /// <summary>
    ///  [DOUBLE_CLICK]
    /// </summary>
    DoubleClicked = 0x0002,

    /// <summary>
    ///  [MOUSE_WHEELED]
    /// </summary>
    Wheeled = 0x0004,

    /// <summary>
    ///  [MOUSE_HWHEELED]
    /// </summary>
    HorizontalWheeled = 0x0008
}