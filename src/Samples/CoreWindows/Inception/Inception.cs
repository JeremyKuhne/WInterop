// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Drawing;
using WInterop.Gdi;
using WInterop.Windows;

namespace Inception
{
    public class Inception : WindowClass
    {
        private Rectangle _client;
        private Rectangle _screen;
        private MonitorHandle _initialMonitor;
        private TimerId _timerId = new TimerId(1);

        public Inception() : base(backgroundBrush: BrushHandle.NoBrush) { }

        protected override LRESULT WindowProcedure(WindowHandle window, WindowMessage message, WPARAM wParam, LPARAM lParam)
        {
            switch (message)
            {
                case WindowMessage.Create:
                    _initialMonitor = window.MonitorFromWindow();
                    _screen = Windows.GetMonitorInfo(_initialMonitor).Monitor;
                    window.SetTimer(_timerId, 200);
                    return 0;
                case WindowMessage.Size:
                    _client = new Rectangle(default, new Message.Size(wParam, lParam).NewSize);
                    return 0;
                case WindowMessage.Timer:
                    // Update via timer if we aren't primarily on the main monitor
                    if (window.MonitorFromWindow() != _initialMonitor)
                        window.Invalidate();
                    return 0;
                case WindowMessage.Move:
                    window.Invalidate();
                    return 0;
                case WindowMessage.Paint:
                    using (DeviceContext clientDC = window.BeginPaint())
                    using (DeviceContext screenDC = Gdi.GetDeviceContext())
                    {
                        clientDC.SetStretchBlitMode(StretchMode.HalfTone);
                        screenDC.StretchBlit(clientDC, _screen, _client, RasterOperation.Common.SourceCopy);
                    }
                    return 0;
                case WindowMessage.Destroy:
                    window.KillTimer(_timerId);
                    break;
            }

            return base.WindowProcedure(window, message, wParam, lParam);
        }
    }
}
