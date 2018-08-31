// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Runtime.InteropServices;

namespace WInterop.Storage.Unsafe
{
    // https://msdn.microsoft.com/en-us/library/windows/hardware/ff545793.aspx
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public struct FILE_FULL_EA_INFORMATION
    {
        public uint NextEntryOffset;
        public byte Flags;
        public byte EaNameLength;
        public ushort EaValueLength;
        private char _EaName;
        public ReadOnlySpan<char> EaName => TrailingArray<char>.GetBufferInBytes(ref _EaName, EaNameLength);
    }
}
