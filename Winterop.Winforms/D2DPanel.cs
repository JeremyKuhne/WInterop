using System;
using System.Windows.Forms;
using WInterop.Direct2d;
using WInterop.Errors;
using WInterop.Windows;

namespace WInterop.Winforms
{
    public class D2DPanel : Control
    {
        private bool _baseResourcesValid;
        private IWindowRenderTarget? _renderTarget;
        private object _objectLock=new object();

        private event EventHandler<D2DPaintEventArgs>? _d2DPaint;
        private event EventHandler<D2DPaintEventArgs>? _createD2DResources;

        public D2DPanel() : base()
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
                OnCreateD2DResources();
                _baseResourcesValid = true;
            }
        }

        private void CreateResourcesInternal(WindowHandle window, in Windows.Message.Size size)
        {
            if (!_baseResourcesValid)
            {
                _renderTarget = Direct2dFactory.CreateWindowRenderTarget(
                    default, new WindowRenderTargetProperties(window, size.NewSize));
                OnCreateD2DResources();
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
                OnD2DPaint();
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

        protected virtual void OnD2DPaint()
        {
            if (_baseResourcesValid)
            {
                _d2DPaint?.Invoke(this, new D2DPaintEventArgs(RenderTarget!));
            }
        }

        protected virtual void OnCreateD2DResources()
        {
            _createD2DResources?.Invoke(this, new D2DPaintEventArgs(RenderTarget!));
        }

        public event EventHandler<D2DPaintEventArgs> D2DPaint
        {
            add
            {
                lock (_objectLock)
                {
                    _d2DPaint += value;
                }
            }
            remove
            {
                lock (_objectLock)
                {
                    _d2DPaint -= value;
                }
            }
        }

        public event EventHandler<D2DPaintEventArgs> CreateD2DResources
        {
            add
            {
                lock (_objectLock)
                {
                    _createD2DResources += value;
                    if (_baseResourcesValid)
                    {
                        // If the event was wired _after_ we already have created the resources
                        // we need to fire it immediately.
                        OnCreateD2DResources();
                    }
                }
            }
            remove
            {
                lock (_objectLock)
                {
                    _createD2DResources -= value;
                }
            }
        }
    }
}
