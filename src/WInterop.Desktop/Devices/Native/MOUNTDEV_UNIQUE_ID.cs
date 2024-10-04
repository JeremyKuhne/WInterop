// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

namespace WInterop.Devices;

// https://msdn.microsoft.com/en-us/library/windows/hardware/ff562264.aspx
[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
public struct MOUNTDEV_UNIQUE_ID
{
    public ushort UniqueIdLength;
    private readonly byte _UniqueId;

    [UnscopedRef]
    public readonly ReadOnlySpan<byte> UniqueId => TrailingArray<byte>.GetBuffer(in _UniqueId, UniqueIdLength);
}