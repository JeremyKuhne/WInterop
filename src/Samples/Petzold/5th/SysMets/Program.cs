// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Drawing;
using System.Linq;
using WInterop.Gdi;
using WInterop.Windows;

namespace SysMets
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
            Windows.CreateMainWindowAndRun(new SysMetsFinal(), "Get system metrics with scroll wheel support");
        }
    }

    class SysMetsFinal : WindowClass
    {
        int cxChar, cxCaps, cyChar, cxClient, cyClient, iMaxWidth;
        int iDeltaPerLine, iAccumDelta; // for mouse wheel logic

        protected override LResult WindowProcedure(WindowHandle window, MessageType message, WParam wParam, LParam lParam)
        {
            ScrollInfo si;

            switch (message)
            {
                case MessageType.Create:
                    using (DeviceContext dc = window.GetDeviceContext())
                    {
                        dc.GetTextMetrics(out TEXTMETRIC tm);
                        cxChar = tm.tmAveCharWidth;
                        cxCaps = ((tm.tmPitchAndFamily.PitchTypes & FontPitchTypes.VariablePitch) != 0 ? 3 : 2) * cxChar / 2;
                        cyChar = tm.tmHeight + tm.tmExternalLeading;
                    }

                    // Save the width of the three columns
                    iMaxWidth = 40 * cxChar + 22 * cxCaps;

                    // Fall through for mouse wheel information
                    goto case MessageType.SettingChange;
                case MessageType.SettingChange:
                    uint ulScrollLines = Windows.SystemParameters.GetWheelScrollLines();

                    // ulScrollLines usually equals 3 or 0 (for no scrolling)
                    // WHEEL_DELTA equals 120, so iDeltaPerLine will be 40
                    if (ulScrollLines > 0)
                        iDeltaPerLine = (int)(120 / ulScrollLines);
                    else
                        iDeltaPerLine = 0;
                    return 0;
                case MessageType.Size:
                    cxClient = lParam.LowWord;
                    cyClient = lParam.HighWord;

                    // Set vertical scroll bar range and page size
                    si = new ScrollInfo
                    {
                        fMask = ScrollInfoMask.Range | ScrollInfoMask.Page,
                        nMin = 0,
                        nMax = Metrics.SystemMetrics.Count - 1,
                        nPage = (uint)(cyClient / cyChar),
                    };
                    window.SetScrollInfo(ScrollBar.Vertical, ref si, true);

                    // Set horizontal scroll bar range and page size
                    si.nMax = 2 + iMaxWidth / cxChar;
                    si.nPage = (uint)(cxClient / cxChar);
                    window.SetScrollInfo(ScrollBar.Horizontal, ref si, true);

                    return 0;
                case MessageType.VerticalScroll:
                    // Get all the vertical scroll bar information
                    si = new ScrollInfo
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
                        window.ScrollWindow(new Point(0, cyChar * (iVertPos - si.nPos)));
                        window.UpdateWindow();
                    }
                    return 0;
                case MessageType.HorizontalScroll:
                    // Get all the horizontal scroll bar information
                    si = new ScrollInfo
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
                        window.ScrollWindow(new Point(cxChar * (iHorzPos - si.nPos), 0));
                    }
                    return 0;
                case MessageType.KeyDown:
                    switch ((VirtualKey)wParam)
                    {
                        case VirtualKey.Home:
                            window.SendMessage(MessageType.VerticalScroll, (uint)ScrollCommand.Top, 0);
                            break;
                        case VirtualKey.End:
                            window.SendMessage(MessageType.VerticalScroll, (uint)ScrollCommand.Bottom, 0);
                            break;
                        case VirtualKey.Prior:
                            window.SendMessage(MessageType.VerticalScroll, (uint)ScrollCommand.PageUp, 0);
                            break;
                        case VirtualKey.Next:
                            window.SendMessage(MessageType.VerticalScroll, (uint)ScrollCommand.PageDown, 0);
                            break;
                        case VirtualKey.Up:
                            window.SendMessage(MessageType.VerticalScroll, (uint)ScrollCommand.LineUp, 0);
                            break;
                        case VirtualKey.Down:
                            window.SendMessage(MessageType.VerticalScroll, (uint)ScrollCommand.LineDown, 0);
                            break;
                        case VirtualKey.Left:
                            window.SendMessage(MessageType.HorizontalScroll, (uint)ScrollCommand.PageUp, 0);
                            break;
                        case VirtualKey.Right:
                            window.SendMessage(MessageType.HorizontalScroll, (uint)ScrollCommand.PageDown, 0);
                            break;
                    }
                    return 0;
                case MessageType.MouseWheel:
                    if (iDeltaPerLine == 0)
                        break;
                    iAccumDelta += (short)wParam.HighWord; // 120 or -120
                    while (iAccumDelta >= iDeltaPerLine)
                    {
                        window.SendMessage(MessageType.VerticalScroll, (uint)ScrollCommand.LineUp, 0);
                        iAccumDelta -= iDeltaPerLine;
                    }
                    while (iAccumDelta <= -iDeltaPerLine)
                    {
                        window.SendMessage(MessageType.VerticalScroll, (uint)ScrollCommand.LineDown, 0);
                        iAccumDelta += iDeltaPerLine;
                    }
                    return 0;
                case MessageType.Paint:
                    using (DeviceContext dc = window.BeginPaint(out PaintStruct ps))
                    {
                        // Get vertical scroll bar position
                        si = new ScrollInfo
                        {
                            fMask = ScrollInfoMask.Position
                        };
                        window.GetScrollInfo(ScrollBar.Vertical, ref si);
                        iVertPos = si.nPos;

                        // Get horizontal scroll bar position
                        window.GetScrollInfo(ScrollBar.Horizontal, ref si);
                        iHorzPos = si.nPos;

                        // Find painting limits
                        int iPaintBeg = Math.Max(0, iVertPos + ps.Paint.Top / cyChar);
                        int iPaintEnd = Math.Min(Metrics.SystemMetrics.Count - 1, iVertPos + ps.Paint.Bottom / cyChar);

                        var keys = Metrics.SystemMetrics.Keys.ToArray();
                        for (int i = iPaintBeg; i <= iPaintEnd; i++)
                        {
                            var metric = keys[i];
                            int x = cxChar * (1 - iHorzPos);
                            int y = cyChar * (i - iVertPos);

                            dc.TextOut(new Point(x, y), metric.ToString().AsSpan());
                            dc.TextOut(new Point(x + 22 * cxCaps, y), Metrics.SystemMetrics[metric].AsSpan());
                            dc.SetTextAlignment(new TextAlignment(TextAlignment.Horizontal.Right, TextAlignment.Vertical.Top));
                            dc.TextOut(new Point(x + 22 * cxCaps + 40 * cxChar, y), Windows.GetSystemMetrics(metric).ToString().AsSpan());
                            dc.SetTextAlignment(new TextAlignment(TextAlignment.Horizontal.Left, TextAlignment.Vertical.Top));
                        }
                    }
                    return 0;
            }

            return base.WindowProcedure(window, message, wParam, lParam);
        }
    }
}
