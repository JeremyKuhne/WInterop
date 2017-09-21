// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Runtime.InteropServices;

namespace WInterop.FileManagement.Types
{
    /// <summary>
    /// Used with GetFileInformationByHandleEx and FileIdBothDirectoryInfo/RestartInfo.
    /// </summary>
    /// <remarks>
    /// Very close to FILE_BOTH_DIR_INFORMATION, but not exactly the same.
    /// </remarks>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public struct FILE_ID_BOTH_DIR_INFO
    {
        // https://msdn.microsoft.com/en-us/library/windows/desktop/aa364226.aspx

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
        public long EndOfFile;
        public long AllocationSize;
        public FileAttributes FileAttributes;
        public uint FileNameLength;
        public uint EaSize;
        public sbyte ShortNameLength;
        private FixedString.Size12 _ShortName;
        public Span<char> ShortName => _ShortName.Buffer;
        public long FileId;

        // This is a variable length string
        // WCHAR FileName[1];
        public TrailingString.Unsized FileName;
    }
}
