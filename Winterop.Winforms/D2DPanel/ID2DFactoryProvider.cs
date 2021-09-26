namespace WInterop.Winforms
{
    public interface ID2DFactoryProvider
    {
        Direct2d.IFactory GetDirect2dFactory();
        DirectWrite.IFactory GetDirectWriteFactory();
    }
}
