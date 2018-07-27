// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Drawing;
using System.Threading;
using WInterop.Gdi;
using WInterop.Modules;
using WInterop.Resources;
using WInterop.Windows;

namespace RandRect
{
    /// <summary>
    /// Sample from Programming Windows, 5th Edition.
    /// Original (c) Charles Petzold, 1998
    /// Figure 5-26, Pages 200-202.
    /// </summary>
    static class Program
    {
        [STAThread]
        static void Main()
        {
            const string szAppName = "RandRect";

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
                "Random Rectangles",
                WindowStyles.OverlappedWindow);

            window.ShowWindow(ShowWindow.Normal);
            window.UpdateWindow();

            while (true)
            {
                if (Windows.PeekMessage(out MSG message, 0, 0, PeekMessageOptions.PM_REMOVE))
                {
                    if (message.message == WindowMessage.Quit)
                        break;
                    Windows.TranslateMessage(ref message);
                    Windows.DispatchMessage(ref message);
                }

                // We're crazy fast 20 years past the source sample,
                // sleeping to make this a bit more interesting.
                Thread.Sleep(100);
                DrawRectangle(window);
            }
        }

        static int cxClient, cyClient;
        static Random rand = new Random();

        static LRESULT WindowProcedure(WindowHandle window, WindowMessage message, WPARAM wParam, LPARAM lParam)
        {
            switch (message)
            {
                case WindowMessage.Size:
                    cxClient = lParam.LowWord;
                    cyClient = lParam.HighWord;
                    return 0;
                case WindowMessage.Destroy:
                    Windows.PostQuitMessage(0);
                    return 0;
            }

            return Windows.DefaultWindowProcedure(window, message, wParam, lParam);
        }

        static void DrawRectangle(WindowHandle window)
        {
            if (cxClient == 0 || cyClient == 0)
                return;

            Rectangle rect = Rectangle.FromLTRB(
                rand.Next() % cxClient,
                rand.Next() % cyClient,
                rand.Next() % cxClient,
                rand.Next() % cyClient);

            using (BrushHandle brush = Gdi.CreateSolidBrush(
                Color.FromArgb((byte)(rand.Next() % 256), (byte)(rand.Next() % 256), (byte)(rand.Next() % 256))))
            {
                using (DeviceContext dc = window.GetDeviceContext())
                {
                    dc.FillRectangle(rect, brush);
                }
            }
        }
    }
}
