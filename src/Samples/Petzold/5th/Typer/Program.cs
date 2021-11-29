// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Drawing;
using WInterop.Gdi;
using WInterop.Windows;

namespace Typer;

/// <summary>
///  Sample from Programming Windows, 5th Edition.
///  Original (c) Charles Petzold, 1998
///  Figure 6-3, Pages 236-240.
/// </summary>
internal static class Program
{
    [STAThread]
    private static void Main()
    {
        Windows.CreateMainWindowAndRun(new Typer(), "Typing Program");
    }
}

internal class Typer : WindowClass
{
    private int cxChar, cyChar, cxClient, cyClient, cxBuffer, cyBuffer, xCaret, yCaret;
    private CharacterSet dwCharSet = CharacterSet.Default;
    private char[,] pBuffer;

    protected override LResult WindowProcedure(WindowHandle window, MessageType message, WParam wParam, LParam lParam)
    {
        switch (message)
        {
            case MessageType.InputLanguageChange:
                dwCharSet = (CharacterSet)(uint)wParam;
                goto case MessageType.Create;
            case MessageType.Create:
                using (DeviceContext dc = window.GetDeviceContext())
                {
                    using (FontHandle font = Gdi.CreateFont(characterSet: dwCharSet, pitch: FontPitch.FixedPitch))
                    {
                        dc.SelectObject(font);
                        dc.GetTextMetrics(out TextMetrics tm);
                        cxChar = tm.AverageCharWidth;
                        cyChar = tm.Height;
                        dc.SelectObject(StockFont.System);
                    }
                }
                goto CalculateSize;
            case MessageType.Size:
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
                    Windows.SetCaretPosition(new Point(xCaret * cxChar, yCaret * cyChar));

                window.Invalidate(true);
                return 0;
            case MessageType.SetFocus:
                // create and show the caret
                window.CreateCaret(default, new Size(cxChar, cyChar));
                Windows.SetCaretPosition(new Point(xCaret * cxChar, yCaret * cyChar));
                window.ShowCaret();
                return 0;
            case MessageType.KillFocus:
                // hide and destroy the caret
                window.HideCaret();
                Windows.DestroyCaret();
                return 0;
            case MessageType.KeyDown:
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
                            using (FontHandle font = Gdi.CreateFont(characterSet: dwCharSet, pitch: FontPitch.FixedPitch))
                            {
                                dc.SelectObject(font);
                                unsafe
                                {
                                    fixed (char* c = &pBuffer[xCaret, yCaret])
                                        dc.TextOut(
                                            new Point(xCaret * cxChar, yCaret * cyChar),
                                            new ReadOnlySpan<char>(c, cxBuffer - xCaret));
                                }
                                dc.SelectObject(StockFont.System);
                            }

                            window.ShowCaret();
                        }
                        break;
                }
                Windows.SetCaretPosition(new Point(xCaret * cxChar, yCaret * cyChar));
                return 0;

            case MessageType.Char:
                for (int i = 0; i < lParam.LowWord; i++)
                {
                    switch ((char)wParam)
                    {
                        case '\b': // backspace
                            if (xCaret > 0)
                            {
                                xCaret--;
                                window.SendMessage(MessageType.KeyDown, (uint)VirtualKey.Delete, 1);
                            }
                            break;
                        case '\t': // tab
                            do
                            {
                                window.SendMessage(MessageType.Char, ' ', 1);
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
                                using (FontHandle font = Gdi.CreateFont(characterSet: dwCharSet, pitch: FontPitch.FixedPitch))
                                {
                                    dc.SelectObject(font);
                                    unsafe
                                    {
                                        fixed (char* c = &pBuffer[xCaret, yCaret])
                                            dc.TextOut(
                                                new Point(xCaret * cxChar, yCaret * cyChar),
                                                new ReadOnlySpan<char>(c, 1));
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
                Windows.SetCaretPosition(new Point(xCaret * cxChar, yCaret * cyChar));
                return 0;
            case MessageType.Paint:
                using (DeviceContext dc = window.BeginPaint())
                {
                    using (FontHandle font = Gdi.CreateFont(0, 0, 0, 0, FontWeight.DoNotCare, false, false, false, dwCharSet,
                        OutputPrecision.Default, ClippingPrecision.Default, Quality.Default, FontPitch.FixedPitch, FontFamilyType.DoNotCare, null))
                    {
                        dc.SelectObject(font);
                        unsafe
                        {
                            for (int y = 0; y < cyBuffer; y++)
                                fixed (char* c = &pBuffer[0, y])
                                    dc.TextOut(new Point(0, y * cyChar), new ReadOnlySpan<char>(c, cxBuffer));
                        }
                        dc.SelectObject(StockFont.System);
                    }
                }
                return 0;
        }

        return base.WindowProcedure(window, message, wParam, lParam);
    }
}
