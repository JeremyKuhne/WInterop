// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Drawing;
using WInterop.Direct2d;
using WInterop.DirectX;
using WInterop.Windows;

namespace Direct2dDemo
{
    // https://docs.microsoft.com/windows/desktop/Direct2D/how-to-draw-an-ellipse
    public class DrawEllipse : DirectXWindowClass
    {
        private SolidColorBrush _blackBrush;
        private SolidColorBrush _silverBrush;
        private readonly StrokeStyle _dashDotDotStyle;

        public DrawEllipse() : base()
        {
            StrokeStyleProperties style = new(
                dashCap: CapStyle.Triangle,
                miterLimit: 10.0f,
                dashStyle: DashStyle.DashDotDot);
            _dashDotDotStyle = Direct2dFactory.CreateStrokeStyle(style);
        }

        protected override void CreateResources()
        {
            _blackBrush = RenderTarget.CreateSolidColorBrush(Color.Black);
            _silverBrush = RenderTarget.CreateSolidColorBrush(Color.Silver);
        }

        protected override void OnPaint(WindowHandle window)
        {
            RenderTarget.Clear(Color.White);

            Ellipse ellipse = new((100.0f, 100.0f), 75.0f, 50.0f);
            RenderTarget.DrawEllipse(ellipse, _blackBrush, 10.0f);

            Ellipse ellipse2 = new((300.0f, 100.0f), 75.0f, 50.0f);
            RenderTarget.DrawEllipse(ellipse2, _blackBrush, 10.0f, _dashDotDotStyle);

            Ellipse ellipse3 = new((500.0f, 100.0f), 75.0f, 50.0f);
            RenderTarget.DrawEllipse(ellipse3, _blackBrush, 10.0f, _dashDotDotStyle);
            RenderTarget.FillEllipse(ellipse3, _silverBrush);

            Ellipse ellipse4 = new((700.0f, 100.0f), 75.0f, 50.0f);
            RenderTarget.FillEllipse(ellipse4, _silverBrush);
            RenderTarget.DrawEllipse(ellipse4, _blackBrush, 10.0f, _dashDotDotStyle);
        }

        protected override void Dispose(bool disposing)
        {
            _blackBrush.Dispose();
            _silverBrush.Dispose();
            _dashDotDotStyle.Dispose();
        }
    }
}
