// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace WInterop.Windows.DataTypes
{
    public delegate IntPtr WindowProcedure(
        WindowHandle hwnd,
        MessageType uMsg,
        UIntPtr wParam,
        IntPtr lParam);
}
