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
public readonly unsafe struct RoundedRectangleGeometry : RoundedRectangleGeometry.Interface
{
    private readonly ID2D1RoundedRectangleGeometry* _handle;

    internal RoundedRectangleGeometry(ID2D1RoundedRectangleGeometry* handle) => _handle = handle;

    public readonly RectangleF GetBounds() => Geometry.From(this).GetBounds();

    public readonly RectangleF GetBounds(Matrix3x2 worldTransform)
        => Geometry.From(this).GetBounds(worldTransform);

    public readonly void CombineWithGeometry(Geometry inputGeometry, CombineMode combineMode, SimplifiedGeometrySink geometrySink)
        => Geometry.From(this).CombineWithGeometry(inputGeometry, combineMode, geometrySink);

    public readonly Factory GetFactory() => Resource.From(this).GetFactory();

    public readonly RoundedRectangle GetRoundedRect(RoundedRectangle roundedRect)
    {
        RoundedRectangle rect;
        _handle->GetRoundedRect((D2D1_ROUNDED_RECT*)&rect);
        return rect;
    }

    public readonly void Dispose() => _handle->Release();

    public static implicit operator Geometry(RoundedRectangleGeometry geometry) => new((ID2D1Geometry*)geometry._handle);

    internal interface Interface : Geometry.Interface
    {
        RoundedRectangle GetRoundedRect(RoundedRectangle roundedRect);
    }
}
