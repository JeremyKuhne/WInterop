// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Drawing;
using System.Numerics;
using System.Runtime.InteropServices;

namespace WInterop.Direct2d;

/// <summary>
///  Represents a geometry that has been transformed. [ID2D1TransformedGeometry]
/// </summary>
[Guid(InterfaceIds.IID_ID2D1TransformedGeometry)]
[StructLayout(LayoutKind.Sequential)]
public readonly unsafe struct TransformedGeometry : TransformedGeometry.Interface, IDisposable
{
    internal readonly ID2D1TransformedGeometry* _handle;

    internal TransformedGeometry(ID2D1TransformedGeometry* handle) => _handle = handle;

    public RectangleF GetBounds() => Geometry.From(this).GetBounds();

    public RectangleF GetBounds(Matrix3x2 worldTransform)
        => Geometry.From(this).GetBounds(worldTransform);

    public void CombineWithGeometry(Geometry inputGeometry, CombineMode combineMode, SimplifiedGeometrySink geometrySink)
        => Geometry.From(this).CombineWithGeometry(inputGeometry, combineMode, geometrySink);

    public Factory GetFactory() => Resource.From(this).GetFactory();

    public Geometry SourceGeometry
    {
        get
        {
            Geometry geometry;
            _handle->GetSourceGeometry((ID2D1Geometry**)&geometry);
            return geometry;
        }
    }

    public Matrix3x2 Transform
    {
        get
        {
            Matrix3x2 transform;
            _handle->GetTransform((D2D_MATRIX_3X2_F*)&transform);
            return transform;
        }
    }

    public static implicit operator Geometry(TransformedGeometry geometry) => new((ID2D1Geometry*)geometry._handle);

    public void Dispose() => _handle->Release();

    internal interface Interface : Geometry.Interface
    {
        Geometry SourceGeometry { get; }

        Matrix3x2 Transform { get; }
    }
}
