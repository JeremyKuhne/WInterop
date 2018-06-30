// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace WInterop.File.Types
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
        None = 0x0,

        /// <summary>
        /// File is read only. [FILE_ATTRIBUTE_READONLY]
        /// </summary>
        ReadOnly = 0x00000001,

        /// <summary>
        /// The file is hidden and should not be included in an oridinary
        /// directory listing. [FILE_ATTRIBUTE_HIDDEN]
        /// </summary>
        Hidden = 0x00000002,

        /// <summary>
        /// Operating system file. [FILE_ATTRIBUTE_SYSTEM]
        /// </summary>
        System = 0x00000004,

        /// <summary>
        /// File is a directory. [FILE_ATTRIBUTE_DIRECTORY]
        /// </summary>
        Directory = 0x00000010,

        /// <summary>
        /// File should be archived. Used by backup programs.
        /// [FILE_ATTRIBUTE_ARCHIVE]
        /// </summary>
        Acrchive = 0x00000020,

        /// <summary>
        /// Reserved for system use. [FILE_ATTRIBUTE_DEVICE]
        /// </summary>
        /// <remarks>
        /// Filtered out by CreateFile.
        /// </remarks>
        Device = 0x00000040,

        /// <summary>
        /// No other attributes set. Only valid on its own. [FILE_ATTRIBUTE_NORMAL]
        /// </summary>
        Normal = 0x00000080,

        /// <summary>
        /// Indicates that file is temporary and will attempt to keep as much in the cache as possible.
        /// [FILE_ATTRIBUTE_TEMPORARY]
        /// </summary>
        Temporary = 0x00000100,

        /// <summary>
        /// [FILE_ATTRIBUTE_SPARSE_FILE]
        /// </summary>
        SparseFile = 0x00000200,

        /// <summary>
        /// File is a reparse point and will be further parsed dependent on the reparse point type.
        /// This is mutually exclusive with the ExtendedAttributes flag.
        /// [FILE_ATTRIBUTE_REPARSE_POINT]
        /// </summary>
        ReparsePoint = 0x00000400,

        /// <summary>
        /// [FILE_ATTRIBUTE_COMPRESSED]
        /// </summary>
        Compressed = 0x00000800,

        /// <summary>
        /// Not available immediately. Used by Remote Storage. [FILE_ATTRIBUTE_OFFLINE]
        /// </summary>
        Offline = 0x00001000,

        /// <summary>
        /// Not to be indexed by the content indexing service. [FILE_ATTRIBUTE_NOT_CONTENT_INDEXED]
        /// </summary>
        NotContentIndexed = 0x00002000,

        /// <summary>
        /// File is encrypted. [FILE_ATTRIBUTE_ENCRYPTED]
        /// </summary>
        Encrypted = 0x00004000,

        /// <summary>
        /// Configured with integrity (ReFS only). [FILE_ATTRIBUTE_INTEGRITY_STREAM]
        /// </summary>
        IntegrityStream = 0x00008000,

        /// <summary>
        /// Reserved for system use. [FILE_ATTRIBUTE_VIRTUAL]
        /// </summary>
        /// <remarks>
        /// Filtered out by CreateFile.
        /// </remarks>
        Virtual = 0x00010000,

        /// <summary>
        /// User data stream should not be read by the background data integrity checker.
        /// Only used on Storage Spaces and ReFS volumes. [FILE_ATTRIBUTE_NO_SCRUB_DATA]
        /// </summary>
        NoScrubData = 0x00020000,

        /// <summary>
        /// File has extended attributes (FILE_EA_INFORMATION). This is mutually exclusive
        /// with the ReparsePoint flag.
        /// [FILE_ATTRIBUTE_EA]
        /// </summary>
        /// <remarks>
        /// Filtered out by CreateFile.
        /// </remarks>
        ExtendedAttributes = 0x00040000,

        // The next four attributes show up in RS2, likely all related to the new "On-demand Sync"

        // FILE_ATTRIBUTE_RECALL_ON_OPEN = 0x00040000,

        /// <summary>
        /// [FILE_ATTRIBUTE_PINNED]
        /// </summary>
        Pinned = 0x00080000,

        /// <summary>
        /// [FILE_ATTRIBUTE_UNPINNED]
        /// </summary>
        Unpinned = 0x00100000,

        /// <summary>
        /// [FILE_ATTRIBUTE_RECALL_ON_DATA_ACCESS]
        /// </summary>
        RecallOnDataAccess = 0x00400000,

        /// <summary>
        /// Be careful with this, check for invalid <i>before</i> checking other flags.
        /// [INVALID_FILE_ATTRIBUTES]
        /// </summary>
        Invalid = unchecked((uint)(-1))
    }
}
