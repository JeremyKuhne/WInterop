// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Storage
{
    // https://msdn.microsoft.com/en-us/library/windows/desktop/aa365511.aspx
    // https://msdn.microsoft.com/en-us/library/dd541667.aspx
    public enum ReparseTag : uint
    {
        None = 0x0,

        /// <summary>
        /// Reparse point is a mounted folder ("junction" or "soft link"). [IO_REPARSE_TAG_MOUNT_POINT]
        /// </summary>
        /// <remarks>
        /// Mounted Folders:
        /// https://msdn.microsoft.com/en-us/library/windows/desktop/aa365733.aspx
        /// </remarks>
        MountPoint = 0xA0000003,

        /// <summary>
        /// Obsolete. Used by legacy Hierarchical Storage Manager product. [IO_REPARSE_TAG_HSM]
        /// </summary>
        HierarchicalStorageManager = 0xC0000004,

        /// <summary>
        /// Home server drive extender. [IO_REPARSE_TAG_DRIVER_EXTENDER]
        /// </summary>
        DriveExtender = 0x80000005,

        /// <summary>
        /// Obsolete. Used by legacy Hierarchical Storage Manager product. [IO_REPARSE_TAG_HSM2]
        /// </summary>
        HierarchicalStorageManager2 = 0x80000006,

        /// <summary>
        /// Used by Single-Instance Storage filter driver. Server-side interpretation
        /// only, not meaningful over the wire. [IO_REPARSE_TAG_SIS]
        /// </summary>
        /// <remarks>
        /// https://en.wikipedia.org/wiki/Single-instance_storage
        /// </remarks>
        SingleInstanceStorage = 0x80000007,

        /// <summary>
        /// Mount point for a Windows Imaging File Format image (WIM). [IO_REPARSE_TAG_WIM]
        /// </summary>
        /// <remarks>
        /// https://msdn.microsoft.com/en-us/windows/hardware/commercialize/manufacture/desktop/mount-and-modify-a-windows-image-using-dism
        /// </remarks>
        WindowsImaging = 0x80000008,

        /// <summary>
        /// Used with Cluster Shared Volumes (CSV). [IO_REPARSE_TAG_CSV]
        /// </summary>
        ClusterSharedVolume = 0x80000009,

        /// <summary>
        /// Used by the Distributed File System. Server-side interpretation
        /// only, not meaningful over the wire. [IO_REPARSE_TAG_DFS]
        /// </summary>
        DistributedFileSystem = 0x8000000A,

        /// <summary>
        /// Used by the Distributed File System. Server-side interpretation
        /// only, not meaningful over the wire. [IO_REPARSE_TAG_DFSR]
        /// </summary>
        DistributedFileSystemRemote = 0x80000012,

        /// <summary>
        /// Used by the filter manager test harness. [IO_REPARSE_TAG_FILTER_MANAGER]
        /// </summary>
        FilterManager = 0x8000000B,

        /// <summary>
        /// Reparse point is a symbolic link. [IO_REPARSE_TAG_SYMLINK]
        /// </summary>
        SymbolicLink = 0xA000000C,

        /// <summary>
        /// Data deduplication reparse point. [IO_REPARSE_TAG_DEDUP]
        /// </summary>
        /// <remarks>
        /// https://technet.microsoft.com/en-us/library/hh831700.aspx
        /// https://blogs.technet.microsoft.com/filecab/2012/05/20/introduction-to-data-deduplication-in-windows-server-2012/
        /// </remarks>
        DataDeduplication = 0x80000013,

        /// <summary>
        /// Network File System (NFS) reparse point. [IO_REPARSE_TAG_NFS]
        /// </summary>
        /// <remarks>
        /// Can be a block or character device, a socket, a fifo, or
        /// a symbolic link.
        /// 
        /// https://msdn.microsoft.com/en-us/library/dn617178.aspx
        /// </remarks>
        NetworkFileSystem = 0x80000014,

        /// <summary>
        /// A OneDrive (previously SkyDrive) placeholder file.
        /// [IO_REPARSE_TAG_FILE_PLACEHOLDER]
        /// </summary>
        /// <remarks>
        /// Introduced in Windows 8.1, removed in Windows 10.
        /// https://msdn.microsoft.com/en-us/windows/compatibility/placeholder-files
        /// </remarks>
        PlaceholderFile = 0x80000015,

        /// <summary>
        /// Windows Overlay Filter reparse tag. [IO_REPARSE_TAG_WOF]
        /// </summary>
        /// <remarks>
        /// Introduced in Windows 8.1 update 1 specifically to support Wimboot.
        /// https://blogs.windows.com/itpro/2014/04/10/what-is-windows-image-boot-wimboot
        /// https://technet.microsoft.com/en-us/library/dn594399.aspx
        /// </remarks>
        WindowsOverlayFilter = 0x80000017
    }
}
