// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace WInterop.Windows
{
    // https://docs.microsoft.com/en-us/windows/desktop/winmsg/extended-window-styles
    [Flags]
    public enum ExtendedWindowStyles : uint
    {
        /// <summary>
        ///  Default styles are standard left-to-right defaults. RTL styles are overrides and ase defined
        ///  below. Includes [WS_EX_LEFT], [WS_EX_LTRREADING], [WS_EX_RIGHTSCROLLBAR].
        /// </summary>
        Default = 0x0,

        /// <summary>
        ///  Window has double border. [WS_EX_DLGMODALFRAME]
        /// </summary>
        DialogModalFrame = 0x00000001,

        /// <summary>
        ///  The child window created with this style does not send the WM_PARENTNOTIFY message to its parent
        ///  window when it is created or destroyed. [WS_EX_NOPARENTNOTIFY]
        /// </summary>
        NoParentNotify = 0x00000004,

        /// <summary>
        ///  The window should be placed above all non-topmost windows and should stay above them, even when
        ///  the window is deactivated. [WS_EX_TOPMOST]
        /// </summary>
        TopMost = 0x00000008,

        /// <summary>
        ///  The window accepts drag-drop files. [WS_EX_ACCEPTFILES]
        /// </summary>
        AcceptFiles = 0x00000010,

        /// <summary>
        ///  The window should not be painted until siblings beneath the window (that were created by the
        ///  same thread) have been painted. [WS_EX_TRANSPARENT]
        /// </summary>
        Transparent = 0x00000020,

        /// <summary>
        ///  The window is a MDI child window. [WS_EX_MDICHILD]
        /// </summary>
        MdiChild = 0x00000040,

        /// <summary>
        ///  The window is intended to be used as a floating toolbar. [WS_EX_TOOLWINDOW]
        /// </summary>
        ToolWindow = 0x00000080,

        /// <summary>
        ///  The window has a border with a raised edge. [WS_EX_WINDOWEDGE]
        /// </summary>
        WindowEdge = 0x00000100,

        /// <summary>
        ///  The window has a border with a sunken edge. [WS_EX_CLIENTEDGE]
        /// </summary>
        ClientEdge = 0x00000200,

        /// <summary>
        ///  The title bar of the window includes a question mark. [WS_EX_CONTEXTHELP]
        /// </summary>
        ContextHelp = 0x00000400,

        /// <summary>
        ///  The window has generic "right-aligned" properties. [WS_EX_RIGHT]
        /// </summary>
        Right = 0x00001000,

        /// <summary>
        ///  If the shell language supports reading-order alignment, the window text is displayed using
        ///  right-to-left reading-order properties. [WS_EX_RTLREADING]
        /// </summary>
        RtlReading = 0x00002000,

        /// <summary>
        ///  If the shell language supports reading-order alignment, the vertical scroll bar (if
        ///  present) will be on the left instead of the right. [WS_EX_LEFTSCROLLBAR]
        /// </summary>
        LeftScrollBar = 0x00004000,

        /// <summary>
        ///  The window itself contains child windows that should take part in dialog box navigation.
        ///  [WS_EX_CONTROLPARENT]
        /// </summary>
        ControlParent = 0x00010000,

        /// <summary>
        ///  The window has a three-dimensional border style intended to be used for items that do not
        ///  accept user input. [WS_EX_STATICEDGE]
        /// </summary>
        StaticEdge = 0x00020000,

        /// <summary>
        ///  Forces a top-level window onto the taskbar when the window is visible. [WS_EX_APPWINDOW]
        /// </summary>
        AppWindow = 0x00040000,

        /// <summary>
        ///  [WS_EX_OVERLAPPEDWINDOW]
        /// </summary>
        OverlappedWindow = WindowEdge | ClientEdge,

        /// <summary>
        ///  [WS_EX_PALETTEWINDOW]
        /// </summary>
        PaletteWindow = WindowEdge | ToolWindow | TopMost,

        /// <summary>
        ///  The window is a layered window. [WS_EX_LAYERED]
        /// </summary>
        Layered = 0x00080000,

        /// <summary>
        ///  The window does not pass its window layout to its child windows. [WS_EX_NOINHERITLAYOUT]
        /// </summary>
        NoInheritLayout = 0x00100000,

        /// <summary>
        ///  The window does not render to a redirection surface. [WS_EX_NOREDIRECTIONBITMAP]
        /// </summary>
        NoRedirectionBitmap = 0x00200000,

        /// <summary>
        ///  [WS_EX_LAYOUTRTL]
        /// </summary>
        LayoutRtl = 0x00400000,

        /// <summary>
        ///  Paints all descendants of a window in bottom-to-top painting order using double-buffering.
        ///  [WS_EX_COMPOSITED]
        /// </summary>
        Composited = 0x02000000,

        /// <summary>
        ///  A top-level window created with this style does not become the foreground window when the
        ///  user clicks it. [WS_EX_NOACTIVATE]
        /// </summary>
        NoActivate = 0x08000000
    }
}