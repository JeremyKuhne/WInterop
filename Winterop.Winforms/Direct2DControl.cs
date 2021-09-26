using System;
using System.Windows.Forms;
using WInterop.Direct2d;
using WInterop.Errors;
using WInterop.Windows;

namespace WInterop.Winforms
{
    public class Direct2DControl : Control
    {
        private bool _baseResourcesValid;
        private IWindowRenderTarget? _renderTarget;
        private object _objectLock=new object();

        private event EventHandler<DirectXPaintEventArgs>? _directXPaint;
        private event EventHandler<DirectXPaintEventArgs>? _createDirectXResources;

        public Direct2DControl() : base()
        {
        }

        protected IRenderTarget? RenderTarget => _renderTarget;

        protected static Direct2d.IFactory Direct2dFactory { get; } = Direct2d.Direct2d.CreateFactory();
        protected static DirectWrite.IFactory DirectWriteFactory { get; } = DirectWrite.DirectWrite.CreateFactory();

        private void CreateResourcesInternal(WindowHandle window)
        {
            if (!_baseResourcesValid)
            {
                _renderTarget = Direct2dFactory.CreateWindowRenderTarget(
                    default, new WindowRenderTargetProperties(window, window.GetClientRectangle().Size));
                OnCreateResources();
                _baseResourcesValid = true;
            }
        }

        private void CreateResourcesInternal(WindowHandle window, in Windows.Message.Size size)
        {
            if (!_baseResourcesValid)
            {
                _renderTarget = Direct2dFactory.CreateWindowRenderTarget(
                    default, new WindowRenderTargetProperties(window, size.NewSize));
                OnCreateResources();
                _baseResourcesValid = true;
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
            if (_baseResourcesValid)
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
            if (_baseResourcesValid)
            {
                _directXPaint?.Invoke(this, new DirectXPaintEventArgs(RenderTarget!));
            }
        }

        protected virtual void OnCreateResources()
        {
            _createDirectXResources?.Invoke(this, new DirectXPaintEventArgs(RenderTarget!));
        }

        public event EventHandler<DirectXPaintEventArgs> DirectXPaint
        {
            add
            {
                lock (_objectLock)
                {
                    _directXPaint += value;
                }
            }
            remove
            {
                lock (_objectLock)
                {
                    _directXPaint -= value;
                }
            }
        }

        public event EventHandler<DirectXPaintEventArgs> CreateDirectXResources
        {
            add
            {
                lock (_objectLock)
                {
                    _createDirectXResources += value;
                    if (_baseResourcesValid)
                    {
                        // If the event was wired _after_ we already have created the resources
                        // we need to fire it immediately.
                        OnCreateResources();
                    }
                }
            }
            remove
            {
                lock (_objectLock)
                {
                    _createDirectXResources -= value;
                }
            }
        }
    }
}
