// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Drawing;
using WInterop.Gdi;
using WInterop.Modules;
using WInterop.Windows;

namespace LineDemo;

/// <summary>
///  Sample from Programming Windows, 5th Edition.
///  Original (c) Charles Petzold, 1998
///  Figure 5-14, Pages 153-155.
/// </summary>
internal static class Program
{
    [STAThread]
    private static void Main()
    {
        ModuleInstance module = ModuleInstance.GetModuleForType(typeof(Program));
        WindowClassInfo wndclass = new WindowClassInfo
        {
            Style = ClassStyle.HorizontalRedraw | ClassStyle.VerticalRedraw,
            WindowProcedure = WindowProcedure,
            Instance = module,
            Icon = IconId.Application,
            Cursor = CursorId.Arrow,
            Background = StockBrush.White,
            ClassName = "LineDemo"
        };

        Windows.RegisterClass(ref wndclass);

        WindowHandle window = Windows.CreateWindow(
            "LineDemo",
            "Line Demonstration",
            WindowStyles.OverlappedWindow,
            instance: module);

        window.ShowWindow(ShowWindowCommand.Normal);
        window.UpdateWindow();

        while (Windows.GetMessage(out WindowMessage message))
        {
            Windows.TranslateMessage(ref message);
            Windows.DispatchMessage(ref message);
        }
    }

    private static int s_cxClient, s_cyClient;

    private static LResult WindowProcedure(WindowHandle window, MessageType message, WParam wParam, LParam lParam)
    {
        switch (message)
        {
            case MessageType.Size:
                s_cxClient = lParam.LowWord;
                s_cyClient = lParam.HighWord;
                return 0;
            case MessageType.Paint:
                using (DeviceContext dc = window.BeginPaint())
                {
                    dc.Rectangle(Rectangle.FromLTRB(s_cxClient / 8, s_cyClient / 8, 7 * s_cxClient / 8, 7 * s_cyClient / 8));
                    dc.MoveTo(new Point(0, 0));
                    dc.LineTo(new Point(s_cxClient, s_cyClient));
                    dc.MoveTo(new Point(0, s_cyClient));
                    dc.LineTo(new Point(s_cxClient, 0));
                    dc.Ellipse(Rectangle.FromLTRB(s_cxClient / 8, s_cyClient / 8, 7 * s_cxClient / 8, 7 * s_cyClient / 8));
                    dc.RoundRectangle(
                        Rectangle.FromLTRB(s_cxClient / 4, s_cyClient / 4, 3 * s_cxClient / 4, 3 * s_cyClient / 4),
                        new Size(s_cxClient / 4, s_cyClient / 4));
                }
                return 0;
            case MessageType.Destroy:
                Windows.PostQuitMessage(0);
                return 0;
        }

        return Windows.DefaultWindowProcedure(window, message, wParam, lParam);
    }
}
