// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Windows.Types
{
    // https://msdn.microsoft.com/en-us/library/windows/desktop/ms633591.aspx
    public enum WindowLong : int
    {
        GWL_WNDPROC         = -4,
        GWL_HINSTANCE       = -6,
        GWL_HWNDPARENT      = -8,
        GWL_STYLE           = -16,
        GWL_EXSTYLE         = -20,
        GWL_USERDATA        = -21,
        GWL_ID              = -12
    }
}
