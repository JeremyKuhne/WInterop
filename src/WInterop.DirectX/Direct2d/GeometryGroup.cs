// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Drawing;
using System.Numerics;
using System.Runtime.InteropServices;

namespace WInterop.Direct2d;

/// <summary>
///  [ID2D1GeometryGroup]
/// </summary>
[StructLayout(LayoutKind.Sequential)]
[Guid(InterfaceIds.IID_ID2D1GeometryGroup)]
public readonly unsafe struct GeometryGroup : GeometryGroup.Interface, IDisposable
{
    internal readonly ID2D1GeometryGroup* _handle;

    internal GeometryGroup(ID2D1GeometryGroup* handle) => _handle = handle;

    public RectangleF GetBounds() => Geometry.From(this).GetBounds();

    public RectangleF GetBounds(Matrix3x2 worldTransform)
        => Geometry.From(this).GetBounds(worldTransform);

    public void CombineWithGeometry(Geometry inputGeometry, CombineMode combineMode, SimplifiedGeometrySink geometrySink)
        => Geometry.From(this).CombineWithGeometry(inputGeometry, combineMode, geometrySink);

    public Factory GetFactory() => Geometry.From(this).GetFactory();

    public FillMode GetFillMode() => (FillMode)_handle->GetFillMode();

    public void GetSourceGeometries(Span<Geometry> geometries)
    {
        fixed (void* g = geometries)
        {
            _handle->GetSourceGeometries((ID2D1Geometry**)&g, (uint)geometries.Length);
        }
    }

    public uint GetSourceGeometryCount() => _handle->GetSourceGeometryCount();

    public void Dispose() => _handle->Release();

    public static implicit operator Geometry(GeometryGroup geometry) => new((ID2D1Geometry*)geometry._handle);

    internal unsafe interface Interface : Geometry.Interface
    {
        FillMode GetFillMode();

        uint GetSourceGeometryCount();

        unsafe void GetSourceGeometries(Span<Geometry> geometries);
    }
}
