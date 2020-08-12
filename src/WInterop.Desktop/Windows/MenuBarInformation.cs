// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Drawing;
using WInterop.Windows.Native;
using WInterop.Windows;

namespace WInterop.Windows
{
    public struct MenuBarInformation
    {
        public Rectangle Bar;
        public MenuHandle Menu;
        public WindowHandle SubMenu;
        public bool BarFocused;
        public bool ItemFocused;

        public static implicit operator MenuBarInformation(MENUBARINFO info)
        {
            return new MenuBarInformation
            {
                Bar = info.rcBar,
                Menu = new MenuHandle(info.hMenu, ownsHandle: false),
                SubMenu = info.hwndMenu,
                BarFocused = info.fBarFocused,
                ItemFocused = info.fFocused
            };
        }

        public static unsafe explicit operator MENUBARINFO(MenuBarInformation info)
        {
            return new MENUBARINFO
            {
                cbSize = (uint)sizeof(MENUBARINFO),
                rcBar = info.Bar,
                hMenu = (HMENU)info.Menu,
                hwndMenu = info.SubMenu.HWND,
                fBarFocused = info.BarFocused,
                fFocused = info.ItemFocused
            };
        }
    }
}
