// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Runtime.InteropServices;
using WInterop.Storage;

namespace WInterop.Devices.Native
{
    // https://msdn.microsoft.com/en-us/library/windows/hardware/ff552012.aspx
    [StructLayout(LayoutKind.Explicit)]
    public struct REPARSE_DATA_BUFFER
    {
        [FieldOffset(0)]
        public ReparseTag ReparseTag;

        [FieldOffset(4)]
        public ushort ReparseDataLength;

        [FieldOffset(6)]
        public ushort Reserved;

        [FieldOffset(8)]
        public SymbolicLinkReparseBuffer SymbolicLinkData;

        [FieldOffset(8)]
        public MountPointReparseBuffer MountPointData;

        [FieldOffset(8)]
        public GenericReparseBuffer GenericData;

        [StructLayout(LayoutKind.Sequential)]
        public struct SymbolicLinkReparseBuffer
        {
            private readonly ushort SubstituteNameOffset;
            private readonly ushort SubstituteNameLength;
            private readonly ushort PrintNameOffset;
            private readonly ushort PrintNameLength;
            public uint Flags;
            private char _PathBuffer;
            public ReadOnlySpan<char> SubstituteName => TrailingArray<char>.GetBufferInBytes(in _PathBuffer, SubstituteNameLength, SubstituteNameOffset);
            public ReadOnlySpan<char> PrintName => TrailingArray<char>.GetBufferInBytes(in _PathBuffer, PrintNameLength, PrintNameOffset);
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct MountPointReparseBuffer
        {
            private readonly ushort SubstituteNameOffset;
            private readonly ushort SubstituteNameLength;
            private readonly ushort PrintNameOffset;
            private readonly ushort PrintNameLength;
            private char _PathBuffer;
            public ReadOnlySpan<char> SubstituteName => TrailingArray<char>.GetBufferInBytes(in _PathBuffer, SubstituteNameLength, SubstituteNameOffset);
            public ReadOnlySpan<char> PrintName => TrailingArray<char>.GetBufferInBytes(in _PathBuffer, PrintNameLength, PrintNameOffset);
        }

        public struct GenericReparseBuffer
        {
            public byte DataBuffer;
        }
    }
}