// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Drawing;
using WInterop.Gdi;
using WInterop.Windows;

namespace Checker;

internal class Checker4 : WindowClass
{
    private const int DIVISIONS = 5;
    private readonly WindowHandle[,] _hwndChild = new WindowHandle[DIVISIONS, DIVISIONS];
    private int _cxBlock, _cyBlock;
    public static int idFocus = 0;
    private readonly Checker4Child _childClass = (Checker4Child)(new Checker4Child().Register());

    protected override LResult WindowProcedure(WindowHandle window, MessageType message, WParam wParam, LParam lParam)
    {
        int x, y;
        switch (message)
        {
            case MessageType.Create:
                for (x = 0; x < DIVISIONS; x++)
                    for (y = 0; y < DIVISIONS; y++)
                        _hwndChild[x, y] = _childClass.CreateWindow(
                            Windows.DefaultBounds,
                            style: WindowStyles.ChildWindow | WindowStyles.Visible,
                            parentWindow: window,
                            menuHandle: (MenuHandle)(y << 8 | x));
                return 0;
            case MessageType.Size:
                _cxBlock = lParam.LowWord / DIVISIONS;
                _cyBlock = lParam.HighWord / DIVISIONS;
                for (x = 0; x < DIVISIONS; x++)
                    for (y = 0; y < DIVISIONS; y++)
                        _hwndChild[x, y].MoveWindow(
                            new Rectangle(x * _cxBlock, y * _cyBlock, _cxBlock, _cyBlock),
                            repaint: true);
                return 0;
            case MessageType.LeftButtonDown:
                Windows.MessageBeep(BeepType.Ok);
                return 0;
            // On set-focus message, set focus to child window
            case MessageType.SetFocus:
                window.GetDialogItem(idFocus).SetFocus();
                return 0;
            // On key-down message, possibly change the focus window
            case MessageType.KeyDown:
                x = idFocus & 0xFF;
                y = idFocus >> 8;
                switch ((VirtualKey)wParam)
                {
                    case VirtualKey.Up: y--; break;
                    case VirtualKey.Down: y++; break;
                    case VirtualKey.Left: x--; break;
                    case VirtualKey.Right: x++; break;
                    case VirtualKey.Home: x = y = 0; break;
                    case VirtualKey.End: x = y = DIVISIONS - 1; break;
                    default: return 0;
                }
                x = (x + DIVISIONS) % DIVISIONS;
                y = (y + DIVISIONS) % DIVISIONS;
                idFocus = y << 8 | x;
                window.GetDialogItem(idFocus).SetFocus();
                return 0;
        }

        return base.WindowProcedure(window, message, wParam, lParam);
    }
}

internal class Checker4Child : WindowClass
{
    public Checker4Child()
        : base(windowExtraBytes: IntPtr.Size)
    {
    }

    protected override LResult WindowProcedure(WindowHandle window, MessageType message, WParam wParam, LParam lParam)
    {
        switch (message)
        {
            case MessageType.Create:
                window.SetWindowLong(0, IntPtr.Zero); // on/off flag
                return 0;
            case MessageType.KeyDown:
                // Send most key presses to the parent window
                if ((VirtualKey)wParam != VirtualKey.Return && (VirtualKey)wParam != VirtualKey.Space)
                {
                    window.GetParent().SendMessage(message, wParam, lParam);
                    return 0;
                }

                // For Return and Space, fall through to toggle the square
                goto case MessageType.LeftButtonDown;
            case MessageType.LeftButtonDown:
                window.SetWindowLong(0, (IntPtr)(1 ^ (int)window.GetWindowLong(0)));
                window.SetFocus();
                window.Invalidate(false);
                return 0;
            // For focus messages, invalidate the window for repaint
            case MessageType.SetFocus:
                Checker4.idFocus = (int)window.GetWindowLong(WindowLong.Id);
                // Fall through
                goto case MessageType.KillFocus;
            case MessageType.KillFocus:
                window.Invalidate();
                return 0;
            case MessageType.Paint:
                using (DeviceContext dc = window.BeginPaint())
                {
                    Rectangle rect = window.GetClientRectangle();
                    dc.Rectangle(rect);

                    if (window.GetWindowLong(0) != IntPtr.Zero)
                    {
                        dc.MoveTo(new Point(0, 0));
                        dc.LineTo(new Point(rect.Right, rect.Bottom));
                        dc.MoveTo(new Point(0, rect.Bottom));
                        dc.LineTo(new Point(rect.Right, 0));
                    }

                    // Draw the "focus" rectangle
                    if (window == Windows.GetFocus())
                    {
                        rect.Inflate(rect.Width / -10, rect.Height / -10);

                        dc.SelectObject(StockBrush.Null);
                        using PenHandle pen = Gdi.CreatePen(PenStyle.Dash, 0, default);
                        dc.SelectObject(pen);
                        dc.Rectangle(rect);
                        dc.SelectObject(StockPen.Black);
                    }
                }
                return 0;
        }

        return base.WindowProcedure(window, message, wParam, lParam);
    }
}
