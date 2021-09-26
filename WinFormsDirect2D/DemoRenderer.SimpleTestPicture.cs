using WInterop.Direct2d;
using System.Drawing;
using WInterop.Winforms;
using System.Diagnostics.CodeAnalysis;

namespace WinFormsDirect2D
{
    internal partial class DemoRenderer
    {
        private class SimpleTestPicture : IRenderItem
        {
            private ISolidColorBrush? _lightSlateGrayBrush;
            private ISolidColorBrush? _cornflowerBlueBrush;

            public SimpleTestPicture()
            {
            }

            [AllowNull]
            public ID2DFactoryProvider D2DFactoryProvider { get; set; }

            public void CreateD2DResources(IRenderTarget renderTarget)
            {
                _lightSlateGrayBrush = renderTarget.CreateSolidColorBrush(Color.LightSlateGray);
                _cornflowerBlueBrush = renderTarget.CreateSolidColorBrush(Color.CornflowerBlue);
            }

            public void D2DPaint(IRenderTarget renderTarget)
            {
                renderTarget.SetTransform();
                renderTarget.Clear(Color.White);
                Size size = renderTarget.GetSize().ToSize();

                _lightSlateGrayBrush!.GetColor(out ColorF color);

                for (int x = 0; x < size.Width; x += 10)
                {
                    renderTarget.DrawLine(
                        new Point(x, 0), new Point(x, size.Height),
                        _lightSlateGrayBrush, 0.5f);
                }

                for (int y = 0; y < size.Height; y += 10)
                {
                    renderTarget.DrawLine(
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

                renderTarget.FillRectangle(rectangle1, _lightSlateGrayBrush);
                renderTarget.DrawRectangle(rectangle2, _cornflowerBlueBrush);
            }
        }
    }
}
