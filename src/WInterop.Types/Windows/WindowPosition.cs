﻿// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace WInterop.Windows
{
    // https://msdn.microsoft.com/en-us/library/windows/desktop/ms633545.aspx
    [Flags]
    public enum WindowPosition : uint
    {
        SWP_NOSIZE         = 0x0001,
        SWP_NOMOVE         = 0x0002,
        SWP_NOZORDER       = 0x0004,
        SWP_NOREDRAW       = 0x0008,
        SWP_NOACTIVATE     = 0x0010,
        SWP_FRAMECHANGED   = 0x0020,
        SWP_SHOWWINDOW     = 0x0040,
        SWP_HIDEWINDOW     = 0x0080,
        SWP_NOCOPYBITS     = 0x0100,
        SWP_NOOWNERZORDER  = 0x0200,
        SWP_NOSENDCHANGING = 0x0400,
        SWP_DRAWFRAME      = SWP_FRAMECHANGED,
        SWP_NOREPOSITION   = SWP_NOOWNERZORDER,
        SWP_DEFERERASE     = 0x2000,
        SWP_ASYNCWINDOWPOS = 0x4000
    }
}
