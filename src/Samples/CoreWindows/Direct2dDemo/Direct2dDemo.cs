// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Drawing;
using WInterop.Direct2d;
using WInterop.Gdi;
using WInterop.Windows;

namespace Direct2dDemo
{
    // https://docs.microsoft.com/en-us/windows/desktop/Direct2D/direct2d-quickstart
    public class Direct2dDemo : WindowClass
    {
        private IFactory _factory;
        private IWindowRenderTarget _renderTarget;
        private ISolidColorBrush _lightSlateGrayBrush;
        private ISolidColorBrush _cornflowerBlueBrush;

        public Direct2dDemo() : base(backgroundBrush: BrushHandle.NoBrush) { }

        private void CreateDeviceResources(WindowHandle window)
        {
            if (_renderTarget == null)
            {
                _renderTarget = _factory.CreateWindowRenderTarget(
                    default,
                    new WindowRenderTargetProperties(window, window.GetClientRectangle().Size));

                _lightSlateGrayBrush = _renderTarget.CreateSolidColorBrush(Color.LightSlateGray);
                _cornflowerBlueBrush = _renderTarget.CreateSolidColorBrush(Color.CornflowerBlue);
            }
        }

        protected override LResult WindowProcedure(WindowHandle window, MessageType message, WParam wParam, LParam lParam)
        {
            switch (message)
            {
                case MessageType.Create:
                    _factory = Direct2d.CreateFactory();
                    CreateDeviceResources(window);
                    return 0;
                case MessageType.Size:
                    _renderTarget.Resize(new Message.Size(wParam, lParam).NewSize);
                    window.Invalidate();
                    return 0;
                case MessageType.DisplayChange:
                    window.Invalidate();
                    return 0;
                case MessageType.Paint:
                    CreateDeviceResources(window);
                    _renderTarget.BeginDraw();
                    _renderTarget.SetTransform();
                    _renderTarget.Clear(Color.White);
                    Size size = _renderTarget.GetSize().ToSize();

                    _lightSlateGrayBrush.GetColor(out ColorF color);

                    for (int x = 0; x < size.Width; x += 10)
                    {
                        _renderTarget.DrawLine(
                            new Point(x, 0), new Point(x, size.Height),
                            _lightSlateGrayBrush, 0.5f);
                    }

                    for (int y = 0; y < size.Height; y += 10)
                    {
                        _renderTarget.DrawLine(
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

                    _renderTarget.FillRectangle(rectangle1, _lightSlateGrayBrush);
                    _renderTarget.DrawRectangle(rectangle2, _cornflowerBlueBrush);

                    _renderTarget.EndDraw();
                    window.ValidateRectangle();
                    return 0;
            }

            return base.WindowProcedure(window, message, wParam, lParam);
        }
    }
}
