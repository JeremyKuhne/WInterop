// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace WInterop.Windows.Types
{
    public delegate LRESULT WindowProcedure(
        WindowHandle hwnd,
        WindowMessage uMsg,
        WPARAM wParam,
        LPARAM lParam);
}
