// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.


namespace WInterop.Windows.Types
{
    // https://msdn.microsoft.com/en-us/library/windows/desktop/ms633573(v=vs.85).aspx

    /// <summary>
    /// Callback that processes messages sent to a window. [WindowProc]
    /// </summary>
    public delegate LRESULT WindowProcedure(
        WindowHandle hwnd,
        WindowMessage uMsg,
        WPARAM wParam,
        LPARAM lParam);
}
