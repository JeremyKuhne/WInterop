﻿// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Drawing;
using WInterop.Gdi;
using WInterop.Windows;

namespace Beeper;

internal class Beeper1 : WindowClass
{
    private bool _flipFlop = false;
    private const int ID_TIMER = 1;

    protected override LResult WindowProcedure(WindowHandle window, MessageType message, WParam wParam, LParam lParam)
    {
        switch (message)
        {
            case MessageType.Create:
                window.SetTimer(ID_TIMER, 1000);
                return 0;
            case MessageType.Timer:
                Windows.MessageBeep();
                _flipFlop = !_flipFlop;
                window.Invalidate();
                return 0;
            case MessageType.Paint:
                using (DeviceContext dc = window.BeginPaint())
                {
                    using BrushHandle brush = Gdi.CreateSolidBrush(_flipFlop ? Color.Red : Color.Blue);
                    dc.FillRectangle(window.GetClientRectangle(), brush);
                }
                return 0;
            case MessageType.Destroy:
                window.KillTimer(ID_TIMER);
                break;
        }

        return base.WindowProcedure(window, message, wParam, lParam);
    }
}
