// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Devices
{
    public static partial class ControlCodes
    {
        /// <summary>
        /// Codes for controlling the mount point manager. (\\.\MountPointManager
        /// or \Device\MountPointManager)
        /// </summary>
        public enum MountManager : uint
        {
            // Maybe I Should Drive - Drive Letter Assignment & The Mount Manager
            // http://www.osronline.com/article.cfm?id=107

            /// <summary>
            /// Create a mount point. Uses MOUNTMGR_CREATE_POINT_INPUT.
            /// [IOCTL_MOUNTMGR_CREATE_POINT]
            /// </summary>
            /// <remarks>
            /// https://msdn.microsoft.com/en-us/library/windows/hardware/ff560457.aspx
            /// CTL_CODE(MOUNTMGRCONTROLTYPE, 0, METHOD_BUFFERED, FILE_READ_ACCESS | FILE_WRITE_ACCESS)
            /// </remarks>
            CreatePoint = 0x006dc000,

            /// <summary>
            /// Works like QueryPoints, but deletes symbolic links and database entries for matches.
            /// [IOCTL_MOUNTMGR_DELETE_POINTS]
            /// </summary>
            /// <remarks>
            /// https://msdn.microsoft.com/en-us/library/windows/hardware/ff560461.aspx
            /// CTL_CODE(MOUNTMGRCONTROLTYPE, 1, METHOD_BUFFERED, FILE_READ_ACCESS | FILE_WRITE_ACCESS)
            /// </remarks>
            DeletePoints = 0x006dc004,

            /// <summary>
            /// Returns mount points that match MOUNTMGR_MOUNT_POINT. Returns matches in MOUNTMGR_MOUNT_POINTS.
            /// [IOCTL_MOUNTMGR_QUERY_POINTS]
            /// </summary>
            /// <remarks>
            /// https://msdn.microsoft.com/en-us/library/windows/hardware/ff560474.aspx
            /// CTL_CODE(MOUNTMGRCONTROLTYPE, 2, METHOD_BUFFERED, FILE_ANY_ACCESS)
            /// </remarks>
            QueryPoints = 0x006d0008,

            /// <summary>
            /// Same as DeletePoints, but does not remove symbolic links.
            /// [IOCTL_MOUNTMGR_DELETE_POINTS_DBONLY]
            /// </summary>
            /// <remarks>
            /// https://msdn.microsoft.com/en-us/library/windows/hardware/ff560464.aspx
            /// CTL_CODE(MOUNTMGRCONTROLTYPE, 3, METHOD_BUFFERED, FILE_READ_ACCESS | FILE_WRITE_ACCESS)
            /// </remarks>
            DeletePointsDatabaseOnly = 0x006dc00c,

            /// <summary>
            /// Gets the existing drive letter or assigns the next available letter if
            /// applicable. Uses MOUNTMGR_DRIVE_LETTER_TARGET for input and MOUNTMGR_DRIVE_LETTER_INFORMATION
            /// for output. [IOCTL_MOUNTMGR_NEXT_DRIVE_LETTER]
            /// </summary>
            /// <remarks>
            /// https://msdn.microsoft.com/en-us/library/windows/hardware/ff560473.aspx
            /// CTL_CODE(MOUNTMGRCONTROLTYPE, 4, METHOD_BUFFERED, FILE_READ_ACCESS | FILE_WRITE_ACCESS)
            /// </remarks>
            NextDriveLetter = 0x006dc010,

            /// <summary>
            /// Tell the mount manager to auto assign drive letters when they are introduced to the system.
            /// [IOCTL_MOUNTMGR_AUTO_DL_ASSIGNMENTS]
            /// </summary>
            /// <remarks>
            /// https://msdn.microsoft.com/en-us/library/windows/hardware/ff560448.aspx
            /// CTL_CODE(MOUNTMGRCONTROLTYPE, 5, METHOD_BUFFERED, FILE_READ_ACCESS | FILE_WRITE_ACCESS)
            /// </remarks>
            AutoDriveLetterAssignments = 0x006dc014,

            /// <summary>
            /// Alert the mount manager that a volume mount point has been created. Allows the mount point
            /// to persist if volumes are moved from one system to another. SetVolumeMountPoint() calls
            /// this. Uses MOUNTMGR_VOLUME_MOUNT_POINT. [IOCTL_MOUNTMGR_VOLUME_MOUNT_POINT_CREATED]
            /// </summary>
            /// <remarks>
            /// https://msdn.microsoft.com/en-us/library/windows/hardware/ff560485.aspx
            /// CTL_CODE(MOUNTMGRCONTROLTYPE, 6, METHOD_BUFFERED, FILE_READ_ACCESS | FILE_WRITE_ACCESS)
            /// </remarks>
            VolumeMountPointCreated = 0x006dc018,

            /// <summary>
            /// Alert the mount manager that a volume mount point has been deleted. DeleteVolumeMountPoint()
            /// calls this. Uses MOUNTMGR_VOLUME_MOUNT_POINT.
            /// [IOCTL_MOUNTMGR_VOLUME_MOUNT_POINT_DELETED]
            /// </summary>
            /// <remarks>
            /// https://msdn.microsoft.com/en-us/library/windows/hardware/ff560491.aspx
            /// CTL_CODE(MOUNTMGRCONTROLTYPE, 7, METHOD_BUFFERED, FILE_READ_ACCESS | FILE_WRITE_ACCESS)
            /// </remarks>
            VolumeMountPointDeleted = 0x006dc01c,

            /// <summary>
            /// Used to track the state of the mount manager. Returns the Epic number that reflects the current
            /// change state. Uses MOUNTMGR_CHANGE_NOTIFY_INFO. [IOCTL_MOUNTMGR_CHANGE_NOTIFY]
            /// </summary>
            /// <remarks>
            /// https://msdn.microsoft.com/en-us/library/windows/hardware/ff560451.aspx
            /// CTL_CODE(MOUNTMGRCONTROLTYPE, 8, METHOD_BUFFERED, FILE_READ_ACCESS)
            /// </remarks>
            ChangeNotify = 0x006d4020,

            /// <summary>
            /// Tells the mount manager to reserve the symbolic link reserved if the volume
            /// goes offline. Uses MOUNTMGR_TARGET_NAME. [IOCTL_MOUNTMGR_KEEP_LINKS_WHEN_OFFLINE]
            /// </summary>
            /// <remarks>
            /// https://msdn.microsoft.com/en-us/library/windows/hardware/ff560468.aspx
            /// CTL_CODE(MOUNTMGRCONTROLTYPE, 9, METHOD_BUFFERED, FILE_READ_ACCESS | FILE_WRITE_ACCESS)
            /// </remarks>
            KeepLinksWhenOffline = 0x006dc024,

            /// <summary>
            /// Tells the mount manager to scan it's dead mounted device list to see if it can
            /// resolve unique ids. Used primarily for cluster support.
            /// [IOCTL_MOUNTMGR_CHECK_UNPROCESSED_VOLUMES]
            /// </summary>
            /// <remarks>
            /// https://msdn.microsoft.com/en-us/library/windows/hardware/ff560454.aspx
            /// CTL_CODE(MOUNTMGRCONTROLTYPE, 10, METHOD_BUFFERED, FILE_READ_ACCESS)
            /// </remarks>
            CheckUnprocessedVolumes = 0x006d4028,

            /// <summary>
            /// Allows manually notifying the mount manager that a volume is available.
            /// Uses MOUNTMGR_TARGET_NAME. [IOCTL_MOUNTMGR_VOLUME_ARRIVAL_NOTIFICATION]
            /// </summary>
            /// <remarks>
            /// https://msdn.microsoft.com/en-us/library/windows/hardware/ff560477.aspx
            /// CTL_CODE(MOUNTMGRCONTROLTYPE, 11, METHOD_BUFFERED, FILE_READ_ACCESS)
            /// </remarks>
            VolumeArrivalNotification = 0x006d402c,

            /// <summary>
            /// Gets the DOS drive letter for the given volume if it has one.
            /// Takes MOUNTMGR_TARGET_NAME as input and returns MOUNTMGR_VOLUE_PATHS.
            /// [IOCTL_MOUNTMGR_QUERY_DOS_VOLUME_PATH]
            /// </summary>
            /// <remarks>
            /// Added in Windows XP. IoVolumeDeviceToDosName() uses this as does GetFinalPathNameByHandle().
            /// CTL_CODE(MOUNTMGRCONTROLTYPE, 12, METHOD_BUFFERED, FILE_ANY_ACCESS)
            /// </remarks>
            QueryDosVolumePath = 0x006d0030,

            /// <summary>
            /// Gets all volume path names for the given volume.
            /// Takes MOUNTMGR_TARGET_NAME as input and returns MOUNTMGR_VOLUE_PATHS.
            /// [IOCTL_MOUNTMGR_QUERY_DOS_VOLUME_PATHS]
            /// </summary>
            /// <remarks>
            /// Added in Windows XP. GetVolumePathNamesForVolumeNameW() uses this.
            /// CTL_CODE(MOUNTMGRCONTROLTYPE, 13, METHOD_BUFFERED, FILE_ANY_ACCESS)
            /// </remarks>
            QueryDosVolumePaths = 0x006d0034,

            /// <summary>
            /// [IOCTL_MOUNTMGR_SCRUB_REGISTRY]
            /// </summary>
            /// <remarks>
            /// Added in Windows Server 2003.
            /// 
            /// CTL_CODE(MOUNTMGRCONTROLTYPE, 14, METHOD_BUFFERED, FILE_READ_ACCESS | FILE_WRITE_ACCESS)
            /// </remarks>
            ScrubRegistry = 0x006dc038,

            /// <summary>
            /// [IOCTL_MOUNTMGR_QUERY_AUTO_MOUNT]
            /// </summary>
            /// <remarks>
            /// Added in Windows Server 2003.
            /// 
            /// CTL_CODE(MOUNTMGRCONTROLTYPE, 15, METHOD_BUFFERED, FILE_ANY_ACCESS)
            /// </remarks>
            QueryAutoMount = 0x006d003c,

            /// <summary>
            /// [IOCTL_MOUNTMGR_SET_AUTO_MOUNT]
            /// </summary>
            /// <remarks>
            /// Added in Windows Server 2003.
            /// 
            /// CTL_CODE(MOUNTMGRCONTROLTYPE, 16, METHOD_BUFFERED, FILE_READ_ACCESS | FILE_WRITE_ACCESS)
            /// </remarks>
            SetAutoMount = 0x006dc040,

            /// <summary>
            /// Assigns the drive letter for the boot volume. Similar to NextDriveLetter, but with a
            /// special treatment of WinPE boot volumes (which are 'X' by default). Takes no input or output.
            /// [IOCTL_MOUNTMGR_BOOT_DL_ASSIGNMENT]
            /// </summary>
            /// <remarks>
            /// Added in Windows 7.
            /// CTL_CODE(MOUNTMGRCONTROLTYPE, 17, METHOD_BUFFERED, FILE_READ_ACCESS | FILE_WRITE_ACCESS)
            /// </remarks>
            BootDriveLetterAssignment = 0x006dc044,

            /// <summary>
            /// [IOCTL_MOUNTMGR_TRACELOG_CACHE]
            /// </summary>
            /// <remarks>
            /// Added in Windows 7.
            /// 
            /// CTL_CODE(MOUNTMGRCONTROLTYPE, 18, METHOD_BUFFERED, FILE_READ_ACCESS)
            /// </remarks>
            TracelogCache = 0x006d4048,

            /// <summary>
            /// [IOCTL_MOUNTMGR_PREPARE_VOLUME_DELETE]
            /// </summary>
            /// <remarks>
            /// Added in Windows 8.
            /// 
            /// CTL_CODE(MOUNTMGRCONTROLTYPE, 19, METHOD_BUFFERED, FILE_READ_ACCESS | FILE_WRITE_ACCESS)
            /// </remarks>
            PrepareVolumeDelete = 0x006dc04c,

            /// <summary>
            /// [IOCTL_MOUNTMGR_CANCEL_VOLUME_DELETE]
            /// </summary>
            /// <remarks>
            /// Added in Windows 8.
            /// 
            /// CTL_CODE(MOUNTMGRCONTROLTYPE, 20, METHOD_BUFFERED, FILE_READ_ACCESS | FILE_WRITE_ACCESS)
            /// </remarks>
            CancelVolumeDelete = 0x006dc050,

            /// <summary>
            /// [IOCTL_MOUNTMGR_SILO_ARRIVAL]
            /// </summary>
            /// <remarks>
            /// Added in Windows 10 Anniversary Update (RS1).
            /// 
            /// CTL_CODE(MOUNTMGRCONTROLTYPE, 21, METHOD_BUFFERED, FILE_READ_ACCESS | FILE_WRITE_ACCESS)
            /// </remarks>
            SiloArrival = 0x006dc054
        }
    }
}
