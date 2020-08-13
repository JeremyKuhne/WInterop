// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Drawing;
using System.Threading;
using WInterop.Gdi;
using WInterop.Modules;
using WInterop.Windows;

namespace RandRect
{
    /// <summary>
    ///  Sample from Programming Windows, 5th Edition.
    ///  Original (c) Charles Petzold, 1998
    ///  Figure 5-26, Pages 200-202.
    /// </summary>
    internal static class Program
    {
        [STAThread]
        private static void Main()
        {
            // Hack for launching as a .NET Core Windows Application
            WInterop.Console.Console.TryFreeConsole();

            const string szAppName = "RandRect";

            ModuleInstance module = ModuleInstance.GetModuleForType(typeof(Program));
            WindowClassInfo wndclass = new WindowClassInfo
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
                szAppName,
                "Random Rectangles",
                WindowStyles.OverlappedWindow,
                bounds: Windows.DefaultBounds,
                instance: module);

            window.ShowWindow(ShowWindowCommand.Normal);
            window.UpdateWindow();

            while (true)
            {
                if (Windows.PeekMessage(out WindowMessage message, options: PeekMessageOptions.Remove))
                {
                    if (message.Type == MessageType.Quit)
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

        private static int s_cxClient, s_cyClient;
        private static readonly Random s_rand = new Random();

        private static LResult WindowProcedure(WindowHandle window, MessageType message, WParam wParam, LParam lParam)
        {
            switch (message)
            {
                case MessageType.Size:
                    s_cxClient = lParam.LowWord;
                    s_cyClient = lParam.HighWord;
                    return 0;
                case MessageType.Destroy:
                    Windows.PostQuitMessage(0);
                    return 0;
            }

            return Windows.DefaultWindowProcedure(window, message, wParam, lParam);
        }

        private static void DrawRectangle(WindowHandle window)
        {
            if (s_cxClient == 0 || s_cyClient == 0)
                return;

            Rectangle rect = Rectangle.FromLTRB(
                s_rand.Next() % s_cxClient,
                s_rand.Next() % s_cyClient,
                s_rand.Next() % s_cxClient,
                s_rand.Next() % s_cyClient);

            using (BrushHandle brush = Gdi.CreateSolidBrush(
                Color.FromArgb((byte)(s_rand.Next() % 256), (byte)(s_rand.Next() % 256), (byte)(s_rand.Next() % 256))))
            {
                using (DeviceContext dc = window.GetDeviceContext())
                {
                    dc.FillRectangle(rect, brush);
                }
            }
        }
    }
}
