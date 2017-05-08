// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace WInterop.Windows.Types
{
    // https://msdn.microsoft.com/en-us/library/windows/desktop/ms644907.aspx
    public delegate void TimerProcedure(
        WindowHandle hwnd,
        WindowMessage uMsg,
        TimerId idEvent,
        uint dwTime);
}
