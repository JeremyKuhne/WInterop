// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using WInterop.Gdi;
using WInterop.Modules.Types;
using WInterop.Resources.Types;
using WInterop.Windows;

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
                "Typing Program",
                WindowStyles.OverlappedWindow);

            window.ShowWindow(ShowWindow.Normal);
            window.UpdateWindow();

            while (Windows.GetMessage(out MSG message))
            {
                Windows.TranslateMessage(ref message);
                Windows.DispatchMessage(ref message);
            }
        }

        static int cxChar, cyChar, cxClient, cyClient, cxBuffer, cyBuffer, xCaret, yCaret;
        static CharacterSet dwCharSet = CharacterSet.DEFAULT_CHARSET;
        static char[,] pBuffer;

        static LRESULT WindowProcedure(WindowHandle window, WindowMessage message, WPARAM wParam, LPARAM lParam)
        {
            switch (message)
            {
                case WindowMessage.InputLanguageChange:
                    dwCharSet = (CharacterSet)(uint)wParam;
                    goto case WindowMessage.Create;
                case WindowMessage.Create:
                    using (DeviceContext dc = window.GetDeviceContext())
                    {
                        using (FontHandle font = Windows.CreateFont(0, 0, 0, 0, FontWeight.DoNotCare, false, false, false, dwCharSet,
                            OutputPrecision.Default, ClippingPrecision.Default, Quality.Default, FontPitch.FixedPitch, FontFamily.DoNotCare, null))
                        {
                            dc.SelectObject(font);
                            dc.GetTextMetrics(out TEXTMETRIC tm);
                            cxChar = tm.tmAveCharWidth;
                            cyChar = tm.tmHeight;
                            dc.SelectObject(StockFont.System);
                        }
                    }
                    goto CalculateSize;
                case WindowMessage.Size:
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

                    if (window == Windows.GetFocus())
                        Windows.SetCaretPosition(xCaret * cxChar, yCaret * cyChar);

                    window.Invalidate(true);
                    return 0;
                case WindowMessage.SetFocus:
                    // create and show the caret
                    window.CreateCaret(default, cxChar, cyChar);
                    Windows.SetCaretPosition(xCaret * cxChar, yCaret * cyChar);
                    window.ShowCaret();
                    return 0;
                case WindowMessage.KillFocus:
                    // hide and destroy the caret
                    window.HideCaret();
                    Windows.DestroyCaret();
                    return 0;
                case WindowMessage.KeyDown:
                    switch ((VirtualKey)wParam)
                    {
                        case VirtualKey.Home:
                            xCaret = 0;
                            break;
                        case VirtualKey.End:
                            xCaret = cxBuffer - 1;
                            break;
                        case VirtualKey.Prior:
                            yCaret = 0;
                            break;
                        case VirtualKey.Next:
                            yCaret = cyBuffer - 1;
                            break;
                        case VirtualKey.Left:
                            xCaret = Math.Max(xCaret - 1, 0);
                            break;
                        case VirtualKey.Right:
                            xCaret = Math.Min(xCaret + 1, cxBuffer - 1);
                            break;
                        case VirtualKey.Up:
                            yCaret = Math.Max(yCaret - 1, 0);
                            break;
                        case VirtualKey.Down:
                            yCaret = Math.Min(yCaret + 1, cyBuffer - 1);
                            break;
                        case VirtualKey.Delete:
                            for (int x = xCaret; x < cxBuffer - 1; x++)
                                pBuffer[x, yCaret] = pBuffer[x + 1, yCaret];

                            pBuffer[cxBuffer - 1, yCaret] = ' ';
                            window.HideCaret();
                            using (DeviceContext dc = window.GetDeviceContext())
                            {
                                using (FontHandle font = Windows.CreateFont(0, 0, 0, 0, FontWeight.DoNotCare, false, false, false, dwCharSet,
                                    OutputPrecision.Default, ClippingPrecision.Default, Quality.Default, FontPitch.FixedPitch, FontFamily.DoNotCare, null))
                                {
                                    dc.SelectObject(font);
                                    unsafe
                                    {
                                        fixed (char* c = &pBuffer[xCaret, yCaret])
                                            dc.TextOut(xCaret * cxChar, yCaret * cyChar, c, cxBuffer - xCaret);
                                    }
                                    dc.SelectObject(StockFont.System);
                                }

                                window.ShowCaret();
                            }
                            break;
                    }
                    Windows.SetCaretPosition(xCaret * cxChar, yCaret * cyChar);
                    return 0;

                case WindowMessage.Char:
                    for (int i = 0; i < lParam.LowWord; i++)
                    {
                        switch ((char)wParam)
                        {
                            case '\b': // backspace
                                if (xCaret > 0)
                                {
                                    xCaret--;
                                    window.SendMessage(WindowMessage.KeyDown, (uint)VirtualKey.Delete, 1);
                                }
                                break;
                            case '\t': // tab
                                do
                                {
                                    window.SendMessage(WindowMessage.Char, ' ', 1);
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
                                window.Invalidate(false);
                                break;
                            default: // character codes
                                pBuffer[xCaret, yCaret] = (char)wParam;
                                window.HideCaret();
                                using (DeviceContext dc = window.GetDeviceContext())
                                {
                                    using (FontHandle font = Windows.CreateFont(0, 0, 0, 0, FontWeight.DoNotCare, false, false, false, dwCharSet,
                                        OutputPrecision.Default, ClippingPrecision.Default, Quality.Default, FontPitch.FixedPitch, FontFamily.DoNotCare, null))
                                    {
                                        dc.SelectObject(font);
                                        unsafe
                                        {
                                            fixed (char* c = &pBuffer[xCaret, yCaret])
                                                dc.TextOut(xCaret * cxChar, yCaret * cyChar, c, 1);
                                        }
                                        dc.SelectObject(StockFont.System);
                                    }

                                    window.ShowCaret();
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
                    Windows.SetCaretPosition(xCaret * cxChar, yCaret * cyChar);
                    return 0;
                case WindowMessage.Paint:
                    using (DeviceContext dc = window.BeginPaint())
                    {
                        using (FontHandle font = Windows.CreateFont(0, 0, 0, 0, FontWeight.DoNotCare, false, false, false, dwCharSet,
                            OutputPrecision.Default, ClippingPrecision.Default, Quality.Default, FontPitch.FixedPitch, FontFamily.DoNotCare, null))
                        {
                            dc.SelectObject(font);
                            unsafe
                            {
                                for (int y = 0; y < cyBuffer; y++)
                                    fixed (char* c = &pBuffer[0, y])
                                        dc.TextOut(0, y * cyChar, c, cxBuffer);
                            }
                            dc.SelectObject(StockFont.System);
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
