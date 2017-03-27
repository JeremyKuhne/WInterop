// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace WInterop.FileManagement.DataTypes
{
    /// <summary>
    /// <a href="https://msdn.microsoft.com/en-us/library/windows/desktop/gg258117.aspx">File attribute constants</a>. This is a superset of what is defined
    /// in System.IO. Combined with SecurityQosFlags and FileFlags for CreateFile.
    /// </summary>
    [Flags]
    public enum FileAttributes : uint
    {
        /// <summary>
        /// Not technically a defined attribute, added here for convenience.
        /// </summary>
        NONE = 0x0,

        FILE_ATTRIBUTE_READONLY = 0x00000001,
        FILE_ATTRIBUTE_HIDDEN = 0x00000002,
        FILE_ATTRIBUTE_SYSTEM = 0x00000004,

        /// <summary>
        /// File is a directory.
        /// </summary>
        FILE_ATTRIBUTE_DIRECTORY = 0x00000010,
        FILE_ATTRIBUTE_ARCHIVE = 0x00000020,

        /// <summary>
        /// Reserved for system use.
        /// </summary>
        /// <remarks>
        /// Filtered out by CreateFile.
        /// </remarks>
        FILE_ATTRIBUTE_DEVICE = 0x00000040,
        FILE_ATTRIBUTE_NORMAL = 0x00000080,

        /// <summary>
        /// Indicates that file is temporary and will attempt to keep as much in the cache as possible.
        /// </summary>
        FILE_ATTRIBUTE_TEMPORARY = 0x00000100,
        FILE_ATTRIBUTE_SPARSE_FILE = 0x00000200,

        /// <summary>
        /// Link of some sort
        /// </summary>
        FILE_ATTRIBUTE_REPARSE_POINT = 0x00000400,
        FILE_ATTRIBUTE_COMPRESSED = 0x00000800,

        /// <summary>
        /// Not available immediately. Used by Remote Storage.
        /// </summary>
        FILE_ATTRIBUTE_OFFLINE = 0x00001000,

        /// <summary>
        /// Not to be indexed by the content indexing service.
        /// </summary>
        FILE_ATTRIBUTE_NOT_CONTENT_INDEXED = 0x00002000,
        FILE_ATTRIBUTE_ENCRYPTED = 0x00004000,

        /// <summary>
        /// Configured with integrity (ReFS only)
        /// </summary>
        FILE_ATTRIBUTE_INTEGRITY_STREAM = 0x00008000,

        /// <summary>
        /// Reserved for system use.
        /// </summary>
        /// <remarks>
        /// Filtered out by CreateFile.
        /// </remarks>
        FILE_ATTRIBUTE_VIRTUAL = 0x00010000,

        /// <summary>
        /// User data stream should not be read by the background data integrity checker.
        /// Only used on Storage Spaces and ReFS volumes.
        /// </summary>
        FILE_ATTRIBUTE_NO_SCRUB_DATA = 0x00020000,

        /// <summary>
        /// File has extended attributes? (FILE_EA_INFORMATION?)
        /// </summary>
        /// <remarks>
        /// Filtered out by CreateFile.
        /// </remarks>
        FILE_ATTRIBUTE_EA = 0x00040000,

        // The next four attributes show up in RS2, likely all related to the new "On-demand Sync"

        // FILE_ATTRIBUTE_RECALL_ON_OPEN = 0x00040000,

        FILE_ATTRIBUTE_PINNED = 0x00080000,

        FILE_ATTRIBUTE_UNPINNED = 0x00100000,

        FILE_ATTRIBUTE_RECALL_ON_DATA_ACCESS = 0x00400000,

        /// <summary>
        /// Be careful with this, check for invalid <i>before</i> checking other flags.
        /// </summary>
        INVALID_FILE_ATTRIBUTES = unchecked((uint)(-1))
    }
}
