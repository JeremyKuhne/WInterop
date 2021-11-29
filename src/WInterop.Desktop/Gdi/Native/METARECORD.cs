// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Runtime.InteropServices;

namespace WInterop.Gdi.Native;

/// <docs>https://docs.microsoft.com/windows/win32/api/wingdi/ns-wingdi-metarecord</docs>
[StructLayout(LayoutKind.Sequential)]
public struct METARECORD
{
    /// <summary>
    ///  Record size, in ushorts.
    /// </summary>
    public uint rdSize;

    /// <summary>
    ///  The function number.
    /// </summary>
    public ushort rdFunction;

    /// <summary>
    ///  Parameters, in reverse order.
    /// </summary>
    private ushort _rdParm;

    public ReadOnlySpan<ushort> rdParam
        => TrailingArray<ushort>.GetBuffer(in _rdParm, rdSize - sizeof(uint) - sizeof(ushort));
}