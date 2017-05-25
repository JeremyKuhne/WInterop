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

            ModuleInstance module = Marshal.GetHINSTANCE(typeof(Program).Module);
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

        static void DrawBoxOutline(WindowHandle window, POINT ptBeg, POINT ptEnd)
        {
            using (DeviceContext dc = window.GetDeviceContext())
            {
                dc.SetRasterOperation(PenMixMode.Not);
                dc.SelectObject(StockBrush.Null);
                dc.Rectangle(ptBeg.x, ptBeg.y, ptEnd.x, ptEnd.y);
            }
        }

        static bool fBlocking, fValidBox;
        static POINT ptBeg, ptEnd, ptBoxBeg, ptBoxEnd;

        static LRESULT WindowProcedure(WindowHandle window, WindowMessage message, WPARAM wParam, LPARAM lParam)
        {
            switch (message)
            {
                case WindowMessage.LeftButtonDown:
                    ptBeg.x = ptEnd.x = lParam.LowWord;
                    ptBeg.y = ptEnd.y = lParam.HighWord;
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
                        ptEnd.x = lParam.LowWord;
                        ptEnd.y = lParam.HighWord;
                        DrawBoxOutline(window, ptBeg, ptEnd);
                    }
                    return 0;
                case WindowMessage.LeftButtonUp:
                    if (fBlocking)
                    {
                        DrawBoxOutline(window, ptBeg, ptEnd);
                        ptBoxBeg = ptBeg;
                        ptBoxEnd.x = lParam.LowWord;
                        ptBoxEnd.y = lParam.HighWord;
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
                            dc.Rectangle(ptBoxBeg.x, ptBoxBeg.y,
                                ptBoxEnd.x, ptBoxEnd.y);
                        }
                        if (fBlocking)
                        {
                            dc.SetRasterOperation(PenMixMode.Not);
                            dc.SelectObject(StockBrush.Null);
                            dc.Rectangle(ptBeg.x, ptBeg.y, ptEnd.x, ptEnd.y);
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
