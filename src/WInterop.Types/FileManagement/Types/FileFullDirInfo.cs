// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace WInterop.FileManagement.Types
{
    /// <summary>
    /// Managed wrapper for FILE_FULL_DIR_INFO
    /// </summary>
    public struct FileFullDirInfo
    {
        /// <summary>
        /// Byte offset within the parent directory, undefined for NTFS.
        /// </summary>
        public uint FileIndex;

        public DateTime CreationTimeUtc;
        public DateTime LastAccessTimeUtc;
        public DateTime LastWriteTimeUtc;
        public DateTime ChangeTimeUtc;

        public long EndOfFile;
        public long AllocationSize;
        public FileAttributes FileAttributes;

        /// <summary>
        /// Size, in bytes, of the extended attributes for a file -or-
        /// reparse tag if the file is a reparse point.
        /// </summary>
        public uint ExtendedAttributesSize;

        public string FileName;

        public unsafe FileFullDirInfo(FILE_FULL_DIR_INFO* info)
        {
            FileIndex = info->FileIndex;
            CreationTimeUtc = info->CreationTime.ToDateTimeUtc();
            LastAccessTimeUtc = info->LastAccessTime.ToDateTimeUtc();
            LastWriteTimeUtc = info->LastWriteTime.ToDateTimeUtc();
            ChangeTimeUtc = info->ChangeTime.ToDateTimeUtc();
            EndOfFile = info->EndOfFile;
            AllocationSize = info->AllocationSize;
            FileAttributes = info->FileAttributes;
            ExtendedAttributesSize = info->EaSize;
            FileName = FILE_FULL_DIR_INFO.GetFileName(info);
        }
    }
}
