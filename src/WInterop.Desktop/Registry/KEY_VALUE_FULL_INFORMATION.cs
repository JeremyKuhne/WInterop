// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Runtime.InteropServices;

namespace WInterop.Registry
{
    // https://msdn.microsoft.com/en-us/library/windows/hardware/ff554217.aspx
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public struct KEY_VALUE_FULL_INFORMATION
    {
        public uint TitleIndex;
        public RegistryValueType Type;
        public uint DataOffset;
        public uint DataLength;
        public uint NameLength;
        private TrailingString _Name;
        public ReadOnlySpan<char> Name => _Name.GetBuffer(NameLength);
    }
}
