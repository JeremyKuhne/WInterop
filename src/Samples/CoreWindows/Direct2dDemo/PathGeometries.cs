// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Drawing;
using System.Numerics;
using WInterop.Direct2d;
using WInterop.DirectX;
using WInterop.Errors;
using WInterop.Windows;

namespace Direct2dDemo
{
    public class PathGeometries : DirectXWindowClass
    {
        IPathGeometry _leftMountainGeometry;
        IPathGeometry _rightMountainGeometry;
        IPathGeometry _sunGeometry;
        IPathGeometry _riverGeometry;

        ISolidColorBrush _sceneBrush;
        IBitmapBrush _gridPatternBrush;
        IRadialGradientBrush _radialGradientBrush;

        public PathGeometries() : base()
        {
            // Left mountain
            _leftMountainGeometry = Direct2dFactory.CreatePathGeometry();
            var sink = _leftMountainGeometry.Open();
            sink.SetFillMode(FillMode.Winding);
            sink.BeginFigure(new PointF(346, 255), FigureBegin.Filled);
            Span<PointF> lines = stackalloc PointF[]
            {
                new PointF(267, 177),
                new PointF(236, 192),
                new PointF(212, 160),
                new PointF(156, 255),
                new PointF(346, 255)
            };

            sink.AddLines(lines);
            sink.EndFigure(FigureEnd.Closed);
            sink.Close();

            // Right mountain
            _rightMountainGeometry = Direct2dFactory.CreatePathGeometry();
            sink = _rightMountainGeometry.Open();
            sink.SetFillMode(FillMode.Winding);
            sink.BeginFigure(new PointF(575, 263), FigureBegin.Filled);
            lines = stackalloc PointF[]
            {
                new PointF(481, 146),
                new PointF(449, 181),
                new PointF(433, 159),
                new PointF(401, 214),
                new PointF(381, 199),
                new PointF(323, 263),
                new PointF(575, 263)
            };

            sink.AddLines(lines);
            sink.EndFigure(FigureEnd.Closed);
            sink.Close();

            // Sun
            _sunGeometry = Direct2dFactory.CreatePathGeometry();
            sink = _sunGeometry.Open();
            sink.SetFillMode(FillMode.Winding);
            sink.BeginFigure(new PointF(270, 255), FigureBegin.Filled);
            sink.AddArc(new ArcSegment(new PointF(440, 255), new SizeF(85, 85)));
            sink.EndFigure(FigureEnd.Closed);

            sink.BeginFigure(new PointF(299, 182), FigureBegin.Hollow);
            sink.AddBezier((299, 182), (294, 176), (285, 178));
            sink.AddBezier((276, 179), (272, 173), (272, 173));
            sink.EndFigure(FigureEnd.Open);

            sink.BeginFigure(new PointF(354, 156), FigureBegin.Hollow);
            sink.AddBezier((354, 156), (358, 149), (354, 142));
            sink.AddBezier((349, 134), (354, 127), (354, 127));
            sink.EndFigure(FigureEnd.Open);

            sink.BeginFigure(new PointF(322, 164), FigureBegin.Hollow);
            sink.AddBezier((322, 164), (322, 156), (314, 152));
            sink.AddBezier((306, 149), (305, 141), (305, 141));
            sink.EndFigure(FigureEnd.Open);

            sink.BeginFigure(new PointF(385, 164), FigureBegin.Hollow);
            sink.AddBezier((385, 164), (392, 161), (394,152));
            sink.AddBezier((395, 144), (402, 141), (402, 142));
            sink.EndFigure(FigureEnd.Open);

            sink.BeginFigure(new PointF(408, 182), FigureBegin.Hollow);
            sink.AddBezier((408, 182), (416, 184), (422, 178));
            sink.AddBezier((428, 171), (435, 173), (435, 173));
            sink.EndFigure(FigureEnd.Open);

            sink.Close();

            // River
            _riverGeometry = Direct2dFactory.CreatePathGeometry();
            sink = _riverGeometry.Open();
            sink.SetFillMode(FillMode.Winding);
            sink.BeginFigure(new PointF(183, 392), FigureBegin.Filled);
            sink.AddBezier((238, 284), (472, 345), (356, 303));
            sink.AddBezier((237, 261), (333, 256), (333, 256));
            sink.AddBezier((335, 257), (241, 261), (411, 306));
            sink.AddBezier((574, 350), (288, 324), (296, 392));
            sink.EndFigure(FigureEnd.Open);
            sink.Close();
        }

        protected override void CreateResources(WindowHandle window)
        {
            _sceneBrush = RenderTarget.CreateSolidColorBrush(Color.Black);

            // Background grid brush
            var bitmapTarget = RenderTarget.CreateCompatibleRenderTarget(new SizeF(10, 10));
            var gridBrush = bitmapTarget.CreateSolidColorBrush(new ColorF(.93f, .94f, .96f));
            bitmapTarget.BeginDraw();
            bitmapTarget.FillRectangle(RectangleF.FromLTRB(0, 0, 10, 1), gridBrush);
            bitmapTarget.FillRectangle(RectangleF.FromLTRB(0, 0, 1, 10), gridBrush);
            bitmapTarget.EndDraw().ThrowIfFailed();
            IBitmap bitmap = bitmapTarget.GetBitmap();
            _gridPatternBrush = RenderTarget.CreateBitmapBrush(bitmap, new BitmapBrushProperties(ExtendMode.Wrap, ExtendMode.Wrap));

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
        }

        protected override void OnPaint(WindowHandle window)
        {
            SizeF targetSize = RenderTarget.GetSize();

            RenderTarget.SetTransform();
            RenderTarget.Clear(Color.White);
            RenderTarget.FillRectangle(RenderTarget.GetSize(), _gridPatternBrush);
            RenderTarget.SetTransform(Matrix3x2.CreateScale(Math.Min(targetSize.Width / 840.0f, targetSize.Height / 700.0f) * 1.4f));

            RenderTarget.FillGeometry(_sunGeometry, _radialGradientBrush);
            _sceneBrush.SetColor(Color.Black);
            RenderTarget.DrawGeometry(_sunGeometry, _sceneBrush);

            _sceneBrush.SetColor(Color.OliveDrab);
            RenderTarget.FillGeometry(_leftMountainGeometry, _sceneBrush);
            _sceneBrush.SetColor(Color.Black);
            RenderTarget.DrawGeometry(_leftMountainGeometry, _sceneBrush);

            _sceneBrush.SetColor(Color.LightSkyBlue);
            RenderTarget.FillGeometry(_riverGeometry, _sceneBrush);
            _sceneBrush.SetColor(Color.Black);
            RenderTarget.DrawGeometry(_riverGeometry, _sceneBrush);

            _sceneBrush.SetColor(Color.YellowGreen);
            RenderTarget.FillGeometry(_rightMountainGeometry, _sceneBrush);
            _sceneBrush.SetColor(Color.Black);
            RenderTarget.DrawGeometry(_rightMountainGeometry, _sceneBrush);
        }
    }
}
