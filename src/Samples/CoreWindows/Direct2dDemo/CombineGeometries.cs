// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Drawing;
using System.Numerics;
using WInterop.Direct2d;
using WInterop.DirectX;
using WInterop.Windows;

namespace Direct2dDemo;

// https://docs.microsoft.com/windows/win32/direct2d/how-to-combine-geometries
public class CombineGeometries : DirectXWindowClass
{
    private readonly EllipseGeometry _circleGeometry1;
    private readonly EllipseGeometry _circleGeometry2;
    private readonly PathGeometry _geometryUnion;
    private readonly PathGeometry _geometryIntersect;
    private readonly PathGeometry _geometryXOR;
    private readonly PathGeometry _geometryExclude;
    private SolidColorBrush _brush;
    private SolidColorBrush _strokeBrush;
    private SolidColorBrush _fillBrush;

    public CombineGeometries() : base()
    {
        _circleGeometry1 = Direct2dFactory.CreateEllipseGeometry(new((75.0f, 75.0f), 50.0f, 50.0f));
        _circleGeometry2 = Direct2dFactory.CreateEllipseGeometry(new((125.0f, 75.0f), 50.0f, 50.0f));
        _geometryUnion = Direct2dFactory.CreatePathGeometry();
        using (var sink = _geometryUnion.Open())
        {
            _circleGeometry1.CombineWithGeometry(_circleGeometry2, CombineMode.Union, sink);
        }

        _geometryIntersect = Direct2dFactory.CreatePathGeometry();
        using (var sink = _geometryIntersect.Open())
        {
            _circleGeometry1.CombineWithGeometry(_circleGeometry2, CombineMode.Intersect, sink);
        }

        _geometryXOR = Direct2dFactory.CreatePathGeometry();
        using (var sink = _geometryXOR.Open())
        {
            _circleGeometry1.CombineWithGeometry(_circleGeometry2, CombineMode.XOr, sink);
        }

        _geometryExclude = Direct2dFactory.CreatePathGeometry();
        using (var sink = _geometryExclude.Open())
        {
            _circleGeometry1.CombineWithGeometry(_circleGeometry2, CombineMode.Exclude, sink);
        }
    }

    protected override void CreateResources()
    {
        _brush = RenderTarget.CreateSolidColorBrush(Color.Black);
        _strokeBrush = RenderTarget.CreateSolidColorBrush(Color.Blue);
        _fillBrush = RenderTarget.CreateSolidColorBrush(new(Color.CornflowerBlue, 0.5f));
    }

    protected override void OnPaint(WindowHandle window)
    {
        RenderTarget.Transform = Matrix3x2.Identity;
        RenderTarget.Clear(Color.White);

        RenderTarget.Transform = Matrix3x2.CreateTranslation(new(20, 100));
        RenderTarget.FillGeometry(_circleGeometry1, _fillBrush);
        RenderTarget.DrawGeometry(_circleGeometry1, _strokeBrush);
        RenderTarget.FillGeometry(_circleGeometry2, _fillBrush);
        RenderTarget.DrawGeometry(_circleGeometry2, _strokeBrush);
        RenderTarget.Transform = Matrix3x2.Identity;

        RenderTarget.Transform = Matrix3x2.CreateTranslation(new(300, 0));
        RenderTarget.FillGeometry(_geometryUnion, _fillBrush);
        RenderTarget.DrawGeometry(_geometryUnion, _strokeBrush);
        RenderTarget.Transform = Matrix3x2.Identity;

        RenderTarget.Transform = Matrix3x2.CreateTranslation(new(550, 0));
        RenderTarget.FillGeometry(_geometryIntersect, _fillBrush);
        RenderTarget.DrawGeometry(_geometryIntersect, _strokeBrush);
        RenderTarget.Transform = Matrix3x2.Identity;

        RenderTarget.Transform = Matrix3x2.CreateTranslation(new(300, 200));
        RenderTarget.FillGeometry(_geometryXOR, _fillBrush);
        RenderTarget.DrawGeometry(_geometryXOR, _strokeBrush);
        RenderTarget.Transform = Matrix3x2.Identity;

        RenderTarget.Transform = Matrix3x2.CreateTranslation(new(550, 200));
        RenderTarget.FillGeometry(_geometryExclude, _fillBrush);
        RenderTarget.DrawGeometry(_geometryExclude, _strokeBrush);
        RenderTarget.Transform = Matrix3x2.Identity;
    }
}
