// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Linq;
using WInterop.Gdi;
using WInterop.Gdi.Types;
using WInterop.Modules.Types;
using WInterop.Resources.Types;
using WInterop.Windows;
using WInterop.Windows.Types;

namespace SysMets4
{
    /// <summary>
    /// Sample from Programming Windows, 5th Edition.
    /// Original (c) Charles Petzold, 1998
    /// Figure 6-2, Pages 224-231.
    /// </summary>
    static class Program
    {
        [STAThread]
        static void Main()
        {
            const string szAppName = "SysMets4";

            ModuleInstance module = ModuleInstance.GetModuleForType(typeof(Program));
            WindowClass wndclass = new WindowClass
            {
                Style = ClassStyle.HorizontalRedraw | ClassStyle.VerticalRedraw,
                WindowProcedure = WindowProcedure,
                Instance = module,
                Icon = IconId.Application,
                Cursor = CursorId.Arrow,
                Background = StockBrush.White,
                ClassName = szAppName
            };

            Windows.RegisterClass(ref wndclass);

            WindowHandle window = Windows.CreateWindow(
                module,
                szAppName,
                "Get System Metrics No. 4",
                WindowStyles.OverlappedWindow | WindowStyles.VerticalScroll | WindowStyles.HorizontalScroll);

            window.ShowWindow(ShowWindow.Normal);
            window.UpdateWindow();

            while (Windows.GetMessage(out MSG message))
            {
                Windows.TranslateMessage(ref message);
                Windows.DispatchMessage(ref message);
            }
        }

        static int cxChar, cxCaps, cyChar, cxClient, cyClient, iMaxWidth;

        static LRESULT WindowProcedure(WindowHandle window, WindowMessage message, WPARAM wParam, LPARAM lParam)
        {
            SCROLLINFO si;

            switch (message)
            {
                case WindowMessage.Create:
                    using (DeviceContext dc = window.GetDeviceContext())
                    {
                        dc.GetTextMetrics(out TEXTMETRIC tm);
                        cxChar = tm.tmAveCharWidth;
                        cxCaps = ((tm.tmPitchAndFamily.PitchTypes & FontPitchTypes.VariablePitch) != 0 ? 3 : 2) * cxChar / 2;
                        cyChar = tm.tmHeight + tm.tmExternalLeading;
                    }

                    // Save the width of the three columns
                    iMaxWidth = 40 * cxChar + 22 * cxCaps;

                    return 0;
                case WindowMessage.Size:
                    cxClient = lParam.LowWord;
                    cyClient = lParam.HighWord;

                    // Set vertical scroll bar range and page size
                    si = new SCROLLINFO
                    {
                        fMask = ScrollInfoMask.Range | ScrollInfoMask.Page,
                        nMin = 0,
                        nMax = SysMets.sysmetrics.Count - 1,
                        nPage = (uint)(cyClient / cyChar),
                    };
                    window.SetScrollInfo(ScrollBar.Vertical, ref si, true);

                    // Set horizontal scroll bar range and page size
                    si.nMax = 2 + iMaxWidth / cxChar;
                    si.nPage = (uint)(cxClient / cxChar);
                    window.SetScrollInfo(ScrollBar.Horizontal, ref si, true);

                    return 0;
                case WindowMessage.VerticalScroll:
                    // Get all the vertical scroll bar information
                    si = new SCROLLINFO
                    {
                        fMask = ScrollInfoMask.All
                    };
                    window.GetScrollInfo(ScrollBar.Vertical, ref si);

                    // Save the position for comparison later on
                    int iVertPos = si.nPos;

                    switch ((ScrollCommand)wParam.LowWord)
                    {
                        case ScrollCommand.Top:
                            si.nPos = si.nMin;
                            break;
                        case ScrollCommand.Bottom:
                            si.nPos = si.nMax;
                            break;
                        case ScrollCommand.LineUp:
                            si.nPos -= 1;
                            break;
                        case ScrollCommand.LineDown:
                            si.nPos += 1;
                            break;
                        case ScrollCommand.PageUp:
                            si.nPos -= (int)si.nPage;
                            break;
                        case ScrollCommand.PageDown:
                            si.nPos += (int)si.nPage;
                            break;
                        case ScrollCommand.ThumbTrack:
                            si.nPos = si.nTrackPos;
                            break;
                    }

                    // Set the position and then retrieve it. Due to adjustments
                    // by Windows it may not be the same as the value set.
                    si.fMask = ScrollInfoMask.Position;
                    window.SetScrollInfo(ScrollBar.Vertical, ref si, true);
                    window.GetScrollInfo(ScrollBar.Vertical, ref si);

                    // If the position has changed, scroll the window and update it
                    if (si.nPos != iVertPos)
                    {
                        window.ScrollWindow(0, cyChar * (iVertPos - si.nPos));
                        window.UpdateWindow();
                    }
                    return 0;
                case WindowMessage.HorizontalScroll:
                    // Get all the horizontal scroll bar information
                    si = new SCROLLINFO
                    {
                        fMask = ScrollInfoMask.All
                    };
                    window.GetScrollInfo(ScrollBar.Horizontal, ref si);

                    // Save the position for comparison later on
                    int iHorzPos = si.nPos;
                    switch ((ScrollCommand)wParam.LowWord)
                    {
                        case ScrollCommand.LineLeft:
                            si.nPos -= 1;
                            break;
                        case ScrollCommand.LineRight:
                            si.nPos += 1;
                            break;
                        case ScrollCommand.PageLeft:
                            si.nPos -= (int)si.nPage;
                            break;
                        case ScrollCommand.PageRight:
                            si.nPos += (int)si.nPage;
                            break;
                        case ScrollCommand.ThumbPosition:
                            si.nPos = si.nTrackPos;
                            break;
                    }

                    // Set the position and then retrieve it. Due to adjustments
                    // by Windows it may not be the same as the value set.
                    si.fMask = ScrollInfoMask.Position;
                    window.SetScrollInfo(ScrollBar.Horizontal, ref si, true);
                    window.GetScrollInfo(ScrollBar.Horizontal, ref si);

                    // If the position has changed, scroll the window
                    if (si.nPos != iHorzPos)
                    {
                        window.ScrollWindow(cxChar * (iHorzPos - si.nPos), 0);
                    }
                    return 0;
                case WindowMessage.KeyDown:
                    switch ((VirtualKey)wParam)
                    {
                        case VirtualKey.Home:
                            window.SendMessage(WindowMessage.VerticalScroll, (uint)ScrollCommand.Top, 0);
                            break;
                        case VirtualKey.End:
                            window.SendMessage(WindowMessage.VerticalScroll, (uint)ScrollCommand.Bottom, 0);
                            break;
                        case VirtualKey.Prior:
                            window.SendMessage(WindowMessage.VerticalScroll, (uint)ScrollCommand.PageUp, 0);
                            break;
                        case VirtualKey.Next:
                            window.SendMessage(WindowMessage.VerticalScroll, (uint)ScrollCommand.PageDown, 0);
                            break;
                        case VirtualKey.Up:
                            window.SendMessage(WindowMessage.VerticalScroll, (uint)ScrollCommand.LineUp, 0);
                            break;
                        case VirtualKey.Down:
                            window.SendMessage(WindowMessage.VerticalScroll, (uint)ScrollCommand.LineDown, 0);
                            break;
                        case VirtualKey.Left:
                            window.SendMessage(WindowMessage.HorizontalScroll, (uint)ScrollCommand.PageUp, 0);
                            break;
                        case VirtualKey.Right:
                            window.SendMessage(WindowMessage.HorizontalScroll, (uint)ScrollCommand.PageDown, 0);
                            break;
                    }
                    return 0;
                case WindowMessage.Paint:
                    PAINTSTRUCT ps;
                    using (DeviceContext dc = window.BeginPaint(out ps))
                    {
                        // Get vertical scroll bar position
                        si = new SCROLLINFO
                        {
                            fMask = ScrollInfoMask.Position
                        };
                        window.GetScrollInfo(ScrollBar.Vertical, ref si);
                        iVertPos = si.nPos;

                        // Get horizontal scroll bar position
                        window.GetScrollInfo(ScrollBar.Horizontal, ref si);
                        iHorzPos = si.nPos;

                        // Find painting limits
                        int iPaintBeg = Math.Max(0, iVertPos + ps.rcPaint.top / cyChar);
                        int iPaintEnd = Math.Min(SysMets.sysmetrics.Count - 1, iVertPos + ps.rcPaint.bottom / cyChar);

                        var keys = SysMets.sysmetrics.Keys.ToArray();
                        for (int i = iPaintBeg; i <= iPaintEnd; i++)
                        {
                            var metric = keys[i];
                            int x = cxChar * (1 - iHorzPos);
                            int y = cyChar * (i - iVertPos);

                            dc.TextOut(x, y, metric.ToString());
                            dc.TextOut(x + 22 * cxCaps, y, SysMets.sysmetrics[metric]);
                            dc.SetTextAlignment(new TextAlignment(TextAlignment.Horizontal.Right, TextAlignment.Vertical.Top));
                            dc.TextOut(x + 22 * cxCaps + 40 * cxChar, y, WindowMethods.GetSystemMetrics(metric).ToString());
                            dc.SetTextAlignment(new TextAlignment(TextAlignment.Horizontal.Left, TextAlignment.Vertical.Top));
                        }
                    }
                    return 0;
                case WindowMessage.Destroy:
                    Windows.PostQuitMessage(0);
                    return 0;
            }

            return Windows.DefaultWindowProcedure(window, message, wParam, lParam);
        }
    }
}
