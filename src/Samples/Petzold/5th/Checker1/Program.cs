// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Drawing;
using WInterop.Errors;
using WInterop.Gdi;
using WInterop.Windows;

namespace Checker1
{
    /// <summary>
    /// Sample from Programming Windows, 5th Edition.
    /// Original (c) Charles Petzold, 1998
    /// Figure 7-4, Pages 288-290.
    /// </summary>
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Windows.CreateMainWindowAndRun(new Checker1(), "Checker1 Mouse Hit-Test Demo");
        }
    }

    public class Checker1 : WindowClass
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
                case MessageType.LeftButtonDown:
                    int x = lParam.LowWord / cxBlock;
                    int y = lParam.HighWord / cyBlock;
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
                                dc.Rectangle(new Rectangle(
                                    x * cxBlock, y * cyBlock, (x + 1) * cxBlock, (y + 1) * cyBlock));

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
