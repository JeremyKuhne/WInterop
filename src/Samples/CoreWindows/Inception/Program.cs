// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Drawing;
using WInterop.Gdi;
using WInterop.Windows;

namespace BallBounce
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Windows.CreateMainWindowAndRun(new Inception(), "Inception");
        }
    }

    public class Inception : WindowClass
    {
        Rectangle _client;
        Rectangle _screen;

        public Inception() : base(backgroundBrush: BrushHandle.NoBrush) { }

        protected override LRESULT WindowProcedure(WindowHandle window, WindowMessage message, WPARAM wParam, LPARAM lParam)
        {
            switch (message)
            {
                case WindowMessage.Create:
                    _screen = new Rectangle(0, 0, Windows.GetSystemMetrics(SystemMetric.ScreenWidth), Windows.GetSystemMetrics(SystemMetric.ScreenHeight));
                    return 0;
                case WindowMessage.Size:
                    _client = new Rectangle(default, new Message.Size(wParam, lParam).NewSize);
                    return 0;
                case WindowMessage.Move:
                    window.Invalidate();
                    return 0;
                case WindowMessage.Paint:
                    using (DeviceContext clientDC = window.BeginPaint())
                    using (DeviceContext screenDC = Gdi.GetDeviceContext())
                    using (DeviceContext memoryDC = clientDC.CreateCompatibleDeviceContext())
                    {
                        clientDC.SetStretchBlitMode(StretchMode.HalfTone);
                        screenDC.StretchBlit(clientDC, _screen, _client, RasterOperation.Common.SourceCopy);
                    }
                    return 0;
            }

            return base.WindowProcedure(window, message, wParam, lParam);
        }
    }
}
