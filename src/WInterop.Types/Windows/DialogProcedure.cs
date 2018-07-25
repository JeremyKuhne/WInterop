﻿// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace WInterop.Windows.Types
{
    // https://msdn.microsoft.com/en-us/library/windows/desktop/ms645469.aspx
    public delegate IntPtr DialogProcedure(
        WindowHandle hwndDlg,
        WindowMessage uMsg,
        WPARAM wParam,
        LPARAM lParam);
}
