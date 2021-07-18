// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Runtime.InteropServices;

namespace WInterop.Storage.Native
{
    // https://msdn.microsoft.com/en-us/library/windows/hardware/ff540248.aspx
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public struct FILE_DIRECTORY_INFORMATION
    {
        public uint NextEntryOffset;
        public uint FileIndex;
        public LongFileTime CreationTime;
        public LongFileTime LastAccessTime;
        public LongFileTime LastWriteTime;
        public LongFileTime ChangeTime;
        public long EndOfFile;
        public long AllocationSize;
        public AllFileAttributes FileAttributes;
        public uint FileNameLength;
        private char _FileName;
        public ReadOnlySpan<char> FileName => TrailingArray<char>.GetBufferInBytes(in _FileName, FileNameLength);
    }
}