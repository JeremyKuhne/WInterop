// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace WInterop.Windows;

/// <summary>
///  Mouse key states for mouse messages
///  https://msdn.microsoft.com/en-us/library/windows/desktop/ms645607.aspx
/// </summary>
[Flags]
public enum MouseKey : ushort
{
    /// <summary>
    ///  Left mouse button is down. (MK_LBUTTON)
    /// </summary>
    LeftButton = 0x0001,

    /// <summary>
    ///  Right mouse button is down. (MK_RBUTTON)
    /// </summary>
    RightButton = 0x0002,

    /// <summary>
    ///  Shift key is down. (MK_SHIFT)
    /// </summary>
    Shift = 0x0004,

    /// <summary>
    ///  Control key is down. (MK_CONTROL)
    /// </summary>
    Control = 0x0008,

    /// <summary>
    ///  Middle mouse button is down. (MK_MBUTTON)
    /// </summary>
    MiddleButton = 0x0010,

    /// <summary>
    ///  First extra mouse button is down. (MK_XBUTTON1)
    /// </summary>
    ExtraButton1 = 0x0020,

    /// <summary>
    ///  Second extra mouse button is down. (MK_XBUTTON2)
    /// </summary>
    ExtraButton2 = 0x0040
}