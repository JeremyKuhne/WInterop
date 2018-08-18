// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Drawing;
using WInterop.Direct2d;
using WInterop.Windows;

namespace Direct2dDemo
{
    // https://docs.microsoft.com/en-us/windows/desktop/Direct2D/how-to-draw-an-ellipse
    public class DrawEllipse : Direct2dWindowClass
    {
        private ISolidColorBrush _blackBrush;
        private ISolidColorBrush _silverBrush;
        private IStrokeStyle _dashDotDotStyle;

        public DrawEllipse() : base()
        {
            StrokeStyleProperties style = new StrokeStyleProperties(dashCap: CapStyle.Triangle, miterLimit: 10.0f, dashStyle: DashStyle.DashDotDot);
            _dashDotDotStyle = Factory.CreateStrokeStyle(style);
        }

        protected override void CreateResources()
        {
            _blackBrush = RenderTarget.CreateSolidColorBrush(Color.Black);
            _silverBrush = RenderTarget.CreateSolidColorBrush(Color.Silver);
        }

        protected override void OnPaint(WindowHandle window)
        {
            RenderTarget.Clear(Color.White);

            Ellipse ellipse = new Ellipse(new PointF(100.0f, 100.0f), 75.0f, 50.0f);
            RenderTarget.DrawEllipse(ellipse, _blackBrush, 10.0f);

            Ellipse ellipse2 = new Ellipse(new PointF(300.0f, 100.0f), 75.0f, 50.0f);
            RenderTarget.DrawEllipse(ellipse2, _blackBrush, 10.0f, _dashDotDotStyle);

            Ellipse ellipse3 = new Ellipse(new PointF(500.0f, 100.0f), 75.0f, 50.0f);
            RenderTarget.DrawEllipse(ellipse3, _blackBrush, 10.0f, _dashDotDotStyle);
            RenderTarget.FillEllipse(ellipse3, _silverBrush);

            Ellipse ellipse4 = new Ellipse(new PointF(700.0f, 100.0f), 75.0f, 50.0f);
            RenderTarget.FillEllipse(ellipse4, _silverBrush);
            RenderTarget.DrawEllipse(ellipse4, _blackBrush, 10.0f, _dashDotDotStyle);
        }
    }
}
