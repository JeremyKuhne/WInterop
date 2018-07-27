// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Drawing;
using WInterop.ErrorHandling;
using WInterop.Gdi;
using WInterop.Modules;
using WInterop.Resources;
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
            const string szAppName = "Checker1";

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
                "Checker1 Mouse Hit-Test Demo",
                WindowStyles.OverlappedWindow);

            window.ShowWindow(ShowWindow.Normal);
            window.UpdateWindow();

            while (Windows.GetMessage(out MSG message))
            {
                Windows.TranslateMessage(ref message);
                Windows.DispatchMessage(ref message);
            }
        }

        const int DIVISIONS = 5;
        static bool[,] fState = new bool[DIVISIONS, DIVISIONS];
        static int cxBlock, cyBlock;

        static LRESULT WindowProcedure(WindowHandle window, WindowMessage message, WPARAM wParam, LPARAM lParam)
        {
            switch (message)
            {
                case WindowMessage.Size:
                    cxBlock = lParam.LowWord / DIVISIONS;
                    cyBlock = lParam.HighWord / DIVISIONS;
                    return 0;
                case WindowMessage.LeftButtonDown:
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
                        ErrorMethods.MessageBeep(0);
                    }

                    return 0;
                case WindowMessage.Paint:
                    using (DeviceContext dc = window.BeginPaint())
                    {
                        for (x = 0; x < DIVISIONS; x++)
                            for (y = 0; y < DIVISIONS; y++)
                            {
                                dc.Rectangle(new Rectangle(
                                    x * cxBlock, y * cyBlock, (x + 1) * cxBlock, (y + 1) * cyBlock));

                                if (fState[x,y])
                                {
                                    dc.MoveTo(x * cxBlock, y * cyBlock);
                                    dc.LineTo((x + 1) * cxBlock, (y + 1) * cyBlock);
                                    dc.MoveTo(x * cxBlock, (y + 1) * cyBlock);
                                    dc.LineTo((x + 1) * cxBlock, y * cyBlock);
                                }
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
