// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.


using System.Collections.Generic;
using WInterop.Windows.Types;

namespace SysMets3
{
    public static class SysMets
    {
        public static Dictionary<SystemMetric, string> sysmetrics = new Dictionary<SystemMetric, string>
        {
            { SystemMetric.SM_CXSCREEN, "Screen width in pixels" },
            { SystemMetric.SM_CYSCREEN, "Screen height in pixels" },
            { SystemMetric.SM_CXVSCROLL, "Vertical scroll width" },
            { SystemMetric.SM_CYHSCROLL, "Horizontal scroll height" },
            { SystemMetric.SM_CYCAPTION, "Caption bar height" },
            { SystemMetric.SM_CXBORDER, "Window border width" },
            { SystemMetric.SM_CYBORDER, "Window border height" },
            { SystemMetric.SM_CXFIXEDFRAME, "Dialog window frame width" },
            { SystemMetric.SM_CYFIXEDFRAME, "Dialog window frame height" },
            { SystemMetric.SM_CYVTHUMB, "Vertical scroll thumb height" },
            { SystemMetric.SM_CXHTHUMB, "Horizontal scroll thumb width" },
            { SystemMetric.SM_CXICON, "Icon width" },
            { SystemMetric.SM_CYICON, "Icon height" },
            { SystemMetric.SM_CXCURSOR, "Cursor width" },
            { SystemMetric.SM_CYCURSOR, "Cursor height" },
            { SystemMetric.SM_CYMENU, "Menu bar height" },
            { SystemMetric.SM_CXFULLSCREEN, "Full screen client area width" },
            { SystemMetric.SM_CYFULLSCREEN, "Full screen client area height" },
            { SystemMetric.SM_CYKANJIWINDOW, "Kanji window height" },
            { SystemMetric.SM_MOUSEPRESENT, "Mouse present flag" },
            { SystemMetric.SM_CYVSCROLL, "Vertical scroll arrow height" },
            { SystemMetric.SM_CXHSCROLL, "Horizontal scroll arrow width" },
            { SystemMetric.SM_DEBUG, "Debug version flag" },
            { SystemMetric.SM_SWAPBUTTON, "Mouse buttons swapped flag" },
            { SystemMetric.SM_CXMIN, "Minimum window width" },
            { SystemMetric.SM_CYMIN, "Minimum window height" },
            { SystemMetric.SM_CXSIZE, "Min/Max/Close button width" },
            { SystemMetric.SM_CYSIZE, "Min/Max/Close button height" },
            { SystemMetric.SM_CXSIZEFRAME, "Window sizing frame width" },
            { SystemMetric.SM_CYSIZEFRAME, "Window sizing frame height" },
            { SystemMetric.SM_CXMINTRACK, "Minimum window tracking width" },
            { SystemMetric.SM_CYMINTRACK, "Minimum window tracking height" },
            { SystemMetric.SM_CXDOUBLECLK, "Double click x tolerance" },
            { SystemMetric.SM_CYDOUBLECLK, "Double click y tolerance" },
            { SystemMetric.SM_CXICONSPACING, "Horizontal icon spacing" },
            { SystemMetric.SM_CYICONSPACING, "Vertical icon spacing" },
            { SystemMetric.SM_MENUDROPALIGNMENT, "Left or right menu drop" },
            { SystemMetric.SM_PENWINDOWS, "Pen extensions installed" },
            { SystemMetric.SM_DBCSENABLED, "Double-Byte Char Set enabled" },
            { SystemMetric.SM_CMOUSEBUTTONS, "Number of mouse buttons" },
            { SystemMetric.SM_SECURE, "Security present flag" },
            { SystemMetric.SM_CXEDGE, "3-D border width" },
            { SystemMetric.SM_CYEDGE, "3-D border height" },
            { SystemMetric.SM_CXMINSPACING, "Minimized window spacing width" },
            { SystemMetric.SM_CYMINSPACING, "Minimized window spacing height" },
            { SystemMetric.SM_CXSMICON, "Small icon width" },
            { SystemMetric.SM_CYSMICON, "Small icon height" },
            { SystemMetric.SM_CYSMCAPTION, "Small caption height" },
            { SystemMetric.SM_CXSMSIZE, "Small caption button width" },
            { SystemMetric.SM_CYSMSIZE, "Small caption button height" },
            { SystemMetric.SM_CXMENUSIZE, "Menu bar button width" },
            { SystemMetric.SM_CYMENUSIZE, "Menu bar button height" },
            { SystemMetric.SM_ARRANGE, "How minimized windows arranged" },
            { SystemMetric.SM_CXMINIMIZED, "Minimized window width" },
            { SystemMetric.SM_CYMINIMIZED, "Minimized window height" },
            { SystemMetric.SM_CXMAXTRACK, "Maximum draggable width" },
            { SystemMetric.SM_CYMAXTRACK, "Maximum draggable height" },
            { SystemMetric.SM_CXMAXIMIZED, "Width of maximized window" },
            { SystemMetric.SM_CYMAXIMIZED, "Height of maximized window" },
            { SystemMetric.SM_NETWORK, "Network present flag" },
            { SystemMetric.SM_CLEANBOOT, "How system was booted" },
            { SystemMetric.SM_CXDRAG, "Avoid drag x tolerance" },
            { SystemMetric.SM_CYDRAG, "Avoid drag y tolerance" },
            { SystemMetric.SM_SHOWSOUNDS, "Present sounds visually" },
            { SystemMetric.SM_CXMENUCHECK, "Menu check-mark width" },
            { SystemMetric.SM_CYMENUCHECK, "Menu check-mark height" },
            { SystemMetric.SM_SLOWMACHINE, "Slow processor flag" },
            { SystemMetric.SM_MIDEASTENABLED, "Hebrew and Arabic enabled flag" },
            { SystemMetric.SM_MOUSEWHEELPRESENT, "Mouse wheel present flag" },
            { SystemMetric.SM_XVIRTUALSCREEN, "Virtual screen x origin" },
            { SystemMetric.SM_YVIRTUALSCREEN, "Virtual screen y origin" },
            { SystemMetric.SM_CXVIRTUALSCREEN, "Virtual screen width" },
            { SystemMetric.SM_CYVIRTUALSCREEN, "Virtual screen height" },
            { SystemMetric.SM_CMONITORS, "Number of monitors" },
            { SystemMetric.SM_SAMEDISPLAYFORMAT, "Same color format flag" }
        };
    }
}
