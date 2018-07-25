// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace WInterop.Windows
{
    // https://msdn.microsoft.com/en-us/library/windows/desktop/ms644940.aspx
    [Flags]
    public enum QueueStatus : uint
    {
        QS_KEY             = 0x0001,
        QS_MOUSEMOVE       = 0x0002,
        QS_MOUSEBUTTON     = 0x0004,
        QS_POSTMESSAGE     = 0x0008,
        QS_TIMER           = 0x0010,
        QS_PAINT           = 0x0020,
        QS_SENDMESSAGE     = 0x0040,
        QS_HOTKEY          = 0x0080,
        QS_ALLPOSTMESSAGE  = 0x0100,
        QS_RAWINPUT        = 0x0400,
        QS_TOUCH           = 0x0800,
        QS_POINTER         = 0x1000,
        QS_MOUSE           = QS_MOUSEMOVE | QS_MOUSEBUTTON,
        QS_INPUT           = QS_MOUSE | QS_KEY | QS_RAWINPUT | QS_TOUCH | QS_POINTER,
        QS_ALLEVENTS       = QS_INPUT | QS_POSTMESSAGE | QS_TIMER | QS_PAINT | QS_HOTKEY,
        QS_ALLINPUT        = QS_ALLEVENTS | QS_SENDMESSAGE
    }
}
