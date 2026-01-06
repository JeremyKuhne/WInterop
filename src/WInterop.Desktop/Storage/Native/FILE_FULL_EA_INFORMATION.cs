// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

namespace WInterop.Storage.Native;

// https://msdn.microsoft.com/en-us/library/windows/hardware/ff545793.aspx
[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
public struct FILE_FULL_EA_INFORMATION
{
    public uint NextEntryOffset;
    public byte Flags;
    public byte EaNameLength;
    public ushort EaValueLength;
    private readonly char _EaName;

    [UnscopedRef]
    public readonly ReadOnlySpan<char> EaName => TrailingArray<char>.GetBufferInBytes(in _EaName, EaNameLength);
}