using System;
using System.Windows.Forms;
using WInterop.Direct2d;
using WInterop.Errors;
using WInterop.Windows;

namespace WInterop.Winforms
{
    public class Direct2DControl : Control
    {
        private bool _resourcesValid;
        private IWindowRenderTarget? _renderTarget;

        public event EventHandler<DirectXPaintEventArgs>? DirectXPaint;
        public event EventHandler<DirectXPaintEventArgs>? CreateDirectXResources;

        public Direct2DControl() : base()
        {
        }

        protected IRenderTarget? RenderTarget => _renderTarget;

        protected static Direct2d.IFactory Direct2dFactory { get; } = Direct2d.Direct2d.CreateFactory();
        protected static DirectWrite.IFactory DirectWriteFactory { get; } = DirectWrite.DirectWrite.CreateFactory();

        protected virtual void OnCreateResources()
        {
            CreateDirectXResources?.Invoke(this, new DirectXPaintEventArgs(RenderTarget!));
        }

        private void CreateResourcesInternal(WindowHandle window)
        {
            if (!_resourcesValid)
            {
                _renderTarget = Direct2dFactory.CreateWindowRenderTarget(
                    default, new WindowRenderTargetProperties(window, window.GetClientRectangle().Size));
                OnCreateResources();
                _resourcesValid = true;
            }
        }

        private void CreateResourcesInternal(WindowHandle window, in Windows.Message.Size size)
        {
            if (!_resourcesValid)
            {
                _renderTarget = Direct2dFactory.CreateWindowRenderTarget(
                    default, new WindowRenderTargetProperties(window, size.NewSize));
                OnCreateResources();
                _resourcesValid = true;
            }
            else
            {
                _renderTarget!.Resize(size.NewSize);
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            var windowHandle = new WindowHandle(new Windows.Native.HWND(Handle));
            CreateResourcesInternal(windowHandle);
            if (_resourcesValid)
            {
                _renderTarget!.BeginDraw();
                OnDirectXPaint();
                HResult result = _renderTarget.EndDraw();
            }
        }

        protected override void OnResize(EventArgs e)
        {
            var sizeMessage = Windows.Message.Size.FromDrawingSize(ClientSize);
            var windowHandle = new WindowHandle(new Windows.Native.HWND(Handle));
            CreateResourcesInternal(windowHandle, in sizeMessage);
            base.OnResize(e);
        }

        protected virtual void OnDirectXPaint()
        {
            if (_resourcesValid)
            {
                DirectXPaint?.Invoke(this, new DirectXPaintEventArgs(RenderTarget!));
            }
        }
    }
}
