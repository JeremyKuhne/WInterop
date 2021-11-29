// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Drawing;
using System.Runtime.InteropServices;
using System.Numerics;

namespace WInterop.Direct2d;

/// <summary>
///  [ID2D1EllipseGeometry]
/// </summary>
[StructLayout(LayoutKind.Sequential)]
[Guid(InterfaceIds.IID_ID2D1ElipseGeometry)]
public readonly unsafe struct EllipseGeometry : EllipseGeometry.Interface, IDisposable
{
    internal readonly ID2D1EllipseGeometry* _handle;

    internal EllipseGeometry(ID2D1EllipseGeometry* handle) => _handle = handle;

    public RectangleF GetBounds() => Geometry.From(this).GetBounds();

    public RectangleF GetBounds(Matrix3x2 worldTransform)
        => Geometry.From(this).GetBounds(worldTransform);

    public void CombineWithGeometry(Geometry inputGeometry, CombineMode combineMode, SimplifiedGeometrySink geometrySink)
        => Geometry.From(this).CombineWithGeometry(inputGeometry, combineMode, geometrySink);

    public Ellipse GetEllipse()
    {
        Ellipse ellipse;
        _handle->GetEllipse((D2D1_ELLIPSE*)&ellipse);
        return ellipse;
    }

    public Factory GetFactory() => Resource.From(this).GetFactory();

    public void Dispose() => _handle->Release();

    public static implicit operator Geometry(EllipseGeometry geometry) => new((ID2D1Geometry*)geometry._handle);

    internal unsafe interface Interface : Geometry.Interface
    {
        Ellipse GetEllipse();
    }
}
