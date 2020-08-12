// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Runtime.InteropServices;

namespace WInterop.Storage.Native
{
    /// <summary>
    /// <a href="https://msdn.microsoft.com/en-us/library/windows/hardware/ff540289.aspx">FILE_FULL_DIR_INFORMATION</a> structure.
    /// Used with GetFileInformationByHandleEx and FileIdBothDirectoryInfo/RestartInfo as well as NtQueryFileInformation.
    /// Equivalent to <a href="https://msdn.microsoft.com/en-us/library/windows/desktop/hh447298.aspx">FILE_FULL_DIR_INFO</a> structure.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public struct FILE_FULL_DIR_INFORMATION
    {
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

        /// <summary>
        /// File attributes.
        /// </summary>
        /// <remarks>
        /// Note that MSDN documentation isn't correct for this- it can return
        /// any FILE_ATTRIBUTE that is currently set on the file, not just the
        /// ones documented.
        /// </remarks>
        public AllFileAttributes FileAttributes;

        /// <summary>
        /// The length of the file name in bytes (without null).
        /// </summary>
        public uint FileNameLength;

        /// <summary>
        /// The extended attribute size OR the reparse tag if a reparse point.
        /// </summary>
        public uint EaSize;

        private char _FileName;
        public ReadOnlySpan<char> FileName => TrailingArray<char>.GetBufferInBytes(ref _FileName, FileNameLength);

        /// <summary>
        /// Gets the next info pointer or null if there are no more.
        /// </summary>
        public static unsafe FILE_FULL_DIR_INFORMATION* GetNextInfo(FILE_FULL_DIR_INFORMATION* info)
        {
            uint nextOffset = (*info).NextEntryOffset;
            if (nextOffset == 0)
                return null;

            return (FILE_FULL_DIR_INFORMATION*)((byte*)info + nextOffset);
        }
    }
}
