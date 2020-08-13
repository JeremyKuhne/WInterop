// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Drawing;
using WInterop.Direct2d;
using WInterop.DirectX;
using WInterop.Windows;

namespace Direct2dDemo
{
    // https://docs.microsoft.com/en-us/windows/desktop/Direct2D/direct2d-quickstart
    public class Direct2dDemo : DirectXWindowClass
    {
        private ISolidColorBrush _lightSlateGrayBrush;
        private ISolidColorBrush _cornflowerBlueBrush;

        protected override void CreateResources()
        {
            _lightSlateGrayBrush = RenderTarget.CreateSolidColorBrush(Color.LightSlateGray);
            _cornflowerBlueBrush = RenderTarget.CreateSolidColorBrush(Color.CornflowerBlue);
        }

        protected override void OnPaint(WindowHandle window)
        {
            RenderTarget.SetTransform();
            RenderTarget.Clear(Color.White);
            Size size = RenderTarget.GetSize().ToSize();

            _lightSlateGrayBrush.GetColor(out ColorF color);

            for (int x = 0; x < size.Width; x += 10)
            {
                RenderTarget.DrawLine(
                    new Point(x, 0), new Point(x, size.Height),
                    _lightSlateGrayBrush, 0.5f);
            }

            for (int y = 0; y < size.Height; y += 10)
            {
                RenderTarget.DrawLine(
                    new Point(0, y), new Point(size.Width, y),
                    _lightSlateGrayBrush, 0.5f);
            }

            Rectangle rectangle1 = Rectangle.FromLTRB(
                size.Width / 2 - 50,
                size.Height / 2 - 50,
                size.Width / 2 + 50,
                size.Height / 2 + 50);

            Rectangle rectangle2 = Rectangle.FromLTRB(
                size.Width / 2 - 100,
                size.Height / 2 - 100,
                size.Width / 2 + 100,
                size.Height / 2 + 100);

            RenderTarget.FillRectangle(rectangle1, _lightSlateGrayBrush);
            RenderTarget.DrawRectangle(rectangle2, _cornflowerBlueBrush);
        }
    }
}
