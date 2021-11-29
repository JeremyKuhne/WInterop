// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Drawing;
using WInterop.Gdi;
using WInterop.Windows;

namespace SineWave;

/// <summary>
///  Sample from Programming Windows, 5th Edition.
///  Original (c) Charles Petzold, 1998
///  Figure 5-6, Pages 147-148.
/// </summary>
internal static class Program
{
    [STAThread]
    private static void Main()
    {
        Windows.CreateMainWindowAndRun(new SineWave(), "Sine Wave Using PolyLine");
    }
}

internal class SineWave : WindowClass
{
    private int _cxClient, _cyClient;
    private readonly Point[] _apt = new Point[1000];

    protected override LResult WindowProcedure(WindowHandle window, MessageType message, WParam wParam, LParam lParam)
    {
        switch (message)
        {
            case MessageType.Size:
                _cxClient = lParam.LowWord;
                _cyClient = lParam.HighWord;
                return 0;
            case MessageType.Paint:
                using (DeviceContext dc = window.BeginPaint())
                {
                    dc.MoveTo(new Point(0, _cyClient / 2));
                    dc.LineTo(new Point(_cxClient, _cyClient / 2));

                    for (int i = 0; i < _apt.Length; i++)
                    {
                        _apt[i].X = i * _cxClient / _apt.Length;
                        _apt[i].Y = (int)(_cyClient / 2 * (1 - Math.Sin(Math.PI * 2 * i / _apt.Length)));
                    }
                    dc.Polyline(_apt);
                }
                return 0;
        }

        return base.WindowProcedure(window, message, wParam, lParam);
    }
}
