// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Drawing;
using WInterop.Gdi;
using WInterop.Windows;

namespace Beeper
{
    internal class Beeper2 : WindowClass
    {
        private static bool s_fFlipFlop = false;
        private const int ID_TIMER = 1;

        protected override LResult WindowProcedure(WindowHandle window, MessageType message, WParam wParam, LParam lParam)
        {
            switch (message)
            {
                case MessageType.Create:
                    window.SetTimer(ID_TIMER, 1000, TimerProcedure);
                    return 0;
                case MessageType.Destroy:
                    window.KillTimer(ID_TIMER);
                    break;
            }

            return base.WindowProcedure(window, message, wParam, lParam);
        }

        private static void TimerProcedure(WindowHandle window, MessageType message, TimerId timerId, uint time)
        {
            Windows.MessageBeep();
            s_fFlipFlop = !s_fFlipFlop;
            using DeviceContext dc = window.GetDeviceContext();
            using BrushHandle brush = Gdi.CreateSolidBrush(s_fFlipFlop ? Color.Red : Color.Blue);
            dc.FillRectangle(window.GetClientRectangle(), brush);
        }
    }
}
