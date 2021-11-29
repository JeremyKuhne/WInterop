// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Runtime.CompilerServices;

namespace WInterop.Direct2d;

/// <summary>
///  Description of a pixel format. [D2D1_PIXEL_FORMAT]
/// </summary>
public readonly struct PixelFormat
{
    public readonly Dxgi.Format Format;
    public readonly AlphaMode AlphaMode;

    public PixelFormat(Dxgi.Format format = Dxgi.Format.Unknown, AlphaMode alphaMode = AlphaMode.Unknown)
    {
        Format = format;
        AlphaMode = alphaMode;
    }

    internal PixelFormat(D2D1_PIXEL_FORMAT format)
    {
        Format = (Dxgi.Format)format.format;
        AlphaMode = (AlphaMode)format.alphaMode;
    }

    internal D2D1_PIXEL_FORMAT ToD2D() => Unsafe.As<PixelFormat, D2D1_PIXEL_FORMAT>(ref Unsafe.AsRef(this));
}
