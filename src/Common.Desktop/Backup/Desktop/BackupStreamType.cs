// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Backup.Desktop
{
    /// <summary>
    /// The types returned in WIN32_STREAM_ID dwStreamId. Defines taken from winnt.h.
    /// See <a href="https://msdn.microsoft.com/en-us/library/dd305136.aspx">[MS-BKUP]</a> specification.
    /// </summary>
    public enum BackupStreamType : uint
    {
        BACKUP_INVALID = 0x00000000,

        /// <summary>
        /// Standard data
        /// </summary>
        BACKUP_DATA = 0x00000001,

        /// <summary>
        /// Extended attribute data
        /// </summary>
        BACKUP_EA_DATA = 0x00000002,

        /// <summary>
        /// Security descriptor data
        /// </summary>
        BACKUP_SECURITY_DATA = 0x00000003,

        /// <summary>
        /// Alternative data streams
        /// </summary>
        BACKUP_ALTERNATE_DATA = 0x00000004,

        /// <summary>
        /// Hard link information
        /// </summary>
        BACKUP_LINK = 0x00000005,

        BACKUP_PROPERTY_DATA = 6,

        /// <summary>
        /// Object identifiers
        /// </summary>
        BACKUP_OBJECT_ID = 0x00000007,

        /// <summary>
        /// Reparse points
        /// </summary>
        BACKUP_REPARSE_DATA = 0x00000008,

        /// <summary>
        /// Data in a sparse file
        /// </summary>
        BACKUP_SPARSE_BLOCK = 0x00000009,

        /// <summary>
        /// Transactional file system
        /// </summary>
        BACKUP_TXFS_DATA = 0x0000000A
    }
}
