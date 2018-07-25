// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace WInterop.Windows
{
    // https://msdn.microsoft.com/en-us/library/windows/desktop/ms646273.aspx
    [Flags]
    public enum MouseEvent : uint
    {
        MOUSEEVENTF_MOVE               = 0x0001, /* mouse move */
        MOUSEEVENTF_LEFTDOWN           = 0x0002, /* left button down */
        MOUSEEVENTF_LEFTUP             = 0x0004, /* left button up */
        MOUSEEVENTF_RIGHTDOWN          = 0x0008, /* right button down */
        MOUSEEVENTF_RIGHTUP            = 0x0010, /* right button up */
        MOUSEEVENTF_MIDDLEDOWN         = 0x0020, /* middle button down */
        MOUSEEVENTF_MIDDLEUP           = 0x0040, /* middle button up */
        MOUSEEVENTF_XDOWN              = 0x0080, /* x button down */
        MOUSEEVENTF_XUP                = 0x0100, /* x button down */
        MOUSEEVENTF_WHEEL              = 0x0800, /* wheel button rolled */
        MOUSEEVENTF_HWHEEL             = 0x1000, /* hwheel button rolled */
        MOUSEEVENTF_MOVE_NOCOALESCE    = 0x2000, /* do not coalesce mouse moves */
        MOUSEEVENTF_VIRTUALDESK        = 0x4000, /* map to entire virtual desktop */
        MOUSEEVENTF_ABSOLUTE           = 0x8000  /* absolute move */
    }
}
