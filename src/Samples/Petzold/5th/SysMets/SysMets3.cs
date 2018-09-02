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
    class SysMets3 : SysMets2
    {
        protected int cxClient;

        protected override LResult WindowProcedure(WindowHandle window, MessageType message, WParam wParam, LParam lParam)
        {
            ScrollInfo si;

            switch (message)
            {
                case MessageType.Size:
                    int iMaxWidth = 40 * cxChar + 22 * cxCaps;

                    cxClient = lParam.LowWord;
                    cyClient = lParam.HighWord;

                    // Set vertical scroll bar range and page size
                    si = new ScrollInfo
                    {
                        Mask = ScrollInfoMask.Range | ScrollInfoMask.Page,
                        Min = 0,
                        Max = Metrics.SystemMetrics.Count - 1,
                        Page = (uint)(cyClient / cyChar),
                    };
                    window.SetScrollInfo(ScrollBar.Vertical, ref si, true);

                    // Set horizontal scroll bar range and page size
                    si.Max = 2 + iMaxWidth / cxChar;
                    si.Page = (uint)(cxClient / cxChar);
                    window.SetScrollInfo(ScrollBar.Horizontal, ref si, true);

                    return 0;
                case MessageType.VerticalScroll:
                    // Get all the vertical scroll bar information
                    si = new ScrollInfo
                    {
                        Mask = ScrollInfoMask.All
                    };
                    window.GetScrollInfo(ScrollBar.Vertical, ref si);

                    // Save the position for comparison later on
                    int iVertPos = si.Position;

                    switch ((ScrollCommand)wParam.LowWord)
                    {
                        case ScrollCommand.Top:
                            si.Position = si.Min;
                            break;
                        case ScrollCommand.Bottom:
                            si.Position = si.Max;
                            break;
                        case ScrollCommand.LineUp:
                            si.Position -= 1;
                            break;
                        case ScrollCommand.LineDown:
                            si.Position += 1;
                            break;
                        case ScrollCommand.PageUp:
                            si.Position -= (int)si.Page;
                            break;
                        case ScrollCommand.PageDown:
                            si.Position += (int)si.Page;
                            break;
                        case ScrollCommand.ThumbTrack:
                            si.Position = si.TrackPosition;
                            break;
                    }

                    // Set the position and then retrieve it. Due to adjustments
                    // by Windows it may not be the same as the value set.
                    si.Mask = ScrollInfoMask.Position;
                    window.SetScrollInfo(ScrollBar.Vertical, ref si, true);
                    window.GetScrollInfo(ScrollBar.Vertical, ref si);

                    // If the position has changed, scroll the window and update it
                    if (si.Position != iVertPos)
                    {
                        window.ScrollWindow(new Point(0, cyChar * (iVertPos - si.Position)));
                        window.UpdateWindow();
                    }
                    return 0;
                case MessageType.HorizontalScroll:
                    // Get all the horizontal scroll bar information
                    si = new ScrollInfo
                    {
                        Mask = ScrollInfoMask.All
                    };
                    window.GetScrollInfo(ScrollBar.Horizontal, ref si);

                    // Save the position for comparison later on
                    int iHorzPos = si.Position;
                    switch ((ScrollCommand)wParam.LowWord)
                    {
                        case ScrollCommand.LineLeft:
                            si.Position -= 1;
                            break;
                        case ScrollCommand.LineRight:
                            si.Position += 1;
                            break;
                        case ScrollCommand.PageLeft:
                            si.Position -= (int)si.Page;
                            break;
                        case ScrollCommand.PageRight:
                            si.Position += (int)si.Page;
                            break;
                        case ScrollCommand.ThumbPosition:
                            si.Position = si.TrackPosition;
                            break;
                    }

                    // Set the position and then retrieve it. Due to adjustments
                    // by Windows it may not be the same as the value set.
                    si.Mask = ScrollInfoMask.Position;
                    window.SetScrollInfo(ScrollBar.Horizontal, ref si, true);
                    window.GetScrollInfo(ScrollBar.Horizontal, ref si);

                    // If the position has changed, scroll the window
                    if (si.Position != iHorzPos)
                    {
                        window.ScrollWindow(new Point(cxChar * (iHorzPos - si.Position), 0));
                    }
                    return 0;

                case MessageType.Paint:
                    using (DeviceContext dc = window.BeginPaint(out PaintStruct ps))
                    {
                        // Get vertical scroll bar position
                        si = new ScrollInfo
                        {
                            Mask = ScrollInfoMask.Position
                        };
                        window.GetScrollInfo(ScrollBar.Vertical, ref si);
                        iVertPos = si.Position;

                        // Get horizontal scroll bar position
                        window.GetScrollInfo(ScrollBar.Horizontal, ref si);
                        iHorzPos = si.Position;

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
