// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Windows
{
    /// <docs>https://docs.microsoft.com/windows/win32/api/winuser/nc-winuser-timerproc</docs>
    public delegate void TimerProcedure(
        WindowHandle hwnd,
        MessageType uMsg,
        TimerId idEvent,
        uint dwTime);
}
