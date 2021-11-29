// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Console;

public enum EventType : ushort
{
    /// <summary>
    ///  [FOCUS_EVENT]
    /// </summary>
    Focus = 0x0010,

    /// <summary>
    ///  [KEY_EVENT]
    /// </summary>
    Key = 0x0001,

    /// <summary>
    ///  [MENU_EVENT]
    /// </summary>
    Menu = 0x0008,

    /// <summary>
    ///  [MOUSE_EVENT]
    /// </summary>
    Mouse = 0x0002,

    /// <summary>
    ///  [WINDOW_BUFFER_SIZE_EVENT]
    /// </summary>
    WindowBufferSize = 0x0004
}