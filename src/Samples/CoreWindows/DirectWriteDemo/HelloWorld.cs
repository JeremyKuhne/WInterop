// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Drawing;
using WInterop.Direct2d;
using WInterop.DirectWrite;
using WInterop.DirectX;
using WInterop.Windows;

namespace DirectWriteDemo
{
    // https://docs.microsoft.com/en-us/windows/desktop/DirectWrite/getting-started-with-directwrite
    public class HelloWorld : DirectXWindowClass
    {
        protected ITextFormat _textFormat;
        protected ITextLayout _textLayout;
        protected ITypography _typography;

        protected ISolidColorBrush _blackBrush;

        public HelloWorld() : base()
        {
            _textFormat = DirectWriteFactory.CreateTextFormat("Gabriola", fontSize: 64);
            _textFormat.SetTextAlignment(TextAlignment.Center);
            _textFormat.SetParagraphAlignment(ParagraphAlignment.Center);
        }

        protected override void CreateResources()
        {
            string text = "Hello World From ... DirectWrite!";
            _blackBrush = RenderTarget.CreateSolidColorBrush(Color.Black);
            _textLayout = DirectWriteFactory.CreateTextLayout(text, _textFormat, RenderTarget.GetSize());

            // (21, 12) is the range around "DirectWrite!"
            _textLayout.SetFontSize(100, (21, 12));
            _typography = DirectWriteFactory.CreateTypography();
            _typography.AddFontFeature(new FontFeature(FontFeatureTag.StylisticSet7, 1));
            _textLayout.SetTypography(_typography, (0, text.Length));
            _textLayout.SetUnderline(true, (21, 12));
            _textLayout.SetFontWeight(FontWeight.Bold, (21, 12));
        }

        protected override void OnSize(WindowHandle window, in Message.Size sizeMessage)
        {
            _textLayout.SetMaxSize(sizeMessage.NewSize);
        }

        protected override void OnPaint(WindowHandle window)
        {
            RenderTarget.Clear(Color.CornflowerBlue);
            RenderTarget.DrawTextLayout(default, _textLayout, _blackBrush);
        }
    }
}
