// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Drawing;
using System.Numerics;
using WInterop.Direct2d;
using WInterop.DirectX;
using WInterop.Windows;

namespace Direct2dDemo
{
    public class PathGeometries : DirectXWindowClass
    {
        private readonly PathGeometry _leftMountainGeometry;
        private readonly PathGeometry _rightMountainGeometry;
        private readonly PathGeometry _sunGeometry;
        private readonly PathGeometry _riverGeometry;
        private SolidColorBrush _sceneBrush;
        private BitmapBrush _gridPatternBrush;
        private RadialGradientBrush _radialGradientBrush;

        public PathGeometries() : base()
        {
            // Left mountain
            _leftMountainGeometry = Direct2dFactory.CreatePathGeometry();
            var sink = _leftMountainGeometry.Open();
            sink.SetFillMode(FillMode.Winding);
            sink.BeginFigure((346, 255), FigureBegin.Filled);

            sink.AddLines(
                stackalloc PointF[]
                {
                    new(267, 177),
                    new(236, 192),
                    new(212, 160),
                    new(156, 255),
                    new(346, 255)
                });
            sink.EndFigure(FigureEnd.Closed);
            sink.Close();

            // Right mountain
            _rightMountainGeometry = Direct2dFactory.CreatePathGeometry();
            sink = _rightMountainGeometry.Open();
            sink.SetFillMode(FillMode.Winding);
            sink.BeginFigure((575, 263), FigureBegin.Filled);

            sink.AddLines(
                stackalloc PointF[]
                {
                    new(481, 146),
                    new(449, 181),
                    new(433, 159),
                    new(401, 214),
                    new(381, 199),
                    new(323, 263),
                    new(575, 263)
                });

            sink.EndFigure(FigureEnd.Closed);
            sink.Close();

            // Sun
            _sunGeometry = Direct2dFactory.CreatePathGeometry();
            sink = _sunGeometry.Open();
            sink.SetFillMode(FillMode.Winding);
            sink.BeginFigure((270, 255), FigureBegin.Filled);
            sink.AddArc(new((440, 255), (85, 85)));
            sink.EndFigure(FigureEnd.Closed);

            sink.BeginFigure((299, 182), FigureBegin.Hollow);
            sink.AddBezier(new((299, 182), (294, 176), (285, 178)));
            sink.AddBezier(new((276, 179), (272, 173), (272, 173)));
            sink.EndFigure(FigureEnd.Open);

            sink.BeginFigure((354, 156), FigureBegin.Hollow);
            sink.AddBezier(new((354, 156), (358, 149), (354, 142)));
            sink.AddBezier(new((349, 134), (354, 127), (354, 127)));
            sink.EndFigure(FigureEnd.Open);

            sink.BeginFigure((322, 164), FigureBegin.Hollow);
            sink.AddBezier(new((322, 164), (322, 156), (314, 152)));
            sink.AddBezier(new((306, 149), (305, 141), (305, 141)));
            sink.EndFigure(FigureEnd.Open);

            sink.BeginFigure((385, 164), FigureBegin.Hollow);
            sink.AddBezier(new((385, 164), (392, 161), (394, 152)));
            sink.AddBezier(new((395, 144), (402, 141), (402, 142)));
            sink.EndFigure(FigureEnd.Open);

            sink.BeginFigure((408, 182), FigureBegin.Hollow);
            sink.AddBezier(new((408, 182), (416, 184), (422, 178)));
            sink.AddBezier(new((428, 171), (435, 173), (435, 173)));
            sink.EndFigure(FigureEnd.Open);

            sink.Close();

            // River
            _riverGeometry = Direct2dFactory.CreatePathGeometry();
            sink = _riverGeometry.Open();
            sink.SetFillMode(FillMode.Winding);
            sink.BeginFigure((183, 392), FigureBegin.Filled);
            sink.AddBezier(new((238, 284), (472, 345), (356, 303)));
            sink.AddBezier(new((237, 261), (333, 256), (333, 256)));
            sink.AddBezier(new((335, 257), (241, 261), (411, 306)));
            sink.AddBezier(new((574, 350), (288, 324), (296, 392)));
            sink.EndFigure(FigureEnd.Open);
            sink.Close();
        }

        protected override void CreateResources()
        {
            _sceneBrush = RenderTarget.CreateSolidColorBrush(Color.Black);

            // Background grid brush
            var bitmapTarget = RenderTarget.CreateCompatibleRenderTarget((10, 10));
            var gridBrush = bitmapTarget.CreateSolidColorBrush(.93f, .94f, .96f);
            bitmapTarget.BeginDraw();
            bitmapTarget.FillRectangle((0, 0, 10, 1), gridBrush);
            bitmapTarget.FillRectangle((0, 0, 1, 10), gridBrush);
            bitmapTarget.EndDraw();
            Bitmap bitmap = bitmapTarget.Bitmap;
            _gridPatternBrush = RenderTarget.CreateBitmapBrush(bitmap, new(ExtendMode.Wrap, ExtendMode.Wrap));

            // Gradient brush
            using var gradientStops = RenderTarget.CreateGradientStopCollection(
                stackalloc GradientStop[]
                {
                    new(0.0f, Color.Gold),
                    new(0.85f, new(Color.Orange, 0.8f)),
                    new(1.0f, new(Color.OrangeRed, 0.7f))
                });

            _radialGradientBrush = RenderTarget.CreateRadialGradientBrush(
                new((330, 330), (140, 140), 140, 140),
                gradientStops);
        }

        protected override void OnPaint(WindowHandle window)
        {
            SizeF targetSize = RenderTarget.Size;

            RenderTarget.Transform = Matrix3x2.Identity;
            RenderTarget.Clear(Color.White);
            RenderTarget.FillRectangle(new(RenderTarget.Size), _gridPatternBrush);
            RenderTarget.Transform = Matrix3x2.CreateScale(Math.Min(targetSize.Width / 840.0f, targetSize.Height / 700.0f) * 1.4f);

            RenderTarget.FillGeometry(_sunGeometry, _radialGradientBrush);
            _sceneBrush.Color = Color.Black;
            RenderTarget.DrawGeometry(_sunGeometry, _sceneBrush);

            _sceneBrush.Color = Color.OliveDrab;
            RenderTarget.FillGeometry(_leftMountainGeometry, _sceneBrush);
            _sceneBrush.Color = Color.Black;
            RenderTarget.DrawGeometry(_leftMountainGeometry, _sceneBrush);

            _sceneBrush.Color = Color.LightSkyBlue;
            RenderTarget.FillGeometry(_riverGeometry, _sceneBrush);
            _sceneBrush.Color = Color.Black;
            RenderTarget.DrawGeometry(_riverGeometry, _sceneBrush);

            _sceneBrush.Color = Color.YellowGreen;
            RenderTarget.FillGeometry(_rightMountainGeometry, _sceneBrush);
            _sceneBrush.Color = Color.Black;
            RenderTarget.DrawGeometry(_rightMountainGeometry, _sceneBrush);
        }
    }
}
