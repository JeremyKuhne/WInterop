// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Runtime.InteropServices;

namespace WInterop.File.Types
{
    // https://msdn.microsoft.com/en-us/library/windows/hardware/ff545793.aspx
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public struct FILE_FULL_EA_INFORMATION
    {
        public uint NextEntryOffset;
        public byte Flags;
        public byte EaNameLength;
        public ushort EaValueLength;
        private TrailingString _EaName;
        public ReadOnlySpan<char> EaName => _EaName.GetBuffer(EaNameLength);
    }
}
