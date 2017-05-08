// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Linq;
using System.Runtime.InteropServices;
using WInterop.Extensions.WindowExtensions;
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
    /// Figure 7-12, Pages 318-325.
    /// </summary>
    static class Program
    {
        [STAThread]
        static void Main()
        {
            const string szAppName = "SysMets";

            SafeModuleHandle module = Marshal.GetHINSTANCE(typeof(Program).Module);
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

            Windows.RegisterClass(wndclass);

            WindowHandle window = Windows.CreateWindow(
                module,
                szAppName,
                "Get System Metrics",
                WindowStyle.OverlappedWindow | WindowStyle.VerticalScroll | WindowStyle.HorizontalScroll);

            window.ShowWindow(ShowWindow.Normal);
            window.UpdateWindow();

            while (Windows.GetMessage(out MSG message))
            {
                Windows.TranslateMessage(ref message);
                Windows.DispatchMessage(ref message);
            }
        }

        static int cxChar, cxCaps, cyChar, cxClient, cyClient, iMaxWidth;
        static int iDeltaPerLine, iAccumDelta; // for mouse wheel logic

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
                        cxCaps = ((tm.tmPitchAndFamily & PitchAndFamily.TMPF_FIXED_PITCH) != 0 ? 3 : 2) * cxChar / 2;
                        cyChar = tm.tmHeight + tm.tmExternalLeading;
                    }

                    // Save the width of the three columns
                    iMaxWidth = 40 * cxChar + 22 * cxCaps;

                    // Fall through for mouse wheel information
                    goto case WindowMessage.SettingChange;
                case WindowMessage.SettingChange:
                    uint ulScrollLines = Windows.SystemParameters.GetWheelScrollLines();

                    // ulScrollLines usually equals 3 or 0 (for no scrolling)
                    // WHEEL_DELTA equals 120, so iDeltaPerLine will be 40
                    if (ulScrollLines > 0)
                        iDeltaPerLine = (int)(120 / ulScrollLines);
                    else
                        iDeltaPerLine = 0;
                    return 0;
                case WindowMessage.Size:
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
                    window.SetScrollInfo(ScrollBar.SB_VERT, ref si, true);

                    // Set horizontal scroll bar range and page size
                    si.nMax = 2 + iMaxWidth / cxChar;
                    si.nPage = (uint)(cxClient / cxChar);
                    window.SetScrollInfo(ScrollBar.SB_HORZ, ref si, true);

                    return 0;
                case WindowMessage.VerticalScroll:
                    // Get all the vertical scroll bar information
                    si = new SCROLLINFO
                    {
                        fMask = ScrollInfoMask.SIF_ALL
                    };
                    window.GetScrollInfo(ScrollBar.SB_VERT, ref si);

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
                    window.SetScrollInfo(ScrollBar.SB_VERT, ref si, true);
                    window.GetScrollInfo(ScrollBar.SB_VERT, ref si);

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
                        fMask = ScrollInfoMask.SIF_ALL
                    };
                    window.GetScrollInfo(ScrollBar.SB_HORZ, ref si);

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
                    window.SetScrollInfo(ScrollBar.SB_HORZ, ref si, true);
                    window.GetScrollInfo(ScrollBar.SB_HORZ, ref si);

                    // If the position has changed, scroll the window
                    if (si.nPos != iHorzPos)
                    {
                        window.ScrollWindow(cxChar * (iHorzPos - si.nPos), 0);
                    }
                    return 0;
                case WindowMessage.KeyDown:
                    switch ((VirtualKey)wParam)
                    {
                        case VirtualKey.VK_HOME:
                            window.SendMessage(WindowMessage.VerticalScroll, (uint)ScrollBarCommand.SB_TOP, 0);
                            break;
                        case VirtualKey.VK_END:
                            window.SendMessage(WindowMessage.VerticalScroll, (uint)ScrollBarCommand.SB_BOTTOM, 0);
                            break;
                        case VirtualKey.VK_PRIOR:
                            window.SendMessage(WindowMessage.VerticalScroll, (uint)ScrollBarCommand.SB_PAGEUP, 0);
                            break;
                        case VirtualKey.VK_NEXT:
                            window.SendMessage(WindowMessage.VerticalScroll, (uint)ScrollBarCommand.SB_PAGEDOWN, 0);
                            break;
                        case VirtualKey.VK_UP:
                            window.SendMessage(WindowMessage.VerticalScroll, (uint)ScrollBarCommand.SB_LINEUP, 0);
                            break;
                        case VirtualKey.VK_DOWN:
                            window.SendMessage(WindowMessage.VerticalScroll, (uint)ScrollBarCommand.SB_LINEDOWN, 0);
                            break;
                        case VirtualKey.VK_LEFT:
                            window.SendMessage(WindowMessage.HorizontalScroll, (uint)ScrollBarCommand.SB_PAGEUP, 0);
                            break;
                        case VirtualKey.VK_RIGHT:
                            window.SendMessage(WindowMessage.HorizontalScroll, (uint)ScrollBarCommand.SB_PAGEDOWN, 0);
                            break;
                    }
                    return 0;
                case WindowMessage.MouseWheel:
                    if (iDeltaPerLine == 0)
                        break;
                    iAccumDelta += (short)wParam.HighWord; // 120 or -120
                    while (iAccumDelta >= iDeltaPerLine)
                    {
                        window.SendMessage(WindowMessage.VerticalScroll, (uint)ScrollBarCommand.SB_LINEUP, 0);
                        iAccumDelta -= iDeltaPerLine;
                    }
                    while (iAccumDelta <= -iDeltaPerLine)
                    {
                        window.SendMessage(WindowMessage.VerticalScroll, (uint)ScrollBarCommand.SB_LINEDOWN, 0);
                        iAccumDelta += iDeltaPerLine;
                    }
                    return 0;
                case WindowMessage.Paint:
                    PAINTSTRUCT ps;
                    using (DeviceContext dc = window.BeginPaint(out ps))
                    {
                        // Get vertical scroll bar position
                        si = new SCROLLINFO
                        {
                            fMask = ScrollInfoMask.SIF_POS
                        };
                        window.GetScrollInfo(ScrollBar.SB_VERT, ref si);
                        iVertPos = si.nPos;

                        // Get horizontal scroll bar position
                        window.GetScrollInfo(ScrollBar.SB_HORZ, ref si);
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
                            dc.SetTextAlignment(TextAlignment.TA_RIGHT | TextAlignment.TA_TOP);
                            dc.TextOut(x + 22 * cxCaps + 40 * cxChar, y, WindowMethods.GetSystemMetrics(metric).ToString());
                            dc.SetTextAlignment(TextAlignment.TA_LEFT | TextAlignment.TA_TOP);
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
