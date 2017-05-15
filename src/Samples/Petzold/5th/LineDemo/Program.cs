// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Runtime.InteropServices;
using WInterop.Extensions.WindowExtensions;
using WInterop.Gdi.Types;
using WInterop.Modules.Types;
using WInterop.Resources.Types;
using WInterop.Windows;
using WInterop.Windows.Types;

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
            SafeModuleHandle module = Marshal.GetHINSTANCE(typeof(Program).Module);
            WindowClass wndclass = new WindowClass
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
                module,
                "LineDemo",
                "Line Demonstration",
                WindowStyles.OverlappedWindow);

            window.ShowWindow(ShowWindow.Normal);
            window.UpdateWindow();

            while (Windows.GetMessage(out MSG message))
            {
                Windows.TranslateMessage(ref message);
                Windows.DispatchMessage(ref message);
            }
        }

        static int cxClient, cyClient;

        static LRESULT WindowProcedure(WindowHandle window, WindowMessage message, WPARAM wParam, LPARAM lParam)
        {
            switch (message)
            {
                case WindowMessage.Size:
                    cxClient = lParam.LowWord;
                    cyClient = lParam.HighWord;
                    return 0;
                case WindowMessage.Paint:
                    using (DeviceContext dc = window.BeginPaint())
                    {
                        dc.Rectangle(cxClient / 8, cyClient / 8, 7 * cxClient / 8, 7 * cyClient / 8);
                        dc.MoveTo(0, 0);
                        dc.LineTo(cxClient, cyClient);
                        dc.MoveTo(0, cyClient);
                        dc.LineTo(cxClient, 0);
                        dc.Ellipse(cxClient / 8, cyClient / 8, 7 * cxClient / 8, 7 * cyClient / 8);
                        dc.RoundRectangle(cxClient / 4, cyClient / 4, 3 * cxClient / 4, 3 * cyClient / 4,
                            cxClient / 4, cyClient / 4);
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
