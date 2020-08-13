// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Windows
{
    // https://docs.microsoft.com/windows/win32/api/winuser/nf-winuser-getsystemmetrics
    public enum SystemMetric : int
    {
        /// <summary>
        ///  The width of the screen of the primary display monitor, in pixels. [SM_CXSCREEN]
        /// </summary>
        /// <remarks>
        ///  This is the same value obtained by calling GetDeviceCaps as follows: GetDeviceCaps(hdcPrimaryMonitor, HORZRES).
        /// </remarks>
        ScreenWidth = 0,

        /// <summary>
        ///  The height of the screen of the primary display monitor, in pixels. [SM_CYSCREEN]
        /// </summary>
        /// <remarks>
        ///  This is the same value obtained by calling GetDeviceCaps as follows: GetDeviceCaps(hdcPrimaryMonitor, VERTRES).
        /// </remarks>
        ScreenHeight = 1,

        /// <summary>
        ///  Vertical scroll bar width in pixels. [SM_CXVSCROLL]
        /// </summary>
        VerticalScrollWidth = 2,

        /// <summary>
        ///  Horizontal scroll height in pixels. [SM_CYHSCROLL]
        /// </summary>
        HorizontalScrollHeight = 3,

        /// <summary>
        ///  Caption area height in pixels. [SM_CYCAPTION]
        /// </summary>
        CaptionAreaHeight = 4,

        /// <summary>
        ///  Border width in pixels. [SM_CXBORDER]
        /// </summary>
        BorderWidth = 5,

        /// <summary>
        ///  Border height in pixels. [SM_CYBORDER]
        /// </summary>
        BorderHeight = 6,

        /// <summary>
        ///  Horizontal border height for an unsizable window. [SM_CXDLGFRAME]
        /// </summary>
        DialogFrameHeight = 7,

        /// <summary>
        ///  Horizontal border height for an unsizable window. [SM_CXFIXEDFRAME]
        /// </summary>
        FixedFrameHeight = DialogFrameHeight,

        /// <summary>
        ///  Vertical border width for an unsizable window. [SM_CYDLGFRAME]
        /// </summary>
        DialogFrameWidth = 8,

        /// <summary>
        ///  Vertical border width for an unsizable window. [SM_CYFIXEDFRAME]
        /// </summary>
        FixedFrameWidth = DialogFrameWidth,

        /// <summary>
        ///  The height of the thumb box in a vertical scroll bar, in pixels. [SM_CYVTHUMB]
        /// </summary>
        VerticalThumbHeight = 9,

        /// <summary>
        ///  The width of the thumb box in a horizontal scroll bar, in pixels. [SM_CXHTHUMB]
        /// </summary>
        HorizontalThumbWidth = 10,

        /// <summary>
        ///  The default width of an icon, in pixels. [SM_CXICON]
        /// </summary>
        IconWidth = 11,

        /// <summary>
        ///  The default height of an icon, in pixels. [SM_CYICON]
        /// </summary>
        IconHeight = 12,

        /// <summary>
        ///  The width of a cursor, in pixels. [SM_CXCURSOR]
        /// </summary>
        CursorWidth = 13,

        /// <summary>
        ///  The height of a cursor, in pixels. [SM_CYCURSOR]
        /// </summary>
        CursorHeight = 14,

        /// <summary>
        ///  The height of a single-line menu bar, in pixels. [SM_CYMENU]
        /// </summary>
        MenuHeight = 15,

        /// <summary>
        ///  The width of the client area for a full-screen window on the primary display monitor, in pixels. [SM_CXFULLSCREEN]
        /// </summary>
        FullScreenWindowWidth = 16,

        /// <summary>
        ///  The height of the client area for a full-screen window on the primary display monitor, in pixels. [SM_CYFULLSCREEN]
        /// </summary>
        FullScreenWindowHeight = 17,

        /// <summary>
        ///  For double byte character set versions of the system, this is the height of the Kanji window
        ///  at the bottom of the screen, in pixels. [SM_CYKANJIWINDOW]
        /// </summary>
        KanjiWindowHeight = 18,

        /// <summary>
        ///  Nonzero if a mouse is installed. [SM_MOUSEPRESENT]
        /// </summary>
        MousePresent = 19,

        /// <summary>
        ///  The height of the arrow bitmap on a vertical scroll bar, in pixels. [SM_CYVSCROLL]
        /// </summary>
        VerticalScrollArrowHeight = 20,

        /// <summary>
        ///  The width of the arrow bitmap on a horizontal scroll bar, in pixels. [SM_CXHSCROLL]
        /// </summary>
        HorizontalScrollArrowWidth = 21,

        /// <summary>
        ///  Nonzero if debug User.exe is installed. [SM_DEBUG]
        /// </summary>
        Debug = 22,

        /// <summary>
        ///  Nonzero if the left and right buttons are swapped. [SM_SWAPBUTTON]
        /// </summary>
        SwapButton = 23,

        /// <summary>
        ///  [SM_RESERVED1]
        /// </summary>
        Reserved1 = 24,

        /// <summary>
        ///  [SM_RESERVED2]
        /// </summary>
        Reserved2 = 25,

        /// <summary>
        ///  [SM_RESERVED3]
        /// </summary>
        Reserved3 = 26,

        /// <summary>
        ///  [SM_RESERVED4]
        /// </summary>
        Reserved4 = 27,

        /// <summary>
        ///  The minimum width of a window, in pixels. [SM_CXMIN]
        /// </summary>
        MinimumWidth = 28,

        /// <summary>
        ///  The minimum height of a window, in pixels. [SM_CYMIN]
        /// </summary>
        MinimumHeight = 29,

        /// <summary>
        ///  The width of a button in a window caption or title bar, in pixels. [SM_CXSIZE]
        /// </summary>
        SizeWidth = 30,

        /// <summary>
        ///  The height of a button in a window caption or title bar, in pixels. [SM_CYSIZE]
        /// </summary>
        SizeHeight = 31,

        /// <summary>
        ///  The thickness of the horizontal sizing border around the perimeter of a window that can be resized, in pixels. [SM_CXFRAME]
        /// </summary>
        FrameHeight = 32,

        /// <summary>
        ///  The thickness of the horizontal sizing border around the perimeter of a window that can be resized, in pixels. [SM_CXSIZEFRAME]
        /// </summary>
        SizeFrameHeight = FrameHeight,

        /// <summary>
        ///  The thickness of the vertical sizing border around the perimeter of a window that can be resized, in pixels. [SM_CYFRAME]
        /// </summary>
        FrameWidth = 33,

        /// <summary>
        ///  The thickness of the vertical sizing border around the perimeter of a window that can be resized, in pixels. [SM_CYSIZEFRAME]
        /// </summary>
        SizeFrameWidth = FrameWidth,

        /// <summary>
        ///  The minimum tracking width of a window, in pixels. [SM_CXMINTRACK]
        /// </summary>
        MinimumTrackingWidth = 34,

        /// <summary>
        ///  The minimum tracking height of a window, in pixels. [SM_CYMINTRACK]
        /// </summary>
        MinimumTrackingHeight = 35,

        /// <summary>
        ///  The width of the rectangle around the location of a first click in a double-click sequence, in pixels. [SM_CXDOUBLECLK]
        /// </summary>
        DoubleClickWidth = 36,

        /// <summary>
        ///  The height of the rectangle around the location of a first click in a double-click sequence, in pixels. [SM_CYDOUBLECLK]
        /// </summary>
        DoubleClickHeight = 37,

        /// <summary>
        ///  The height of a grid cell for items in large icon view, in pixels. [SM_CXICONSPACING]
        /// </summary>
        IconSpacingHeight = 38,

        /// <summary>
        ///  The width of a grid cell for items in large icon view, in pixels. [SM_CYICONSPACING]
        /// </summary>
        IconSpacingWidth = 39,

        /// <summary>
        ///  Nonzero if drop-down menus are right-aligned with the corresponding menu-bar item. [SM_MENUDROPALIGNMENT]
        /// </summary>
        MenuDropAlignment = 40,

        /// <summary>
        ///  Nonzero if the Microsoft Windows for Pen computing extensions are installed. [SM_PENWINDOWS]
        /// </summary>
        PenWindows = 41,

        /// <summary>
        ///  Nonzero if User32.dll supports DBCS. [SM_DBCSENABLED]
        /// </summary>
        DbcsEnabled = 42,

        /// <summary>
        ///  Number of buttons on the mouse. [SM_CMOUSEBUTTONS]
        /// </summary>
        MouseButtonsCount = 43,

        /// <summary>
        ///  Ignored. [SM_SECURE]
        /// </summary>
        Secure = 44,

        /// <summary>
        ///  The width of a window border, in pixels. [SM_CXEDGE]
        /// </summary>
        EdgeWidth = 45,

        /// <summary>
        ///  The height of a window border, in pixels. [SM_CYEDGE]
        /// </summary>
        EdgeHeight = 46,

        /// <summary>
        ///  The width of a grid cell for a minimized window, in pixels. [SM_CXMINSPACING]
        /// </summary>
        MinimumSpacingWidth = 47,

        /// <summary>
        ///  The height of a grid cell for a minimized window, in pixels. [SM_CYMINSPACING]
        /// </summary>
        MinimumSpacingHeight = 48,

        /// <summary>
        ///  The recommended width of a small icon, in pixels. [SM_CXSMICON]
        /// </summary>
        SmallIconWidth = 49,

        /// <summary>
        ///  The recommended height of a small icon, in pixels. [SM_CYSMICON]
        /// </summary>
        SmallIconHeight = 50,

        /// <summary>
        ///  The height of a small caption, in pixels. [SM_CYSMCAPTION]
        /// </summary>
        SmallCaptionHeight = 51,

        /// <summary>
        ///  The wdith of small caption buttons, in pixels. [SM_CXSMSIZE]
        /// </summary>
        SmallCaptionButtonWidth = 52,

        /// <summary>
        ///  The height of small caption buttons, in pixels. [SM_CYSMSIZE]
        /// </summary>
        SmallCaptionButtonHeight = 53,

        /// <summary>
        ///  The width of menu bar buttons. [SM_CXMENUSIZE]
        /// </summary>
        MenuBarButtonWidth = 54,

        /// <summary>
        ///  The height of menu bar buttons. [SM_CYMENUSIZE]
        /// </summary>
        MenuBarButtonHeight = 55,

        /// <summary>
        ///  Flags on how the system minimizes windows. [SM_ARRANGE]
        /// </summary>
        Arrange = 56,

        /// <summary>
        ///  The width of a minimized window, in pixels. [SM_CXMINIMIZED]
        /// </summary>
        MinimizedWidth = 57,

        /// <summary>
        ///  The height of a minimized window, in pixels. [SM_CYMINIMIZED]
        /// </summary>
        MinimizedHeight = 58,

        /// <summary>
        ///  The default maximum width of a window that has a caption and sizing borders, in pixels. [SM_CXMAXTRACK]
        /// </summary>
        MaximumTrackingWidth = 59,

        /// <summary>
        ///  The default maximum height of a window that has a caption and sizing borders, in pixels. [SM_CYMAXTRACK]
        /// </summary>
        MaximumTrackingHeight = 60,

        /// <summary>
        ///  The default width, in pixels, of a maximized top-level window on the primary display monitor. [SM_CXMAXIMIZED]
        /// </summary>
        MaximizedWidth = 61,

        /// <summary>
        ///  The default height, in pixels, of a maximized top-level window on the primary display monitor. [SM_CYMAXIMIZED]
        /// </summary>
        MaximizedHeight = 62,

        /// <summary>
        ///  The least significant bit is set if a network is present. [SM_NETWORK]
        /// </summary>
        Network = 63,

        /// <summary>
        ///  1 if fail-safe boot, 2 if fail-safe with networking. [SM_CLEANBOOT]
        /// </summary>
        CleanBoot = 67,

        /// <summary>
        ///  Horizontal pixels that the mouse must move before a drag begins. [SM_CXDRAG]
        /// </summary>
        HorizontalDrag = 68,

        /// <summary>
        ///  Vertical pixels that the mouse must move before a drag begins. [SM_CYDRAG]
        /// </summary>
        VerticalDrag = 69,

        /// <summary>
        ///  Nonzero if the user requires an application to present information visually. [SM_SHOWSOUNDS]
        /// </summary>
        ShowSounds = 70,

        /// <summary>
        ///  The width of the default menu check-mark bitmap, in pixels. [SM_CXMENUCHECK]
        /// </summary>
        MenuCheckWidth = 71,

        /// <summary>
        ///  The height of the default menu check-mark bitmap, in pixels. [SM_CYMENUCHECK]
        /// </summary>
        MenuCheckHeight = 72,

        /// <summary>
        ///  Nonzero if the computer has a low-end (slow) processor. [SM_SLOWMACHINE]
        /// </summary>
        SlowMachine = 73,

        /// <summary>
        ///  Nonzero if the system is enabled for Hebrew and Arabic languages. [SM_MIDEASTENABLED]
        /// </summary>
        MideastEnabled = 74,

        /// <summary>
        ///  Nonzero if a mouse wheel is present. [SM_MOUSEWHEELPRESENT]
        /// </summary>
        MouseWheelPresent = 75,

        /// <summary>
        ///  Left of the virtual screen. [SM_XVIRTUALSCREEN]
        /// </summary>
        VirtualScreenLeft = 76,

        /// <summary>
        ///  Top of the virtual screen. [SM_YVIRTUALSCREEN]
        /// </summary>
        VirtualScreenTop = 77,

        /// <summary>
        ///  The width of the virtual screen, in pixels. [SM_CXVIRTUALSCREEN]
        /// </summary>
        VirtualScreenWidth = 78,

        /// <summary>
        ///  The height of the virtual screen, in pixels. [SM_CYVIRTUALSCREEN]
        /// </summary>
        VirtualScreenHeight = 79,

        /// <summary>
        ///  The number of display monitors on a desktop. [SM_CMONITORS]
        /// </summary>
        MonitorsCount = 80,

        /// <summary>
        ///  Nonzero if all the display monitors have the same color format. [SM_SAMEDISPLAYFORMAT]
        /// </summary>
        SameDisplayFormat = 81,

        /// <summary>
        ///  Nonzero if IMM/IME features are enabled. [SM_IMMENABLED]
        /// </summary>
        ImmEnabled = 82,

        /// <summary>
        ///  The width of the left and right edges of the focus rectangle that the DrawFocusRect draws. [SM_CXFOCUSBORDER]
        /// </summary>
        FocusBorderWidth = 83,

        /// <summary>
        ///  The height of the top and bottom edges of the focus rectangle drawn by DrawFocusRect. [SM_CYFOCUSBORDER]
        /// </summary>
        FocusBorderHeight = 84,

        /// <summary>
        ///  Nonzero if the Tablet PC Input Service is started. [SM_TABLETPC]
        /// </summary>
        TabletPC = 86,

        /// <summary>
        ///  Nonzero if the current operating system is Windows Media Center Edition. [SM_MEDIACENTER]
        /// </summary>
        MediaCenter = 87,

        /// <summary>
        ///  Nonzero if the current operating system is Windows Starter Edition. [SM_STARTER]
        /// </summary>
        Starter = 88,

        /// <summary>
        ///  Build number for Windows Server 2003 R2. [SM_SERVERR2]
        /// </summary>
        ServerR2 = 89,

        /// <summary>
        ///  Nonzero if a mouse with a horizontal scroll wheel is installed. [SM_MOUSEHORIZONTALWHEELPRESENT]
        /// </summary>
        MouseHorizontalWheelPresent = 91,

        /// <summary>
        ///  The amount of border padding for captioned windows, in pixels. [SM_CXPADDEDBORDER]
        /// </summary>
        AddedBorderPadding = 92,

        /// <summary>
        ///  Type of digitizers that are installed. Returns <see cref="NaturalInputDevice"/> flags. [SM_DIGITIZER]
        /// </summary>
        Digitizer = 94,

        /// <summary>
        ///  Aggregate maximum of the maximum number of contacts supported by every digitizer in the system. [SM_MAXIMUMTOUCHES]
        /// </summary>
        MaximumTouches = 95
    }
}
