// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Drawing;
using System.Numerics;
using System.Runtime.InteropServices;

namespace WInterop.Direct2d;

/// <summary>
///  [ID2D1RoundedRectangleGeometry]
/// </summary>
[StructLayout(LayoutKind.Sequential)]
[Guid(InterfaceIds.IID_ID2D1RoundedRectangleGeometry)]
public unsafe struct RoundedRectangleGeometry : RoundedRectangleGeometry.Interface
{
    private readonly ID2D1RoundedRectangleGeometry* _handle;

    internal RoundedRectangleGeometry(ID2D1RoundedRectangleGeometry* handle) => _handle = handle;

    public RectangleF GetBounds() => Geometry.From(this).GetBounds();

    public RectangleF GetBounds(Matrix3x2 worldTransform)
        => Geometry.From(this).GetBounds(worldTransform);

    public void CombineWithGeometry(Geometry inputGeometry, CombineMode combineMode, SimplifiedGeometrySink geometrySink)
        => Geometry.From(this).CombineWithGeometry(inputGeometry, combineMode, geometrySink);

    public Factory GetFactory() => Resource.From(this).GetFactory();

    public RoundedRectangle GetRoundedRect(RoundedRectangle roundedRect)
    {
        RoundedRectangle rect;
        _handle->GetRoundedRect((D2D1_ROUNDED_RECT*)&rect);
        return rect;
    }

    public void Dispose() => _handle->Release();

    public static implicit operator Geometry(RoundedRectangleGeometry geometry) => new((ID2D1Geometry*)geometry._handle);

    internal interface Interface : Geometry.Interface
    {
        RoundedRectangle GetRoundedRect(RoundedRectangle roundedRect);
    }
}
