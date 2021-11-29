// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Drawing;
using System.Numerics;
using System.Runtime.InteropServices;

namespace WInterop.Direct2d;

/// <summary>
///  Paints an area with a radial gradient. [ID2D1RadialGradientBrush]
/// </summary>
[StructLayout(LayoutKind.Sequential)]
[Guid(InterfaceIds.IID_ID2D1RadialGradientBrush)]
public readonly unsafe struct RadialGradientBrush : RadialGradientBrush.Interface, IDisposable
{
    private readonly ID2D1RadialGradientBrush* _handle;

    public Factory GetFactory() => Resource.From(this).GetFactory();

    public float Opacity
    {
        get => Brush.From(this).Opacity;
        set => Brush.From(this).Opacity = value;
    }

    public Matrix3x2 Transform
    {
        get => Brush.From(this).Transform;
        set => Brush.From(this).Transform = value;
    }

    /// <inheritdoc />
    public PointF Center
    {
        get => _handle->GetCenter().ToPointF();
        set => _handle->SetCenter(value.ToD2D());
    }

    public PointF GradientOriginOffset
    {
        get => _handle->GetGradientOriginOffset().ToPointF();
        set => _handle->SetGradientOriginOffset(value.ToD2D());
    }

    public float RadiusX
    {
        get => _handle->GetRadiusX();
        set => _handle->SetRadiusX(value);
    }

    public float RadiusY
    {
        get => _handle->GetRadiusY();
        set => _handle->SetRadiusY(value);
    }

    public PointF GetCenter() => _handle->GetCenter().ToPointF();

    public PointF GetGradientOriginOffset() => _handle->GetGradientOriginOffset().ToPointF();

    public GradientStopCollection GetGradientStopCollection()
    {
        ID2D1GradientStopCollection* collection;
        _handle->GetGradientStopCollection(&collection);
        return new(collection);
    }

    public void Dispose() => _handle->Release();

    public static implicit operator Brush(RadialGradientBrush brush) => new((ID2D1Brush*)brush._handle);

    internal interface Interface : Brush.Interface
    {
        /// <summary>
        ///  The center of the radial gradient. This will be in local coordinates and
        ///  will not depend on the geometry being filled.
        /// </summary>
        PointF Center { get; set; }

        /// <summary>
        ///  The offset of the origin relative to the radial gradient center.
        /// </summary>
        PointF GradientOriginOffset { get; set; }

        float RadiusX { get; set; }

        float RadiusY { get; set; }

        PointF GetCenter();

        PointF GetGradientOriginOffset();

        GradientStopCollection GetGradientStopCollection();
    }
}
