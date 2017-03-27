// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace WInterop.FileManagement.DataTypes
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
            FileIndex = (*info).FileIndex;
            CreationTime = DateTime.FromFileTime((*info).CreationTime);
            LastAccessTime = DateTime.FromFileTime((*info).LastAccessTime);
            LastWriteTime = DateTime.FromFileTime((*info).LastWriteTime);
            ChangeTime = DateTime.FromFileTime((*info).ChangeTime);
            EndOfFile = (*info).EndOfFile;
            AllocationSize = (*info).AllocationSize;
            FileAttributes = (*info).FileAttributes;
            ExtendedAttributesSize = (*info).EaSize;
            FileName = FILE_FULL_DIR_INFO.GetFileName(info);
        }
    }
}
