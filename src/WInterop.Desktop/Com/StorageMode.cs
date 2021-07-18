// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace WInterop.Com
{
    /// <summary>
    ///  Structured Storage instantiation modes.
    /// <see cref="https://msdn.microsoft.com/en-us/library/windows/desktop/aa380337.aspx"/>
    /// </summary>
    [Flags]
    public enum StorageMode : uint
    {
        /// <summary>
        ///  Read only, and each change to a storage or stream element is written as it occurs.
        ///  Fails if the given storage object already exists.
        ///  [STGM_DIRECT] [STGM_READ] [STGM_FAILIFTHERE]
        /// </summary>
        Default = 0x00000000,

        /// <summary>
        ///  Changes are buffered and written only if an explicit commit operation is called.
        ///  Calling Revert on the various interfaces will ignore changes. Compound files only
        ///  support transaction with storages, not streams.
        ///  [STGM_TRANSACTED]
        /// </summary>
        Transacted = 0x00010000,

        /// <summary>
        ///  Efficient, feature restricted mode. See online docs for details. [STGM_SIMPLE]
        /// </summary>
        Simple = 0x08000000,

        /// <summary>
        ///  Write only mode. [STGM_WRITE]
        /// </summary>
        Write = 0x00000001,

        /// <summary>
        ///  ReadWrite mode. [STGM_READWRITE]
        /// </summary>
        ReadWrite = 0x00000002,

        /// <summary>
        ///  Allow other access. [STGM_SHARE_DENY_NONE]
        ///  (The default.)
        /// </summary>
        ShareDenyNone = 0x00000040,

        /// <summary>
        ///  Prevent others from reading. [STGM_SHARE_DENY_READ]
        /// </summary>
        ShareDenyRead = 0x00000030,

        /// <summary>
        ///  Prevent others from writing. [STGM_SHARE_DENY_WRITE]
        /// </summary>
        ShareDenyWrite = 0x00000020,

        /// <summary>
        ///  Prevent others from opening in any mode. [STGM_SHARE_EXCLUSIVE]
        /// </summary>
        ShareExclusive = 0x00000010,

        /// <summary>
        ///  Open with exclusive acess to the most recent committed version.
        ///  [STGM_PRIORITY]
        /// </summary>
        Priority = 0x00040000,

        /// <summary>
        ///  Deletes the underlying file when the root storage object is released.
        ///  Only valid when creating, not opening existing objects. [STGM_DELETEONRELEASE]
        /// </summary>
        DeleteOnRelease = 0x04000000,

        /// <summary>
        ///  When in transacted mode, stores uncommitted modifications in the
        ///  original file. [STGM_NOSCRATCH]
        /// </summary>
        NoScratch = 0x00100000,

        /// <summary>
        ///  Removes existing objects of the same name. [STGM_CREATE]
        /// </summary>
        Create = 0x00001000,

        /// <summary>
        ///  Creates a new object while persisting existing data in a stream
        ///  called "Contents". [STGM_CONVERT]
        /// </summary>
        Convert = 0x00020000,

        /// <summary>
        ///  In transacted mode, prevents creating a snapshot copy. [STGM_NOSNAPSHOT]
        /// </summary>
        NoSnapshot = 0x00200000,

        /// <summary>
        ///  Allows single writer and multiple readers in direct mode. Requires using
        ///  IDirectWriterLock to obtain writer access. [STGM_DIRECT_SWMR]
        /// </summary>
        DirectSingleWriterMultipleReader = 0x00400000
    }
}