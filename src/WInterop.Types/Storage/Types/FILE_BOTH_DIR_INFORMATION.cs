// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Runtime.InteropServices;

namespace WInterop.Storage.Types
{
    // https://msdn.microsoft.com/en-us/library/windows/hardware/ff540235.aspx
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public struct FILE_BOTH_DIR_INFORMATION
    {
        public uint NextEntryOffset;
        public uint FileIndex;
        public LongFileTime CreationTime;
        public LongFileTime LastAccessTime;
        public LongFileTime LastWriteTime;
        public LongFileTime ChangeTime;
        public long EndOfFile;
        public long AllocationSize;
        public FileAttributes FileAttributes;
        public uint FileNameLength;
        public uint EaSize;
        public sbyte ShortNameLength;
        private FixedString.Size12 _ShortName;
        public Span<char> ShortName => _ShortName.Buffer;
        private TrailingString _FileName;
        public ReadOnlySpan<char> FileName => _FileName.GetBuffer(FileNameLength);
    }
}
