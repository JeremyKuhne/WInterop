// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Windows;

/// <summary>
///  Callback that processes messages sent to a window. [WindowProc]
/// </summary>
/// <docs>https://msdn.microsoft.com/en-us/library/windows/desktop/ms633573(v=vs.85).aspx</docs>
public delegate LResult WindowProcedure(
    WindowHandle hwnd,
    MessageType uMsg,
    WParam wParam,
    LParam lParam);