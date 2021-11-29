// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Runtime.InteropServices;

namespace WInterop.Gdi.Native;

// https://docs.microsoft.com/en-us/windows/desktop/api/wingdi/ns-wingdi-tagbitmapinfo
[StructLayout(LayoutKind.Sequential)]
public struct BITMAPINFO
{
    public BitmapInfoHeader bmiHeader;
    private readonly RgbQuad _bmiColors;

    // TODO: Compute the count from the BitmapInfoHeader
    // public ReadOnlySpan<RgbQuad> bmiColors = TrailingArray<RgbQuad>.GetBuffer
}