// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Runtime.InteropServices;
using WInterop.ErrorHandling;
using WInterop.Extensions.WindowExtensions;
using WInterop.Gdi.Types;
using WInterop.Modules.Types;
using WInterop.Resources.Types;
using WInterop.Windows;
using WInterop.Windows.Types;

namespace Checker2
{
    /// <summary>
    /// Sample from Programming Windows, 5th Edition.
    /// Original (c) Charles Petzold, 1998
    /// Figure 7-6, Pages 293-296.
    /// </summary>
    static class Program
    {
        [STAThread]
        static void Main()
        {
            const string szAppName = "Checker2";

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
                "Checker2 Mouse Hit-Test Demo",
                WindowStyle.WS_OVERLAPPEDWINDOW);

            window.ShowWindow(ShowWindowCommand.SW_SHOWNORMAL);
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

        static LRESULT WindowProcedure(WindowHandle window, MessageType message, WPARAM wParam, LPARAM lParam)
        {
            switch (message)
            {
                case MessageType.WM_SIZE:
                    cxBlock = lParam.LowWord / DIVISIONS;
                    cyBlock = lParam.HighWord / DIVISIONS;
                    return 0;
                case MessageType.WM_SETFOCUS:
                    Windows.ShowCursor(true);
                    return 0;
                case MessageType.WM_KILLFOCUS:
                    Windows.ShowCursor(false);
                    return 0;
                case MessageType.WM_KEYDOWN:
                    POINT point = Windows.GetCursorPosition();
                    window.ScreenToClient(ref point);
                    int x = Math.Max(0, Math.Min(DIVISIONS - 1, point.x / cxBlock));
                    int y = Math.Max(0, Math.Min(DIVISIONS - 1, point.y / cyBlock));
                    switch ((VirtualKey)wParam)
                    {
                        case VirtualKey.VK_UP:
                            y--;
                            break;
                        case VirtualKey.VK_DOWN:
                            y++;
                            break;
                        case VirtualKey.VK_LEFT:
                            x--;
                            break;
                        case VirtualKey.VK_RIGHT:
                            x++;
                            break;
                        case VirtualKey.VK_HOME:
                            x = y = 0;
                            break;
                        case VirtualKey.VK_END:
                            x = y = DIVISIONS - 1;
                            break;
                        case VirtualKey.VK_RETURN:
                        case VirtualKey.VK_SPACE:
                            window.SendMessage(MessageType.WM_LBUTTONDOWN, (WPARAM)MouseKey.MK_LBUTTON,
                                new LPARAM(y * cyBlock, x * cxBlock));
                            break;
                    }
                    x = (x + DIVISIONS) % DIVISIONS;
                    y = (y + DIVISIONS) % DIVISIONS;
                    point.x = x * cxBlock + cxBlock / 2;
                    point.y = y * cyBlock + cyBlock / 2;
                    window.ClientToScreen(ref point);
                    Windows.SetCursorPosition(point.x, point.y);
                    return 0;
                case MessageType.WM_LBUTTONDOWN:
                    x = lParam.LowWord / cxBlock;
                    y = lParam.HighWord / cyBlock;
                    if (x < DIVISIONS && y < DIVISIONS)
                    {
                        fState[x, y] ^= true;
                        RECT rect = new RECT
                        {
                            left = x * cxBlock,
                            top = y * cyBlock,
                            right = (x + 1) * cxBlock,
                            bottom = (y + 1) * cyBlock
                        };
                        window.InvalidateRectangle(rect, false);
                    }
                    else
                    {
                        ErrorMethods.MessageBeep(0);
                    }

                    return 0;
                case MessageType.WM_PAINT:
                    using (DeviceContext dc = window.BeginPaint())
                    {
                        for (x = 0; x < DIVISIONS; x++)
                            for (y = 0; y < DIVISIONS; y++)
                            {
                                dc.Rectangle(x * cxBlock, y * cyBlock,
                                    (x + 1) * cxBlock, (y + 1) * cyBlock);
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
                case MessageType.WM_DESTROY:
                    Windows.PostQuitMessage(0);
                    return 0;
            }

            return Windows.DefaultWindowProcedure(window, message, wParam, lParam);
        }
    }
}
