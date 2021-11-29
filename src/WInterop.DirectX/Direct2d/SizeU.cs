// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Drawing;
using System.Runtime.CompilerServices;

namespace WInterop.Direct2d;

/// <summary>
///  [D2D_SIZE_U]
/// </summary>
public readonly struct SizeU
{
    public readonly uint Width;
    public readonly uint Height;

    public SizeU(uint width, uint height)
    {
        Width = width;
        Height = height;
    }

    internal SizeU(D2D_SIZE_U size)
    {
        Width = size.width;
        Height = size.height;
    }

    public static implicit operator Size(SizeU size) => checked(new((int)size.Width, (int)size.Height));
    public static implicit operator SizeU(Size size) => checked(new((uint)size.Width, (uint)size.Height));

    internal D2D_SIZE_U ToD2D() => Unsafe.As<SizeU, D2D_SIZE_U>(ref Unsafe.AsRef(this));
}
