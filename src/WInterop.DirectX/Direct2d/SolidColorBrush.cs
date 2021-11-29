// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Numerics;
using System.Runtime.InteropServices;

namespace WInterop.Direct2d;

/// <summary>
///  Paints an area with a solid color. [ID2D1SolidColorBrush]
/// </summary>
[StructLayout(LayoutKind.Sequential)]
[Guid(InterfaceIds.IID_ID2D1SolidColorBrush)]
public readonly unsafe struct SolidColorBrush : SolidColorBrush.Interface, IDisposable
{
    private readonly ID2D1SolidColorBrush* _handle;

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

    public ColorF Color
    {
        get => _handle->GetColor();
        set => _handle->SetColor((DXGI_RGBA*)&value);
    }

    public void Dispose() => _handle->Release();

    public static implicit operator Brush(SolidColorBrush brush) => new((ID2D1Brush*)brush._handle);

    internal interface Interface : Brush.Interface
    {
        ColorF Color { get; set; }
    }
}
