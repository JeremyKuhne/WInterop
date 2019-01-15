// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.


using FluentAssertions;
using WInterop.Devices;
using Xunit;

namespace DeviceTests
{
    public partial class ControlCodeConstruction
    {
        [Theory,
            // IOCTL_MOUNTMGR_CREATE_POINT                 CTL_CODE(MOUNTMGRCONTROLTYPE, 0, METHOD_BUFFERED, FILE_READ_ACCESS | FILE_WRITE_ACCESS)
            InlineData(ControlCodes.MountManager.CreatePoint, 0, ControlCodeMethod.Buffered, ControlCodeAccess.ReadWrite),
            // IOCTL_MOUNTMGR_DELETE_POINTS                CTL_CODE(MOUNTMGRCONTROLTYPE, 1, METHOD_BUFFERED, FILE_READ_ACCESS | FILE_WRITE_ACCESS)
            InlineData(ControlCodes.MountManager.DeletePoints, 1, ControlCodeMethod.Buffered, ControlCodeAccess.ReadWrite),
            // IOCTL_MOUNTMGR_QUERY_POINTS                 CTL_CODE(MOUNTMGRCONTROLTYPE, 2, METHOD_BUFFERED, FILE_ANY_ACCESS)
            InlineData(ControlCodes.MountManager.QueryPoints, 2, ControlCodeMethod.Buffered, ControlCodeAccess.Any),
            // IOCTL_MOUNTMGR_DELETE_POINTS_DBONLY         CTL_CODE(MOUNTMGRCONTROLTYPE, 3, METHOD_BUFFERED, FILE_READ_ACCESS | FILE_WRITE_ACCESS)
            InlineData(ControlCodes.MountManager.DeletePointsDatabaseOnly, 3, ControlCodeMethod.Buffered, ControlCodeAccess.ReadWrite),
            // IOCTL_MOUNTMGR_NEXT_DRIVE_LETTER            CTL_CODE(MOUNTMGRCONTROLTYPE, 4, METHOD_BUFFERED, FILE_READ_ACCESS | FILE_WRITE_ACCESS)
            InlineData(ControlCodes.MountManager.NextDriveLetter, 4, ControlCodeMethod.Buffered, ControlCodeAccess.ReadWrite),
            // IOCTL_MOUNTMGR_AUTO_DL_ASSIGNMENTS          CTL_CODE(MOUNTMGRCONTROLTYPE, 5, METHOD_BUFFERED, FILE_READ_ACCESS | FILE_WRITE_ACCESS)
            InlineData(ControlCodes.MountManager.AutoDriveLetterAssignments, 5, ControlCodeMethod.Buffered, ControlCodeAccess.ReadWrite),
            // IOCTL_MOUNTMGR_VOLUME_MOUNT_POINT_CREATED   CTL_CODE(MOUNTMGRCONTROLTYPE, 6, METHOD_BUFFERED, FILE_READ_ACCESS | FILE_WRITE_ACCESS)
            InlineData(ControlCodes.MountManager.VolumeMountPointCreated, 6, ControlCodeMethod.Buffered, ControlCodeAccess.ReadWrite),
            // IOCTL_MOUNTMGR_VOLUME_MOUNT_POINT_DELETED   CTL_CODE(MOUNTMGRCONTROLTYPE, 7, METHOD_BUFFERED, FILE_READ_ACCESS | FILE_WRITE_ACCESS)
            InlineData(ControlCodes.MountManager.VolumeMountPointDeleted, 7, ControlCodeMethod.Buffered, ControlCodeAccess.ReadWrite),
            // IOCTL_MOUNTMGR_CHANGE_NOTIFY                CTL_CODE(MOUNTMGRCONTROLTYPE, 8, METHOD_BUFFERED, FILE_READ_ACCESS)
            InlineData(ControlCodes.MountManager.ChangeNotify, 8, ControlCodeMethod.Buffered, ControlCodeAccess.Read),
            // IOCTL_MOUNTMGR_KEEP_LINKS_WHEN_OFFLINE      CTL_CODE(MOUNTMGRCONTROLTYPE, 9, METHOD_BUFFERED, FILE_READ_ACCESS | FILE_WRITE_ACCESS)
            InlineData(ControlCodes.MountManager.KeepLinksWhenOffline, 9, ControlCodeMethod.Buffered, ControlCodeAccess.ReadWrite),
            // IOCTL_MOUNTMGR_CHECK_UNPROCESSED_VOLUMES    CTL_CODE(MOUNTMGRCONTROLTYPE, 10, METHOD_BUFFERED, FILE_READ_ACCESS)
            InlineData(ControlCodes.MountManager.CheckUnprocessedVolumes, 10, ControlCodeMethod.Buffered, ControlCodeAccess.Read),
            // IOCTL_MOUNTMGR_VOLUME_ARRIVAL_NOTIFICATION  CTL_CODE(MOUNTMGRCONTROLTYPE, 11, METHOD_BUFFERED, FILE_READ_ACCESS)
            InlineData(ControlCodes.MountManager.VolumeArrivalNotification, 11, ControlCodeMethod.Buffered, ControlCodeAccess.Read),
            // IOCTL_MOUNTMGR_QUERY_DOS_VOLUME_PATH        CTL_CODE(MOUNTMGRCONTROLTYPE, 12, METHOD_BUFFERED, FILE_ANY_ACCESS)
            InlineData(ControlCodes.MountManager.QueryDosVolumePath, 12, ControlCodeMethod.Buffered, ControlCodeAccess.Any),
            // IOCTL_MOUNTMGR_QUERY_DOS_VOLUME_PATHS       CTL_CODE(MOUNTMGRCONTROLTYPE, 13, METHOD_BUFFERED, FILE_ANY_ACCESS)
            InlineData(ControlCodes.MountManager.QueryDosVolumePaths, 13, ControlCodeMethod.Buffered, ControlCodeAccess.Any),
            // IOCTL_MOUNTMGR_SCRUB_REGISTRY               CTL_CODE(MOUNTMGRCONTROLTYPE, 14, METHOD_BUFFERED, FILE_READ_ACCESS | FILE_WRITE_ACCESS)
            InlineData(ControlCodes.MountManager.ScrubRegistry, 14, ControlCodeMethod.Buffered, ControlCodeAccess.ReadWrite),
            // IOCTL_MOUNTMGR_QUERY_AUTO_MOUNT             CTL_CODE(MOUNTMGRCONTROLTYPE, 15, METHOD_BUFFERED, FILE_ANY_ACCESS)
            InlineData(ControlCodes.MountManager.QueryAutoMount, 15, ControlCodeMethod.Buffered, ControlCodeAccess.Any),
            // IOCTL_MOUNTMGR_SET_AUTO_MOUNT               CTL_CODE(MOUNTMGRCONTROLTYPE, 16, METHOD_BUFFERED, FILE_READ_ACCESS | FILE_WRITE_ACCESS)
            InlineData(ControlCodes.MountManager.SetAutoMount, 16, ControlCodeMethod.Buffered, ControlCodeAccess.ReadWrite),
            // IOCTL_MOUNTMGR_BOOT_DL_ASSIGNMENT           CTL_CODE(MOUNTMGRCONTROLTYPE, 17, METHOD_BUFFERED, FILE_READ_ACCESS | FILE_WRITE_ACCESS)
            InlineData(ControlCodes.MountManager.BootDriveLetterAssignment, 17, ControlCodeMethod.Buffered, ControlCodeAccess.ReadWrite),
            // IOCTL_MOUNTMGR_TRACELOG_CACHE               CTL_CODE(MOUNTMGRCONTROLTYPE, 18, METHOD_BUFFERED, FILE_READ_ACCESS)
            InlineData(ControlCodes.MountManager.TracelogCache, 18, ControlCodeMethod.Buffered, ControlCodeAccess.Read),
            // IOCTL_MOUNTMGR_PREPARE_VOLUME_DELETE        CTL_CODE(MOUNTMGRCONTROLTYPE, 19, METHOD_BUFFERED, FILE_READ_ACCESS | FILE_WRITE_ACCESS)
            InlineData(ControlCodes.MountManager.PrepareVolumeDelete, 19, ControlCodeMethod.Buffered, ControlCodeAccess.ReadWrite),
            // IOCTL_MOUNTMGR_CANCEL_VOLUME_DELETE         CTL_CODE(MOUNTMGRCONTROLTYPE, 20, METHOD_BUFFERED, FILE_READ_ACCESS | FILE_WRITE_ACCESS)
            InlineData(ControlCodes.MountManager.CancelVolumeDelete, 20, ControlCodeMethod.Buffered, ControlCodeAccess.ReadWrite),
            // IOCTL_MOUNTMGR_SILO_ARRIVAL                 CTL_CODE(MOUNTMGRCONTROLTYPE, 21, METHOD_BUFFERED, FILE_READ_ACCESS | FILE_WRITE_ACCESS)
            InlineData(ControlCodes.MountManager.SiloArrival, 21, ControlCodeMethod.Buffered, ControlCodeAccess.ReadWrite)
            ]
        public void ValidateMountManagerCodes(ControlCodes.MountManager code, uint function, ControlCodeMethod method, ControlCodeAccess access)
        {
            ControlCode generatedCode = new ControlCode(ControlCodeDeviceType.MountManager, function, method, access);
            ((ControlCode)code).Should().Be(generatedCode, $"generated code is 0x{generatedCode.Value:x8}");
        }

        [Theory,
            // IOCTL_MOUNTDEV_QUERY_UNIQUE_ID              CTL_CODE(MOUNTDEVCONTROLTYPE, 0, METHOD_BUFFERED, FILE_ANY_ACCESS)
            InlineData(ControlCodes.MountDevice.QueryUniqueId, 0, ControlCodeMethod.Buffered, ControlCodeAccess.Any),
            // IOCTL_MOUNTDEV_UNIQUE_ID_CHANGE_NOTIFY      CTL_CODE(MOUNTDEVCONTROLTYPE, 1, METHOD_BUFFERED, FILE_READ_ACCESS | FILE_WRITE_ACCESS)
            InlineData(ControlCodes.MountDevice.UniqueIdChangeNotify, 1, ControlCodeMethod.Buffered, ControlCodeAccess.ReadWrite),
            // IOCTL_MOUNTDEV_QUERY_DEVICE_NAME            CTL_CODE(MOUNTDEVCONTROLTYPE, 2, METHOD_BUFFERED, FILE_ANY_ACCESS)
            InlineData(ControlCodes.MountDevice.QueryDeviceName, 2, ControlCodeMethod.Buffered, ControlCodeAccess.Any),
            // IOCTL_MOUNTDEV_QUERY_SUGGESTED_LINK_NAME    CTL_CODE(MOUNTDEVCONTROLTYPE, 3, METHOD_BUFFERED, FILE_ANY_ACCESS)
            InlineData(ControlCodes.MountDevice.QuerySuggestedLinkName, 3, ControlCodeMethod.Buffered, ControlCodeAccess.Any),
            // IOCTL_MOUNTDEV_LINK_CREATED                 CTL_CODE(MOUNTDEVCONTROLTYPE, 4, METHOD_BUFFERED, FILE_READ_ACCESS | FILE_WRITE_ACCESS)
            InlineData(ControlCodes.MountDevice.LinkCreated, 4, ControlCodeMethod.Buffered, ControlCodeAccess.ReadWrite),
            // IOCTL_MOUNTDEV_LINK_DELETED                 CTL_CODE(MOUNTDEVCONTROLTYPE, 5, METHOD_BUFFERED, FILE_READ_ACCESS | FILE_WRITE_ACCESS)
            InlineData(ControlCodes.MountDevice.LinkDeleted, 5, ControlCodeMethod.Buffered, ControlCodeAccess.ReadWrite),
            // IOCTL_MOUNTDEV_QUERY_STABLE_GUID            CTL_CODE(MOUNTDEVCONTROLTYPE, 6, METHOD_BUFFERED, FILE_ANY_ACCESS)
            InlineData(ControlCodes.MountDevice.QueryStableGuid, 6, ControlCodeMethod.Buffered, ControlCodeAccess.Any),
            // IOCTL_MOUNTDEV_QUERY_INTERFACE_NAME         CTL_CODE(MOUNTDEVCONTROLTYPE, 7, METHOD_BUFFERED, FILE_ANY_ACCESS)
            InlineData(ControlCodes.MountDevice.QueryInterfaceName, 7, ControlCodeMethod.Buffered, ControlCodeAccess.Any),
            ]
        public void ValidateMountDeviceCodes(ControlCodes.MountDevice code, uint function, ControlCodeMethod method, ControlCodeAccess access)
        {
            ControlCode generatedCode = new ControlCode(ControlCodeDeviceType.MountDevice, function, method, access);
            ((ControlCode)code).Should().Be(generatedCode, $"generated code is 0x{generatedCode.Value:x8}");
        }

        [Theory,
            // FSCTL_REQUEST_OPLOCK_LEVEL_1    CTL_CODE(FILE_DEVICE_FILE_SYSTEM,  0, METHOD_BUFFERED, FILE_ANY_ACCESS)
            InlineData(ControlCodes.FileSystem.RequestOpportunisticLockLevel1, 0, ControlCodeMethod.Buffered, ControlCodeAccess.Any),
            // FSCTL_REQUEST_OPLOCK_LEVEL_2    CTL_CODE(FILE_DEVICE_FILE_SYSTEM,  1, METHOD_BUFFERED, FILE_ANY_ACCESS)
            InlineData(ControlCodes.FileSystem.RequestOpportunisticLockLevel2, 1, ControlCodeMethod.Buffered, ControlCodeAccess.Any),
            // FSCTL_REQUEST_BATCH_OPLOCK      CTL_CODE(FILE_DEVICE_FILE_SYSTEM,  2, METHOD_BUFFERED, FILE_ANY_ACCESS)
            InlineData(ControlCodes.FileSystem.RequestBatchOpportunisticLock, 2, ControlCodeMethod.Buffered, ControlCodeAccess.Any),
            // FSCTL_OPLOCK_BREAK_ACKNOWLEDGE  CTL_CODE(FILE_DEVICE_FILE_SYSTEM,  3, METHOD_BUFFERED, FILE_ANY_ACCESS)
            InlineData(ControlCodes.FileSystem.OpportunisticLockBreakAcknowledge, 3, ControlCodeMethod.Buffered, ControlCodeAccess.Any),
            // FSCTL_OPBATCH_ACK_CLOSE_PENDING CTL_CODE(FILE_DEVICE_FILE_SYSTEM,  4, METHOD_BUFFERED, FILE_ANY_ACCESS)
            InlineData(ControlCodes.FileSystem.OpportunisticBatchAcknowledgeClosePending, 4, ControlCodeMethod.Buffered, ControlCodeAccess.Any),
            // FSCTL_OPLOCK_BREAK_NOTIFY       CTL_CODE(FILE_DEVICE_FILE_SYSTEM,  5, METHOD_BUFFERED, FILE_ANY_ACCESS)
            InlineData(ControlCodes.FileSystem.OpportunisticLockBreakNotify, 5, ControlCodeMethod.Buffered, ControlCodeAccess.Any),
            // FSCTL_LOCK_VOLUME               CTL_CODE(FILE_DEVICE_FILE_SYSTEM,  6, METHOD_BUFFERED, FILE_ANY_ACCESS)
            InlineData(ControlCodes.FileSystem.LockVolume, 6, ControlCodeMethod.Buffered, ControlCodeAccess.Any),
            // FSCTL_UNLOCK_VOLUME             CTL_CODE(FILE_DEVICE_FILE_SYSTEM,  7, METHOD_BUFFERED, FILE_ANY_ACCESS)
            InlineData(ControlCodes.FileSystem.UnlockVolume, 7, ControlCodeMethod.Buffered, ControlCodeAccess.Any),
            // FSCTL_DISMOUNT_VOLUME           CTL_CODE(FILE_DEVICE_FILE_SYSTEM,  8, METHOD_BUFFERED, FILE_ANY_ACCESS)
            InlineData(ControlCodes.FileSystem.DismountVolume, 8, ControlCodeMethod.Buffered, ControlCodeAccess.Any),
            // FSCTL_IS_VOLUME_MOUNTED         CTL_CODE(FILE_DEVICE_FILE_SYSTEM, 10, METHOD_BUFFERED, FILE_ANY_ACCESS)
            InlineData(ControlCodes.FileSystem.IsVolumeMounted, 10, ControlCodeMethod.Buffered, ControlCodeAccess.Any),
            // FSCTL_IS_PATHNAME_VALID         CTL_CODE(FILE_DEVICE_FILE_SYSTEM, 11, METHOD_BUFFERED, FILE_ANY_ACCESS) // PATHNAME_BUFFER,
            InlineData(ControlCodes.FileSystem.IsPathNameValid, 11, ControlCodeMethod.Buffered, ControlCodeAccess.Any),
            // FSCTL_MARK_VOLUME_DIRTY         CTL_CODE(FILE_DEVICE_FILE_SYSTEM, 12, METHOD_BUFFERED, FILE_ANY_ACCESS)
            InlineData(ControlCodes.FileSystem.MarkVolumeDirty, 12, ControlCodeMethod.Buffered, ControlCodeAccess.Any),
            // FSCTL_QUERY_RETRIEVAL_POINTERS  CTL_CODE(FILE_DEVICE_FILE_SYSTEM, 14,  METHOD_NEITHER, FILE_ANY_ACCESS)
            InlineData(ControlCodes.FileSystem.QueryRetrievalPointers, 14, ControlCodeMethod.Neither, ControlCodeAccess.Any),
            // FSCTL_GET_COMPRESSION           CTL_CODE(FILE_DEVICE_FILE_SYSTEM, 15, METHOD_BUFFERED, FILE_ANY_ACCESS)
            InlineData(ControlCodes.FileSystem.GetCompression, 15, ControlCodeMethod.Buffered, ControlCodeAccess.Any),
            // FSCTL_SET_COMPRESSION           CTL_CODE(FILE_DEVICE_FILE_SYSTEM, 16, METHOD_BUFFERED, FILE_READ_DATA | FILE_WRITE_DATA)
            InlineData(ControlCodes.FileSystem.SetCompression, 16, ControlCodeMethod.Buffered, ControlCodeAccess.ReadWrite),
            // FSCTL_SET_BOOTLOADER_ACCESSED   CTL_CODE(FILE_DEVICE_FILE_SYSTEM, 19,  METHOD_NEITHER, FILE_ANY_ACCESS)
            InlineData(ControlCodes.FileSystem.MarkAsSystemHive, 19, ControlCodeMethod.Neither, ControlCodeAccess.Any),
            // FSCTL_MARK_AS_SYSTEM_HIVE       FSCTL_SET_BOOTLOADER_ACCESSED <- This was the original code for 19
            // FSCTL_OPLOCK_BREAK_ACK_NO_2     CTL_CODE(FILE_DEVICE_FILE_SYSTEM, 20, METHOD_BUFFERED, FILE_ANY_ACCESS)
            InlineData(ControlCodes.FileSystem.OpportunisticLockBreakAcknowledge2, 20, ControlCodeMethod.Buffered, ControlCodeAccess.Any),
            // FSCTL_INVALIDATE_VOLUMES        CTL_CODE(FILE_DEVICE_FILE_SYSTEM, 21, METHOD_BUFFERED, FILE_ANY_ACCESS)
            InlineData(ControlCodes.FileSystem.InvalidateVolumes, 21, ControlCodeMethod.Buffered, ControlCodeAccess.Any),
            // FSCTL_QUERY_FAT_BPB             CTL_CODE(FILE_DEVICE_FILE_SYSTEM, 22, METHOD_BUFFERED, FILE_ANY_ACCESS) // FSCTL_QUERY_FAT_BPB_BUFFER
            InlineData(ControlCodes.FileSystem.QueryFatBiosParameterBlock, 22, ControlCodeMethod.Buffered, ControlCodeAccess.Any),
            // FSCTL_REQUEST_FILTER_OPLOCK     CTL_CODE(FILE_DEVICE_FILE_SYSTEM, 23, METHOD_BUFFERED, FILE_ANY_ACCESS)
            InlineData(ControlCodes.FileSystem.RequestFilterOpportunisticLock, 23, ControlCodeMethod.Buffered, ControlCodeAccess.Any),
            // FSCTL_FILESYSTEM_GET_STATISTICS CTL_CODE(FILE_DEVICE_FILE_SYSTEM, 24, METHOD_BUFFERED, FILE_ANY_ACCESS) // FILESYSTEM_STATISTICS
            InlineData(ControlCodes.FileSystem.FileSystemGetStatistics, 24, ControlCodeMethod.Buffered, ControlCodeAccess.Any),
            // FSCTL_GET_NTFS_VOLUME_DATA      CTL_CODE(FILE_DEVICE_FILE_SYSTEM, 25, METHOD_BUFFERED, FILE_ANY_ACCESS) // NTFS_VOLUME_DATA_BUFFER
            InlineData(ControlCodes.FileSystem.GetNtfsVolumeData, 25, ControlCodeMethod.Buffered, ControlCodeAccess.Any),
            // FSCTL_GET_NTFS_FILE_RECORD      CTL_CODE(FILE_DEVICE_FILE_SYSTEM, 26, METHOD_BUFFERED, FILE_ANY_ACCESS) // NTFS_FILE_RECORD_INPUT_BUFFER, NTFS_FILE_RECORD_OUTPUT_BUFFER
            InlineData(ControlCodes.FileSystem.GetNtfsFileRecord, 26, ControlCodeMethod.Buffered, ControlCodeAccess.Any),
            // FSCTL_GET_VOLUME_BITMAP         CTL_CODE(FILE_DEVICE_FILE_SYSTEM, 27,  METHOD_NEITHER, FILE_ANY_ACCESS) // STARTING_LCN_INPUT_BUFFER, VOLUME_BITMAP_BUFFER
            InlineData(ControlCodes.FileSystem.GetVolumeBitmap, 27, ControlCodeMethod.Neither, ControlCodeAccess.Any),
            // FSCTL_GET_RETRIEVAL_POINTERS    CTL_CODE(FILE_DEVICE_FILE_SYSTEM, 28,  METHOD_NEITHER, FILE_ANY_ACCESS) // STARTING_VCN_INPUT_BUFFER, RETRIEVAL_POINTERS_BUFFER
            InlineData(ControlCodes.FileSystem.GetRetrievalPointers, 28, ControlCodeMethod.Neither, ControlCodeAccess.Any),
            // FSCTL_MOVE_FILE                 CTL_CODE(FILE_DEVICE_FILE_SYSTEM, 29, METHOD_BUFFERED, FILE_SPECIAL_ACCESS) // MOVE_FILE_DATA,
            InlineData(ControlCodes.FileSystem.MoveFile, 29, ControlCodeMethod.Buffered, ControlCodeAccess.Any),
            // FSCTL_IS_VOLUME_DIRTY           CTL_CODE(FILE_DEVICE_FILE_SYSTEM, 30, METHOD_BUFFERED, FILE_ANY_ACCESS)
            InlineData(ControlCodes.FileSystem.IsVolumeDirty, 30, ControlCodeMethod.Buffered, ControlCodeAccess.Any),
            // FSCTL_ALLOW_EXTENDED_DASD_IO    CTL_CODE(FILE_DEVICE_FILE_SYSTEM, 32, METHOD_NEITHER,  FILE_ANY_ACCESS)
            InlineData(ControlCodes.FileSystem.AllowExtendedDasdIo, 32, ControlCodeMethod.Neither, ControlCodeAccess.Any),
            // FSCTL_FIND_FILES_BY_SID         CTL_CODE(FILE_DEVICE_FILE_SYSTEM, 35, METHOD_NEITHER, FILE_ANY_ACCESS)
            InlineData(ControlCodes.FileSystem.FindFilesBySid, 35, ControlCodeMethod.Neither, ControlCodeAccess.Any),
            // FSCTL_SET_OBJECT_ID             CTL_CODE(FILE_DEVICE_FILE_SYSTEM, 38, METHOD_BUFFERED, FILE_SPECIAL_ACCESS) // FILE_OBJECTID_BUFFER
            InlineData(ControlCodes.FileSystem.SetObjectId, 38, ControlCodeMethod.Buffered, ControlCodeAccess.Any),
            // FSCTL_GET_OBJECT_ID             CTL_CODE(FILE_DEVICE_FILE_SYSTEM, 39, METHOD_BUFFERED, FILE_ANY_ACCESS) // FILE_OBJECTID_BUFFER
            InlineData(ControlCodes.FileSystem.GetObjectId, 39, ControlCodeMethod.Buffered, ControlCodeAccess.Any),
            // FSCTL_DELETE_OBJECT_ID          CTL_CODE(FILE_DEVICE_FILE_SYSTEM, 40, METHOD_BUFFERED, FILE_SPECIAL_ACCESS)
            InlineData(ControlCodes.FileSystem.DeleteObjectId, 40, ControlCodeMethod.Buffered, ControlCodeAccess.Any),
            // FSCTL_SET_REPARSE_POINT         CTL_CODE(FILE_DEVICE_FILE_SYSTEM, 41, METHOD_BUFFERED, FILE_SPECIAL_ACCESS) // REPARSE_DATA_BUFFER,
            InlineData(ControlCodes.FileSystem.SetReparsePoint, 41, ControlCodeMethod.Buffered, ControlCodeAccess.Any),
            // FSCTL_GET_REPARSE_POINT         CTL_CODE(FILE_DEVICE_FILE_SYSTEM, 42, METHOD_BUFFERED, FILE_ANY_ACCESS) // REPARSE_DATA_BUFFER
            InlineData(ControlCodes.FileSystem.GetReparsePoint, 42, ControlCodeMethod.Buffered, ControlCodeAccess.Any),
            // FSCTL_DELETE_REPARSE_POINT      CTL_CODE(FILE_DEVICE_FILE_SYSTEM, 43, METHOD_BUFFERED, FILE_SPECIAL_ACCESS) // REPARSE_DATA_BUFFER,
            InlineData(ControlCodes.FileSystem.DeleteReparsePoint, 43, ControlCodeMethod.Buffered, ControlCodeAccess.Any),
            // FSCTL_ENUM_USN_DATA             CTL_CODE(FILE_DEVICE_FILE_SYSTEM, 44,  METHOD_NEITHER, FILE_ANY_ACCESS) // MFT_ENUM_DATA,
            InlineData(ControlCodes.FileSystem.EnumUsnData, 44, ControlCodeMethod.Neither, ControlCodeAccess.Any),
            // FSCTL_SECURITY_ID_CHECK         CTL_CODE(FILE_DEVICE_FILE_SYSTEM, 45,  METHOD_NEITHER, FILE_READ_DATA)  // BULK_SECURITY_TEST_DATA,
            InlineData(ControlCodes.FileSystem.SecurityIdCheck, 45, ControlCodeMethod.Neither, ControlCodeAccess.Read),
            // FSCTL_READ_USN_JOURNAL          CTL_CODE(FILE_DEVICE_FILE_SYSTEM, 46,  METHOD_NEITHER, FILE_ANY_ACCESS) // READ_USN_JOURNAL_DATA, USN
            InlineData(ControlCodes.FileSystem.ReadUsnJournal, 46, ControlCodeMethod.Neither, ControlCodeAccess.Any),
            // FSCTL_SET_OBJECT_ID_EXTENDED    CTL_CODE(FILE_DEVICE_FILE_SYSTEM, 47, METHOD_BUFFERED, FILE_SPECIAL_ACCESS)
            InlineData(ControlCodes.FileSystem.SetObjectIdExtended, 47, ControlCodeMethod.Buffered, ControlCodeAccess.Any),
            // FSCTL_CREATE_OR_GET_OBJECT_ID   CTL_CODE(FILE_DEVICE_FILE_SYSTEM, 48, METHOD_BUFFERED, FILE_ANY_ACCESS) // FILE_OBJECTID_BUFFER
            InlineData(ControlCodes.FileSystem.CreateOrGetObjectId, 48, ControlCodeMethod.Buffered, ControlCodeAccess.Any),
            // FSCTL_SET_SPARSE                CTL_CODE(FILE_DEVICE_FILE_SYSTEM, 49, METHOD_BUFFERED, FILE_SPECIAL_ACCESS)
            InlineData(ControlCodes.FileSystem.SetSparse, 49, ControlCodeMethod.Buffered, ControlCodeAccess.Any),
            // FSCTL_SET_ZERO_DATA             CTL_CODE(FILE_DEVICE_FILE_SYSTEM, 50, METHOD_BUFFERED, FILE_WRITE_DATA) // FILE_ZERO_DATA_INFORMATION,
            InlineData(ControlCodes.FileSystem.SetZeroData, 50, ControlCodeMethod.Buffered, ControlCodeAccess.Write),
            // FSCTL_QUERY_ALLOCATED_RANGES    CTL_CODE(FILE_DEVICE_FILE_SYSTEM, 51,  METHOD_NEITHER, FILE_READ_DATA)  // FILE_ALLOCATED_RANGE_BUFFER, FILE_ALLOCATED_RANGE_BUFFER
            InlineData(ControlCodes.FileSystem.QueryAllocatedRanges, 51, ControlCodeMethod.Neither, ControlCodeAccess.Read),
            // FSCTL_ENABLE_UPGRADE            CTL_CODE(FILE_DEVICE_FILE_SYSTEM, 52, METHOD_BUFFERED, FILE_WRITE_DATA)
            InlineData(ControlCodes.FileSystem.EnableUpgrade, 52, ControlCodeMethod.Buffered, ControlCodeAccess.Write),
            // FSCTL_SET_ENCRYPTION            CTL_CODE(FILE_DEVICE_FILE_SYSTEM, 53,  METHOD_NEITHER, FILE_ANY_ACCESS) // ENCRYPTION_BUFFER, DECRYPTION_STATUS_BUFFER
            InlineData(ControlCodes.FileSystem.SetEncryption, 53, ControlCodeMethod.Neither, ControlCodeAccess.Any),
            // FSCTL_ENCRYPTION_FSCTL_IO       CTL_CODE(FILE_DEVICE_FILE_SYSTEM, 54,  METHOD_NEITHER, FILE_ANY_ACCESS)
            InlineData(ControlCodes.FileSystem.EncryptionIo, 54, ControlCodeMethod.Neither, ControlCodeAccess.Any),
            // FSCTL_WRITE_RAW_ENCRYPTED       CTL_CODE(FILE_DEVICE_FILE_SYSTEM, 55,  METHOD_NEITHER, FILE_SPECIAL_ACCESS) // ENCRYPTED_DATA_INFO, EXTENDED_ENCRYPTED_DATA_INFO
            InlineData(ControlCodes.FileSystem.WriteRawEncrypted, 55, ControlCodeMethod.Neither, ControlCodeAccess.Any),
            // FSCTL_READ_RAW_ENCRYPTED        CTL_CODE(FILE_DEVICE_FILE_SYSTEM, 56,  METHOD_NEITHER, FILE_SPECIAL_ACCESS) // REQUEST_RAW_ENCRYPTED_DATA, ENCRYPTED_DATA_INFO, EXTENDED_ENCRYPTED_DATA_INFO
            InlineData(ControlCodes.FileSystem.ReadRawEncrypted, 56, ControlCodeMethod.Neither, ControlCodeAccess.Any),
            // FSCTL_CREATE_USN_JOURNAL        CTL_CODE(FILE_DEVICE_FILE_SYSTEM, 57,  METHOD_NEITHER, FILE_ANY_ACCESS) // CREATE_USN_JOURNAL_DATA,
            InlineData(ControlCodes.FileSystem.CreateUsnJournal, 57, ControlCodeMethod.Neither, ControlCodeAccess.Any),
            // FSCTL_READ_FILE_USN_DATA        CTL_CODE(FILE_DEVICE_FILE_SYSTEM, 58,  METHOD_NEITHER, FILE_ANY_ACCESS) // Read the Usn Record for a file
            InlineData(ControlCodes.FileSystem.ReadFileUsnData, 58, ControlCodeMethod.Neither, ControlCodeAccess.Any),
            // FSCTL_WRITE_USN_CLOSE_RECORD    CTL_CODE(FILE_DEVICE_FILE_SYSTEM, 59,  METHOD_NEITHER, FILE_ANY_ACCESS) // Generate Close Usn Record
            InlineData(ControlCodes.FileSystem.WriteUsnCloseRecord, 59, ControlCodeMethod.Neither, ControlCodeAccess.Any),
            // FSCTL_EXTEND_VOLUME             CTL_CODE(FILE_DEVICE_FILE_SYSTEM, 60, METHOD_BUFFERED, FILE_ANY_ACCESS)
            InlineData(ControlCodes.FileSystem.ExtendVolume, 60, ControlCodeMethod.Buffered, ControlCodeAccess.Any),
            // FSCTL_QUERY_USN_JOURNAL         CTL_CODE(FILE_DEVICE_FILE_SYSTEM, 61, METHOD_BUFFERED, FILE_ANY_ACCESS)
            InlineData(ControlCodes.FileSystem.QueryUsnJournal, 61, ControlCodeMethod.Buffered, ControlCodeAccess.Any),
            // FSCTL_DELETE_USN_JOURNAL        CTL_CODE(FILE_DEVICE_FILE_SYSTEM, 62, METHOD_BUFFERED, FILE_ANY_ACCESS)
            InlineData(ControlCodes.FileSystem.DeleteUsnJournal, 62, ControlCodeMethod.Buffered, ControlCodeAccess.Any),
            // FSCTL_MARK_HANDLE               CTL_CODE(FILE_DEVICE_FILE_SYSTEM, 63, METHOD_BUFFERED, FILE_ANY_ACCESS)
            InlineData(ControlCodes.FileSystem.MarkHandle, 63, ControlCodeMethod.Buffered, ControlCodeAccess.Any),
            // FSCTL_SIS_COPYFILE              CTL_CODE(FILE_DEVICE_FILE_SYSTEM, 64, METHOD_BUFFERED, FILE_ANY_ACCESS)
            InlineData(ControlCodes.FileSystem.SisCopyFile, 64, ControlCodeMethod.Buffered, ControlCodeAccess.Any),
            // FSCTL_SIS_LINK_FILES            CTL_CODE(FILE_DEVICE_FILE_SYSTEM, 65, METHOD_BUFFERED, FILE_READ_DATA | FILE_WRITE_DATA)
            InlineData(ControlCodes.FileSystem.SisLinkFiles, 65, ControlCodeMethod.Buffered, ControlCodeAccess.ReadWrite),
            // FSCTL_RECALL_FILE               CTL_CODE(FILE_DEVICE_FILE_SYSTEM, 69, METHOD_NEITHER, FILE_ANY_ACCESS)
            InlineData(ControlCodes.FileSystem.RecallFile, 69, ControlCodeMethod.Neither, ControlCodeAccess.Any),
            // FSCTL_READ_FROM_PLEX            CTL_CODE(FILE_DEVICE_FILE_SYSTEM, 71, METHOD_OUT_DIRECT, FILE_READ_DATA)
            InlineData(ControlCodes.FileSystem.ReadFromPlex, 71, ControlCodeMethod.OutDirect, ControlCodeAccess.Read),
            // FSCTL_FILE_PREFETCH             CTL_CODE(FILE_DEVICE_FILE_SYSTEM, 72, METHOD_BUFFERED, FILE_SPECIAL_ACCESS) // FILE_PREFETCH
            InlineData(ControlCodes.FileSystem.FilePrefetch, 72, ControlCodeMethod.Buffered, ControlCodeAccess.Any),
            // FSCTL_MAKE_MEDIA_COMPATIBLE         CTL_CODE(FILE_DEVICE_FILE_SYSTEM, 76, METHOD_BUFFERED, FILE_WRITE_DATA) // UDFS R/W
            InlineData(ControlCodes.FileSystem.MakeMediaCompatible, 76, ControlCodeMethod.Buffered, ControlCodeAccess.Write),
            // FSCTL_SET_DEFECT_MANAGEMENT         CTL_CODE(FILE_DEVICE_FILE_SYSTEM, 77, METHOD_BUFFERED, FILE_WRITE_DATA) // UDFS R/W
            InlineData(ControlCodes.FileSystem.SetDefectManagement, 77, ControlCodeMethod.Buffered, ControlCodeAccess.Write),
            // FSCTL_QUERY_SPARING_INFO            CTL_CODE(FILE_DEVICE_FILE_SYSTEM, 78, METHOD_BUFFERED, FILE_ANY_ACCESS) // UDFS R/W
            InlineData(ControlCodes.FileSystem.QuerySparingInfo, 78, ControlCodeMethod.Buffered, ControlCodeAccess.Any),
            // FSCTL_QUERY_ON_DISK_VOLUME_INFO     CTL_CODE(FILE_DEVICE_FILE_SYSTEM, 79, METHOD_BUFFERED, FILE_ANY_ACCESS) // C/UDFS
            InlineData(ControlCodes.FileSystem.QueryOnDiskVolumeInfo, 79, ControlCodeMethod.Buffered, ControlCodeAccess.Any),
            // FSCTL_SET_VOLUME_COMPRESSION_STATE  CTL_CODE(FILE_DEVICE_FILE_SYSTEM, 80, METHOD_BUFFERED, FILE_SPECIAL_ACCESS) // VOLUME_COMPRESSION_STATE
            InlineData(ControlCodes.FileSystem.SetVolumeCompressionState, 80, ControlCodeMethod.Buffered, ControlCodeAccess.Any),
            // FSCTL_TXFS_MODIFY_RM                CTL_CODE(FILE_DEVICE_FILE_SYSTEM, 81, METHOD_BUFFERED, FILE_WRITE_DATA) // TxF
            InlineData(ControlCodes.FileSystem.TxfsModifyRm, 81, ControlCodeMethod.Buffered, ControlCodeAccess.Write),
            // FSCTL_TXFS_QUERY_RM_INFORMATION     CTL_CODE(FILE_DEVICE_FILE_SYSTEM, 82, METHOD_BUFFERED, FILE_READ_DATA)  // TxF
            InlineData(ControlCodes.FileSystem.TxfsQueryRmInformation, 82, ControlCodeMethod.Buffered, ControlCodeAccess.Read),
            // FSCTL_TXFS_ROLLFORWARD_REDO         CTL_CODE(FILE_DEVICE_FILE_SYSTEM, 84, METHOD_BUFFERED, FILE_WRITE_DATA) // TxF
            InlineData(ControlCodes.FileSystem.TxfsRollForwardRedo, 84, ControlCodeMethod.Buffered, ControlCodeAccess.Write),
            // FSCTL_TXFS_ROLLFORWARD_UNDO         CTL_CODE(FILE_DEVICE_FILE_SYSTEM, 85, METHOD_BUFFERED, FILE_WRITE_DATA) // TxF
            InlineData(ControlCodes.FileSystem.TxfsRollForwardUndo, 85, ControlCodeMethod.Buffered, ControlCodeAccess.Write),
            // FSCTL_TXFS_START_RM                 CTL_CODE(FILE_DEVICE_FILE_SYSTEM, 86, METHOD_BUFFERED, FILE_WRITE_DATA) // TxF
            InlineData(ControlCodes.FileSystem.TxfsStartRm, 86, ControlCodeMethod.Buffered, ControlCodeAccess.Write),
            // FSCTL_TXFS_SHUTDOWN_RM              CTL_CODE(FILE_DEVICE_FILE_SYSTEM, 87, METHOD_BUFFERED, FILE_WRITE_DATA) // TxF
            InlineData(ControlCodes.FileSystem.TxfsShutdownRm, 87, ControlCodeMethod.Buffered, ControlCodeAccess.Write),
            // FSCTL_TXFS_READ_BACKUP_INFORMATION  CTL_CODE(FILE_DEVICE_FILE_SYSTEM, 88, METHOD_BUFFERED, FILE_READ_DATA)  // TxF
            InlineData(ControlCodes.FileSystem.TxfsReadBackupInformation, 88, ControlCodeMethod.Buffered, ControlCodeAccess.Read),
            // FSCTL_TXFS_WRITE_BACKUP_INFORMATION CTL_CODE(FILE_DEVICE_FILE_SYSTEM, 89, METHOD_BUFFERED, FILE_WRITE_DATA) // TxF
            InlineData(ControlCodes.FileSystem.TxfsWriteBackupInformation, 89, ControlCodeMethod.Buffered, ControlCodeAccess.Write),
            // FSCTL_TXFS_CREATE_SECONDARY_RM      CTL_CODE(FILE_DEVICE_FILE_SYSTEM, 90, METHOD_BUFFERED, FILE_WRITE_DATA) // TxF
            InlineData(ControlCodes.FileSystem.TxfsCreateSecondaryRm, 90, ControlCodeMethod.Buffered, ControlCodeAccess.Write),
            // FSCTL_TXFS_GET_METADATA_INFO        CTL_CODE(FILE_DEVICE_FILE_SYSTEM, 91, METHOD_BUFFERED, FILE_READ_DATA)  // TxF
            InlineData(ControlCodes.FileSystem.TxfsGetMetadataInfo, 91, ControlCodeMethod.Buffered, ControlCodeAccess.Read),
            // FSCTL_TXFS_GET_TRANSACTED_VERSION   CTL_CODE(FILE_DEVICE_FILE_SYSTEM, 92, METHOD_BUFFERED, FILE_READ_DATA)  // TxF
            InlineData(ControlCodes.FileSystem.TxfsGetTransactedVersion, 92, ControlCodeMethod.Buffered, ControlCodeAccess.Read),
            // FSCTL_TXFS_SAVEPOINT_INFORMATION    CTL_CODE(FILE_DEVICE_FILE_SYSTEM, 94, METHOD_BUFFERED, FILE_WRITE_DATA) // TxF
            InlineData(ControlCodes.FileSystem.TxfsSavePointInformation, 94, ControlCodeMethod.Buffered, ControlCodeAccess.Write),
            // FSCTL_TXFS_CREATE_MINIVERSION       CTL_CODE(FILE_DEVICE_FILE_SYSTEM, 95, METHOD_BUFFERED, FILE_WRITE_DATA) // TxF
            InlineData(ControlCodes.FileSystem.TxfsCreateMiniversion, 95, ControlCodeMethod.Buffered, ControlCodeAccess.Write),
            // FSCTL_TXFS_TRANSACTION_ACTIVE       CTL_CODE(FILE_DEVICE_FILE_SYSTEM, 99, METHOD_BUFFERED, FILE_READ_DATA)  // TxF
            InlineData(ControlCodes.FileSystem.TxfsTransactionActive, 99, ControlCodeMethod.Buffered, ControlCodeAccess.Read),
            // FSCTL_SET_ZERO_ON_DEALLOCATION      CTL_CODE(FILE_DEVICE_FILE_SYSTEM, 101, METHOD_BUFFERED, FILE_SPECIAL_ACCESS)
            InlineData(ControlCodes.FileSystem.SetZeroOnDeallocation, 101, ControlCodeMethod.Buffered, ControlCodeAccess.Any),
            // FSCTL_SET_REPAIR                    CTL_CODE(FILE_DEVICE_FILE_SYSTEM, 102, METHOD_BUFFERED, FILE_ANY_ACCESS)
            InlineData(ControlCodes.FileSystem.SetRepair, 102, ControlCodeMethod.Buffered, ControlCodeAccess.Any),
            // FSCTL_GET_REPAIR                    CTL_CODE(FILE_DEVICE_FILE_SYSTEM, 103, METHOD_BUFFERED, FILE_ANY_ACCESS)
            InlineData(ControlCodes.FileSystem.GetRepair, 103, ControlCodeMethod.Buffered, ControlCodeAccess.Any),
            // FSCTL_WAIT_FOR_REPAIR               CTL_CODE(FILE_DEVICE_FILE_SYSTEM, 104, METHOD_BUFFERED, FILE_ANY_ACCESS)
            InlineData(ControlCodes.FileSystem.WaitForRepair, 104, ControlCodeMethod.Buffered, ControlCodeAccess.Any),
            // FSCTL_INITIATE_REPAIR               CTL_CODE(FILE_DEVICE_FILE_SYSTEM, 106, METHOD_BUFFERED, FILE_ANY_ACCESS)
            InlineData(ControlCodes.FileSystem.InitiateRepair, 106, ControlCodeMethod.Buffered, ControlCodeAccess.Any),
            // FSCTL_CSC_INTERNAL                  CTL_CODE(FILE_DEVICE_FILE_SYSTEM, 107, METHOD_NEITHER, FILE_ANY_ACCESS) // CSC internal implementation
            InlineData(ControlCodes.FileSystem.CscInternal, 107, ControlCodeMethod.Neither, ControlCodeAccess.Any),
            // FSCTL_SHRINK_VOLUME                 CTL_CODE(FILE_DEVICE_FILE_SYSTEM, 108, METHOD_BUFFERED, FILE_SPECIAL_ACCESS) // SHRINK_VOLUME_INFORMATION
            InlineData(ControlCodes.FileSystem.ShrinkVolume, 108, ControlCodeMethod.Buffered, ControlCodeAccess.Any),
            // FSCTL_SET_SHORT_NAME_BEHAVIOR       CTL_CODE(FILE_DEVICE_FILE_SYSTEM, 109, METHOD_BUFFERED, FILE_ANY_ACCESS)
            InlineData(ControlCodes.FileSystem.SetShortNameBehavior, 109, ControlCodeMethod.Buffered, ControlCodeAccess.Any),
            // FSCTL_DFSR_SET_GHOST_HANDLE_STATE   CTL_CODE(FILE_DEVICE_FILE_SYSTEM, 110, METHOD_BUFFERED, FILE_ANY_ACCESS)
            InlineData(ControlCodes.FileSystem.DfsrSetGhostHandleState, 110, ControlCodeMethod.Buffered, ControlCodeAccess.Any),
            // FSCTL_TXFS_LIST_TRANSACTION_LOCKED_FILES  CTL_CODE(FILE_DEVICE_FILE_SYSTEM, 120, METHOD_BUFFERED, FILE_READ_DATA) // TxF
            InlineData(ControlCodes.FileSystem.TxfsListTransactionLockedFiles, 120, ControlCodeMethod.Buffered, ControlCodeAccess.Read),
            // FSCTL_TXFS_LIST_TRANSACTIONS        CTL_CODE(FILE_DEVICE_FILE_SYSTEM, 121, METHOD_BUFFERED, FILE_READ_DATA) // TxF
            InlineData(ControlCodes.FileSystem.TxfsListTransactions, 121, ControlCodeMethod.Buffered, ControlCodeAccess.Read),
            // FSCTL_QUERY_PAGEFILE_ENCRYPTION     CTL_CODE(FILE_DEVICE_FILE_SYSTEM, 122, METHOD_BUFFERED, FILE_ANY_ACCESS)
            InlineData(ControlCodes.FileSystem.QueryPagefileEncryption, 122, ControlCodeMethod.Buffered, ControlCodeAccess.Any),
            // FSCTL_RESET_VOLUME_ALLOCATION_HINTS CTL_CODE(FILE_DEVICE_FILE_SYSTEM, 123, METHOD_BUFFERED, FILE_ANY_ACCESS)
            InlineData(ControlCodes.FileSystem.ResetVolumeAllocationHints, 123, ControlCodeMethod.Buffered, ControlCodeAccess.Any),
            // FSCTL_QUERY_DEPENDENT_VOLUME        CTL_CODE(FILE_DEVICE_FILE_SYSTEM, 124, METHOD_BUFFERED, FILE_ANY_ACCESS)    // Dependency File System Filter
            InlineData(ControlCodes.FileSystem.QueryDependentVolume, 124, ControlCodeMethod.Buffered, ControlCodeAccess.Any),
            // FSCTL_SD_GLOBAL_CHANGE              CTL_CODE(FILE_DEVICE_FILE_SYSTEM, 125, METHOD_BUFFERED, FILE_ANY_ACCESS) // Query/Change NTFS Security Descriptors
            InlineData(ControlCodes.FileSystem.SecurityDescriptorGlobalChange, 125, ControlCodeMethod.Buffered, ControlCodeAccess.Any),
            // FSCTL_TXFS_READ_BACKUP_INFORMATION2 CTL_CODE(FILE_DEVICE_FILE_SYSTEM, 126, METHOD_BUFFERED, FILE_ANY_ACCESS) // TxF
            InlineData(ControlCodes.FileSystem.TxfsReadBackupInformation2, 126, ControlCodeMethod.Buffered, ControlCodeAccess.Any),
            // FSCTL_LOOKUP_STREAM_FROM_CLUSTER    CTL_CODE(FILE_DEVICE_FILE_SYSTEM, 127, METHOD_BUFFERED, FILE_ANY_ACCESS)
            InlineData(ControlCodes.FileSystem.LookupStreamFromCluster, 127, ControlCodeMethod.Buffered, ControlCodeAccess.Any),
            // FSCTL_TXFS_WRITE_BACKUP_INFORMATION2 CTL_CODE(FILE_DEVICE_FILE_SYSTEM, 128, METHOD_BUFFERED, FILE_ANY_ACCESS) // TxF
            InlineData(ControlCodes.FileSystem.TxfsWriteBackupInformation2, 128, ControlCodeMethod.Buffered, ControlCodeAccess.Any),
            // FSCTL_FILE_TYPE_NOTIFICATION        CTL_CODE(FILE_DEVICE_FILE_SYSTEM, 129, METHOD_BUFFERED, FILE_ANY_ACCESS)
            InlineData(ControlCodes.FileSystem.FileTypeNotification, 129, ControlCodeMethod.Buffered, ControlCodeAccess.Any),
            // FSCTL_FILE_LEVEL_TRIM               CTL_CODE(FILE_DEVICE_FILE_SYSTEM, 130, METHOD_BUFFERED, FILE_WRITE_DATA)
            InlineData(ControlCodes.FileSystem.FileLevelTrim, 130, ControlCodeMethod.Buffered, ControlCodeAccess.Write),
            // FSCTL_GET_BOOT_AREA_INFO            CTL_CODE(FILE_DEVICE_FILE_SYSTEM, 140, METHOD_BUFFERED, FILE_ANY_ACCESS) // BOOT_AREA_INFO
            InlineData(ControlCodes.FileSystem.GetBootAreaInfo, 140, ControlCodeMethod.Buffered, ControlCodeAccess.Any),
            // FSCTL_GET_RETRIEVAL_POINTER_BASE    CTL_CODE(FILE_DEVICE_FILE_SYSTEM, 141, METHOD_BUFFERED, FILE_ANY_ACCESS) // RETRIEVAL_POINTER_BASE
            InlineData(ControlCodes.FileSystem.GetRetrievalPointerBase, 141, ControlCodeMethod.Buffered, ControlCodeAccess.Any),
            // FSCTL_SET_PERSISTENT_VOLUME_STATE   CTL_CODE(FILE_DEVICE_FILE_SYSTEM, 142, METHOD_BUFFERED, FILE_ANY_ACCESS)  // FILE_FS_PERSISTENT_VOLUME_INFORMATION
            InlineData(ControlCodes.FileSystem.SetPersistentVolumeState, 142, ControlCodeMethod.Buffered, ControlCodeAccess.Any),
            // FSCTL_QUERY_PERSISTENT_VOLUME_STATE CTL_CODE(FILE_DEVICE_FILE_SYSTEM, 143, METHOD_BUFFERED, FILE_ANY_ACCESS)  // FILE_FS_PERSISTENT_VOLUME_INFORMATION
            InlineData(ControlCodes.FileSystem.QueryPersistentVolumeState, 143, ControlCodeMethod.Buffered, ControlCodeAccess.Any),
            // FSCTL_REQUEST_OPLOCK                CTL_CODE(FILE_DEVICE_FILE_SYSTEM, 144, METHOD_BUFFERED, FILE_ANY_ACCESS)
            InlineData(ControlCodes.FileSystem.RequestOpportunisticLock, 144, ControlCodeMethod.Buffered, ControlCodeAccess.Any),
            // FSCTL_CSV_TUNNEL_REQUEST            CTL_CODE(FILE_DEVICE_FILE_SYSTEM, 145, METHOD_BUFFERED, FILE_ANY_ACCESS) // CSV_TUNNEL_REQUEST
            InlineData(ControlCodes.FileSystem.CsvTunnelRequest, 145, ControlCodeMethod.Buffered, ControlCodeAccess.Any),
            // FSCTL_IS_CSV_FILE                   CTL_CODE(FILE_DEVICE_FILE_SYSTEM, 146, METHOD_BUFFERED, FILE_ANY_ACCESS) // IS_CSV_FILE
            InlineData(ControlCodes.FileSystem.IsCsvFile, 146, ControlCodeMethod.Buffered, ControlCodeAccess.Any),
            // FSCTL_QUERY_FILE_SYSTEM_RECOGNITION CTL_CODE(FILE_DEVICE_FILE_SYSTEM, 147, METHOD_BUFFERED, FILE_ANY_ACCESS) //
            InlineData(ControlCodes.FileSystem.QueryFileSystemRecognition, 147, ControlCodeMethod.Buffered, ControlCodeAccess.Any),
            // FSCTL_CSV_GET_VOLUME_PATH_NAME      CTL_CODE(FILE_DEVICE_FILE_SYSTEM, 148, METHOD_BUFFERED, FILE_ANY_ACCESS)
            InlineData(ControlCodes.FileSystem.CsvGetVolumePathName, 148, ControlCodeMethod.Buffered, ControlCodeAccess.Any),
            // FSCTL_CSV_GET_VOLUME_NAME_FOR_VOLUME_MOUNT_POINT CTL_CODE(FILE_DEVICE_FILE_SYSTEM, 149, METHOD_BUFFERED, FILE_ANY_ACCESS)
            InlineData(ControlCodes.FileSystem.CsvGetVolumeNameForVolumeMountPoint, 149, ControlCodeMethod.Buffered, ControlCodeAccess.Any),
            // FSCTL_CSV_GET_VOLUME_PATH_NAMES_FOR_VOLUME_NAME CTL_CODE(FILE_DEVICE_FILE_SYSTEM, 150,  METHOD_BUFFERED, FILE_ANY_ACCESS)
            InlineData(ControlCodes.FileSystem.CsvGetVolumePathNamesForVolumeName, 150, ControlCodeMethod.Buffered, ControlCodeAccess.Any),
            // FSCTL_IS_FILE_ON_CSV_VOLUME         CTL_CODE(FILE_DEVICE_FILE_SYSTEM, 151,  METHOD_BUFFERED, FILE_ANY_ACCESS)
            InlineData(ControlCodes.FileSystem.IsFileOnCsvVolume, 151, ControlCodeMethod.Buffered, ControlCodeAccess.Any),
            // FSCTL_CORRUPTION_HANDLING           CTL_CODE(FILE_DEVICE_FILE_SYSTEM, 152, METHOD_BUFFERED, FILE_ANY_ACCESS)
            InlineData(ControlCodes.FileSystem.CorruptionHandling, 152, ControlCodeMethod.Buffered, ControlCodeAccess.Any),
            // FSCTL_OFFLOAD_READ                  CTL_CODE(FILE_DEVICE_FILE_SYSTEM, 153, METHOD_BUFFERED, FILE_READ_ACCESS)
            InlineData(ControlCodes.FileSystem.OffloadRead, 153, ControlCodeMethod.Buffered, ControlCodeAccess.Read),
            // FSCTL_OFFLOAD_WRITE                 CTL_CODE(FILE_DEVICE_FILE_SYSTEM, 154, METHOD_BUFFERED, FILE_WRITE_ACCESS)
            InlineData(ControlCodes.FileSystem.OffloadWrite, 154, ControlCodeMethod.Buffered, ControlCodeAccess.Write),
            // FSCTL_CSV_INTERNAL                  CTL_CODE(FILE_DEVICE_FILE_SYSTEM, 155,  METHOD_BUFFERED, FILE_ANY_ACCESS)
            InlineData(ControlCodes.FileSystem.CsvInternal, 155, ControlCodeMethod.Buffered, ControlCodeAccess.Any),
            // FSCTL_SET_PURGE_FAILURE_MODE        CTL_CODE(FILE_DEVICE_FILE_SYSTEM, 156, METHOD_BUFFERED, FILE_ANY_ACCESS)
            InlineData(ControlCodes.FileSystem.SetPurgeFailureMode, 156, ControlCodeMethod.Buffered, ControlCodeAccess.Any),
            // FSCTL_QUERY_FILE_LAYOUT             CTL_CODE(FILE_DEVICE_FILE_SYSTEM, 157, METHOD_NEITHER, FILE_ANY_ACCESS)
            InlineData(ControlCodes.FileSystem.QueryFileLayout, 157, ControlCodeMethod.Neither, ControlCodeAccess.Any),
            // FSCTL_IS_VOLUME_OWNED_BYCSVFS       CTL_CODE(FILE_DEVICE_FILE_SYSTEM, 158,  METHOD_BUFFERED, FILE_ANY_ACCESS)
            InlineData(ControlCodes.FileSystem.IsVolumeOwnedByCsvfs, 158, ControlCodeMethod.Buffered, ControlCodeAccess.Any),
            // FSCTL_GET_INTEGRITY_INFORMATION     CTL_CODE(FILE_DEVICE_FILE_SYSTEM, 159, METHOD_BUFFERED, FILE_ANY_ACCESS)                  // FSCTL_GET_INTEGRITY_INFORMATION_BUFFER
            InlineData(ControlCodes.FileSystem.GetIntegrityInformation, 159, ControlCodeMethod.Buffered, ControlCodeAccess.Any),
            // FSCTL_SET_INTEGRITY_INFORMATION     CTL_CODE(FILE_DEVICE_FILE_SYSTEM, 160, METHOD_BUFFERED, FILE_READ_DATA | FILE_WRITE_DATA) // FSCTL_SET_INTEGRITY_INFORMATION_BUFFER
            InlineData(ControlCodes.FileSystem.SetIntegrityInformation, 160, ControlCodeMethod.Buffered, ControlCodeAccess.ReadWrite),
            // FSCTL_QUERY_FILE_REGIONS            CTL_CODE(FILE_DEVICE_FILE_SYSTEM, 161, METHOD_BUFFERED, FILE_ANY_ACCESS)
            InlineData(ControlCodes.FileSystem.QueryFileRegions, 161, ControlCodeMethod.Buffered, ControlCodeAccess.Any),
            // FSCTL_DEDUP_FILE                    CTL_CODE(FILE_DEVICE_FILE_SYSTEM, 165, METHOD_BUFFERED, FILE_ANY_ACCESS)
            InlineData(ControlCodes.FileSystem.DedupFile, 165, ControlCodeMethod.Buffered, ControlCodeAccess.Any),
            // FSCTL_DEDUP_QUERY_FILE_HASHES       CTL_CODE(FILE_DEVICE_FILE_SYSTEM, 166, METHOD_NEITHER, FILE_READ_DATA)
            InlineData(ControlCodes.FileSystem.DedupQueryFileHashes, 166, ControlCodeMethod.Neither, ControlCodeAccess.Read),
            // FSCTL_RKF_INTERNAL                  CTL_CODE(FILE_DEVICE_FILE_SYSTEM, 171, METHOD_NEITHER, FILE_ANY_ACCESS) // Resume Key Filter
            InlineData(ControlCodes.FileSystem.ResumeKeyFilterInternal, 171, ControlCodeMethod.Neither, ControlCodeAccess.Any),
            // FSCTL_SCRUB_DATA                    CTL_CODE(FILE_DEVICE_FILE_SYSTEM, 172, METHOD_BUFFERED, FILE_ANY_ACCESS)
            InlineData(ControlCodes.FileSystem.ScrubData, 172, ControlCodeMethod.Buffered, ControlCodeAccess.Any),
            // FSCTL_REPAIR_COPIES                 CTL_CODE(FILE_DEVICE_FILE_SYSTEM, 173, METHOD_BUFFERED, FILE_READ_DATA | FILE_WRITE_DATA)
            InlineData(ControlCodes.FileSystem.RepairCopies, 173, ControlCodeMethod.Buffered, ControlCodeAccess.ReadWrite),
            // FSCTL_DISABLE_LOCAL_BUFFERING       CTL_CODE(FILE_DEVICE_FILE_SYSTEM, 174, METHOD_BUFFERED, FILE_ANY_ACCESS)
            InlineData(ControlCodes.FileSystem.DisableLocalBuffering, 174, ControlCodeMethod.Buffered, ControlCodeAccess.Any),
            // FSCTL_CSV_MGMT_LOCK                 CTL_CODE(FILE_DEVICE_FILE_SYSTEM, 175, METHOD_BUFFERED, FILE_ANY_ACCESS)
            InlineData(ControlCodes.FileSystem.CsvManagementLock, 175, ControlCodeMethod.Buffered, ControlCodeAccess.Any),
            // FSCTL_CSV_QUERY_DOWN_LEVEL_FILE_SYSTEM_CHARACTERISTICS CTL_CODE(FILE_DEVICE_FILE_SYSTEM, 176, METHOD_BUFFERED, FILE_ANY_ACCESS)
            InlineData(ControlCodes.FileSystem.CsvQueryDownLevelFileSystemCharacteristics, 176, ControlCodeMethod.Buffered, ControlCodeAccess.Any),
            // FSCTL_ADVANCE_FILE_ID               CTL_CODE(FILE_DEVICE_FILE_SYSTEM, 177, METHOD_BUFFERED, FILE_ANY_ACCESS)
            InlineData(ControlCodes.FileSystem.AdvanceFileId, 177, ControlCodeMethod.Buffered, ControlCodeAccess.Any),
            // FSCTL_CSV_SYNC_TUNNEL_REQUEST       CTL_CODE(FILE_DEVICE_FILE_SYSTEM, 178, METHOD_BUFFERED, FILE_ANY_ACCESS)
            InlineData(ControlCodes.FileSystem.CsvSyncTunnelRequest, 178, ControlCodeMethod.Buffered, ControlCodeAccess.Any),
            // FSCTL_CSV_QUERY_VETO_FILE_DIRECT_IO CTL_CODE(FILE_DEVICE_FILE_SYSTEM, 179, METHOD_BUFFERED, FILE_ANY_ACCESS)
            InlineData(ControlCodes.FileSystem.CsvQueryVetoFileDirectIo, 179, ControlCodeMethod.Buffered, ControlCodeAccess.Any),
            // FSCTL_WRITE_USN_REASON              CTL_CODE(FILE_DEVICE_FILE_SYSTEM, 180, METHOD_BUFFERED, FILE_ANY_ACCESS)
            InlineData(ControlCodes.FileSystem.WriteUsnReason, 180, ControlCodeMethod.Buffered, ControlCodeAccess.Any),
            // FSCTL_CSV_CONTROL                   CTL_CODE(FILE_DEVICE_FILE_SYSTEM, 181, METHOD_BUFFERED, FILE_ANY_ACCESS)
            InlineData(ControlCodes.FileSystem.CsvControl, 181, ControlCodeMethod.Buffered, ControlCodeAccess.Any),
            // FSCTL_GET_REFS_VOLUME_DATA          CTL_CODE(FILE_DEVICE_FILE_SYSTEM, 182, METHOD_BUFFERED, FILE_ANY_ACCESS)
            InlineData(ControlCodes.FileSystem.GetRefsVolumeData, 182, ControlCodeMethod.Buffered, ControlCodeAccess.Any),
            // FSCTL_CSV_H_BREAKING_SYNC_TUNNEL_REQUEST CTL_CODE(FILE_DEVICE_FILE_SYSTEM, 185, METHOD_BUFFERED, FILE_ANY_ACCESS)
            InlineData(ControlCodes.FileSystem.CsvHBreakingSyncTunnelRequest, 185, ControlCodeMethod.Buffered, ControlCodeAccess.Any),
            ]
        public void ValidateFileSystemDeviceCodes(ControlCodes.FileSystem code, uint function, ControlCodeMethod method, ControlCodeAccess access)
        {
            ControlCode generatedCode = new ControlCode(ControlCodeDeviceType.FileSystem, function, method, access);
            ((ControlCode)code).Should().Be(generatedCode, $"generated code is 0x{generatedCode.Value:x8}");
        }

    }
}
