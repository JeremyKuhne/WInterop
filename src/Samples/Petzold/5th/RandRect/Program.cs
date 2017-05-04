// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Runtime.InteropServices;
using System.Threading;
using WInterop.Extensions.WindowExtensions;
using WInterop.Gdi.Types;
using WInterop.Modules.Types;
using WInterop.Resources.Types;
using WInterop.Windows;
using WInterop.Windows.Types;

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

            SafeModuleHandle module = Marshal.GetHINSTANCE(typeof(Program).Module);
            WindowClass wndclass = new WindowClass
            {
                Style = WindowClassStyle.CS_HREDRAW | WindowClassStyle.CS_VREDRAW,
                WindowProcedure = WindowProcedure,
                Instance = module,
                Icon = IconId.IDI_APPLICATION,
                Cursor = CursorId.IDC_ARROW,
                Background = StockBrush.WHITE_BRUSH,
                ClassName = szAppName
            };

            Windows.RegisterClass(wndclass);

            WindowHandle window = Windows.CreateWindow(
                module,
                szAppName,
                "Random Rectangles",
                WindowStyle.WS_OVERLAPPEDWINDOW);

            window.ShowWindow(ShowWindowCommand.SW_SHOWNORMAL);
            window.UpdateWindow();

            while (true)
            {
                if (Windows.PeekMessage(out MSG message, 0, 0, PeekMessageOptions.PM_REMOVE))
                {
                    if (message.message == MessageType.WM_QUIT)
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

        static LRESULT WindowProcedure(WindowHandle window, MessageType message, WPARAM wParam, LPARAM lParam)
        {
            switch (message)
            {
                case MessageType.WM_SIZE:
                    cxClient = lParam.LowWord;
                    cyClient = lParam.HighWord;
                    return 0;
                case MessageType.WM_DESTROY:
                    Windows.PostQuitMessage(0);
                    return 0;
            }

            return Windows.DefaultWindowProcedure(window, message, wParam, lParam);
        }

        static void DrawRectangle(WindowHandle window)
        {
            if (cxClient == 0 || cyClient == 0)
                return;

            RECT rect = new RECT(rand.Next() % cxClient, rand.Next() % cyClient,
                rand.Next() % cxClient, rand.Next() % cyClient);
            using (BrushHandle brush = Windows.CreateSolidBrush((byte)(rand.Next() % 256), (byte)(rand.Next() % 256), (byte)(rand.Next() % 256)))
            {
                using (DeviceContext dc = window.GetDeviceContext())
                {
                    dc.FillRectangle(rect, brush);
                }
            }
        }
    }
}
