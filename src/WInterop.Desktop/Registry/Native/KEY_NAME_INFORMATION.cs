// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Diagnostics.CodeAnalysis;

namespace WInterop.Registry.Native;

// https://msdn.microsoft.com/en-us/library/windows/hardware/ff553381.aspx
public struct KEY_NAME_INFORMATION
{
    public uint NameLength;
    private readonly char _Name;

    [UnscopedRef]
    public readonly ReadOnlySpan<char> Name => TrailingArray<char>.GetBufferInBytes(in _Name, NameLength);
}