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

        public DateTime CreationTime;
        public DateTime LastAccessTime;
        public DateTime LastWriteTime;
        public DateTime ChangeTime;

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
            CreationTime = info->CreationTime.ToDateTimeUtc().ToLocalTime();
            LastAccessTime = info->LastAccessTime.ToDateTimeUtc().ToLocalTime();
            LastWriteTime = info->LastWriteTime.ToDateTimeUtc().ToLocalTime();
            ChangeTime = info->ChangeTime.ToDateTimeUtc().ToLocalTime();
            EndOfFile = info->EndOfFile;
            AllocationSize = info->AllocationSize;
            FileAttributes = info->FileAttributes;
            ExtendedAttributesSize = info->EaSize;
            FileName = FILE_FULL_DIR_INFO.GetFileName(info);
        }
    }
}
