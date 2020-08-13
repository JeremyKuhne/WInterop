// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using WInterop.Gdi;
using WInterop.Modules;
using WInterop.Multimedia;
using WInterop.Windows;

namespace HelloWin
{
    /// <summary>
    ///  Sample from Programming Windows, 5th Edition.
    ///  Original (c) Charles Petzold, 1998
    ///  Figure 3-1, Pages 44-46.
    /// </summary>
    internal static class Program
    {
        [STAThread]
        private static void Main()
        {
            const string szAppName = "HelloWin";

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
                "The Hello Program",
                WindowStyles.OverlappedWindow,
                instance: module,
                bounds: Windows.DefaultBounds);

            window.ShowWindow(ShowWindowCommand.Normal);
            window.UpdateWindow();

            while (Windows.GetMessage(out WindowMessage message))
            {
                Windows.TranslateMessage(ref message);
                Windows.DispatchMessage(ref message);
            }
        }

        private static LResult WindowProcedure(WindowHandle window, MessageType message, WParam wParam, LParam lParam)
        {
            switch (message)
            {
                case MessageType.Create:
                    Multimedia.PlaySound(PlaySoundAlias.SystemHand, PlaySoundOptions.Async | PlaySoundOptions.NoDefault);
                    return 0;
                case MessageType.Paint:
                    using (DeviceContext dc = window.BeginPaint())
                    {
                        dc.DrawText(
                            "Hello, Windows 98!",
                            window.GetClientRectangle(),
                            TextFormat.SingleLine | TextFormat.Center | TextFormat.VerticallyCenter);
                    }
                    return 0;
                case MessageType.Destroy:
                    Windows.PostQuitMessage(0);
                    return 0;
            }

            return Windows.DefaultWindowProcedure(window, message, wParam, lParam);
        }
    }
}
