// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Storage
{
    /// <summary>
    ///  Drive types.
    /// </summary>
    /// <docs>https://docs.microsoft.com/en-us/windows/win32/api/fileapi/nf-fileapi-getdrivetypew</docs>
    public enum DriveType : uint
    {
        /// <summary>
        ///  The drive type cannot be determined. [DRIVE_UNKNOWN]
        /// </summary>
        Unknown = 0,

        /// <summary>
        ///  The root path is invalid; for example, there is no volume mounted at the specified path. [DRIVE_NO_ROOT_DIR]
        /// </summary>
        NoRootDir = 1,

        /// <summary>
        ///  The drive has removable media; for example, a floppy drive, thumb drive, or flash card reader. [DRIVE_REMOVABLE]
        /// </summary>
        Removable = 2,

        /// <summary>
        ///  The drive has fixed media; for example, a hard disk drive or flash drive. [DRIVE_FIXED]
        /// </summary>
        Fixed = 3,

        /// <summary>
        ///  The drive is a remote (network) drive. [DRIVE_REMOTE]
        /// </summary>
        Remote = 4,

        /// <summary>
        ///  The drive is a CD-ROM drive. [DRIVE_CDROM]
        /// </summary>
        Cdrom = 5,

        /// <summary>
        ///  The drive is a RAM disk. [DRIVE_RAMDISK]
        /// </summary>
        RamDisk = 6
    }
}