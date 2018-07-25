// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Drawing;
using WInterop.Gdi;
using WInterop.Gdi.Types;
using WInterop.Modules.Types;
using WInterop.Resources.Types;
using WInterop.Windows;
using WInterop.Windows.Types;

namespace Blokout2
{
    /// <summary>
    /// Sample from Programming Windows, 5th Edition.
    /// Original (c) Charles Petzold, 1998
    /// Figure 7-11, Pages 314-317.
    /// </summary>
    static class Program
    {
        [STAThread]
        static void Main()
        {
            const string szAppName = "Blokout2";

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
                "Mouse Button & Capture Demo",
                WindowStyles.OverlappedWindow);

            window.ShowWindow(ShowWindow.Normal);
            window.UpdateWindow();

            while (Windows.GetMessage(out MSG message))
            {
                Windows.TranslateMessage(ref message);
                Windows.DispatchMessage(ref message);
            }
        }

        static void DrawBoxOutline(WindowHandle window, Point ptBeg, Point ptEnd)
        {
            using (DeviceContext dc = window.GetDeviceContext())
            {
                dc.SetRasterOperation(PenMixMode.Not);
                dc.SelectObject(StockBrush.Null);
                dc.Rectangle(ptBeg.X, ptBeg.Y, ptEnd.X, ptEnd.Y);
            }
        }

        static bool fBlocking, fValidBox;
        static Point ptBeg, ptEnd, ptBoxBeg, ptBoxEnd;

        static LRESULT WindowProcedure(WindowHandle window, WindowMessage message, WPARAM wParam, LPARAM lParam)
        {
            switch (message)
            {
                case WindowMessage.LeftButtonDown:
                    ptBeg.X = ptEnd.X = lParam.LowWord;
                    ptBeg.Y = ptEnd.Y = lParam.HighWord;
                    DrawBoxOutline(window, ptBeg, ptEnd);
                    window.SetCapture();
                    Windows.SetCursor(CursorId.Cross);
                    fBlocking = true;
                    return 0;
                case WindowMessage.MouseMove:
                    if (fBlocking)
                    {
                        Windows.SetCursor(CursorId.Cross);
                        DrawBoxOutline(window, ptBeg, ptEnd);
                        ptEnd.X = lParam.LowWord;
                        ptEnd.Y = lParam.HighWord;
                        DrawBoxOutline(window, ptBeg, ptEnd);
                    }
                    return 0;
                case WindowMessage.LeftButtonUp:
                    if (fBlocking)
                    {
                        DrawBoxOutline(window, ptBeg, ptEnd);
                        ptBoxBeg = ptBeg;
                        ptBoxEnd.X = lParam.LowWord;
                        ptBoxEnd.Y = lParam.HighWord;
                        Windows.ReleaseCapture();
                        Windows.SetCursor(CursorId.Arrow);
                        fBlocking = false;
                        fValidBox = true;
                        window.Invalidate(true);
                    }
                    return 0;
                case WindowMessage.Paint:
                    using (DeviceContext dc = window.BeginPaint())
                    {
                        if (fValidBox)
                        {
                            dc.SelectObject(StockBrush.Black);
                            dc.Rectangle(ptBoxBeg.X, ptBoxBeg.Y,
                                ptBoxEnd.X, ptBoxEnd.Y);
                        }
                        if (fBlocking)
                        {
                            dc.SetRasterOperation(PenMixMode.Not);
                            dc.SelectObject(StockBrush.Null);
                            dc.Rectangle(ptBeg.X, ptBeg.Y, ptEnd.X, ptEnd.Y);
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
