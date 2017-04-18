// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using WInterop.Handles.DataTypes;

namespace WInterop.Windows.DataTypes
{
    public delegate IntPtr WindowProc(
        WindowHandle hwnd,
        uint uMsg,
        UIntPtr wParam,
        IntPtr lParam);
}
