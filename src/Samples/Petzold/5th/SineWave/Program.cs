// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Drawing;
using WInterop.Gdi;
using WInterop.Windows;

namespace SineWave
{
    /// <summary>
    /// Sample from Programming Windows, 5th Edition.
    /// Original (c) Charles Petzold, 1998
    /// Figure 5-6, Pages 147-148.
    /// </summary>
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Windows.CreateMainWindowAndRun(new SineWave(), "Sine Wave Using PolyLine");
        }
    }

    class SineWave : WindowClass
    {
        int cxClient, cyClient;
        Point[] apt = new Point[1000];

        protected override LRESULT WindowProcedure(WindowHandle window, WindowMessage message, WPARAM wParam, LPARAM lParam)
        {
            switch (message)
            {
                case WindowMessage.Size:
                    cxClient = lParam.LowWord;
                    cyClient = lParam.HighWord;
                    return 0;
                case WindowMessage.Paint:
                    using (DeviceContext dc = window.BeginPaint())
                    {
                        dc.MoveTo(new Point(0, cyClient / 2));
                        dc.LineTo(new Point(cxClient, cyClient / 2));

                        for (int i = 0; i < apt.Length; i++)
                        {
                            apt[i].X = i * cxClient / apt.Length;
                            apt[i].Y = (int)(cyClient / 2 * (1 - Math.Sin(Math.PI * 2 * i / apt.Length)));
                        }
                        dc.Polyline(apt);
                    }
                    return 0;
            }

            return base.WindowProcedure(window, message, wParam, lParam);
        }
    }
}
