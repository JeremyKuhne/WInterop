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

namespace OwnDraw
{
    /// <summary>
    /// Sample from Programming Windows, 5th Edition.
    /// Original (c) Charles Petzold, 1998
    /// Figure 9-3, Pages 375-380.
    /// </summary>
    static class Program
    {
        [STAThread]
        static void Main()
        {
            const string szAppName = "OwnDraw";

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
                "Owner-Draw Button Demo",
                WindowStyles.OverlappedWindow);

            window.ShowWindow(ShowWindow.Normal);
            window.UpdateWindow();

            while (Windows.GetMessage(out MSG message))
            {
                Windows.TranslateMessage(ref message);
                Windows.DispatchMessage(ref message);
            }
        }

        static void Triangle(DeviceContext dc, POINT[] pt)
        {
            dc.SelectObject(StockBrush.Black);
            dc.Polygon(pt);
            dc.SelectObject(StockBrush.White);
        }

        static WindowHandle hwndSmaller, hwndLarger;
        static int cxClient, cyClient;
        static int btnWidth, btnHeight;
        static SIZE baseUnits;

        const int ID_SMALLER = 1;
        const int ID_LARGER = 2;

        static unsafe  LRESULT WindowProcedure(WindowHandle window, WindowMessage message, WPARAM wParam, LPARAM lParam)
        {
            switch (message)
            {
                case WindowMessage.Create:
                    baseUnits = Windows.GetDialogBaseUnits();
                    btnWidth = baseUnits.cx * 8;
                    btnHeight = baseUnits.cy * 4;

                    // Create the owner-draw pushbuttons
                    CREATESTRUCT* create = (CREATESTRUCT*)lParam;

                    hwndSmaller = Windows.CreateWindow("button", "",
                        WindowStyles.Child | WindowStyles.Visible | (WindowStyles)ButtonStyles.OwnerDrawn,
                        ExtendedWindowStyles.None,
                        0, 0, btnWidth, btnHeight,
                        window, (IntPtr)ID_SMALLER, create->Instance, IntPtr.Zero);

                    hwndLarger = Windows.CreateWindow("button", "",
                        WindowStyles.Child | WindowStyles.Visible | (WindowStyles)ButtonStyles.OwnerDrawn,
                        ExtendedWindowStyles.None,
                        0, 0, btnWidth, btnHeight,
                        window, (IntPtr)ID_LARGER, create->Instance, IntPtr.Zero);
                    return 0;
                case WindowMessage.Size:
                    cxClient = lParam.LowWord;
                    cyClient = lParam.HighWord;

                    // Move the buttons to the new center
                    hwndSmaller.MoveWindow(cxClient / 2 - 3 * btnWidth / 2, cyClient / 2 - btnHeight / 2,
                        btnWidth, btnHeight, true);
                    hwndLarger.MoveWindow(cxClient / 2 + btnWidth / 2, cyClient / 2 - btnHeight / 2,
                        btnWidth, btnHeight, true);
                    return 0;
                case WindowMessage.Command:
                    RECT rc = window.GetWindowRectangle();

                    // Make the window 10% smaller or larger
                    switch ((int)(uint)wParam)
                    {
                        case ID_SMALLER:
                            rc.left += cxClient / 20;
                            rc.right -= cxClient / 20;
                            rc.top += cyClient / 20;
                            rc.bottom -= cyClient / 20;
                            break;
                        case ID_LARGER:
                            rc.left -= cxClient / 20;
                            rc.right += cxClient / 20;
                            rc.top -= cyClient / 20;
                            rc.bottom += cyClient / 20;
                            break;
                    }

                    window.MoveWindow(rc.left, rc.top, rc.right - rc.left, rc.bottom - rc.top, true);
                    return 0;
                case WindowMessage.DrawItem:
                    DRAWITEMSTRUCT* pdis = (DRAWITEMSTRUCT*)lParam;

                    // Fill area with white and frame it black
                    using (DeviceContext dc = pdis->DeviceContext)
                    {
                        RECT rect = pdis->rcItem;

                        dc.FillRectangle(rect, StockBrush.White);
                        dc.FrameRectangle(rect, StockBrush.Black);

                        // Draw inward and outward black triangles
                        int cx = rect.right - rect.left;
                        int cy = rect.bottom - rect.top;

                        POINT[] pt = new POINT[3];

                        switch((int)pdis->CtlID)
                        {
                            case ID_SMALLER:
                                pt[0].x = 3 * cx / 8; pt[0].y = 1 * cy / 8;
                                pt[1].x = 5 * cx / 8; pt[1].y = 1 * cy / 8;
                                pt[2].x = 4 * cx / 8; pt[2].y = 3 * cy / 8;
                                Triangle(dc, pt);
                                pt[0].x = 7 * cx / 8; pt[0].y = 3 * cy / 8;
                                pt[1].x = 7 * cx / 8; pt[1].y = 5 * cy / 8;
                                pt[2].x = 5 * cx / 8; pt[2].y = 4 * cy / 8;
                                Triangle(dc, pt);
                                pt[0].x = 5 * cx / 8; pt[0].y = 7 * cy / 8;
                                pt[1].x = 3 * cx / 8; pt[1].y = 7 * cy / 8;
                                pt[2].x = 4 * cx / 8; pt[2].y = 5 * cy / 8;
                                Triangle(dc, pt);
                                pt[0].x = 1 * cx / 8; pt[0].y = 5 * cy / 8;
                                pt[1].x = 1 * cx / 8; pt[1].y = 3 * cy / 8;
                                pt[2].x = 3 * cx / 8; pt[2].y = 4 * cy / 8;
                                Triangle(dc, pt);
                                break;
                            case ID_LARGER:
                                pt[0].x = 5 * cx / 8; pt[0].y = 3 * cy / 8;
                                pt[1].x = 3 * cx / 8; pt[1].y = 3 * cy / 8;
                                pt[2].x = 4 * cx / 8; pt[2].y = 1 * cy / 8;
                                Triangle(dc, pt);
                                pt[0].x = 5 * cx / 8; pt[0].y = 5 * cy / 8;
                                pt[1].x = 5 * cx / 8; pt[1].y = 3 * cy / 8;
                                pt[2].x = 7 * cx / 8; pt[2].y = 4 * cy / 8;
                                Triangle(dc, pt);
                                pt[0].x = 3 * cx / 8; pt[0].y = 5 * cy / 8;
                                pt[1].x = 5 * cx / 8; pt[1].y = 5 * cy / 8;
                                pt[2].x = 4 * cx / 8; pt[2].y = 7 * cy / 8;
                                Triangle(dc, pt);
                                pt[0].x = 3 * cx / 8; pt[0].y = 3 * cy / 8;
                                pt[1].x = 3 * cx / 8; pt[1].y = 5 * cy / 8;
                                pt[2].x = 1 * cx / 8; pt[2].y = 4 * cy / 8;
                                Triangle(dc, pt);
                                break;
                        }

                        // Invert the rectangle if the button is selected
                        if ((pdis->itemState & OwnerDrawStates.Selected) != 0)
                            dc.InvertRectangle(rect);

                        if ((pdis->itemState & OwnerDrawStates.Focus) != 0)
                        {
                            rect.left += cx / 16;
                            rect.top += cy / 16;
                            rect.right -= cx / 16;
                            rect.bottom -= cy / 16;

                            dc.DrawFocusRectangle(rect);
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
