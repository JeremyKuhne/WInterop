// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace WInterop.Windows
{
    // https://msdn.microsoft.com/en-us/library/windows/desktop/ms646271.aspx
    [Flags]
    public enum KeyEvent : uint
    {
        KEYEVENTF_EXTENDEDKEY = 0x0001,
        KEYEVENTF_KEYUP       = 0x0002,
        KEYEVENTF_UNICODE     = 0x0004,
        KEYEVENTF_SCANCODE    = 0x0008
    }
}
