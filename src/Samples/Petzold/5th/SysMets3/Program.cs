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

namespace SysMets3
{
    /// <summary>
    /// Sample from Programming Windows, 5th Edition.
    /// Original (c) Charles Petzold, 1998
    /// Figure 4-11, Pages 112-117.
    /// </summary>
    static class Program
    {
        [STAThread]
        static void Main()
        {
            SafeModuleHandle module = Marshal.GetHINSTANCE(typeof(Program).Module);
            WindowClass wndclass = new WindowClass
            {
                Style = ClassStyle.HorizontalRedraw | ClassStyle.VerticalRedraw,
                WindowProcedure = WindowProcedure,
                Instance = module,
                Icon = IconId.Application,
                Cursor = CursorId.Arrow,
                Background = StockBrush.White,
                ClassName = "SysMets3"
            };

            Windows.RegisterClass(wndclass);

            WindowHandle window = Windows.CreateWindow(
                module,
                "SysMets3",
                "Get System Metrics No. 3",
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
