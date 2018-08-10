// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Drawing;
using WInterop.Gdi;
using WInterop.Modules;
using WInterop.Windows;

namespace LineDemo
{
    /// <summary>
    /// Sample from Programming Windows, 5th Edition.
    /// Original (c) Charles Petzold, 1998
    /// Figure 5-14, Pages 153-155.
    /// </summary>
    static class Program
    {
        [STAThread]
        static void Main()
        {
            ModuleInstance module = ModuleInstance.GetModuleForType(typeof(Program));
            WindowClassInfo wndclass = new WindowClassInfo
            {
                Style = ClassStyle.HorizontalRedraw | ClassStyle.VerticalRedraw,
                WindowProcedure = WindowProcedure,
                Instance = module,
                Icon = IconId.Application,
                Cursor = CursorId.Arrow,
                Background = StockBrush.White,
                ClassName = "LineDemo"
            };

            Windows.RegisterClass(ref wndclass);

            WindowHandle window = Windows.CreateWindow(
                "LineDemo",
                "Line Demonstration",
                WindowStyles.OverlappedWindow,
                instance: module);

            window.ShowWindow(ShowWindowCommand.Normal);
            window.UpdateWindow();

            while (Windows.GetMessage(out WindowMessage message))
            {
                Windows.TranslateMessage(ref message);
                Windows.DispatchMessage(ref message);
            }
        }

        static int cxClient, cyClient;

        static LRESULT WindowProcedure(WindowHandle window, MessageType message, WPARAM wParam, LPARAM lParam)
        {
            switch (message)
            {
                case MessageType.Size:
                    cxClient = lParam.LowWord;
                    cyClient = lParam.HighWord;
                    return 0;
                case MessageType.Paint:
                    using (DeviceContext dc = window.BeginPaint())
                    {
                        dc.Rectangle(Rectangle.FromLTRB(cxClient / 8, cyClient / 8, 7 * cxClient / 8, 7 * cyClient / 8));
                        dc.MoveTo(new Point(0, 0));
                        dc.LineTo(new Point(cxClient, cyClient));
                        dc.MoveTo(new Point(0, cyClient));
                        dc.LineTo(new Point(cxClient, 0));
                        dc.Ellipse(Rectangle.FromLTRB(cxClient / 8, cyClient / 8, 7 * cxClient / 8, 7 * cyClient / 8));
                        dc.RoundRectangle(
                            Rectangle.FromLTRB(cxClient / 4, cyClient / 4, 3 * cxClient / 4, 3 * cyClient / 4),
                            new Size(cxClient / 4, cyClient / 4));
                    }
                    return 0;
                case MessageType.Destroy:
                    Windows.PostQuitMessage(0);
                    return 0;
            }

            return Windows.DefaultWindowProcedure(window, message, wParam, lParam);
        }
    }
}
