// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace WInterop.Direct2d;

/// <summary>
///  The root brush interface. All brushes can be used to fill or pen a geometry.
///  [ID2D1Brush]
/// </summary>
[StructLayout(LayoutKind.Sequential)]
[Guid(InterfaceIds.IID_ID2D1Brush)]
public readonly unsafe struct Brush : Brush.Interface, IDisposable
{
    internal readonly ID2D1Brush* Handle { get; }

    internal Brush(ID2D1Brush* handle) => Handle = handle;

    public unsafe Factory GetFactory() => Resource.From(this).GetFactory();

    public float Opacity
    {
        get => Handle->GetOpacity();
        set => Handle->SetOpacity(value);
    }

    public Matrix3x2 Transform
    {
        get
        {
            Matrix3x2 matrix;
            Handle->GetTransform((D2D_MATRIX_3X2_F*)&matrix);
            return matrix;
        }
        set => Handle->SetTransform((D2D_MATRIX_3X2_F*)&value);
    }

    internal static ref Brush From<TFrom>(in TFrom from)
        where TFrom : unmanaged, Interface
        => ref Unsafe.AsRef<Brush>(Unsafe.AsPointer(ref Unsafe.AsRef(from)));

    public void Dispose() => Handle->Release();

    internal interface Interface : Resource.Interface
    {
        /// <summary>
        ///  Sets the opacity for when the brush is drawn over the entire fill of the brush.
        /// </summary>
        float Opacity { get; set; }

        /// <summary>
        ///  Sets the transform that applies to everything drawn by the brush.
        /// </summary>
        Matrix3x2 Transform { get; set; }
    }
}
