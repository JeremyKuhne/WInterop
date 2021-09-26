using System.Windows.Forms;
using WInterop.Winforms;

namespace WinFormsDirect2D
{
    public partial class MainForm : Form
    {
        private bool _resourcesCreated;
        private DemoRenderer _demoRenderer;
        private IRenderItem? _currentRenderItem;

        public MainForm()
        {
            InitializeComponent();
            _demoRenderer = new DemoRenderer(d2DPanel);

            foreach (var item in _demoRenderer.Demos)
            {
                ToolStripMenuItem menuItem = new()
                {
                    Text = item.Key,
                    Tag = item.Value
                };

                menuItem.Click += MenuItem_Click;
                viewToolStripMenuItem.DropDownItems.Add(menuItem);
            }
        }

        private void MenuItem_Click(object? sender, System.EventArgs e)
        {
            _currentRenderItem = (IRenderItem)((ToolStripMenuItem)sender!).Tag;
            _resourcesCreated = false;
            d2DPanel.Reset();
        }

        private void D2DPanel_D2DPaint(object sender, WInterop.Winforms.D2DPaintEventArgs e)
        {
            if (!_resourcesCreated || _currentRenderItem is null)
            {
                return;
            }

            _currentRenderItem.D2DPaint(e.RenderTarget);
        }

        private void D2DPanel_CreateD2DResources(object sender, WInterop.Winforms.D2DPaintEventArgs e)
        {
            _resourcesCreated = true;
            if (_currentRenderItem is null)
            {
                return;
            }

            _currentRenderItem.CreateD2DResources(e.RenderTarget);
        }
    }
}
