using WInterop.Direct2d;

namespace WInterop.Winforms
{
    public interface IRenderItem
    {
        void D2DPaint(IRenderTarget renderTarget);
        void CreateD2DResources(IRenderTarget renderTarget);
        ID2DFactoryProvider D2DFactoryProvider { get; set; }
    }
}
