// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Windows
{
    // https://msdn.microsoft.com/en-us/library/windows/desktop/ms724385.aspx
    public enum SystemMetric : int
    {
        /// <summary>
        /// The width of the screen of the primary display monitor, in pixels. [SM_CXSCREEN]
        /// </summary>
        /// <remarks>
        /// This is the same value obtained by calling GetDeviceCaps as follows: GetDeviceCaps(hdcPrimaryMonitor, HORZRES).
        /// </remarks>
        ScreenWidth = 0,

        /// <summary>
        /// The height of the screen of the primary display monitor, in pixels. [SM_CYSCREEN]
        /// </summary>
        /// <remarks>
        ///  This is the same value obtained by calling GetDeviceCaps as follows: GetDeviceCaps(hdcPrimaryMonitor, VERTRES).
        /// </remarks>
        ScreenHeight = 1,

        /// <summary>
        /// Vertical scroll bar width in pixels. [SM_CXVSCROLL]
        /// </summary>
        VerticalScrollWidth = 2,

        /// <summary>
        /// Horizontal scroll height in pixels. [SM_CYHSCROLL]
        /// </summary>
        HorizontalScrollHeight = 3,

        /// <summary>
        /// Caption area height in pixels. [SM_CYCAPTION]
        /// </summary>
        CaptionAreaHeight = 4,

        /// <summary>
        /// Border width in pixels. [SM_CXBORDER]
        /// </summary>
        BorderWidth = 5,

        /// <summary>
        /// Border height in pixels. [SM_CYBORDER]
        /// </summary>
        BorderHeight = 6,

        /// <summary>
        /// [SM_CXDLGFRAME]
        /// </summary>
        DialogFrameHeight = 7,

        /// <summary>
        /// Horizontal border height for an unsizable window. [SM_CXFIXEDFRAME]
        /// </summary>
        FixedFrameHeight = DialogFrameHeight,

        /// <summary>
        /// [SM_CYDLGFRAME]
        /// </summary>
        CYDLGFRAME = 8,

        /// <summary>
        /// [SM_]
        /// </summary>
        CYFIXEDFRAME = CYDLGFRAME,

        /// <summary>
        /// [SM_]
        /// </summary>
        CYVTHUMB = 9,

        /// <summary>
        /// [SM_]
        /// </summary>
        CXHTHUMB = 10,

        /// <summary>
        /// [SM_]
        /// </summary>
        CXICON = 11,

        /// <summary>
        /// [SM_]
        /// </summary>
        CYICON = 12,

        /// <summary>
        /// [SM_]
        /// </summary>
        CXCURSOR = 13,

        /// <summary>
        /// [SM_]
        /// </summary>
        CYCURSOR = 14,

        /// <summary>
        /// [SM_]
        /// </summary>
        CYMENU = 15,

        /// <summary>
        /// [SM_]
        /// </summary>
        CXFULLSCREEN = 16,

        /// <summary>
        /// [SM_]
        /// </summary>
        CYFULLSCREEN = 17,

        /// <summary>
        /// [SM_]
        /// </summary>
        CYKANJIWINDOW = 18,

        /// <summary>
        /// [SM_]
        /// </summary>
        MOUSEPRESENT = 19,

        /// <summary>
        /// [SM_]
        /// </summary>
        CYVSCROLL = 20,

        /// <summary>
        /// [SM_]
        /// </summary>
        CXHSCROLL = 21,

        /// <summary>
        /// [SM_]
        /// </summary>
        DEBUG = 22,

        /// <summary>
        /// [SM_]
        /// </summary>
        SWAPBUTTON = 23,

        /// <summary>
        /// [SM_]
        /// </summary>
        RESERVED1 = 24,

        /// <summary>
        /// [SM_]
        /// </summary>
        RESERVED2 = 25,

        /// <summary>
        /// [SM_]
        /// </summary>
        RESERVED3 = 26,

        /// <summary>
        /// [SM_]
        /// </summary>
        RESERVED4 = 27,

        /// <summary>
        /// [SM_]
        /// </summary>
        CXMIN = 28,

        /// <summary>
        /// [SM_]
        /// </summary>
        CYMIN = 29,

        /// <summary>
        /// [SM_]
        /// </summary>
        CXSIZE = 30,

        /// <summary>
        /// [SM_]
        /// </summary>
        CYSIZE = 31,

        /// <summary>
        /// [SM_]
        /// </summary>
        CXFRAME = 32,

        /// <summary>
        /// [SM_]
        /// </summary>
        CXSIZEFRAME = CXFRAME,

        /// <summary>
        /// [SM_]
        /// </summary>
        CYFRAME = 33,

        /// <summary>
        /// [SM_]
        /// </summary>
        CYSIZEFRAME = CYFRAME,

        /// <summary>
        /// [SM_]
        /// </summary>
        CXMINTRACK = 34,

        /// <summary>
        /// [SM_]
        /// </summary>
        CYMINTRACK = 35,

        /// <summary>
        /// [SM_]
        /// </summary>
        CXDOUBLECLK = 36,

        /// <summary>
        /// [SM_]
        /// </summary>
        CYDOUBLECLK = 37,

        /// <summary>
        /// [SM_]
        /// </summary>
        CXICONSPACING = 38,

        /// <summary>
        /// [SM_]
        /// </summary>
        CYICONSPACING = 39,

        /// <summary>
        /// [SM_]
        /// </summary>
        MENUDROPALIGNMENT = 40,

        /// <summary>
        /// [SM_]
        /// </summary>
        PENWINDOWS = 41,

        /// <summary>
        /// [SM_]
        /// </summary>
        DBCSENABLED = 42,

        /// <summary>
        /// [SM_]
        /// </summary>
        CMOUSEBUTTONS = 43,

        /// <summary>
        /// [SM_]
        /// </summary>
        SECURE = 44,

        /// <summary>
        /// [SM_]
        /// </summary>
        CXEDGE = 45,

        /// <summary>
        /// [SM_]
        /// </summary>
        CYEDGE = 46,

        /// <summary>
        /// [SM_]
        /// </summary>
        CXMINSPACING = 47,

        /// <summary>
        /// [SM_]
        /// </summary>
        CYMINSPACING = 48,

        /// <summary>
        /// [SM_]
        /// </summary>
        CXSMICON = 49,

        /// <summary>
        /// [SM_]
        /// </summary>
        CYSMICON = 50,

        /// <summary>
        /// [SM_]
        /// </summary>
        CYSMCAPTION = 51,

        /// <summary>
        /// [SM_]
        /// </summary>
        CXSMSIZE = 52,

        /// <summary>
        /// [SM_]
        /// </summary>
        CYSMSIZE = 53,

        /// <summary>
        /// [SM_]
        /// </summary>
        CXMENUSIZE = 54,

        /// <summary>
        /// [SM_]
        /// </summary>
        CYMENUSIZE = 55,

        /// <summary>
        /// [SM_]
        /// </summary>
        ARRANGE = 56,

        /// <summary>
        /// [SM_]
        /// </summary>
        CXMINIMIZED = 57,

        /// <summary>
        /// [SM_]
        /// </summary>
        CYMINIMIZED = 58,

        /// <summary>
        /// [SM_]
        /// </summary>
        CXMAXTRACK = 59,

        /// <summary>
        /// [SM_]
        /// </summary>
        CYMAXTRACK = 60,

        /// <summary>
        /// [SM_]
        /// </summary>
        CXMAXIMIZED = 61,

        /// <summary>
        /// [SM_]
        /// </summary>
        CYMAXIMIZED = 62,

        /// <summary>
        /// [SM_]
        /// </summary>
        NETWORK = 63,

        /// <summary>
        /// [SM_]
        /// </summary>
        CLEANBOOT = 67,

        /// <summary>
        /// [SM_]
        /// </summary>
        CXDRAG = 68,

        /// <summary>
        /// [SM_]
        /// </summary>
        CYDRAG = 69,

        /// <summary>
        /// [SM_]
        /// </summary>
        SHOWSOUNDS = 70,

        /// <summary>
        /// [SM_]
        /// </summary>
        CXMENUCHECK = 71,

        /// <summary>
        /// [SM_]
        /// </summary>
        CYMENUCHECK = 72,

        /// <summary>
        /// [SM_]
        /// </summary>
        SLOWMACHINE = 73,

        /// <summary>
        /// [SM_]
        /// </summary>
        MIDEASTENABLED = 74,

        /// <summary>
        /// [SM_]
        /// </summary>
        MOUSEWHEELPRESENT = 75,

        /// <summary>
        /// [SM_]
        /// </summary>
        XVIRTUALSCREEN = 76,

        /// <summary>
        /// [SM_]
        /// </summary>
        YVIRTUALSCREEN = 77,

        /// <summary>
        /// [SM_]
        /// </summary>
        CXVIRTUALSCREEN = 78,

        /// <summary>
        /// [SM_]
        /// </summary>
        CYVIRTUALSCREEN = 79,

        /// <summary>
        /// [SM_]
        /// </summary>
        CMONITORS = 80,

        /// <summary>
        /// [SM_]
        /// </summary>
        SAMEDISPLAYFORMAT = 81,

        /// <summary>
        /// [SM_]
        /// </summary>
        IMMENABLED = 82,

        /// <summary>
        /// [SM_]
        /// </summary>
        CXFOCUSBORDER = 83,

        /// <summary>
        /// [SM_]
        /// </summary>
        CYFOCUSBORDER = 84,

        /// <summary>
        /// [SM_]
        /// </summary>
        TABLETPC = 86,

        /// <summary>
        /// [SM_]
        /// </summary>
        MEDIACENTER = 87,

        /// <summary>
        /// [SM_]
        /// </summary>
        STARTER = 88,

        /// <summary>
        /// [SM_]
        /// </summary>
        SERVERR2 = 89,

        /// <summary>
        /// [SM_]
        /// </summary>
        MOUSEHORIZONTALWHEELPRESENT = 91,

        /// <summary>
        /// [SM_]
        /// </summary>
        CXPADDEDBORDER = 92,

        /// <summary>
        /// [SM_]
        /// </summary>
        DIGITIZER = 94,

        /// <summary>
        /// [SM_]
        /// </summary>
        MAXIMUMTOUCHES = 95
    }
}
