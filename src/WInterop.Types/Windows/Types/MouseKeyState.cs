// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace WInterop.Windows.Types
{
    /// <summary>
    /// Mouse key states for mouse messages
    /// </summary>
    [Flags]
    public enum MouseKeyState : ushort
    {
        MK_LBUTTON         = 0x0001,
        MK_RBUTTON         = 0x0002,
        MK_SHIFT           = 0x0004,
        MK_CONTROL         = 0x0008,
        MK_MBUTTON         = 0x0010,
        MK_XBUTTON1        = 0x0020,
        MK_XBUTTON2        = 0x0040
    }
}
