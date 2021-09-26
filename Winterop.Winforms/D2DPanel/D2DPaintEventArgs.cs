using System;
using WInterop.Direct2d;

namespace WInterop.Winforms
{
    public class D2DPaintEventArgs : EventArgs
    {
        public D2DPaintEventArgs(IRenderTarget renderTarget)
        {
            RenderTarget = renderTarget;
        }

        public IRenderTarget RenderTarget { get; }
    }
}
