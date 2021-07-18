// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Storage
{
    /// <summary>
    ///  The types returned in WIN32_STREAM_ID dwStreamId. Defines taken from winnt.h.
    ///  See <a href="https://msdn.microsoft.com/en-us/library/dd305136.aspx">[MS-BKUP]</a> specification.
    /// </summary>
    public enum BackupStreamType : uint
    {
        /// <summary>
        ///  [BACKUP_DATA]
        /// </summary>
        Invalid = 0x00000000,

        /// <summary>
        ///  Standard data [BACKUP_DATA]
        /// </summary>
        Data = 0x00000001,

        /// <summary>
        ///  Extended attribute data [BACKUP_EA_DATA]
        /// </summary>
        EaData = 0x00000002,

        /// <summary>
        ///  Security descriptor data [BACKUP_SECURITY_DATA]
        /// </summary>
        Security = 0x00000003,

        /// <summary>
        ///  Alternative data streams [BACKUP_ALTERNATE_DATA]
        /// </summary>
        AlternateData = 0x00000004,

        /// <summary>
        ///  Hard link information [BACKUP_LINK]
        /// </summary>
        Link = 0x00000005,

        /// <summary>
        ///  [BACKUP_PROPERTY_DATA]
        /// </summary>
        PropertyData = 6,

        /// <summary>
        ///  Object identifiers [BACKUP_OBJECT_ID]
        /// </summary>
        ObjectId = 0x00000007,

        /// <summary>
        ///  Reparse points [BACKUP_REPARSE_DATA]
        /// </summary>
        ReparseData = 0x00000008,

        /// <summary>
        ///  Data in a sparse file [BACKUP_SPARSE_BLOCK]
        /// </summary>
        SparseBlock = 0x00000009,

        /// <summary>
        ///  Transactional file system [BACKUP_TXFS_DATA]
        /// </summary>
        TxfsData = 0x0000000A
    }
}