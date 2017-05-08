// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace WInterop.Windows.Types
{
    // https://msdn.microsoft.com/en-us/library/windows/desktop/ms632600.aspx
    [Flags]
    public enum WindowStyle : uint
    {
        /// <summary>
        /// Overlapped window with a title bar and border. (WS_OVERLAPPED)
        /// </summary>
        Overlapped         = 0x00000000,

        /// <summary>
        /// Pop-up window. Cannot be used with Child. (WS_POPUP)
        /// </summary>
        PopUp              = 0x80000000,

        /// <summary>
        /// Child window. Cannot have a menu bar or be used with PopUp. (WS_CHILD)
        /// </summary>
        Child              = 0x40000000,

        /// <summary>
        /// Initially minimized. (WS_MINIMIZE)
        /// </summary>
        Minimize           = 0x20000000,

        /// <summary>
        /// Initialy visible. (WS_VISIBLE)
        /// </summary>
        Visible            = 0x10000000,

        /// <summary>
        /// Initially disabled. (WS_DISABLED)
        /// </summary>
        Diabled            = 0x08000000,

        /// <summary>
        /// Clips child windows relative to each other, preventing drawing in each-other
        /// when overlapping. (WS_CLIPSIBLINGS)
        /// </summary>
        ClipSiblings       = 0x04000000,

        /// <summary>
        /// Clips out child windows when drawing in parent's client area. (WS_CLIPCHILDREN)
        /// </summary>
        ClipChildren       = 0x02000000,

        /// <summary>
        /// Initially Maximized. (WS_MAXIMIZE)
        /// </summary>
        Maximize           = 0x01000000,

        /// <summary>
        /// Has title bar and border. (WS_CAPTION)
        /// </summary>
        Caption            = 0x00C00000,

        /// <summary>
        /// Has a thin-line border. (WS_BORDER)
        /// </summary>
        Border             = 0x00800000,

        /// <summary>
        /// Has a dialog style border. Cannot have a title bar. (WS_DLGFRAME)
        /// </summary>
        DialogFrame        = 0x00400000,

        /// <summary>
        /// Has a vertical scroll bar. (WS_VSCROLL)
        /// </summary>
        VerticalScroll     = 0x00200000,

        /// <summary>
        /// Has a horizontal scroll bar. (WS_HSCROLL)
        /// </summary>
        HorizontalScroll   = 0x00100000,

        /// <summary>
        /// Has a window menu in the title bar. Must also specify Caption. (WS_SYSMENU)
        /// </summary>
        SystemMenu         = 0x00080000,

        /// <summary>
        /// Has a sizing border. (WS_THICKFRAME)
        /// </summary>
        ThickFrame         = 0x00040000,

        /// <summary>
        /// First control of a group of controls. (WS_GROUP)
        /// </summary>
        Group              = 0x00020000,

        /// <summary>
        /// Is a control that can recieve focus via the TAB key. (WS_TABSTOP)
        /// </summary>
        TabStop            = 0x00010000,

        /// <summary>
        /// Has a minimize button. Cannot be used with extended style ContextHelp.
        /// Must also have SystemMenu style. (WS_MINIMIZEBOX)
        /// </summary>
        MinimizeBox        = 0x00020000,

        /// <summary>
        /// Has a maximize button. Cannot be used with extended style ContextHelp.
        /// Must also have SystemMenu style. (WS_MAXIMIZEBOX)
        /// </summary>
        MaximizeBox        = 0x00010000,

        /// <summary>
        /// Same as Overlapped. (WS_TILED)
        /// </summary>
        Tiled              = Overlapped,

        /// <summary>
        /// Same as Minimize. (WS_ICONIC)
        /// </summary>
        Iconic             = Minimize,

        /// <summary>
        /// Same as ThickFrame. (WS_SIZEBOX)
        /// </summary>
        SizeBox            = ThickFrame,

        /// <summary>
        /// Same as OverlappedWindow. (WS_TILEDWINDOW)
        /// </summary>
        TiledWindow        = OverlappedWindow,

        /// <summary>
        /// Standard overlapped window style. (WS_OVERLAPPEDWINDOW)
        /// </summary>
        OverlappedWindow   = Overlapped | Caption | SystemMenu | ThickFrame
                                | MinimizeBox | MaximizeBox,

        /// <summary>
        /// Standard pop-up window style. (WS_POPUPWINDOW)
        /// </summary>
        PopUpWindow        = PopUp | Border | SystemMenu,

        /// <summary>
        /// Same as Child. (WS_CHILDWINDOW)
        /// </summary>
        ChildWindow        = Child
    }
}
