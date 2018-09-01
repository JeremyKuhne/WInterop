// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Runtime.InteropServices;

namespace WInterop.Storage.Unsafe
{
    /// <summary>
    /// Used with GetFileInformationByHandleEx and FileIdBothDirectoryInfo/RestartInfo.
    /// Also used with NtQueryDirectoryFile and FileIdBothDirectoryInformation.
    /// Equivalent to FILE_ID_BOTH_DIR_INFO.
    /// </summary>
    /// <remarks>
    /// Very close to FILE_BOTH_DIR_INFORMATION, but not exactly the same.
    /// </remarks>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public struct FILE_ID_BOTH_DIR_INFORMATION
    {
        // https://msdn.microsoft.com/en-us/library/windows/desktop/aa364226.aspx
        // https://msdn.microsoft.com/en-us/library/windows/hardware/ff540303.aspx

        /// <summary>
        /// Byte offset for the next FILE_ID_BOTH_DIR_INFO, or 0 if no entries follow
        /// </summary>
        public uint NextEntryOffset;

        /// <summary>
        /// Byte offset within the parent directory, undefined for NTFS.
        /// </summary>
        public uint FileIndex;
        public LongFileTime CreationTime;
        public LongFileTime LastAccessTime;
        public LongFileTime LastWriteTime;
        public LongFileTime ChangeTime;

        /// <summary>
        /// End of file position, zero based in bytes. As it is zero based, it is
        /// effectively the length of the file in bytes.
        /// </summary>
        public long EndOfFile;

        /// <summary>
        /// The actual allocated size on disk in bytes. Typically a multiple of the
        /// sector or cluster size.
        /// </summary>
        public long AllocationSize;
        public FileAttributes FileAttributes;
        public uint FileNameLength;
        public uint EaSize;
        public sbyte ShortNameLength;
        private FixedString.Size12 _ShortName;
        public Span<char> ShortName => _ShortName.Buffer;

        /// <summary>
        /// Reference number generated for the file by the file system. Not the same as the
        /// 16 byte file object ID in NTFS (FILE_ID_128). This number is not guaranteed to
        /// be unique over time- it can be reused. It is also not guaranteed to be constant-
        /// FAT can change this ID when defragmenting, for example.
        /// </summary>
        public long FileId;
        private char _FileName;
        public ReadOnlySpan<char> FileName => TrailingArray<char>.GetBufferInBytes(ref _FileName, FileNameLength);
    }
}
