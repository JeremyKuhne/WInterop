// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Runtime.InteropServices;
using WInterop.Support;

namespace WInterop.FileManagement.Types
{
    // https://msdn.microsoft.com/en-us/library/windows/desktop/aa364226.aspx
    /// <summary>
    /// Used with GetFileInformationByHandleEx and FileIdBothDirectoryInfo/RestartInfo.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public struct FILE_ID_BOTH_DIR_INFO
    {
        /// <summary>
        /// Byte offset for the next FILE_ID_BOTH_DIR_INFO, or 0 if no entries follow
        /// </summary>
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
        public uint FileAttributes;
        public uint FileNameLength;
        public uint EaSize;
        public sbyte ShortNameLength;
        public FixedString.Size12 ShortName;
        public long FileId;

        // This is a variable length string
        // WCHAR FileName[1];
        public TrailingString.Unsized FileName;
    }
}
