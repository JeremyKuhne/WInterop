// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Drawing;
using WInterop.Direct2d;
using WInterop.Windows;

namespace DirectWriteDemo
{
    // https://docs.microsoft.com/en-us/windows/desktop/DirectWrite/how-to-implement-a-custom-text-renderer
    public class CustomText : HelloWorld
    {
        IRadialGradientBrush _radialGradientBrush;
        CustomTextRenderer _textRenderer;

        protected override void CreateResources()
        {
            base.CreateResources();

            // Gradient brush
            Span<GradientStop> stops = stackalloc[]
            {
                new GradientStop(0.0f, Color.Gold),
                new GradientStop(0.85f, new ColorF(Color.Orange, 0.8f)),
                new GradientStop(1.0f, new ColorF(Color.OrangeRed, 0.7f))
            };

            _radialGradientBrush = RenderTarget.CreateRadialGradientBrush(
                new RadialGradientBrushProperties(new PointF(330, 330), new PointF(140, 140), 140, 140),
                RenderTarget.CreateGradienStopCollection(stops));

            _textRenderer = new CustomTextRenderer(Direct2dFactory, RenderTarget, _blackBrush, _radialGradientBrush);
        }

        protected override void OnPaint(WindowHandle window)
        {
            RenderTarget.Clear(Color.AntiqueWhite);
            _textLayout.Draw(IntPtr.Zero, _textRenderer, 0, 0);
        }
    }
}
