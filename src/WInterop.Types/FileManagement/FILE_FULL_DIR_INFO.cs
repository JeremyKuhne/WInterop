// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Generic;

namespace WInterop.FileManagement.DataTypes
{
    // https://msdn.microsoft.com/en-us/library/windows/desktop/hh447298.aspx
    // https://msdn.microsoft.com/en-us/library/windows/hardware/ff540310.aspx

    /// <summary>
    /// Used with GetFileInformationByHandleEx and FileIdBothDirectoryInfo/RestartInfo.
    /// </summary>
    public struct FILE_FULL_DIR_INFO
    {
        public uint NextEntryOffset;

        /// <summary>
        /// Byte offset within the parent directory, undefined for NTFS.
        /// </summary>
        public uint FileIndex;

        public long CreationTime;
        public long LastAccessTime;
        public long LastWriteTime;
        public long ChangeTime;

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

        /// <summary>
        /// The first character of the file name (the rest of the characters follow)
        /// </summary>
        public char FileName;

        public unsafe static string GetFileName(FILE_FULL_DIR_INFO* info)
        {
            return new string(&(*info).FileName, 0, (int)((*info).FileNameLength / 2));
        }

        public unsafe static FILE_FULL_DIR_INFO* GetNextInfo(FILE_FULL_DIR_INFO* info)
        {
            uint nextOffset = (*info).NextEntryOffset;
            if (nextOffset == 0)
                return null;

            return (FILE_FULL_DIR_INFO*)((byte*)info + nextOffset);
        }
    }
}
