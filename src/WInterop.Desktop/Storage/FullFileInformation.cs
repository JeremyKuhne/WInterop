// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using WInterop.Storage.Native;

namespace WInterop.Storage
{
    /// <summary>
    ///  Managed wrapper for FILE_FULL_DIR_INFORMATION.
    /// </summary>
    public struct FullFileInformation
    {
        /// <summary>
        ///  Byte offset within the parent directory, undefined for NTFS.
        /// </summary>
        public uint FileIndex;

        public DateTimeOffset CreationTimeUtc;
        public DateTimeOffset LastAccessTimeUtc;
        public DateTimeOffset LastWriteTimeUtc;
        public DateTimeOffset ChangeTimeUtc;

        public long EndOfFile;
        public long AllocationSize;
        public AllFileAttributes FileAttributes;

        /// <summary>
        ///  Size, in bytes, of the extended attributes for a file -or-
        ///  reparse tag if the file is a reparse point.
        /// </summary>
        public uint ExtendedAttributesSize;

        public string FileName;

        public unsafe FullFileInformation(FILE_FULL_DIR_INFORMATION* info)
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
            FileName = info->FileName.CreateString();
        }
    }
}