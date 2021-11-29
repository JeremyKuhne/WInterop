// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Numerics;
using System.Runtime.InteropServices;

namespace WInterop.Direct2d;

/// <summary>
///  A bitmap brush allows a bitmap to be used to fill a geometry. [ID2D1BitmapBrush]
/// </summary>
[Guid(InterfaceIds.IID_ID2D1BitmapBrush)]
public readonly unsafe struct BitmapBrush : BitmapBrush.Interface
{
    private readonly ID2D1BitmapBrush* _handle;

    internal BitmapBrush(ID2D1BitmapBrush* handle) => _handle = handle;

    public unsafe Factory GetFactory() => Resource.From(this).GetFactory();

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

    public ExtendMode ExtendModeX
    {
        get => (ExtendMode)_handle->GetExtendModeX();
        set => _handle->SetExtendModeX((D2D1_EXTEND_MODE)value);
    }

    public ExtendMode ExtendModeY
    {
        get => (ExtendMode)_handle->GetExtendModeY();
        set => _handle->SetExtendModeY((D2D1_EXTEND_MODE)value);
    }

    public Bitmap Bitmap
    {
        get
        {
            ID2D1Bitmap* bitmap;
            _handle->GetBitmap(&bitmap);
            return new(bitmap);
        }
        set => _handle->SetBitmap(value._handle);
    }

    public BitmapInterpolationMode InterpolationMode
    {
        get => (BitmapInterpolationMode)_handle->GetInterpolationMode();
        set => _handle->SetInterpolationMode((D2D1_BITMAP_INTERPOLATION_MODE)value);
    }

    public void Dispose() => _handle->Release();

    public static implicit operator Brush(BitmapBrush brush) => new((ID2D1Brush*)brush._handle);

    internal interface Interface : Brush.Interface
    {
        /// <summary>
        ///  How the bitmap is to be treated outside of its natural extent on the X axis.
        /// </summary>
        ExtendMode ExtendModeX { get; set; }

        /// <summary>
        ///  How the bitmap is to be treated outside of its natural extent on the Y axis.
        /// </summary>
        ExtendMode ExtendModeY { get; set; }

        /// <summary>
        ///  Sets the bitmap associated as the source of this brush.
        /// </summary>
        Bitmap Bitmap { get; set; }

        /// <summary>
        ///  The interpolation mode used when this brush is used.
        /// </summary>
        BitmapInterpolationMode InterpolationMode { get; set; }
    }
}
