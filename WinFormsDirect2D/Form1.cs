using System.Drawing;
using System.Windows.Forms;
using WInterop.Direct2d;

namespace WinFormsDirect2D
{
    public partial class Form1 : Form
    {
        private ISolidColorBrush? _lightSlateGrayBrush;
        private ISolidColorBrush? _cornflowerBlueBrush;
        private bool _resourcesCreated;

        public Form1()
        {
            InitializeComponent();
        }

        private void dxControl_DirectXEvent(object sender, WInterop.Winforms.DirectXPaintEventArgs e)
        {
            if (!_resourcesCreated)
            {
                return;
            }

            var RenderTarget = e.RenderTarget;

            RenderTarget.SetTransform();
            RenderTarget.Clear(Color.White);
            Size size = RenderTarget.GetSize().ToSize();

            _lightSlateGrayBrush!.GetColor(out ColorF color);

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

        private void dxControl_CreateDirectXResources(object sender, WInterop.Winforms.DirectXPaintEventArgs e)
        {
            _lightSlateGrayBrush = e.RenderTarget.CreateSolidColorBrush(Color.LightSlateGray);
            _cornflowerBlueBrush = e.RenderTarget.CreateSolidColorBrush(Color.CornflowerBlue);
            _resourcesCreated = true;
        }
    }
}
