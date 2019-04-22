﻿// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using WInterop.Gdi.Native;
using WInterop.Windows.Native;

namespace WInterop.Windows.Native
{
    public struct MENUBARINFO
    {
        public uint cbSize;
        public RECT rcBar;
        public HMENU hMenu;
        public HWND hwndMenu;
        public Boolean32 fBarFocused;
        public Boolean32 fFocused;
    }
}
