// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using WInterop.Gdi.Native;
using WInterop.Windows;

namespace WInterop.Resources.Native
{
    public struct MENUBARINFO
    {
        public uint cbSize;
        public RECT rcBar;
        public IntPtr hMenu;
        public WindowHandle hwndMenu;
        public BOOL fBarFocused;
        public BOOL fFocused;
    }
}
