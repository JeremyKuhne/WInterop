// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace WInterop.FileManagement.Types
{
    /// <summary>
    /// Used with GetFileInformationByHandleEx and FileIdBothDirectoryInfo/RestartInfo.
    /// </summary>
    /// <remarks>
    /// Identical to FILE_FULL_DIR_INFORMATION.
    /// </remarks>
    public struct FILE_FULL_DIR_INFO
    {
        // https://msdn.microsoft.com/en-us/library/windows/desktop/hh447298.aspx
        // https://msdn.microsoft.com/en-us/library/windows/hardware/ff540289.aspx

        /// <summary>
        /// Offset in bytes of the next entry, if any.
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

        // Note that MSDN documentation isn't correct for this- it can return
        // any FILE_ATTRIBUTE that is currently set on the file, not just the
        // ones documented.
        public FileAttributes FileAttributes;

        /// <summary>
        /// The length of the file name in bytes (without null).
        /// </summary>
        public uint FileNameLength;

        /// <summary>
        /// The extended attribute size OR the reparse tag if a reparse point.
        /// </summary>
        public uint EaSize;

        private TrailingString _FileName;

        /// <summary>
        /// The first character of the file name (the rest of the characters follow)
        /// </summary>
        public ReadOnlySpan<char> FileName => _FileName.GetBuffer(FileNameLength);

        public unsafe static FILE_FULL_DIR_INFO* GetNextInfo(FILE_FULL_DIR_INFO* info)
        {
            uint nextOffset = (*info).NextEntryOffset;
            if (nextOffset == 0)
                return null;

            return (FILE_FULL_DIR_INFO*)((byte*)info + nextOffset);
        }
    }
}
