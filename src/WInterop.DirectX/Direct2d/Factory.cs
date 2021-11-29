// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Drawing;
using System.Numerics;

namespace WInterop.Direct2d;

/// <summary>
///  [ID2D1Factory]
/// </summary>
/// <docs>
///  https://docs.microsoft.com/windows/win32/api/d2d1/nn-d2d1-id2d1factory
/// </docs>
public readonly unsafe struct Factory : Factory.Interface, IDisposable
{
    private readonly ID2D1Factory* _handle;

    internal Factory(ID2D1Factory* factory) => _handle = factory;

    public EllipseGeometry CreateEllipseGeometry(Ellipse ellipse)
    {
        ID2D1EllipseGeometry* geometry;
        _handle->CreateEllipseGeometry((D2D1_ELLIPSE*)&ellipse, &geometry).ThrowIfFailed();
        return new(geometry);
    }

    public GeometryGroup CreateGeometryGroup(FillMode fillMode, ReadOnlySpan<Geometry> geometries)
    {
        fixed (Geometry* g = geometries)
        {
            ID2D1GeometryGroup* group;
            _handle->CreateGeometryGroup(
                (D2D1_FILL_MODE)fillMode,
                (ID2D1Geometry**)&g,
                (uint)geometries.Length,
                &group).ThrowIfFailed();

            return new(group);
        }
    }

    public PathGeometry CreatePathGeometry()
    {
        ID2D1PathGeometry* geometry;
        _handle->CreatePathGeometry(&geometry).ThrowIfFailed();
        return new(geometry);
    }

    public RectangleGeometry CreateRectangleGeometry(RectangleF rectangle)
    {
        ID2D1RectangleGeometry* geometry;
        var rect = rectangle.ToD2D();
        _handle->CreateRectangleGeometry(&rect, &geometry).ThrowIfFailed();
        return new(geometry);
    }

    public RoundedRectangleGeometry CreateRoundedRectangleGeometry(RoundedRectangle roundedRectangle)
    {
        ID2D1RoundedRectangleGeometry* geometry;
        _handle->CreateRoundedRectangleGeometry((D2D1_ROUNDED_RECT*)&roundedRectangle, &geometry).ThrowIfFailed();
        return new(geometry);
    }

    public StrokeStyle CreateStrokeStyle(StrokeStyleProperties strokeStyleProperties, ReadOnlySpan<float> dashes = default)
    {
        ID2D1StrokeStyle* style;
        fixed (float* d = dashes)
        {
            _handle->CreateStrokeStyle(
                (D2D1_STROKE_STYLE_PROPERTIES*)&strokeStyleProperties,
                d,
                (uint)dashes.Length,
                &style).ThrowIfFailed();

            return new(style);
        }
    }

    public TransformedGeometry CreateTransformedGeometry(Geometry sourceGeometry, Matrix3x2 transform)
    {
        ID2D1TransformedGeometry* geometry;
        _handle->CreateTransformedGeometry(
            sourceGeometry._handle,
            (D2D_MATRIX_3X2_F*)&transform,
            &geometry).ThrowIfFailed();

        return new(geometry);
    }

    public IWindowRenderTarget CreateWindowRenderTarget(
        RenderTargetProperties renderTargetProperties,
        WindowRenderTargetProperties hwndRenderTargetProperties)
    {
        ID2D1HwndRenderTarget* target;
        _handle->CreateHwndRenderTarget(
            (D2D1_RENDER_TARGET_PROPERTIES*)&renderTargetProperties,
            (D2D1_HWND_RENDER_TARGET_PROPERTIES*)&hwndRenderTargetProperties,
            &target).ThrowIfFailed();

        return new WindowRenderTarget(target);
    }

    public void Dispose() => _handle->Release();

    public SizeF GetDesktopDpi()
    {
        float x;
        float y;
        _handle->GetDesktopDpi(&x, &y);
        return new(x, y);
    }

    public void ReloadSystemMetrics() => _handle->ReloadSystemMetrics().ThrowIfFailed();

    internal unsafe interface Interface
    {
        RectangleGeometry CreateRectangleGeometry(RectangleF rectangle);

        RoundedRectangleGeometry CreateRoundedRectangleGeometry(RoundedRectangle roundedRectangle);

        EllipseGeometry CreateEllipseGeometry(Ellipse ellipse);

        GeometryGroup CreateGeometryGroup(
            FillMode fillMode,
            ReadOnlySpan<Geometry> geometries);

        TransformedGeometry CreateTransformedGeometry(
            Geometry sourceGeometry,
            Matrix3x2 transform);

        /// <summary>
        ///  Returns an initially empty path geometry interface. A geometry sink is created
        ///  off the interface to populate it.
        /// </summary>
        PathGeometry CreatePathGeometry();

        /// <summary>
        ///  Allows a non-default stroke style to be specified for a given geometry at draw time.
        /// </summary>
        StrokeStyle CreateStrokeStyle(
            StrokeStyleProperties strokeStyleProperties,
            ReadOnlySpan<float> dashes = default);

        /// <summary>
        ///  Creates a render target that appears on the display. [CreateHwndRenderTarget]
        /// </summary>
        IWindowRenderTarget CreateWindowRenderTarget(
            RenderTargetProperties renderTargetProperties,
            WindowRenderTargetProperties hwndRenderTargetProperties);

        /// <summary>
        ///  Gets the current desktop DPI.
        /// </summary>
        SizeF GetDesktopDpi();

        /// <summary>
        ///  Forces the factory to refresh any system defaults that it might have changed since factory creation.
        /// </summary>
        void ReloadSystemMetrics();
    }
}
