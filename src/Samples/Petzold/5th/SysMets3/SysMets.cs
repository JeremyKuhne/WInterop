// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.


using System.Collections.Generic;
using WInterop.Windows;

namespace SysMets3
{
    public static class SysMets
    {
        public static Dictionary<SystemMetric, string> sysmetrics = new Dictionary<SystemMetric, string>
        {
            { SystemMetric.CXSCREEN, "Screen width in pixels" },
            { SystemMetric.CYSCREEN, "Screen height in pixels" },
            { SystemMetric.CXVSCROLL, "Vertical scroll width" },
            { SystemMetric.CYHSCROLL, "Horizontal scroll height" },
            { SystemMetric.CYCAPTION, "Caption bar height" },
            { SystemMetric.CXBORDER, "Window border width" },
            { SystemMetric.CYBORDER, "Window border height" },
            { SystemMetric.CXFIXEDFRAME, "Dialog window frame width" },
            { SystemMetric.CYFIXEDFRAME, "Dialog window frame height" },
            { SystemMetric.CYVTHUMB, "Vertical scroll thumb height" },
            { SystemMetric.CXHTHUMB, "Horizontal scroll thumb width" },
            { SystemMetric.CXICON, "Icon width" },
            { SystemMetric.CYICON, "Icon height" },
            { SystemMetric.CXCURSOR, "Cursor width" },
            { SystemMetric.CYCURSOR, "Cursor height" },
            { SystemMetric.CYMENU, "Menu bar height" },
            { SystemMetric.CXFULLSCREEN, "Full screen client area width" },
            { SystemMetric.CYFULLSCREEN, "Full screen client area height" },
            { SystemMetric.CYKANJIWINDOW, "Kanji window height" },
            { SystemMetric.MOUSEPRESENT, "Mouse present flag" },
            { SystemMetric.CYVSCROLL, "Vertical scroll arrow height" },
            { SystemMetric.CXHSCROLL, "Horizontal scroll arrow width" },
            { SystemMetric.DEBUG, "Debug version flag" },
            { SystemMetric.SWAPBUTTON, "Mouse buttons swapped flag" },
            { SystemMetric.CXMIN, "Minimum window width" },
            { SystemMetric.CYMIN, "Minimum window height" },
            { SystemMetric.CXSIZE, "Min/Max/Close button width" },
            { SystemMetric.CYSIZE, "Min/Max/Close button height" },
            { SystemMetric.CXSIZEFRAME, "Window sizing frame width" },
            { SystemMetric.CYSIZEFRAME, "Window sizing frame height" },
            { SystemMetric.CXMINTRACK, "Minimum window tracking width" },
            { SystemMetric.CYMINTRACK, "Minimum window tracking height" },
            { SystemMetric.CXDOUBLECLK, "Double click x tolerance" },
            { SystemMetric.CYDOUBLECLK, "Double click y tolerance" },
            { SystemMetric.CXICONSPACING, "Horizontal icon spacing" },
            { SystemMetric.CYICONSPACING, "Vertical icon spacing" },
            { SystemMetric.MENUDROPALIGNMENT, "Left or right menu drop" },
            { SystemMetric.PENWINDOWS, "Pen extensions installed" },
            { SystemMetric.DBCSENABLED, "Double-Byte Char Set enabled" },
            { SystemMetric.CMOUSEBUTTONS, "Number of mouse buttons" },
            { SystemMetric.SECURE, "Security present flag" },
            { SystemMetric.CXEDGE, "3-D border width" },
            { SystemMetric.CYEDGE, "3-D border height" },
            { SystemMetric.CXMINSPACING, "Minimized window spacing width" },
            { SystemMetric.CYMINSPACING, "Minimized window spacing height" },
            { SystemMetric.CXSMICON, "Small icon width" },
            { SystemMetric.CYSMICON, "Small icon height" },
            { SystemMetric.CYSMCAPTION, "Small caption height" },
            { SystemMetric.CXSMSIZE, "Small caption button width" },
            { SystemMetric.CYSMSIZE, "Small caption button height" },
            { SystemMetric.CXMENUSIZE, "Menu bar button width" },
            { SystemMetric.CYMENUSIZE, "Menu bar button height" },
            { SystemMetric.ARRANGE, "How minimized windows arranged" },
            { SystemMetric.CXMINIMIZED, "Minimized window width" },
            { SystemMetric.CYMINIMIZED, "Minimized window height" },
            { SystemMetric.CXMAXTRACK, "Maximum draggable width" },
            { SystemMetric.CYMAXTRACK, "Maximum draggable height" },
            { SystemMetric.CXMAXIMIZED, "Width of maximized window" },
            { SystemMetric.CYMAXIMIZED, "Height of maximized window" },
            { SystemMetric.NETWORK, "Network present flag" },
            { SystemMetric.CLEANBOOT, "How system was booted" },
            { SystemMetric.CXDRAG, "Avoid drag x tolerance" },
            { SystemMetric.CYDRAG, "Avoid drag y tolerance" },
            { SystemMetric.SHOWSOUNDS, "Present sounds visually" },
            { SystemMetric.CXMENUCHECK, "Menu check-mark width" },
            { SystemMetric.CYMENUCHECK, "Menu check-mark height" },
            { SystemMetric.SLOWMACHINE, "Slow processor flag" },
            { SystemMetric.MIDEASTENABLED, "Hebrew and Arabic enabled flag" },
            { SystemMetric.MOUSEWHEELPRESENT, "Mouse wheel present flag" },
            { SystemMetric.XVIRTUALSCREEN, "Virtual screen x origin" },
            { SystemMetric.YVIRTUALSCREEN, "Virtual screen y origin" },
            { SystemMetric.CXVIRTUALSCREEN, "Virtual screen width" },
            { SystemMetric.CYVIRTUALSCREEN, "Virtual screen height" },
            { SystemMetric.CMONITORS, "Number of monitors" },
            { SystemMetric.SAMEDISPLAYFORMAT, "Same color format flag" }
        };
    }
}
