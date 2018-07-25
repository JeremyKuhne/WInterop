﻿// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Windows.Types
{
    // https://msdn.microsoft.com/en-us/library/windows/desktop/ms633493.aspx
    // https://msdn.microsoft.com/en-us/library/windows/desktop/ms633496.aspx
    // https://msdn.microsoft.com/en-us/library/windows/desktop/ms633498.aspx

    /// <summary>
    /// Callback used to enumerate windows.
    /// [EnumWindowsProc, EnumThreadWndProc, EnumChildProc
    /// </summary>
    /// <param name="window">The found window.</param>
    /// <param name="lParam">Application defined value.</param>
    /// <returns>Return true to continue enumeratoring.</returns>
    public delegate bool EnumerateWindowProcedure(
        WindowHandle window,
        LPARAM lParam);
}
