using System;
using WInterop.Direct2d;

namespace WInterop.Winforms
{
    public class DirectXPaintEventArgs : EventArgs
    {
        public DirectXPaintEventArgs(IRenderTarget renderTarget)
        {
            RenderTarget = renderTarget;
        }

        public IRenderTarget RenderTarget { get; }
    }
}
