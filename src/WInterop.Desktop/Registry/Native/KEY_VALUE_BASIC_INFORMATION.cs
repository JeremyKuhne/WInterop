// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Runtime.InteropServices;

namespace WInterop.Registry.Native;

// https://msdn.microsoft.com/en-us/library/windows/hardware/ff553410.aspx
[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
public struct KEY_VALUE_BASIC_INFORMATION
{
    // https://msdn.microsoft.com/en-us/library/windows/hardware/ff553410.aspx

    public uint TitleIndex;
    public RegistryValueType Type;
    public uint NameLength;
    private char _Name;
    public ReadOnlySpan<char> Name => TrailingArray<char>.GetBufferInBytes(in _Name, NameLength);
}