// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using WInterop.Gdi;
using WInterop.Support;
using WInterop.Windows.Native;

namespace WInterop.Windows;

public static partial class Windows
{
    public static LResult SendMessage<T>(
        this T window,
        ListBoxMessage message,
        WParam wParam = default,
        LParam lParam = default) where T : IHandle<WindowHandle>
        => SendMessage(window, (MessageType)message, wParam, lParam);

    public static LResult SendMessage<T>(
        this T window,
        MessageType message,
        WParam wParam = default,
        LParam lParam = default) where T : IHandle<WindowHandle>
        => WindowsImports.SendMessageW(window.Handle, message, wParam, lParam);

    public static unsafe FontHandle GetFont<T>(this T window, bool getSystemFontHandle = true)
        where T : IHandle<WindowHandle>
    {
        HFONT font = new(window.SendMessage(MessageType.GetFont));
        if (font == HFONT.NULL && getSystemFontHandle)
        {
            // Using the system font
            font = Gdi.Gdi.GetStockFont(StockFont.System);
        }

        return new FontHandle(font, ownsHandle: false);
    }

    public static unsafe void SetFont<T>(this T window, FontHandle font)
        where T : IHandle<WindowHandle>
    {
        window.SendMessage(
            MessageType.SetFont,
            (WParam)font.HFONT.Value,
            (0, IntBoolean.True));          // True to force a redraw
    }
}