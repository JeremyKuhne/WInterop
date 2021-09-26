using WInterop.Direct2d;
using System.Drawing;
using WInterop.Winforms;
using System.Diagnostics.CodeAnalysis;
using WInterop.DirectWrite;

namespace WinFormsDirect2D
{
    internal partial class DemoRenderer
    {
        private class HelloWorld : IRenderItem
        {
            protected ITextFormat? _textFormat;
            protected ITextLayout? _textLayout;
            protected ITypography? _typography;

            protected ISolidColorBrush? _blackBrush;

            public HelloWorld()
            {
            }

            [AllowNull]
            public ID2DFactoryProvider D2DFactoryProvider { get; set; }

            public void InitializeComponent()
            {
                _textFormat = D2DFactoryProvider.GetDirectWriteFactory().CreateTextFormat("Gabriola", fontSize: 64);
                _textFormat.SetTextAlignment(TextAlignment.Center);
                _textFormat.SetParagraphAlignment(ParagraphAlignment.Center);
            }

            public void CreateD2DResources(IRenderTarget renderTarget)
            {
                string text = "Hello World From ... DirectWrite!";
                _blackBrush = renderTarget.CreateSolidColorBrush(Color.Black);
                _textLayout = D2DFactoryProvider.GetDirectWriteFactory().CreateTextLayout(text, _textFormat, renderTarget.GetSize());

                // (21, 12) is the range around "DirectWrite!"
                _textLayout.SetFontSize(100, (21, 12));
                _typography = D2DFactoryProvider.GetDirectWriteFactory().CreateTypography();
                _typography.AddFontFeature(new FontFeature(FontFeatureTag.StylisticSet7, 1));
                _textLayout.SetTypography(_typography, (0, text.Length));
                _textLayout.SetUnderline(true, (21, 12));
                _textLayout.SetFontWeight(FontWeight.Bold, (21, 12));
            }

            public void D2DPaint(IRenderTarget renderTarget)
            {
                renderTarget.Clear(Color.CornflowerBlue);
                renderTarget.DrawTextLayout(default, _textLayout, _blackBrush);
            }
        }
    }
}
