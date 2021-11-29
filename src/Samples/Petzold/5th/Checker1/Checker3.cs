// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Drawing;
using WInterop.Gdi;
using WInterop.Windows;

namespace Checker;

internal class Checker3 : WindowClass
{
    private const int DIVISIONS = 5;
    private readonly WindowHandle[,] _hwndChild = new WindowHandle[DIVISIONS, DIVISIONS];
    private int _cxBlock, _cyBlock;
    private readonly Checker3Child _childClass = (Checker3Child)(new Checker3Child().Register());

    protected override LResult WindowProcedure(WindowHandle window, MessageType message, WParam wParam, LParam lParam)
    {
        switch (message)
        {
            case MessageType.Create:
                for (int x = 0; x < DIVISIONS; x++)
                    for (int y = 0; y < DIVISIONS; y++)
                        _hwndChild[x, y] = _childClass.CreateWindow(
                            Windows.DefaultBounds,
                            style: WindowStyles.ChildWindow | WindowStyles.Visible,
                            parentWindow: window);
                return 0;
            case MessageType.Size:
                _cxBlock = lParam.LowWord / DIVISIONS;
                _cyBlock = lParam.HighWord / DIVISIONS;
                for (int x = 0; x < DIVISIONS; x++)
                    for (int y = 0; y < DIVISIONS; y++)
                        _hwndChild[x, y].MoveWindow(
                            new Rectangle(x * _cxBlock, y * _cyBlock, _cxBlock, _cyBlock),
                            repaint: true);
                return 0;
            case MessageType.LeftButtonDown:
                Windows.MessageBeep(BeepType.Ok);
                return 0;
        }

        return base.WindowProcedure(window, message, wParam, lParam);
    }
}

internal class Checker3Child : WindowClass
{
    public Checker3Child()
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
            case MessageType.LeftButtonDown:
                window.SetWindowLong(0, (IntPtr)(1 ^ (int)window.GetWindowLong(0)));
                window.Invalidate(false);
                return 0;
            case MessageType.Paint:
                using (DeviceContext dc = window.BeginPaint())
                {
                    Rectangle rect = window.GetClientRectangle();
                    dc.Rectangle(rect);

                    if (window.GetWindowLong(0) != IntPtr.Zero)
                    {
                        dc.MoveTo(default);
                        dc.LineTo(new Point(rect.Right, rect.Bottom));
                        dc.MoveTo(new Point(0, rect.Bottom));
                        dc.LineTo(new Point(rect.Right, 0));
                    }
                }
                return 0;
        }

        return base.WindowProcedure(window, message, wParam, lParam);
    }
}
