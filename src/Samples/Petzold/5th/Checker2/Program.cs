// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Drawing;
using WInterop.Gdi;
using WInterop.Windows;

namespace Checker2
{
    /// <summary>
    /// Sample from Programming Windows, 5th Edition.
    /// Original (c) Charles Petzold, 1998
    /// Figure 7-6, Pages 293-296.
    /// </summary>
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Windows.CreateMainWindowAndRun(new Checker2(), "Checker2 Mouse Hit-Test Demo");
        }
    }

    class Checker2 : WindowClass
    {
        const int DIVISIONS = 5;
        bool[,] fState = new bool[DIVISIONS, DIVISIONS];
        int cxBlock, cyBlock;

        protected override LRESULT WindowProcedure(WindowHandle window, MessageType message, WPARAM wParam, LPARAM lParam)
        {
            switch (message)
            {
                case MessageType.Size:
                    cxBlock = lParam.LowWord / DIVISIONS;
                    cyBlock = lParam.HighWord / DIVISIONS;
                    return 0;
                case MessageType.SetFocus:
                    Windows.ShowCursor(true);
                    return 0;
                case MessageType.KillFocus:
                    Windows.ShowCursor(false);
                    return 0;
                case MessageType.KeyDown:
                    Point point = Windows.GetCursorPosition();
                    window.ScreenToClient(ref point);
                    int x = Math.Max(0, Math.Min(DIVISIONS - 1, point.X / cxBlock));
                    int y = Math.Max(0, Math.Min(DIVISIONS - 1, point.Y / cyBlock));
                    switch ((VirtualKey)wParam)
                    {
                        case VirtualKey.Up:
                            y--;
                            break;
                        case VirtualKey.Down:
                            y++;
                            break;
                        case VirtualKey.Left:
                            x--;
                            break;
                        case VirtualKey.Right:
                            x++;
                            break;
                        case VirtualKey.Home:
                            x = y = 0;
                            break;
                        case VirtualKey.End:
                            x = y = DIVISIONS - 1;
                            break;
                        case VirtualKey.Return:
                        case VirtualKey.Space:
                            window.SendMessage(MessageType.LeftButtonDown, (WPARAM)MouseKey.LeftButton,
                                new LPARAM(y * cyBlock, x * cxBlock));
                            break;
                    }
                    x = (x + DIVISIONS) % DIVISIONS;
                    y = (y + DIVISIONS) % DIVISIONS;

                    point = new Point(x * cxBlock + cxBlock / 2,  y * cyBlock + cyBlock / 2);
                    window.ClientToScreen(ref point);
                    Windows.SetCursorPosition(point);
                    return 0;
                case MessageType.LeftButtonDown:
                    x = lParam.LowWord / cxBlock;
                    y = lParam.HighWord / cyBlock;
                    if (x < DIVISIONS && y < DIVISIONS)
                    {
                        fState[x, y] ^= true;
                        Rectangle rect = Rectangle.FromLTRB
                        (
                            x * cxBlock,
                            y * cyBlock,
                            (x + 1) * cxBlock,
                            (y + 1) * cyBlock
                        );
                        window.InvalidateRectangle(rect, false);
                    }
                    else
                    {
                        Windows.MessageBeep(0);
                    }

                    return 0;
                case MessageType.Paint:
                    using (DeviceContext dc = window.BeginPaint())
                    {
                        for (x = 0; x < DIVISIONS; x++)
                            for (y = 0; y < DIVISIONS; y++)
                            {
                                dc.Rectangle(new Rectangle(x * cxBlock, y * cyBlock, (x + 1) * cxBlock, (y + 1) * cyBlock));
                                if (fState[x, y])
                                {
                                    dc.MoveTo(new Point(x * cxBlock, y * cyBlock));
                                    dc.LineTo(new Point((x + 1) * cxBlock, (y + 1) * cyBlock));
                                    dc.MoveTo(new Point(x * cxBlock, (y + 1) * cyBlock));
                                    dc.LineTo(new Point((x + 1) * cxBlock, y * cyBlock));
                                }
                            }
                    }
                    return 0;
            }

            return base.WindowProcedure(window, message, wParam, lParam);
        }
    }
}
