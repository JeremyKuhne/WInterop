// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Windows
{
    // https://msdn.microsoft.com/en-us/library/windows/desktop/ms633548.aspx
    public enum ShowWindowCommand : int
    {
        SW_HIDE            = 0,
        SW_SHOWNORMAL      = 1,
        SW_SHOWMINIMIZED   = 2,
        SW_SHOWMAXIMIZED   = 3,
        SW_SHOWNOACTIVATE  = 4,
        SW_SHOW            = 5,
        SW_MINIMIZE        = 6,
        SW_SHOWMINNOACTIVE = 7,
        SW_SHOWNA          = 8,
        SW_RESTORE         = 9,
        SW_SHOWDEFAULT     = 10,
        SW_FORCEMINIMIZE   = 11,
        SW_NORMAL = SW_SHOWNORMAL,
        SW_MAXIMIZE = SW_SHOWMAXIMIZED
    }
}
