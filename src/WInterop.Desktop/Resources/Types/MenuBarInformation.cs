// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using WInterop.Gdi;
using WInterop.Windows;

namespace WInterop.Resources.Types
{
    public struct MenuBarInformation
    {
        public RECT Bar;
        public MenuHandle Menu;
        public WindowHandle SubMenu;
        public bool BarFocused;
        public bool ItemFocused;

        public static implicit operator MenuBarInformation(MENUBARINFO info)
        {
            return new MenuBarInformation
            {
                Bar = info.rcBar,
                Menu = info.hMenu,
                SubMenu = info.hwndMenu,
                BarFocused = info.fBarFocused,
                ItemFocused = info.fFocused
            };
        }

        public unsafe static implicit operator MENUBARINFO(MenuBarInformation info)
        {
            return new MENUBARINFO
            {
                cbSize = (uint)sizeof(MENUBARINFO),
                rcBar = info.Bar,
                hMenu = (IntPtr)info.Menu,
                hwndMenu = info.SubMenu.HWND,
                fBarFocused = info.BarFocused,
                fFocused = info.ItemFocused
            };
        }
    }
}
