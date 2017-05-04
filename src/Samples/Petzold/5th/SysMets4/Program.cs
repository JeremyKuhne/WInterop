// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Linq;
using System.Runtime.InteropServices;
using WInterop.Gdi;
using WInterop.Gdi.Types;
using WInterop.Modules.Types;
using WInterop.Resources;
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

            SafeModuleHandle module = Marshal.GetHINSTANCE(typeof(Program).Module);
            WindowClass wndclass = new WindowClass
            {
                Style = WindowClassStyle.CS_HREDRAW | WindowClassStyle.CS_VREDRAW,
                WindowProcedure = WindowProcedure,
                Instance = module,
                Icon = ResourceMethods.LoadIcon(IconId.IDI_APPLICATION),
                Cursor = ResourceMethods.LoadCursor(CursorId.IDC_ARROW),
                Background = GdiMethods.GetStockBrush(StockBrush.WHITE_BRUSH),
                ClassName = szAppName
            };

            WindowMethods.RegisterClass(wndclass);

            WindowHandle window = WindowMethods.CreateWindow(
                module,
                szAppName,
                "Get System Metrics No. 4",
                WindowStyle.WS_OVERLAPPEDWINDOW | WindowStyle.WS_VSCROLL | WindowStyle.WS_HSCROLL);

            WindowMethods.ShowWindow(window, ShowWindowCommand.SW_SHOWNORMAL);
            GdiMethods.UpdateWindow(window);

            while (WindowMethods.GetMessage(out MSG message, WindowHandle.Null, 0, 0))
            {
                WindowMethods.TranslateMessage(ref message);
                WindowMethods.DispatchMessage(ref message);
            }
        }

        static int cxChar, cxCaps, cyChar, cxClient, cyClient, iMaxWidth;

        static LRESULT WindowProcedure(WindowHandle window, MessageType message, WPARAM wParam, LPARAM lParam)
        {
            SCROLLINFO si;

            switch (message)
            {
                case MessageType.WM_CREATE:
                    using (DeviceContext dc = GdiMethods.GetDeviceContext(window))
                    {
                        GdiMethods.GetTextMetrics(dc, out TEXTMETRIC tm);
                        cxChar = tm.tmAveCharWidth;
                        cxCaps = ((tm.tmPitchAndFamily & PitchAndFamily.TMPF_FIXED_PITCH) != 0 ? 3 : 2) * cxChar / 2;
                        cyChar = tm.tmHeight + tm.tmExternalLeading;
                    }

                    // Save the width of the three columns
                    iMaxWidth = 40 * cxChar + 22 * cxCaps;

                    return 0;
                case MessageType.WM_SIZE:
                    cxClient = lParam.LowWord;
                    cyClient = lParam.HighWord;

                    // Set vertical scroll bar range and page size
                    si = new SCROLLINFO
                    {
                        fMask = ScrollInfoMask.SIF_RANGE | ScrollInfoMask.SIF_PAGE,
                        nMin = 0,
                        nMax = SysMets.sysmetrics.Count - 1,
                        nPage = (uint)(cyClient / cyChar),
                    };
                    WindowMethods.SetScrollInfo(window, ScrollBar.SB_VERT, ref si, true);

                    // Set horizontal scroll bar range and page size
                    si.nMax = 2 + iMaxWidth / cxChar;
                    si.nPage = (uint)(cxClient / cxChar);
                    WindowMethods.SetScrollInfo(window, ScrollBar.SB_HORZ, ref si, true);

                    return 0;
                case MessageType.WM_VSCROLL:
                    // Get all the vertical scroll bar information
                    si = new SCROLLINFO
                    {
                        fMask = ScrollInfoMask.SIF_ALL
                    };
                    WindowMethods.GetScrollInfo(window, ScrollBar.SB_VERT, ref si);

                    // Save the position for comparison later on
                    int iVertPos = si.nPos;

                    switch ((ScrollBarCommand)wParam.LowWord)
                    {
                        case ScrollBarCommand.SB_TOP:
                            si.nPos = si.nMin;
                            break;
                        case ScrollBarCommand.SB_BOTTOM:
                            si.nPos = si.nMax;
                            break;
                        case ScrollBarCommand.SB_LINEUP:
                            si.nPos -= 1;
                            break;
                        case ScrollBarCommand.SB_LINEDOWN:
                            si.nPos += 1;
                            break;
                        case ScrollBarCommand.SB_PAGEUP:
                            si.nPos -= (int)si.nPage;
                            break;
                        case ScrollBarCommand.SB_PAGEDOWN:
                            si.nPos += (int)si.nPage;
                            break;
                        case ScrollBarCommand.SB_THUMBTRACK:
                            si.nPos = si.nTrackPos;
                            break;
                    }

                    // Set the position and then retrieve it. Due to adjustments
                    // by Windows it may not be the same as the value set.
                    si.fMask = ScrollInfoMask.SIF_POS;
                    WindowMethods.SetScrollInfo(window, ScrollBar.SB_VERT, ref si, true);
                    WindowMethods.GetScrollInfo(window, ScrollBar.SB_VERT, ref si);

                    // If the position has changed, scroll the window and update it
                    if (si.nPos != iVertPos)
                    {
                        WindowMethods.ScrollWindow(window, 0, cyChar * (iVertPos - si.nPos));
                        GdiMethods.UpdateWindow(window);
                    }
                    return 0;
                case MessageType.WM_HSCROLL:
                    // Get all the horizontal scroll bar information
                    si = new SCROLLINFO
                    {
                        fMask = ScrollInfoMask.SIF_ALL
                    };
                    WindowMethods.GetScrollInfo(window, ScrollBar.SB_HORZ, ref si);

                    // Save the position for comparison later on
                    int iHorzPos = si.nPos;
                    switch ((ScrollBarCommand)wParam.LowWord)
                    {
                        case ScrollBarCommand.SB_LINELEFT:
                            si.nPos -= 1;
                            break;
                        case ScrollBarCommand.SB_LINERIGHT:
                            si.nPos += 1;
                            break;
                        case ScrollBarCommand.SB_PAGELEFT:
                            si.nPos -= (int)si.nPage;
                            break;
                        case ScrollBarCommand.SB_PAGERIGHT:
                            si.nPos += (int)si.nPage;
                            break;
                        case ScrollBarCommand.SB_THUMBPOSITION:
                            si.nPos = si.nTrackPos;
                            break;
                    }

                    // Set the position and then retrieve it. Due to adjustments
                    // by Windows it may not be the same as the value set.
                    si.fMask = ScrollInfoMask.SIF_POS;
                    WindowMethods.SetScrollInfo(window, ScrollBar.SB_HORZ, ref si, true);
                    WindowMethods.GetScrollInfo(window, ScrollBar.SB_HORZ, ref si);

                    // If the position has changed, scroll the window
                    if (si.nPos != iHorzPos)
                    {
                        WindowMethods.ScrollWindow(window, cxChar * (iHorzPos - si.nPos), 0);
                    }
                    return 0;
                case MessageType.WM_KEYDOWN:
                    switch ((VirtualKey)wParam)
                    {
                        case VirtualKey.VK_HOME:
                            WindowMethods.SendMessage(window, MessageType.WM_VSCROLL, (uint)ScrollBarCommand.SB_TOP, 0);
                            break;
                        case VirtualKey.VK_END:
                            WindowMethods.SendMessage(window, MessageType.WM_VSCROLL, (uint)ScrollBarCommand.SB_BOTTOM, 0);
                            break;
                        case VirtualKey.VK_PRIOR:
                            WindowMethods.SendMessage(window, MessageType.WM_VSCROLL, (uint)ScrollBarCommand.SB_PAGEUP, 0);
                            break;
                        case VirtualKey.VK_NEXT:
                            WindowMethods.SendMessage(window, MessageType.WM_VSCROLL, (uint)ScrollBarCommand.SB_PAGEDOWN, 0);
                            break;
                        case VirtualKey.VK_UP:
                            WindowMethods.SendMessage(window, MessageType.WM_VSCROLL, (uint)ScrollBarCommand.SB_LINEUP, 0);
                            break;
                        case VirtualKey.VK_DOWN:
                            WindowMethods.SendMessage(window, MessageType.WM_VSCROLL, (uint)ScrollBarCommand.SB_LINEDOWN, 0);
                            break;
                        case VirtualKey.VK_LEFT:
                            WindowMethods.SendMessage(window, MessageType.WM_HSCROLL, (uint)ScrollBarCommand.SB_PAGEUP, 0);
                            break;
                        case VirtualKey.VK_RIGHT:
                            WindowMethods.SendMessage(window, MessageType.WM_HSCROLL, (uint)ScrollBarCommand.SB_PAGEDOWN, 0);
                            break;
                    }
                    return 0;
                case MessageType.WM_PAINT:
                    PAINTSTRUCT ps;
                    using (DeviceContext dc = GdiMethods.BeginPaint(window, out ps))
                    {
                        // Get vertical scroll bar position
                        si = new SCROLLINFO
                        {
                            fMask = ScrollInfoMask.SIF_POS
                        };
                        WindowMethods.GetScrollInfo(window, ScrollBar.SB_VERT, ref si);
                        iVertPos = si.nPos;

                        // Get horizontal scroll bar position
                        WindowMethods.GetScrollInfo(window, ScrollBar.SB_HORZ, ref si);
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

                            GdiMethods.TextOut(dc, x, y, metric.ToString());
                            GdiMethods.TextOut(dc, x + 22 * cxCaps, y, SysMets.sysmetrics[metric]);
                            GdiMethods.SetTextAlignment(dc, TextAlignment.TA_RIGHT | TextAlignment.TA_TOP);
                            GdiMethods.TextOut(dc, x + 22 * cxCaps + 40 * cxChar, y, WindowMethods.GetSystemMetrics(metric).ToString());
                            GdiMethods.SetTextAlignment(dc, TextAlignment.TA_LEFT | TextAlignment.TA_TOP);
                        }
                    }
                    return 0;
                case MessageType.WM_DESTROY:
                    WindowMethods.PostQuitMessage(0);
                    return 0;
            }

            return WindowMethods.DefaultWindowProcedure(window, message, wParam, lParam);
        }
    }
}
