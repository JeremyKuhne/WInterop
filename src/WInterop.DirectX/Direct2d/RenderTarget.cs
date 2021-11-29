// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Drawing;
using System.Numerics;
using System.Runtime.InteropServices;
using WInterop.DirectWrite;
using WInterop.Errors;

namespace WInterop.Direct2d
{
    /// <summary>
    ///  Represents an object that can receive drawing commands. Interfaces that inherit
    ///  from this render the drawing commands they receive in different
    ///  ways. [ID2D1RenderTarget]
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    [Guid(InterfaceIds.IID_ID2D1RenderTarget)]
    internal unsafe class RenderTarget : IRenderTarget, IResource<ID2D1RenderTarget>
    {
        private readonly ID2D1RenderTarget* _handle;

        ID2D1RenderTarget* IHandle<ID2D1RenderTarget>.Handle => _handle;

        public Factory GetFactory() => this.GetFactory<RenderTarget, ID2D1RenderTarget>();

        internal RenderTarget(ID2D1RenderTarget* handle) => _handle = handle;

        public Matrix3x2 Transform
        {
            get
            {
                Matrix3x2 transform;
                _handle->GetTransform((D2D_MATRIX_3X2_F*)&transform);
                return transform;
            }
            set => _handle->SetTransform((D2D_MATRIX_3X2_F*)&value);
        }

        public AntialiasMode AntialiasMode
        {
            get => (AntialiasMode)_handle->GetAntialiasMode();
            set => _handle->SetAntialiasMode((D2D1_ANTIALIAS_MODE)value);
        }

        public TextAntialiasMode TextAntialiasMode
        {
            get => (TextAntialiasMode)_handle->GetTextAntialiasMode();
            set => _handle->SetTextAntialiasMode((D2D1_TEXT_ANTIALIAS_MODE)value);
        }

        public PointF Dpi
        {
            get
            {
                float x;
                float y;
                _handle->GetDpi(&x, &y);
                return new(x, y);
            }
            set => _handle->SetDpi(value.X, value.Y);
        }

        public void BeginDraw() => _handle->BeginDraw();

        public SolidColorBrush CreateSolidColorBrush(ColorF color)
        {
            SolidColorBrush brush;
            _handle->CreateSolidColorBrush(
                (DXGI_RGBA*)&color,
                (ID2D1SolidColorBrush**)&brush).ThrowIfFailed();

            return brush;
        }

        public BitmapBrush CreateBitmapBrush(
            Bitmap bitmap,
            BitmapBrushProperties bitmapBrushProperties,
            BrushProperties brushProperties)
        {
            ID2D1BitmapBrush* brush;
            _handle->CreateBitmapBrush(
                bitmap._handle,
                (D2D1_BITMAP_BRUSH_PROPERTIES*)&bitmapBrushProperties,
                (D2D1_BRUSH_PROPERTIES*)&brushProperties,
                &brush).ThrowIfFailed();

            return new(brush);
        }

        public SolidColorBrush CreateSolidColorBrush(ColorF color, BrushProperties brushProperties)
        {
            SolidColorBrush brush;
            _handle->CreateSolidColorBrush(
                (DXGI_RGBA*)&color,
                (D2D1_BRUSH_PROPERTIES*)&brushProperties,
                (ID2D1SolidColorBrush**)&brush).ThrowIfFailed();

            return brush;
        }

        public GradientStopCollection CreateGradientStopCollection(
            ReadOnlySpan<GradientStop> gradientStops,
            Gamma colorInterpolationGamma,
            ExtendMode extendMode)
        {
            fixed (GradientStop* g = gradientStops)
            {
                GradientStopCollection collection;
                _handle->CreateGradientStopCollection(
                    (D2D1_GRADIENT_STOP*)g,
                    (uint)gradientStops.Length,
                    (D2D1_GAMMA)colorInterpolationGamma,
                    (D2D1_EXTEND_MODE)extendMode,
                    (ID2D1GradientStopCollection**)&collection).ThrowIfFailed();
                return collection;
            }
        }

        public RadialGradientBrush CreateRadialGradientBrush(
            RadialGradientBrushProperties radialGradientBrushProperties,
            BrushProperties brushProperties,
            GradientStopCollection gradientStopCollection)
        {
            RadialGradientBrush brush;
            _handle->CreateRadialGradientBrush(
                (D2D1_RADIAL_GRADIENT_BRUSH_PROPERTIES*)&radialGradientBrushProperties,
                (D2D1_BRUSH_PROPERTIES*)&brushProperties,
                gradientStopCollection._handle,
                (ID2D1RadialGradientBrush**)&brush).ThrowIfFailed();

            return brush;
        }

        public IBitmapRenderTarget CreateCompatibleRenderTarget(
            SizeF? desiredSize = default,
            SizeU? desiredPixelSize = default,
            PixelFormat? desiredFormat = default,
            CompatibleRenderTargetOptions options = CompatibleRenderTargetOptions.None)
        {
            ID2D1BitmapRenderTarget* target;
            var specifiedSize = desiredSize.GetValueOrDefault();
            var specifiedPixelSize = desiredPixelSize.GetValueOrDefault();
            var specifiedFormat = desiredFormat.GetValueOrDefault();
            _handle->CreateCompatibleRenderTarget(specifiedSize.ToD2D(), specifiedPixelSize.ToD2D(), specifiedFormat.ToD2D(), &target);
            return new BitmapRenderTarget(target);
        }

        public void DrawLine(PointF point0, PointF point1, Brush brush, float strokeWidth = 1, StrokeStyle strokeStyle = default)
        {
            _handle->DrawLine(
                point0.ToD2D(),
                point1.ToD2D(),
                brush.Handle,
                strokeWidth,
                strokeStyle._handle);
        }

        public void DrawRectangle(LtrbRectangleF rectangle, Brush brush, float strokeWidth = 1, StrokeStyle strokeStyle = default)
        {
            _handle->DrawRectangle(
                (D2D_RECT_F*)&rectangle,
                brush.Handle,
                strokeWidth,
                strokeStyle._handle);
        }

        public void FillRectangle(LtrbRectangleF rectangle, Brush brush)
        {
            _handle->FillRectangle(
                (D2D_RECT_F*)&rectangle,
                brush.Handle);
        }

        public void DrawRoundedRectangle(
            RoundedRectangle roundedRectangle,
            Brush brush,
            float strokeWidth = 1,
            StrokeStyle strokeStyle = default)
        {
            _handle->DrawRoundedRectangle(
                (D2D1_ROUNDED_RECT*)&roundedRectangle,
                brush.Handle,
                strokeWidth,
                strokeStyle._handle);
        }

        public void FillRoundedRectangle(RoundedRectangle roundedRectangle, Brush brush)
        {
            _handle->FillRoundedRectangle(
                (D2D1_ROUNDED_RECT*)&roundedRectangle,
                brush.Handle);
        }

        public void DrawEllipse(Ellipse ellipse, Brush brush, float strokeWidth = 1, StrokeStyle strokeStyle = default)
        {
            _handle->DrawEllipse(
                (D2D1_ELLIPSE*)&ellipse,
                brush.Handle,
                strokeWidth,
                strokeStyle._handle);
        }

        public void FillEllipse(Ellipse ellipse, Brush brush)
        {
            _handle->FillEllipse(
                (D2D1_ELLIPSE*)&ellipse,
                brush.Handle);
        }

        public void DrawGeometry(Geometry geometry, Brush brush, float strokeWidth = 1, StrokeStyle strokeStyle = default)
        {
            _handle->DrawGeometry(
                geometry._handle,
                brush.Handle,
                strokeWidth,
                strokeStyle._handle);
        }

        public void FillGeometry(Geometry geometry, Brush brush, Brush opacityBrush = default)
        {
            _handle->FillGeometry(
                geometry._handle,
                brush.Handle,
                opacityBrush.Handle);
        }

        public Tags Tags
        {
            get
            {
                Tags tags;
                _handle->GetTags(&tags.One, &tags.Two);
                return tags;
            }
            set => _handle->SetTags(value.One, value.Two);
        }

        public void PopLayer() => _handle->PopLayer();

        public Tags Flush()
        {
            Tags tags;
            _handle->Flush(&tags.One, &tags.Two);
            return tags;
        }

        public void PushAxisAlignedClip(LtrbRectangleF clipRect, AntialiasMode antialiasMode)
        {
            _handle->PushAxisAlignedClip((D2D_RECT_F*)&clipRect, (D2D1_ANTIALIAS_MODE)antialiasMode);
        }

        public void PopAxisAlignedClip() => _handle->PopAxisAlignedClip();

        public void Clear() => _handle->Clear();

        public void Clear(ColorF clearColor) => _handle->Clear((DXGI_RGBA*)&clearColor);

        public Tags EndDraw(out bool recreateTarget)
        {
            Tags tags;
            HResult result = _handle->EndDraw(&tags.One, &tags.Two).ToHResult();
            recreateTarget = result == HResult.D2DERR_RECREATE_TARGET;
            result.ThrowIfFailed();
            return tags;
        }

        public PixelFormat PixelFormat => new(_handle->GetPixelFormat());

        public SizeF Size => _handle->GetSize().ToSizeF();

        public SizeU PixelSize => new(_handle->GetPixelSize());

        public uint MaximumBitmapSize => _handle->GetMaximumBitmapSize();

        public bool IsSupported(RenderTargetProperties renderTargetProperties)
            => _handle->IsSupported((D2D1_RENDER_TARGET_PROPERTIES*)&renderTargetProperties);

        public void Dispose() => _handle->Release();

        public void DrawTextLayout(
            PointF origin,
            TextLayout textLayout,
            Brush defaultFillBrush,
            DrawTextOptions options = DrawTextOptions.None)
        {
            _handle->DrawTextLayout(
                origin.ToD2D(),
                textLayout.Handle,
                defaultFillBrush.Handle,
                (D2D1_DRAW_TEXT_OPTIONS)options);
        }
    }
}
