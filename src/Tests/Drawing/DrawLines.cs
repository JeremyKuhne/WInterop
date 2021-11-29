// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using WInterop.Gdi;
using WInterop.GdiPlus;
using WInterop.Windows;
using System.Drawing;

namespace Windows101;

public class DrawLines : DpiAwareClass
{
    private readonly Pen _redPen = new Pen(Color.Red);

    protected override LResult WindowProcedure(WindowHandle window, MessageType message, WParam wParam, LParam lParam)
    {
        switch (message)
        {
            case MessageType.Paint:
                {
                    using DeviceContext dc = window.BeginPaint();
                    using Graphics graphics = new(dc);

                    dc.MoveTo(30, 50);
                    dc.LineTo(40, 50);

                    for (int i = 0; i < 20; i += 2)
                    {
                        Point start = new Point(50, 50 + i);
                        Point end = new Point(50 + i, 50 + i);
                        dc.MoveTo(start);
                        dc.LineTo(end);

                        start.Y += 1;
                        end.Y += 1;
                        graphics.DrawLine(_redPen, start, end);
                    }

                    // Now in reverse

                    dc.MoveTo(30, 100);
                    dc.LineTo(40, 100);

                    for (int i = 0; i < 20; i += 2)
                    {
                        Point start = new Point(50 + i, 100 + i);
                        Point end = new Point(50, 100 + i);
                        dc.MoveTo(start);
                        dc.LineTo(end);

                        start.Y += 1;
                        end.Y += 1;
                        graphics.DrawLine(_redPen, start, end);
                    }

                    return 0;
                }
        }

        return base.WindowProcedure(window, message, wParam, lParam);
    }
}
