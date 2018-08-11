// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Drawing;
using WInterop.Gdi;
using WInterop.Windows;
using WInterop.Windows.Native;

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
            Windows.CreateMainWindowAndRun(new OwnDraw(), "Owner-Draw Button Demo");
        }
    }

    class OwnDraw : WindowClass
    {
        void Triangle(DeviceContext dc, Point[] pt)
        {
            dc.SelectObject(StockBrush.Black);
            dc.Polygon(pt);
            dc.SelectObject(StockBrush.White);
        }

        WindowHandle hwndSmaller, hwndLarger;
        int cxClient, cyClient;
        int btnWidth, btnHeight;
        Size baseUnits;

        const int ID_SMALLER = 1;
        const int ID_LARGER = 2;

        protected unsafe override LResult WindowProcedure(WindowHandle window, MessageType message, WParam wParam, LParam lParam)
        {
            switch (message)
            {
                case MessageType.Create:
                    baseUnits = Windows.GetDialogBaseUnits();
                    btnWidth = baseUnits.Width * 8;
                    btnHeight = baseUnits.Height * 4;

                    // Create the owner-draw pushbuttons
                    hwndSmaller = Windows.CreateWindow("button", "",
                        WindowStyles.Child | WindowStyles.Visible | (WindowStyles)ButtonStyles.OwnerDrawn,
                        ExtendedWindowStyles.Default,
                        new Rectangle(0, 0, btnWidth, btnHeight),
                        window, (MenuHandle)ID_SMALLER, ModuleInstance, IntPtr.Zero);

                    hwndLarger = Windows.CreateWindow("button", "",
                        WindowStyles.Child | WindowStyles.Visible | (WindowStyles)ButtonStyles.OwnerDrawn,
                        ExtendedWindowStyles.Default,
                        new Rectangle(0, 0, btnWidth, btnHeight),
                        window, (MenuHandle)ID_LARGER, ModuleInstance, IntPtr.Zero);
                    return 0;
                case MessageType.Size:
                    cxClient = lParam.LowWord;
                    cyClient = lParam.HighWord;

                    // Move the buttons to the new center
                    hwndSmaller.MoveWindow(
                        new Rectangle(cxClient / 2 - 3 * btnWidth / 2, cyClient / 2 - btnHeight / 2, btnWidth, btnHeight),
                        repaint: true);
                    hwndLarger.MoveWindow(
                        new Rectangle(cxClient / 2 + btnWidth / 2, cyClient / 2 - btnHeight / 2, btnWidth, btnHeight),
                        repaint: true);
                    return 0;
                case MessageType.Command:
                    Rectangle rc = window.GetWindowRectangle();

                    // Make the window 10% smaller or larger
                    switch ((int)(uint)wParam)
                    {
                        case ID_SMALLER:
                            rc.Inflate(rc.Width / -10, rc.Height / -10);
                            break;
                        case ID_LARGER:
                            rc.Inflate(rc.Width / 10, rc.Height / 10);
                            break;
                    }

                    window.MoveWindow(new Rectangle(rc.Left, rc.Top, rc.Right - rc.Left, rc.Bottom - rc.Top), repaint: true);
                    return 0;
                case MessageType.DrawItem:
                    DRAWITEMSTRUCT* pdis = (DRAWITEMSTRUCT*)lParam;

                    // Fill area with white and frame it black
                    using (DeviceContext dc = new DeviceContext(pdis->hDC))
                    {
                        Rectangle rect = pdis->rcItem;

                        dc.FillRectangle(rect, StockBrush.White);
                        dc.FrameRectangle(rect, StockBrush.Black);

                        // Draw inward and outward black triangles
                        int cx = rect.Right - rect.Left;
                        int cy = rect.Bottom - rect.Top;

                        Point[] pt = new Point[3];

                        switch ((int)pdis->CtlID)
                        {
                            case ID_SMALLER:
                                pt[0].X = 3 * cx / 8; pt[0].Y = 1 * cy / 8;
                                pt[1].X = 5 * cx / 8; pt[1].Y = 1 * cy / 8;
                                pt[2].X = 4 * cx / 8; pt[2].Y = 3 * cy / 8;
                                Triangle(dc, pt);
                                pt[0].X = 7 * cx / 8; pt[0].Y = 3 * cy / 8;
                                pt[1].X = 7 * cx / 8; pt[1].Y = 5 * cy / 8;
                                pt[2].X = 5 * cx / 8; pt[2].Y = 4 * cy / 8;
                                Triangle(dc, pt);
                                pt[0].X = 5 * cx / 8; pt[0].Y = 7 * cy / 8;
                                pt[1].X = 3 * cx / 8; pt[1].Y = 7 * cy / 8;
                                pt[2].X = 4 * cx / 8; pt[2].Y = 5 * cy / 8;
                                Triangle(dc, pt);
                                pt[0].X = 1 * cx / 8; pt[0].Y = 5 * cy / 8;
                                pt[1].X = 1 * cx / 8; pt[1].Y = 3 * cy / 8;
                                pt[2].X = 3 * cx / 8; pt[2].Y = 4 * cy / 8;
                                Triangle(dc, pt);
                                break;
                            case ID_LARGER:
                                pt[0].X = 5 * cx / 8; pt[0].Y = 3 * cy / 8;
                                pt[1].X = 3 * cx / 8; pt[1].Y = 3 * cy / 8;
                                pt[2].X = 4 * cx / 8; pt[2].Y = 1 * cy / 8;
                                Triangle(dc, pt);
                                pt[0].X = 5 * cx / 8; pt[0].Y = 5 * cy / 8;
                                pt[1].X = 5 * cx / 8; pt[1].Y = 3 * cy / 8;
                                pt[2].X = 7 * cx / 8; pt[2].Y = 4 * cy / 8;
                                Triangle(dc, pt);
                                pt[0].X = 3 * cx / 8; pt[0].Y = 5 * cy / 8;
                                pt[1].X = 5 * cx / 8; pt[1].Y = 5 * cy / 8;
                                pt[2].X = 4 * cx / 8; pt[2].Y = 7 * cy / 8;
                                Triangle(dc, pt);
                                pt[0].X = 3 * cx / 8; pt[0].Y = 3 * cy / 8;
                                pt[1].X = 3 * cx / 8; pt[1].Y = 5 * cy / 8;
                                pt[2].X = 1 * cx / 8; pt[2].Y = 4 * cy / 8;
                                Triangle(dc, pt);
                                break;
                        }

                        // Invert the rectangle if the button is selected
                        if ((pdis->itemState & OwnerDrawStates.Selected) != 0)
                            dc.InvertRectangle(rect);

                        if ((pdis->itemState & OwnerDrawStates.Focus) != 0)
                        {
                            rect = Rectangle.FromLTRB(
                                rect.Left + cx / 16,
                                rect.Top + cy / 16,
                                rect.Right - cx / 16,
                                rect.Bottom - cy / 16);

                            dc.DrawFocusRectangle(rect);
                        }
                    }
                    return 0;
            }

            return base.WindowProcedure(window, message, wParam, lParam);
        }
    }
}
