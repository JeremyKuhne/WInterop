// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Drawing;
using WInterop.Gdi;
using WInterop.Windows;

namespace Checker
{
    public class Checker1 : WindowClass
    {
        private const int DIVISIONS = 5;
        private readonly bool[,] _fState = new bool[DIVISIONS, DIVISIONS];
        private int _cxBlock, _cyBlock;

        protected override LResult WindowProcedure(WindowHandle window, MessageType message, WParam wParam, LParam lParam)
        {
            switch (message)
            {
                case MessageType.Size:
                    _cxBlock = lParam.LowWord / DIVISIONS;
                    _cyBlock = lParam.HighWord / DIVISIONS;
                    return 0;
                case MessageType.LeftButtonDown:
                    int x = lParam.LowWord / _cxBlock;
                    int y = lParam.HighWord / _cyBlock;
                    if (x < DIVISIONS && y < DIVISIONS)
                    {
                        _fState[x, y] ^= true;
                        Rectangle rect = Rectangle.FromLTRB
                        (
                            x * _cxBlock,
                            y * _cyBlock,
                            (x + 1) * _cxBlock,
                            (y + 1) * _cyBlock
                        );
                        window.InvalidateRectangle(rect, false);
                    }
                    else
                    {
                        Windows.MessageBeep(0);
                    }

                    return 0;
                case MessageType.Paint:
                    using (DeviceContext dc = window.BeginPaint())
                    {
                        for (x = 0; x < DIVISIONS; x++)
                            for (y = 0; y < DIVISIONS; y++)
                            {
                                dc.Rectangle(new Rectangle(
                                    x * _cxBlock, y * _cyBlock, (x + 1) * _cxBlock, (y + 1) * _cyBlock));

                                if (_fState[x, y])
                                {
                                    dc.MoveTo(new Point(x * _cxBlock, y * _cyBlock));
                                    dc.LineTo(new Point((x + 1) * _cxBlock, (y + 1) * _cyBlock));
                                    dc.MoveTo(new Point(x * _cxBlock, (y + 1) * _cyBlock));
                                    dc.LineTo(new Point((x + 1) * _cxBlock, y * _cyBlock));
                                }
                            }
                    }
                    return 0;
            }

            return base.WindowProcedure(window, message, wParam, lParam);
        }
    }
}
