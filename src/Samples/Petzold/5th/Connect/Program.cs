// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Drawing;
using WInterop.Gdi;
using WInterop.Windows;

namespace Connect;

/// <summary>
///  Sample from Programming Windows, 5th Edition.
///  Original (c) Charles Petzold, 1998
///  Figure 7-1, Pages 278-280.
/// </summary>
internal static class Program
{
    [STAThread]
    private static void Main()
    {
        Windows.CreateMainWindowAndRun(new Connect(), "Connect-the-Points Mouse Demo");
    }
}

internal class Connect : WindowClass
{
    private const int MAXPOINTS = 1000;
    private const int TakeEvery = 10;
    private readonly Point[] pt = new Point[MAXPOINTS];
    private int iCount;
    private int sampleCount;

    protected override LResult WindowProcedure(WindowHandle window, MessageType message, WParam wParam, LParam lParam)
    {
        switch (message)
        {
            case MessageType.LeftButtonDown:
                iCount = 0;
                window.Invalidate(true);
                return 0;
            case MessageType.MouseMove:
                // Machines are way to fast to make this look interesting now, adding TakeEvery
                if ((MouseKey)wParam == MouseKey.LeftButton && iCount < MAXPOINTS && (sampleCount++ % TakeEvery == 0))
                {
                    Point point = new(lParam.LowWord, lParam.HighWord);
                    pt[iCount++] = point;

                    using DeviceContext dc = window.GetDeviceContext();
                    dc.SetPixel(point, default);
                }
                return 0;
            case MessageType.LeftButtonUp:
                window.Invalidate(false);
                return 0;
            case MessageType.Paint:
                using (DeviceContext dc = window.BeginPaint())
                {
                    Windows.SetCursor(CursorId.Wait);
                    Windows.ShowCursor(true);

                    for (int i = 0; i < iCount - 1; i++)
                        for (int j = i + 1; j < iCount; j++)
                        {
                            dc.MoveTo(pt[i]);
                            dc.LineTo(pt[j]);
                        }

                    Windows.ShowCursor(false);
                    Windows.SetCursor(CursorId.Arrow);
                }
                return 0;
        }

        return base.WindowProcedure(window, message, wParam, lParam);
    }
}
