// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Runtime.InteropServices;
using WInterop.Gdi;
using WInterop.Gdi.Types;
using WInterop.Modules.Types;
using WInterop.Resources;
using WInterop.Resources.Types;
using WInterop.Windows;
using WInterop.Windows.Types;

namespace Typer
{
    /// <summary>
    /// Sample from Programming Windows, 5th Edition.
    /// Original (c) Charles Petzold, 1998
    /// Figure 6-3, Pages 236-240.
    /// </summary>
    static class Program
    {
        [STAThread]
        static void Main()
        {
            const string szAppName = "Typer";

            SafeModuleHandle module = Marshal.GetHINSTANCE(typeof(Program).Module);
            WindowClass wndclass = new WindowClass
            {
                Style = WindowClassStyle.CS_HREDRAW | WindowClassStyle.CS_VREDRAW,
                WindowProcedure = WindowProcedure,
                ClassExtraBytes = 0,
                WindowExtraBytes = 0,
                Instance = module,
                Icon = ResourceMethods.LoadIcon(IconId.IDI_APPLICATION),
                Cursor = ResourceMethods.LoadCursor(CursorId.IDC_ARROW),
                Background = GdiMethods.GetStockBrush(StockBrush.WHITE_BRUSH),
                MenuName = null,
                ClassName = szAppName
            };

            WindowMethods.RegisterClass(wndclass);

            WindowHandle window = WindowMethods.CreateWindow(
                module,
                szAppName,
                "Typing Program",
                WindowStyle.WS_OVERLAPPEDWINDOW);

            WindowMethods.ShowWindow(window, ShowWindowCommand.SW_SHOWNORMAL);

            while (WindowMethods.GetMessage(out MSG message, WindowHandle.Null, 0, 0))
            {
                WindowMethods.TranslateMessage(ref message);
                WindowMethods.DispatchMessage(ref message);
            }

            GC.KeepAlive(wndclass);
        }

        static int cxChar, cyChar, cxClient, cyClient, cxBuffer, cyBuffer, xCaret, yCaret;
        static CharacterSet dwCharSet = CharacterSet.DEFAULT_CHARSET;
        static char[,] pBuffer;

        static LRESULT WindowProcedure(WindowHandle window, MessageType message, WPARAM wParam, LPARAM lParam)
        {
            switch (message)
            {
                case MessageType.WM_INPUTLANGCHANGE:
                    dwCharSet = (CharacterSet)(uint)wParam;
                    goto case MessageType.WM_CREATE;
                case MessageType.WM_CREATE:
                    using (DeviceContext dc = GdiMethods.GetDeviceContext(window))
                    {
                        using (FontHandle font = GdiMethods.CreateFont(0, 0, 0, 0, FontWeight.FW_DONTCARE, false, false, false, dwCharSet,
                            OutputPrecision.OUT_DEFAULT_PRECIS, ClippingPrecision.CLIP_DEFAULT_PRECIS, Quality.DEFAULT_QUALITY, PitchAndFamily.TMPF_FIXED_PITCH, null))
                        {
                            GdiMethods.SelectObject(dc, font);
                            GdiMethods.GetTextMetrics(dc, out TEXTMETRIC tm);
                            cxChar = tm.tmAveCharWidth;
                            cyChar = tm.tmHeight;
                            GdiMethods.SelectObject(dc, GdiMethods.GetStockFont(StockFont.SYSTEM_FONT));
                        }
                    }
                    goto CalculateSize;
                case MessageType.WM_SIZE:
                    cxClient = lParam.LowWord;
                    cyClient = lParam.HighWord;

                    CalculateSize:

                    // calculate window size in characters
                    cxBuffer = Math.Max(1, cxClient / cxChar);
                    cyBuffer = Math.Max(1, cyClient / cyChar);

                    // allocate memory for buffer and clear it
                    pBuffer = new char[cxBuffer, cyBuffer];

                    // set caret to upper left corner
                    xCaret = 0;
                    yCaret = 0;

                    if (window == WindowMethods.GetFocus())
                        ResourceMethods.SetCaretPosition(xCaret * cxChar, yCaret * cyChar);

                    GdiMethods.InvalidateRectangle(window, true);
                    return 0;
                case MessageType.WM_SETFOCUS:
                    // create and show the caret
                    ResourceMethods.CreateCaret(window, null, cxChar, cyChar);
                    ResourceMethods.SetCaretPosition(xCaret * cxChar, yCaret * cyChar);
                    ResourceMethods.ShowCaret(window);
                    return 0;
                case MessageType.WM_KILLFOCUS:
                    // hide and destroy the caret
                    ResourceMethods.HideCaret(window);
                    ResourceMethods.DestroyCaret();
                    return 0;
                case MessageType.WM_KEYDOWN:
                    switch ((VirtualKey)wParam)
                    {
                        case VirtualKey.VK_HOME:
                            xCaret = 0;
                            break;
                        case VirtualKey.VK_END:
                            xCaret = cxBuffer - 1;
                            break;
                        case VirtualKey.VK_PRIOR:
                            yCaret = 0;
                            break;
                        case VirtualKey.VK_NEXT:
                            yCaret = cyBuffer - 1;
                            break;
                        case VirtualKey.VK_LEFT:
                            xCaret = Math.Max(xCaret - 1, 0);
                            break;
                        case VirtualKey.VK_RIGHT:
                            xCaret = Math.Min(xCaret + 1, cxBuffer - 1);
                            break;
                        case VirtualKey.VK_UP:
                            yCaret = Math.Max(yCaret - 1, 0);
                            break;
                        case VirtualKey.VK_DOWN:
                            yCaret = Math.Min(yCaret + 1, cyBuffer - 1);
                            break;
                        case VirtualKey.VK_DELETE:
                            for (int x = xCaret; x < cxBuffer - 1; x++)
                                pBuffer[x, yCaret] = pBuffer[x + 1, yCaret];

                            pBuffer[cxBuffer - 1, yCaret] = ' ';
                            ResourceMethods.HideCaret(window);
                            using (DeviceContext dc = GdiMethods.GetDeviceContext(window))
                            {
                                using (FontHandle font = GdiMethods.CreateFont(0, 0, 0, 0, FontWeight.FW_DONTCARE, false, false, false, dwCharSet,
                                    OutputPrecision.OUT_DEFAULT_PRECIS, ClippingPrecision.CLIP_DEFAULT_PRECIS, Quality.DEFAULT_QUALITY, PitchAndFamily.TMPF_FIXED_PITCH, null))
                                {
                                    GdiMethods.SelectObject(dc, font);
                                    unsafe
                                    {
                                        fixed (char* c = &pBuffer[xCaret, yCaret])
                                            GdiMethods.TextOut(dc, xCaret * cxChar, yCaret * cyChar, c, cxBuffer - xCaret);
                                    }
                                    GdiMethods.SelectObject(dc, GdiMethods.GetStockFont(StockFont.SYSTEM_FONT));
                                }

                                ResourceMethods.ShowCaret(window);
                            }
                            break;
                    }
                    ResourceMethods.SetCaretPosition(xCaret * cxChar, yCaret * cyChar);
                    return 0;

                case MessageType.WM_CHAR:
                    for (int i = 0; i < lParam.LowWord; i++)
                    {
                        switch ((char)wParam)
                        {
                            case '\b': // backspace
                                if (xCaret > 0)
                                {
                                    xCaret--;
                                    WindowMethods.SendMessage(window, MessageType.WM_KEYDOWN, (uint)VirtualKey.VK_DELETE, 1);
                                }
                                break;
                            case '\t': // tab
                                do
                                {
                                    WindowMethods.SendMessage(window, MessageType.WM_CHAR, ' ', 1);
                                } while (xCaret % 8 != 0);
                                break;
                            case '\n': // line feed
                                if (++yCaret == cyBuffer)
                                    yCaret = 0;
                                break;
                            case '\r': // carriage return
                                xCaret = 0;
                                if (++yCaret == cyBuffer)
                                    yCaret = 0;
                                break;
                            case '\x1B': // escape
                                for (int y = 0; y < cyBuffer; y++)
                                    for (int x = 0; x < cxBuffer; x++)
                                        pBuffer[x, y] = ' ';
                                xCaret = 0;
                                yCaret = 0;
                                GdiMethods.InvalidateRectangle(window, false);
                                break;
                            default: // character codes
                                pBuffer[xCaret, yCaret] = (char)wParam;
                                ResourceMethods.HideCaret(window);
                                using (DeviceContext dc = GdiMethods.GetDeviceContext(window))
                                {
                                    using (FontHandle font = GdiMethods.CreateFont(0, 0, 0, 0, FontWeight.FW_DONTCARE, false, false, false, dwCharSet,
                                        OutputPrecision.OUT_DEFAULT_PRECIS, ClippingPrecision.CLIP_DEFAULT_PRECIS, Quality.DEFAULT_QUALITY, PitchAndFamily.TMPF_FIXED_PITCH, null))
                                    {
                                        GdiMethods.SelectObject(dc, font);
                                        unsafe
                                        {
                                            fixed (char* c = &pBuffer[xCaret, yCaret])
                                                GdiMethods.TextOut(dc, xCaret * cxChar, yCaret * cyChar, c, 1);
                                        }
                                        GdiMethods.SelectObject(dc, GdiMethods.GetStockFont(StockFont.SYSTEM_FONT));
                                    }

                                    ResourceMethods.ShowCaret(window);
                                }

                                if (++xCaret == cxBuffer)
                                {
                                    xCaret = 0;
                                    if (++yCaret == cyBuffer)
                                        yCaret = 0;
                                }
                                break;
                        }
                    }
                    ResourceMethods.SetCaretPosition(xCaret * cxChar, yCaret * cyChar);
                    return 0;
                case MessageType.WM_PAINT:
                    using (DeviceContext dc = GdiMethods.BeginPaint(window))
                    {
                        using (FontHandle font = GdiMethods.CreateFont(0, 0, 0, 0, FontWeight.FW_DONTCARE, false, false, false, dwCharSet,
                            OutputPrecision.OUT_DEFAULT_PRECIS, ClippingPrecision.CLIP_DEFAULT_PRECIS, Quality.DEFAULT_QUALITY, PitchAndFamily.TMPF_FIXED_PITCH, null))
                        {
                            GdiMethods.SelectObject(dc, font);
                            unsafe
                            {
                                for (int y = 0; y < cyBuffer; y++)
                                    fixed (char* c = &pBuffer[0, y])
                                        GdiMethods.TextOut(dc, 0, y * cyChar, c, cxBuffer);
                            }
                            GdiMethods.SelectObject(dc, GdiMethods.GetStockFont(StockFont.SYSTEM_FONT));
                        }
                    }
                    return 0;
                case MessageType.WM_DESTROY:
                    WindowMethods.PostQuitMessage(0);
                    return 0;
            }

            return WindowMethods.DefaultWindowProcedure(window, message, wParam, lParam);
        }
    }
}
