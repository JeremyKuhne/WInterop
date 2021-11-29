// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using WInterop.Devices.Native;

namespace WInterop.Devices;

public static partial class ControlCodes
{
    /// <summary>
    ///  Native file system control codes.
    /// </summary>
    public enum FileSystem : uint
    {
        // Max value
        // NT 3.5 - 19
        // NT 3.5.1 - 22
        // NT 4.0 - 32
        // 2000 - 70

        // Decommissioned values
        //
        // FSCTL_SHUTDOWN_VOLUME           CTL_CODE(FILE_DEVICE_FILE_SYSTEM, 9, METHOD_BUFFERED, FILE_ANY_ACCESS)
        // FSCTL_MOUNT_DBLS_VOLUME         CTL_CODE(FILE_DEVICE_FILE_SYSTEM, 13, METHOD_BUFFERED, FILE_ANY_ACCESS)  // DoubleSpace
        // FSCTL_READ_COMPRESSION          CTL_CODE(FILE_DEVICE_FILE_SYSTEM, 17, METHOD_NEITHER,  FILE_READ_DATA)
        // FSCTL_WRITE_COMPRESSION         CTL_CODE(FILE_DEVICE_FILE_SYSTEM, 18, METHOD_NEITHER,  FILE_WRITE_DATA)
        // FSCTL_GET_HFS_INFORMATION       CTL_CODE(FILE_DEVICE_FILE_SYSTEM, 31, METHOD_BUFFERED, FILE_ANY_ACCESS)  // HFS file system
        // FSCTL_READ_PROPERTY_DATA        CTL_CODE(FILE_DEVICE_FILE_SYSTEM, 33, METHOD_NEITHER, FILE_ANY_ACCESS)   // NTFS property set storage?
        // FSCTL_WRITE_PROPERTY_DATA       CTL_CODE(FILE_DEVICE_FILE_SYSTEM, 34, METHOD_NEITHER, FILE_ANY_ACCESS)
        // 36 never shipped??
        // FSCTL_DUMP_PROPERTY_DATA        CTL_CODE(FILE_DEVICE_FILE_SYSTEM, 37,  METHOD_NEITHER, FILE_ANY_ACCESS)
        // FSCTL_HSM_MSG                   CTL_CODE(FILE_DEVICE_FILE_SYSTEM, 66, METHOD_BUFFERED, FILE_READ_DATA | FILE_WRITE_DATA)
        // FSCTL_NSS_CONTROL               CTL_CODE(FILE_DEVICE_FILE_SYSTEM, 67, METHOD_BUFFERED, FILE_WRITE_DATA)
        // FSCTL_HSM_DATA                  CTL_CODE(FILE_DEVICE_FILE_SYSTEM, 68, METHOD_NEITHER, FILE_READ_DATA | FILE_WRITE_DATA)
        // FSCTL_NSS_RCONTROL              CTL_CODE(FILE_DEVICE_FILE_SYSTEM, 70, METHOD_BUFFERED, FILE_READ_DATA)
        // 80, 83, 93, 96-98, & 105 apparently never shipped. (Part of WinFS, maybe??)

        // Opportunistic Locks
        // https://msdn.microsoft.com/en-us/library/windows/desktop/aa365433.aspx

        /// <summary>
        ///  Requests a level 1 opportunistic lock on a local file system file. [FSCTL_REQUEST_OPLOCK_LEVEL_1]
        /// </summary>
        /// <remarks>
        ///  https://msdn.microsoft.com/en-us/library/windows/desktop/aa364590.aspx
        ///  CTL_CODE(FILE_DEVICE_FILE_SYSTEM,  0, METHOD_BUFFERED, FILE_ANY_ACCESS)
        /// </remarks>
        RequestOpportunisticLockLevel1 = 0x00090000,

        /// <summary>
        ///  Requests a level 2 opportunistic lock on a local file system file. [FSCTL_REQUEST_OPLOCK_LEVEL_2]
        /// </summary>
        /// <remarks>
        ///  https://msdn.microsoft.com/en-us/library/windows/desktop/aa364591.aspx
        ///  CTL_CODE(FILE_DEVICE_FILE_SYSTEM,  1, METHOD_BUFFERED, FILE_ANY_ACCESS)
        /// </remarks>
        RequestOpportunisticLockLevel2 = 0x00090004,

        /// <summary>
        ///  Requests a batch opportunistic lock on a local file system file. [FSCTL_REQUEST_BATCH_OPLOCK]
        /// </summary>
        /// <remarks>
        ///  https://msdn.microsoft.com/en-us/library/windows/desktop/aa364588.aspx
        ///  CTL_CODE(FILE_DEVICE_FILE_SYSTEM,  2, METHOD_BUFFERED, FILE_ANY_ACCESS)
        /// </remarks>
        RequestBatchOpportunisticLock = 0x00090008,

        /// <summary>
        ///  Responds to notification that an exclusive opportunistic lock on a file is about to be broken.
        ///  [FSCTL_OPLOCK_BREAK_ACKNOWLEDGE]
        /// </summary>
        /// <remarks>
        ///  https://msdn.microsoft.com/en-us/library/windows/desktop/aa364579.aspx
        ///  CTL_CODE(FILE_DEVICE_FILE_SYSTEM,  3, METHOD_BUFFERED, FILE_ANY_ACCESS)
        /// </remarks>
        OpportunisticLockBreakAcknowledge = 0x0009000c,

        /// <summary>
        ///  Notifies a server that a client application is ready to close a file.
        ///  [FSCTL_OPBATCH_ACK_CLOSE_PENDING]
        /// </summary>
        /// <remarks>
        ///  https://msdn.microsoft.com/en-us/library/windows/desktop/aa364578.aspx
        ///  CTL_CODE(FILE_DEVICE_FILE_SYSTEM,  4, METHOD_BUFFERED, FILE_ANY_ACCESS)
        /// </remarks>
        OpportunisticBatchAcknowledgeClosePending = 0x00090010,

        /// <summary>
        ///  Enables the calling application to wait for completion of an opportunistic lock break.
        ///  Use CreateFile to do this.
        ///  [FSCTL_OPLOCK_BREAK_NOTIFY]
        /// </summary>
        /// <remarks>
        ///  https://msdn.microsoft.com/en-us/library/windows/desktop/aa364581.aspx
        ///  CTL_CODE(FILE_DEVICE_FILE_SYSTEM,  5, METHOD_BUFFERED, FILE_ANY_ACCESS)
        /// </remarks>
        OpportunisticLockBreakNotify = 0x00090014,

        /// <summary>
        ///  [FSCTL_LOCK_VOLUME]
        /// </summary>
        /// <remarks>
        ///  CTL_CODE(FILE_DEVICE_FILE_SYSTEM,  6, METHOD_BUFFERED, FILE_ANY_ACCESS)
        /// </remarks>
        LockVolume = 0x00090018,

        /// <summary>
        ///  [FSCTL_UNLOCK_VOLUME]
        /// </summary>
        /// <remarks>
        ///  CTL_CODE(FILE_DEVICE_FILE_SYSTEM,  7, METHOD_BUFFERED, FILE_ANY_ACCESS)
        /// </remarks>
        UnlockVolume = 0x0009001c,

        /// <summary>
        ///  [FSCTL_DISMOUNT_VOLUME]
        /// </summary>
        /// <remarks>
        ///  CTL_CODE(FILE_DEVICE_FILE_SYSTEM,  8, METHOD_BUFFERED, FILE_ANY_ACCESS)
        /// </remarks>
        DismountVolume = 0x00090020,

        /// <summary>
        ///  [FSCTL_IS_VOLUME_MOUNTED]
        /// </summary>
        /// <remarks>
        ///  CTL_CODE(FILE_DEVICE_FILE_SYSTEM, 10, METHOD_BUFFERED, FILE_ANY_ACCESS)
        /// </remarks>
        IsVolumeMounted = 0x00090028,

        /// <summary>
        ///  PATHNAME_BUFFER
        ///  [FSCTL_IS_PATHNAME_VALID]
        /// </summary>
        /// <remarks>
        ///  CTL_CODE(FILE_DEVICE_FILE_SYSTEM, 11, METHOD_BUFFERED, FILE_ANY_ACCESS)
        /// </remarks>
        IsPathNameValid = 0x0009002c,

        /// <summary>
        ///  [FSCTL_MARK_VOLUME_DIRTY]
        /// </summary>
        /// <remarks>
        ///  CTL_CODE(FILE_DEVICE_FILE_SYSTEM, 12, METHOD_BUFFERED, FILE_ANY_ACCESS)
        /// </remarks>
        MarkVolumeDirty = 0x00090030,

        /// <summary>
        ///  [FSCTL_QUERY_RETRIEVAL_POINTERS]
        /// </summary>
        /// <remarks>
        ///  CTL_CODE(FILE_DEVICE_FILE_SYSTEM, 14,  METHOD_NEITHER, FILE_ANY_ACCESS)
        /// </remarks>
        QueryRetrievalPointers = 0x0009003b,

        /// <summary>
        ///  [FSCTL_GET_COMPRESSION]
        /// </summary>
        /// <remarks>
        ///  CTL_CODE(FILE_DEVICE_FILE_SYSTEM, 15, METHOD_BUFFERED, FILE_ANY_ACCESS)
        /// </remarks>
        GetCompression = 0x0009003c,

        /// <summary>
        ///  [FSCTL_SET_COMPRESSION]
        /// </summary>
        /// <remarks>
        ///  CTL_CODE(FILE_DEVICE_FILE_SYSTEM, 16, METHOD_BUFFERED, FILE_READ_DATA | FILE_WRITE_DATA)
        /// </remarks>
        SetCompression = 0x0009c040,

        /// <summary>
        ///  [FSCTL_MARK_AS_SYSTEM_HIVE]
        /// </summary>
        /// <remarks>
        ///  Originally [FSCTL_SET_BOOTLOADER_ACCESSED]
        ///  CTL_CODE(FILE_DEVICE_FILE_SYSTEM, 19,  METHOD_NEITHER, FILE_ANY_ACCESS)
        /// </remarks>
        MarkAsSystemHive = 0x0009004f,

        /// <summary>
        ///  [FSCTL_OPLOCK_BREAK_ACK_NO_2]
        /// </summary>
        /// <remarks>
        ///  CTL_CODE(FILE_DEVICE_FILE_SYSTEM, 20, METHOD_BUFFERED, FILE_ANY_ACCESS)
        /// </remarks>
        OpportunisticLockBreakAcknowledge2 = 0x00090050,

        /// <summary>
        ///  [FSCTL_INVALIDATE_VOLUMES]
        /// </summary>
        /// <remarks>
        ///  CTL_CODE(FILE_DEVICE_FILE_SYSTEM, 21, METHOD_BUFFERED, FILE_ANY_ACCESS)
        /// </remarks>
        InvalidateVolumes = 0x00090054,

        /// <summary>
        ///  Returns the first 0x24 bytes of sector 0 (the BIOS parameter block) for the FAT volume
        ///  associated with the given handle. [FSCTL_QUERY_FAT_BPB]
        /// </summary>
        /// <remarks>
        ///  https://msdn.microsoft.com/en-us/library/ff469628.aspx
        ///  CTL_CODE(FILE_DEVICE_FILE_SYSTEM, 22, METHOD_BUFFERED, FILE_ANY_ACCESS)
        /// </remarks>
        QueryFatBiosParameterBlock = 0x00090058,

        /// <summary>
        ///  [FSCTL_REQUEST_FILTER_OPLOCK]
        /// </summary>
        /// <remarks>
        ///  CTL_CODE(FILE_DEVICE_FILE_SYSTEM, 23, METHOD_BUFFERED, FILE_ANY_ACCESS)
        /// </remarks>
        RequestFilterOpportunisticLock = 0x0009005c,

        /// <summary>
        ///  Uses FILESYSTEM_STATISTICS
        ///  [FSCTL_FILESYSTEM_GET_STATISTICS]
        /// </summary>
        /// <remarks>
        ///  CTL_CODE(FILE_DEVICE_FILE_SYSTEM, 24, METHOD_BUFFERED, FILE_ANY_ACCESS)
        /// </remarks>
        FileSystemGetStatistics = 0x00090060,

        /// <summary>
        ///  NTFS_VOLUME_DATA_BUFFER
        ///  [FSCTL_GET_NTFS_VOLUME_DATA]
        /// </summary>
        /// <remarks>
        ///  CTL_CODE(FILE_DEVICE_FILE_SYSTEM, 25, METHOD_BUFFERED, FILE_ANY_ACCESS)
        /// </remarks>
        GetNtfsVolumeData = 0x00090064,

        /// <summary>
        ///  NTFS_FILE_RECORD_INPUT_BUFFER, NTFS_FILE_RECORD_OUTPUT_BUFFER
        ///  [FSCTL_GET_NTFS_FILE_RECORD]
        /// </summary>
        /// <remarks>
        ///  CTL_CODE(FILE_DEVICE_FILE_SYSTEM, 26, METHOD_BUFFERED, FILE_ANY_ACCESS)
        /// </remarks>
        GetNtfsFileRecord = 0x00090068,

        /// <summary>
        ///  STARTING_LCN_INPUT_BUFFER, VOLUME_BITMAP_BUFFER
        ///  [FSCTL_GET_VOLUME_BITMAP]
        /// </summary>
        /// <remarks>
        ///  CTL_CODE(FILE_DEVICE_FILE_SYSTEM, 27,  METHOD_NEITHER, FILE_ANY_ACCESS)
        /// </remarks>
        GetVolumeBitmap = 0x0009006f,

        /// <summary>
        ///  STARTING_VCN_INPUT_BUFFER, RETRIEVAL_POINTERS_BUFFER
        ///  [FSCTL_GET_RETRIEVAL_POINTERS]
        /// </summary>
        /// <remarks>
        ///  CTL_CODE(FILE_DEVICE_FILE_SYSTEM, 28,  METHOD_NEITHER, FILE_ANY_ACCESS)
        /// </remarks>
        GetRetrievalPointers = 0x00090073,

        /// <summary>
        ///  MOVE_FILE_DATA
        ///  [FSCTL_MOVE_FILE]
        /// </summary>
        /// <remarks>
        ///  CTL_CODE(FILE_DEVICE_FILE_SYSTEM, 29, METHOD_BUFFERED, FILE_SPECIAL_ACCESS)
        /// </remarks>
        MoveFile = 0x00090074,

        /// <summary>
        ///  [FSCTL_IS_VOLUME_DIRTY]
        /// </summary>
        /// <remarks>
        ///  CTL_CODE(FILE_DEVICE_FILE_SYSTEM, 30, METHOD_BUFFERED, FILE_ANY_ACCESS)
        /// </remarks>
        IsVolumeDirty = 0x00090078,

        /// <summary>
        ///  Tells the file system to not perform I/O boundary checks.
        ///  [FSCTL_ALLOW_EXTENDED_DASD_IO]
        /// </summary>
        /// <remarks>
        ///  https://msdn.microsoft.com/en-us/library/windows/desktop/aa364556.aspx
        ///  CTL_CODE(FILE_DEVICE_FILE_SYSTEM, 32, METHOD_NEITHER, FILE_ANY_ACCESS)
        /// </remarks>
        AllowExtendedDasdIo = 0x00090083,

        /// <summary>
        ///  [FSCTL_FIND_FILES_BY_SID]
        /// </summary>
        /// <remarks>
        ///  CTL_CODE(FILE_DEVICE_FILE_SYSTEM, 35, METHOD_NEITHER, FILE_ANY_ACCESS)
        /// </remarks>
        FindFilesBySid = 0x0009008f,

        /// <summary>
        ///  FILE_OBJECTID_BUFFER
        ///  [FSCTL_SET_OBJECT_ID]
        /// </summary>
        /// <remarks>
        ///  CTL_CODE(FILE_DEVICE_FILE_SYSTEM, 38, METHOD_BUFFERED, FILE_SPECIAL_ACCESS)
        /// </remarks>
        SetObjectId = 0x00090098,

        /// <summary>
        ///  Retrieves the object identifier for the specified file or directory. FILE_OBJECTID_BUFFER
        ///  [FSCTL_GET_OBJECT_ID]
        /// </summary>
        /// <remarks>
        ///  CTL_CODE(FILE_DEVICE_FILE_SYSTEM, 39, METHOD_BUFFERED, FILE_ANY_ACCESS)
        /// </remarks>
        GetObjectId = 0x0009009c,

        /// <summary>
        ///  [FSCTL_DELETE_OBJECT_ID]
        /// </summary>
        /// <remarks>
        ///  CTL_CODE(FILE_DEVICE_FILE_SYSTEM, 40, METHOD_BUFFERED, FILE_SPECIAL_ACCESS)
        /// </remarks>
        DeleteObjectId = 0x000900a0,

        /// <summary>
        ///  REPARSE_DATA_BUFFER
        ///  [FSCTL_SET_REPARSE_POINT]
        /// </summary>
        /// <remarks>
        ///  CTL_CODE(FILE_DEVICE_FILE_SYSTEM, 41, METHOD_BUFFERED, FILE_SPECIAL_ACCESS)
        /// </remarks>
        SetReparsePoint = 0x000900a4,

        /// <summary>
        ///  Retrieves the reparse point data associated with the file or directory identified by the specified handle.
        ///  Uses <see cref="REPARSE_DATA_BUFFER"/>. [FSCTL_GET_REPARSE_POINT]
        /// </summary>
        /// <remarks>
        ///  https://msdn.microsoft.com/en-us/library/windows/desktop/aa364571.aspx
        ///  CTL_CODE(FILE_DEVICE_FILE_SYSTEM, 42, METHOD_BUFFERED, FILE_ANY_ACCESS)
        /// </remarks>
        GetReparsePoint = 0x000900a8,

        /// <summary>
        ///  REPARSE_DATA_BUFFER
        ///  [FSCTL_DELETE_REPARSE_POINT]
        /// </summary>
        /// <remarks>
        ///  CTL_CODE(FILE_DEVICE_FILE_SYSTEM, 43, METHOD_BUFFERED, FILE_SPECIAL_ACCESS)
        /// </remarks>
        DeleteReparsePoint = 0x000900ac,

        /// <summary>
        ///  MFT_ENUM_DATA
        ///  [FSCTL_ENUM_USN_DATA]
        /// </summary>
        /// <remarks>
        ///  CTL_CODE(FILE_DEVICE_FILE_SYSTEM, 44,  METHOD_NEITHER, FILE_ANY_ACCESS)
        /// </remarks>
        EnumUsnData = 0x000900b3,

        /// <summary>
        ///  BULK_SECURITY_TEST_DATA
        ///  [FSCTL_SECURITY_ID_CHECK]
        /// </summary>
        /// <remarks>
        ///  CTL_CODE(FILE_DEVICE_FILE_SYSTEM, 45,  METHOD_NEITHER, FILE_READ_DATA)
        /// </remarks>
        SecurityIdCheck = 0x000940b7,

        /// <summary>
        ///  READ_USN_JOURNAL_DATA, USN
        ///  [FSCTL_READ_USN_JOURNAL]
        /// </summary>
        /// <remarks>
        ///  CTL_CODE(FILE_DEVICE_FILE_SYSTEM, 46,  METHOD_NEITHER, FILE_ANY_ACCESS)
        /// </remarks>
        ReadUsnJournal = 0x000900bb,

        /// <summary>
        ///  [FSCTL_SET_OBJECT_ID_EXTENDED]
        /// </summary>
        /// <remarks>
        ///  CTL_CODE(FILE_DEVICE_FILE_SYSTEM, 47, METHOD_BUFFERED, FILE_SPECIAL_ACCESS)
        /// </remarks>
        SetObjectIdExtended = 0x000900bc,

        /// <summary>
        ///  FILE_OBJECTID_BUFFER
        ///  [FSCTL_CREATE_OR_GET_OBJECT_ID]
        /// </summary>
        /// <remarks>
        ///  CTL_CODE(FILE_DEVICE_FILE_SYSTEM, 48, METHOD_BUFFERED, FILE_ANY_ACCESS)
        /// </remarks>
        CreateOrGetObjectId = 0x000900c0,

        /// <summary>
        ///  [FSCTL_SET_SPARSE]
        /// </summary>
        /// <remarks>
        ///  CTL_CODE(FILE_DEVICE_FILE_SYSTEM, 49, METHOD_BUFFERED, FILE_SPECIAL_ACCESS)
        /// </remarks>
        SetSparse = 0x000900c4,

        /// <summary>
        ///  FILE_ZERO_DATA_INFORMATION
        ///  [FSCTL_SET_ZERO_DATA]
        /// </summary>
        /// <remarks>
        ///  CTL_CODE(FILE_DEVICE_FILE_SYSTEM, 50, METHOD_BUFFERED, FILE_WRITE_DATA)
        /// </remarks>
        SetZeroData = 0x000980c8,

        /// <summary>
        ///  FILE_ALLOCATED_RANGE_BUFFER, FILE_ALLOCATED_RANGE_BUFFER
        ///  [FSCTL_QUERY_ALLOCATED_RANGES]
        /// </summary>
        /// <remarks>
        ///  CTL_CODE(FILE_DEVICE_FILE_SYSTEM, 51,  METHOD_NEITHER, FILE_READ_DATA)
        /// </remarks>
        QueryAllocatedRanges = 0x000940cf,

        /// <summary>
        ///  [FSCTL_ENABLE_UPGRADE]
        /// </summary>
        /// <remarks>
        ///  CTL_CODE(FILE_DEVICE_FILE_SYSTEM, 52, METHOD_BUFFERED, FILE_WRITE_DATA)
        /// </remarks>
        EnableUpgrade = 0x000980d0,

        /// <summary>
        ///  ENCRYPTION_BUFFER, DECRYPTION_STATUS_BUFFER
        ///  [FSCTL_SET_ENCRYPTION]
        /// </summary>
        /// <remarks>
        ///  CTL_CODE(FILE_DEVICE_FILE_SYSTEM, 53,  METHOD_NEITHER, FILE_ANY_ACCESS)
        /// </remarks>
        SetEncryption = 0x000900d7,

        /// <summary>
        ///  [FSCTL_ENCRYPTION_FSCTL_IO]
        /// </summary>
        /// <remarks>
        ///  CTL_CODE(FILE_DEVICE_FILE_SYSTEM, 54,  METHOD_NEITHER, FILE_ANY_ACCESS)
        /// </remarks>
        EncryptionIo = 0x000900db,

        /// <summary>
        ///  ENCRYPTED_DATA_INFO, EXTENDED_ENCRYPTED_DATA_INFO
        ///  [FSCTL_WRITE_RAW_ENCRYPTED]
        /// </summary>
        /// <remarks>
        ///  CTL_CODE(FILE_DEVICE_FILE_SYSTEM, 55,  METHOD_NEITHER, FILE_SPECIAL_ACCESS)
        /// </remarks>
        WriteRawEncrypted = 0x000900df,

        /// <summary>
        ///  REQUEST_RAW_ENCRYPTED_DATA, ENCRYPTED_DATA_INFO, EXTENDED_ENCRYPTED_DATA_INFO
        ///  [FSCTL_READ_RAW_ENCRYPTED]
        /// </summary>
        /// <remarks>
        ///  CTL_CODE(FILE_DEVICE_FILE_SYSTEM, 56,  METHOD_NEITHER, FILE_SPECIAL_ACCESS)
        /// </remarks>
        ReadRawEncrypted = 0x000900e3,

        /// <summary>
        ///  CREATE_USN_JOURNAL_DATA
        ///  [FSCTL_CREATE_USN_JOURNAL]
        /// </summary>
        /// <remarks>
        ///  CTL_CODE(FILE_DEVICE_FILE_SYSTEM, 57,  METHOD_NEITHER, FILE_ANY_ACCESS)
        /// </remarks>
        CreateUsnJournal = 0x000900e7,

        /// <summary>
        ///  Read the Usn Record for a file. [FSCTL_READ_FILE_USN_DATA]
        /// </summary>
        /// <remarks>
        ///  CTL_CODE(FILE_DEVICE_FILE_SYSTEM, 58,  METHOD_NEITHER, FILE_ANY_ACCESS)
        /// </remarks>
        ReadFileUsnData = 0x000900eb,

        /// <summary>
        ///  Generate Close Usn Record [FSCTL_WRITE_USN_CLOSE_RECORD]
        /// </summary>
        /// <remarks>
        ///  CTL_CODE(FILE_DEVICE_FILE_SYSTEM, 59,  METHOD_NEITHER, FILE_ANY_ACCESS)
        /// </remarks>
        WriteUsnCloseRecord = 0x000900ef,

        /// <summary>
        ///  [FSCTL_EXTEND_VOLUME]
        /// </summary>
        /// <remarks>
        ///  CTL_CODE(FILE_DEVICE_FILE_SYSTEM, 60, METHOD_BUFFERED, FILE_ANY_ACCESS)
        /// </remarks>
        ExtendVolume = 0x000900f0,

        /// <summary>
        ///  [FSCTL_QUERY_USN_JOURNAL]
        /// </summary>
        /// <remarks>
        ///  CTL_CODE(FILE_DEVICE_FILE_SYSTEM, 61, METHOD_BUFFERED, FILE_ANY_ACCESS)
        /// </remarks>
        QueryUsnJournal = 0x000900f4,

        /// <summary>
        ///  [FSCTL_DELETE_USN_JOURNAL]
        /// </summary>
        /// <remarks>
        ///  CTL_CODE(FILE_DEVICE_FILE_SYSTEM, 62, METHOD_BUFFERED, FILE_ANY_ACCESS)
        /// </remarks>
        DeleteUsnJournal = 0x000900f8,

        /// <summary>
        ///  [FSCTL_MARK_HANDLE]
        /// </summary>
        /// <remarks>
        ///  CTL_CODE(FILE_DEVICE_FILE_SYSTEM, 63, METHOD_BUFFERED, FILE_ANY_ACCESS)
        /// </remarks>
        MarkHandle = 0x000900fc,

        /// <summary>
        ///  [FSCTL_SIS_COPYFILE]
        /// </summary>
        /// <remarks>
        ///  CTL_CODE(FILE_DEVICE_FILE_SYSTEM, 64, METHOD_BUFFERED, FILE_ANY_ACCESS)
        /// </remarks>
        SisCopyFile = 0x00090100,

        /// <summary>
        ///  [FSCTL_SIS_LINK_FILES]
        /// </summary>
        /// <remarks>
        ///  CTL_CODE(FILE_DEVICE_FILE_SYSTEM, 65, METHOD_BUFFERED, FILE_READ_DATA | FILE_WRITE_DATA)
        /// </remarks>
        SisLinkFiles = 0x0009c104,

        /// <summary>
        ///  [FSCTL_RECALL_FILE]
        /// </summary>
        /// <remarks>
        ///  CTL_CODE(FILE_DEVICE_FILE_SYSTEM, 69, METHOD_NEITHER, FILE_ANY_ACCESS)
        /// </remarks>
        RecallFile = 0x00090117,

        /// <summary>
        ///  [FSCTL_READ_FROM_PLEX]
        /// </summary>
        /// <remarks>
        ///  CTL_CODE(FILE_DEVICE_FILE_SYSTEM, 71, METHOD_OUT_DIRECT, FILE_READ_DATA)
        /// </remarks>
        ReadFromPlex = 0x0009411e,

        /// <summary>
        ///  FILE_PREFETCH
        ///  [FSCTL_FILE_PREFETCH]
        /// </summary>
        /// <remarks>
        ///  CTL_CODE(FILE_DEVICE_FILE_SYSTEM, 72, METHOD_BUFFERED, FILE_SPECIAL_ACCESS)
        /// </remarks>
        FilePrefetch = 0x00090120,

        // 76 - 123 added in Vista

        /// <summary>
        ///  UDFS R/W [FSCTL_MAKE_MEDIA_COMPATIBLE]
        /// </summary>
        /// <remarks>
        ///  CTL_CODE(FILE_DEVICE_FILE_SYSTEM, 76, METHOD_BUFFERED, FILE_WRITE_DATA)
        /// </remarks>
        MakeMediaCompatible = 0x00098130,

        /// <summary>
        ///  UDFS R/W [FSCTL_SET_DEFECT_MANAGEMENT]
        /// </summary>
        /// <remarks>
        ///  CTL_CODE(FILE_DEVICE_FILE_SYSTEM, 77, METHOD_BUFFERED, FILE_WRITE_DATA)
        /// </remarks>
        SetDefectManagement = 0x00098134,

        /// <summary>
        ///  UDFS R/W [FSCTL_QUERY_SPARING_INFO]
        /// </summary>
        /// <remarks>
        ///  CTL_CODE(FILE_DEVICE_FILE_SYSTEM, 78, METHOD_BUFFERED, FILE_ANY_ACCESS)
        /// </remarks>
        QuerySparingInfo = 0x00090138,

        /// <summary>
        ///  C/UDFS [FSCTL_QUERY_ON_DISK_VOLUME_INFO]
        /// </summary>
        /// <remarks>
        ///  CTL_CODE(FILE_DEVICE_FILE_SYSTEM, 79, METHOD_BUFFERED, FILE_ANY_ACCESS)
        /// </remarks>
        QueryOnDiskVolumeInfo = 0x0009013c,

        /// <summary>
        ///  VOLUME_COMPRESSION_STATE
        ///  [FSCTL_SET_VOLUME_COMPRESSION_STATE]
        /// </summary>
        /// <remarks>
        ///  CTL_CODE(FILE_DEVICE_FILE_SYSTEM, 80, METHOD_BUFFERED, FILE_SPECIAL_ACCESS)
        /// </remarks>
        SetVolumeCompressionState = 0x00090140,

        /// <summary>
        ///  [FSCTL_TXFS_MODIFY_RM]
        /// </summary>
        /// <remarks>
        ///  CTL_CODE(FILE_DEVICE_FILE_SYSTEM, 81, METHOD_BUFFERED, FILE_WRITE_DATA)
        /// </remarks>
        TxfsModifyRm = 0x00098144,

        /// <summary>
        ///  [FSCTL_TXFS_QUERY_RM_INFORMATION]
        /// </summary>
        /// <remarks>
        ///  CTL_CODE(FILE_DEVICE_FILE_SYSTEM, 82, METHOD_BUFFERED, FILE_READ_DATA)
        /// </remarks>
        TxfsQueryRmInformation = 0x00094148,

        /// <summary>
        ///  [FSCTL_TXFS_ROLLFORWARD_REDO]
        /// </summary>
        /// <remarks>
        ///  CTL_CODE(FILE_DEVICE_FILE_SYSTEM, 84, METHOD_BUFFERED, FILE_WRITE_DATA)
        /// </remarks>
        TxfsRollForwardRedo = 0x00098150,

        /// <summary>
        ///  [FSCTL_TXFS_ROLLFORWARD_UNDO]
        /// </summary>
        /// <remarks>
        ///  CTL_CODE(FILE_DEVICE_FILE_SYSTEM, 85, METHOD_BUFFERED, FILE_WRITE_DATA)
        /// </remarks>
        TxfsRollForwardUndo = 0x00098154,

        /// <summary>
        ///  [FSCTL_TXFS_START_RM]
        /// </summary>
        /// <remarks>
        ///  CTL_CODE(FILE_DEVICE_FILE_SYSTEM, 86, METHOD_BUFFERED, FILE_WRITE_DATA)
        /// </remarks>
        TxfsStartRm = 0x00098158,

        /// <summary>
        ///  [FSCTL_TXFS_SHUTDOWN_RM]
        /// </summary>
        /// <remarks>
        ///  CTL_CODE(FILE_DEVICE_FILE_SYSTEM, 87, METHOD_BUFFERED, FILE_WRITE_DATA)
        /// </remarks>
        TxfsShutdownRm = 0x0009815c,

        /// <summary>
        ///  [FSCTL_TXFS_READ_BACKUP_INFORMATION]
        /// </summary>
        /// <remarks>
        ///  CTL_CODE(FILE_DEVICE_FILE_SYSTEM, 88, METHOD_BUFFERED, FILE_READ_DATA)
        /// </remarks>
        TxfsReadBackupInformation = 0x00094160,

        /// <summary>
        ///  [FSCTL_TXFS_WRITE_BACKUP_INFORMATION]
        /// </summary>
        /// <remarks>
        ///  CTL_CODE(FILE_DEVICE_FILE_SYSTEM, 89, METHOD_BUFFERED, FILE_WRITE_DATA)
        /// </remarks>
        TxfsWriteBackupInformation = 0x00098164,

        /// <summary>
        ///  [FSCTL_TXFS_CREATE_SECONDARY_RM]
        /// </summary>
        /// <remarks>
        ///  CTL_CODE(FILE_DEVICE_FILE_SYSTEM, 90, METHOD_BUFFERED, FILE_WRITE_DATA)
        /// </remarks>
        TxfsCreateSecondaryRm = 0x00098168,

        /// <summary>
        ///  [FSCTL_TXFS_GET_METADATA_INFO]
        /// </summary>
        /// <remarks>
        ///  CTL_CODE(FILE_DEVICE_FILE_SYSTEM, 91, METHOD_BUFFERED, FILE_READ_DATA)
        /// </remarks>
        TxfsGetMetadataInfo = 0x0009416c,

        /// <summary>
        ///  [FSCTL_TXFS_GET_TRANSACTED_VERSION]
        /// </summary>
        /// <remarks>
        ///  CTL_CODE(FILE_DEVICE_FILE_SYSTEM, 92, METHOD_BUFFERED, FILE_READ_DATA)
        /// </remarks>
        TxfsGetTransactedVersion = 0x00094170,

        /// <summary>
        ///  [FSCTL_TXFS_SAVEPOINT_INFORMATION]
        /// </summary>
        /// <remarks>
        ///  CTL_CODE(FILE_DEVICE_FILE_SYSTEM, 94, METHOD_BUFFERED, FILE_WRITE_DATA)
        /// </remarks>
        TxfsSavePointInformation = 0x00098178,

        /// <summary>
        ///  [FSCTL_TXFS_CREATE_MINIVERSION]
        /// </summary>
        /// <remarks>
        ///  CTL_CODE(FILE_DEVICE_FILE_SYSTEM, 95, METHOD_BUFFERED, FILE_WRITE_DATA)
        /// </remarks>
        TxfsCreateMiniversion = 0x0009817c,

        /// <summary>
        ///  [FSCTL_TXFS_TRANSACTION_ACTIVE]
        /// </summary>
        /// <remarks>
        ///  CTL_CODE(FILE_DEVICE_FILE_SYSTEM, 99, METHOD_BUFFERED, FILE_READ_DATA)
        /// </remarks>
        TxfsTransactionActive = 0x0009418c,

        /// <summary>
        ///  [FSCTL_SET_ZERO_ON_DEALLOCATION]
        /// </summary>
        /// <remarks>
        ///  CTL_CODE(FILE_DEVICE_FILE_SYSTEM, 101, METHOD_BUFFERED, FILE_SPECIAL_ACCESS)
        /// </remarks>
        SetZeroOnDeallocation = 0x00090194,

        /// <summary>
        ///  [FSCTL_SET_REPAIR]
        /// </summary>
        /// <remarks>
        ///  CTL_CODE(FILE_DEVICE_FILE_SYSTEM, 102, METHOD_BUFFERED, FILE_ANY_ACCESS)
        /// </remarks>
        SetRepair = 0x00090198,

        /// <summary>
        ///  [FSCTL_GET_REPAIR]
        /// </summary>
        /// <remarks>
        ///  CTL_CODE(FILE_DEVICE_FILE_SYSTEM, 103, METHOD_BUFFERED, FILE_ANY_ACCESS)
        /// </remarks>
        GetRepair = 0x0009019c,

        /// <summary>
        ///  [FSCTL_WAIT_FOR_REPAIR]
        /// </summary>
        /// <remarks>
        ///  CTL_CODE(FILE_DEVICE_FILE_SYSTEM, 104, METHOD_BUFFERED, FILE_ANY_ACCESS)
        /// </remarks>
        WaitForRepair = 0x000901a0,

        /// <summary>
        ///  [FSCTL_INITIATE_REPAIR]
        /// </summary>
        /// <remarks>
        ///  CTL_CODE(FILE_DEVICE_FILE_SYSTEM, 106, METHOD_BUFFERED, FILE_ANY_ACCESS)
        /// </remarks>
        InitiateRepair = 0x000901a8,

        /// <summary>
        ///  CSC internal implementation. [FSCTL_CSC_INTERNAL]
        /// </summary>
        /// <remarks>
        ///  CTL_CODE(FILE_DEVICE_FILE_SYSTEM, 107, METHOD_NEITHER, FILE_ANY_ACCESS)
        /// </remarks>
        CscInternal = 0x000901af,

        /// <summary>
        ///  SHRINK_VOLUME_INFORMATION
        ///  [FSCTL_SHRINK_VOLUME]
        /// </summary>
        /// <remarks>
        ///  CTL_CODE(FILE_DEVICE_FILE_SYSTEM, 108, METHOD_BUFFERED, FILE_SPECIAL_ACCESS)
        /// </remarks>
        ShrinkVolume = 0x000901b0,

        /// <summary>
        ///  [FSCTL_SET_SHORT_NAME_BEHAVIOR]
        /// </summary>
        /// <remarks>
        ///  CTL_CODE(FILE_DEVICE_FILE_SYSTEM, 109, METHOD_BUFFERED, FILE_ANY_ACCESS)
        /// </remarks>
        SetShortNameBehavior = 0x000901b4,

        /// <summary>
        ///  [FSCTL_DFSR_SET_GHOST_HANDLE_STATE]
        /// </summary>
        /// <remarks>
        ///  CTL_CODE(FILE_DEVICE_FILE_SYSTEM, 110, METHOD_BUFFERED, FILE_ANY_ACCESS)
        /// </remarks>
        DfsrSetGhostHandleState = 0x000901b8,

        /// <summary>
        ///  [FSCTL_TXFS_LIST_TRANSACTION_LOCKED_FILES]
        /// </summary>
        /// <remarks>
        ///  CTL_CODE(FILE_DEVICE_FILE_SYSTEM, 120, METHOD_BUFFERED, FILE_READ_DATA)
        /// </remarks>
        TxfsListTransactionLockedFiles = 0x000941e0,

        /// <summary>
        ///  [FSCTL_TXFS_LIST_TRANSACTIONS]
        /// </summary>
        /// <remarks>
        ///  CTL_CODE(FILE_DEVICE_FILE_SYSTEM, 121, METHOD_BUFFERED, FILE_READ_DATA)
        /// </remarks>
        TxfsListTransactions = 0x000941e4,

        /// <summary>
        ///  [FSCTL_QUERY_PAGEFILE_ENCRYPTION]
        /// </summary>
        /// <remarks>
        ///  CTL_CODE(FILE_DEVICE_FILE_SYSTEM, 122, METHOD_BUFFERED, FILE_ANY_ACCESS)
        /// </remarks>
        QueryPagefileEncryption = 0x000901e8,

        /// <summary>
        ///  [FSCTL_RESET_VOLUME_ALLOCATION_HINTS]
        /// </summary>
        /// <remarks>
        ///  CTL_CODE(FILE_DEVICE_FILE_SYSTEM, 123, METHOD_BUFFERED, FILE_ANY_ACCESS)
        /// </remarks>
        ResetVolumeAllocationHints = 0x000901ec,

        /// <summary>
        ///  Dependency File System Filter
        ///  [FSCTL_QUERY_DEPENDENT_VOLUME]
        /// </summary>
        /// <remarks>
        ///  Introduced in Windows 7.
        ///  CTL_CODE(FILE_DEVICE_FILE_SYSTEM, 124, METHOD_BUFFERED, FILE_ANY_ACCESS)
        /// </remarks>
        QueryDependentVolume = 0x000901f0,

        /// <summary>
        ///  Query/Change NTFS Security Descriptors. [FSCTL_SD_GLOBAL_CHANGE]
        /// </summary>
        /// <remarks>
        ///  Introduced in Windows 7.
        ///  CTL_CODE(FILE_DEVICE_FILE_SYSTEM, 125, METHOD_BUFFERED, FILE_ANY_ACCESS)
        /// </remarks>
        SecurityDescriptorGlobalChange = 0x000901f4,

        /// <summary>
        ///  [FSCTL_TXFS_READ_BACKUP_INFORMATION2]
        /// </summary>
        /// <remarks>
        ///  Introduced in Windows Vista.
        ///  CTL_CODE(FILE_DEVICE_FILE_SYSTEM, 126, METHOD_BUFFERED, FILE_ANY_ACCESS)
        /// </remarks>
        TxfsReadBackupInformation2 = 0x000901f8,

        /// <summary>
        ///  [FSCTL_LOOKUP_STREAM_FROM_CLUSTER]
        /// </summary>
        /// <remarks>
        ///  Introduced in Windows 7.
        ///  CTL_CODE(FILE_DEVICE_FILE_SYSTEM, 127, METHOD_BUFFERED, FILE_ANY_ACCESS)
        /// </remarks>
        LookupStreamFromCluster = 0x000901fc,

        /// <summary>
        ///  [FSCTL_TXFS_WRITE_BACKUP_INFORMATION2]
        /// </summary>
        /// <remarks>
        ///  Introduced in Windows 7.
        ///  CTL_CODE(FILE_DEVICE_FILE_SYSTEM, 128, METHOD_BUFFERED, FILE_ANY_ACCESS)
        /// </remarks>
        TxfsWriteBackupInformation2 = 0x00090200,

        /// <summary>
        ///  [FSCTL_FILE_TYPE_NOTIFICATION]
        /// </summary>
        /// <remarks>
        ///  Introduced in Windows 7.
        ///  CTL_CODE(FILE_DEVICE_FILE_SYSTEM, 129, METHOD_BUFFERED, FILE_ANY_ACCESS)
        /// </remarks>
        FileTypeNotification = 0x00090204,

        /// <summary>
        ///  [FSCTL_FILE_LEVEL_TRIM]
        /// </summary>
        /// <remarks>
        ///  Introduced in Windows 8.
        ///  CTL_CODE(FILE_DEVICE_FILE_SYSTEM, 130, METHOD_BUFFERED, FILE_WRITE_DATA)
        /// </remarks>
        FileLevelTrim = 0x00098208,

        /// <summary>
        ///  BOOT_AREA_INFO
        ///  [FSCTL_GET_BOOT_AREA_INFO]
        /// </summary>
        /// <remarks>
        ///  Introduced in Windows 7.
        ///  CTL_CODE(FILE_DEVICE_FILE_SYSTEM, 140, METHOD_BUFFERED, FILE_ANY_ACCESS)
        /// </remarks>
        GetBootAreaInfo = 0x00090230,

        /// <summary>
        ///  RETRIEVAL_POINTER_BASE
        ///  [FSCTL_GET_RETRIEVAL_POINTER_BASE]
        /// </summary>
        /// <remarks>
        ///  Introduced in Windows 7.
        ///  CTL_CODE(FILE_DEVICE_FILE_SYSTEM, 141, METHOD_BUFFERED, FILE_ANY_ACCESS)
        /// </remarks>
        GetRetrievalPointerBase = 0x00090234,

        /// <summary>
        ///  Sets info in FILE_FS_PERSISTENT_VOLUME_INFORMATION.
        ///  [FSCTL_SET_PERSISTENT_VOLUME_STATE]
        /// </summary>
        /// <remarks>
        ///  Introduced in Windows 7.
        ///  CTL_CODE(FILE_DEVICE_FILE_SYSTEM, 142, METHOD_BUFFERED, FILE_ANY_ACCESS)
        /// </remarks>
        SetPersistentVolumeState = 0x00090238,

        /// <summary>
        ///  Returns info in FILE_FS_PERSISTENT_VOLUME_INFORMATION.
        ///  [FSCTL_QUERY_PERSISTENT_VOLUME_STATE]
        /// </summary>
        /// <remarks>
        ///  Introduced in Windows 7.
        ///  CTL_CODE(FILE_DEVICE_FILE_SYSTEM, 143, METHOD_BUFFERED, FILE_ANY_ACCESS)
        /// </remarks>
        QueryPersistentVolumeState = 0x0009023c,

        /// <summary>
        ///  [FSCTL_REQUEST_OPLOCK]
        /// </summary>
        /// <remarks>
        ///  Introduced in Windows 7.
        ///  CTL_CODE(FILE_DEVICE_FILE_SYSTEM, 144, METHOD_BUFFERED, FILE_ANY_ACCESS)
        /// </remarks>
        RequestOpportunisticLock = 0x00090240,

        /// <summary>
        ///  CSV_TUNNEL_REQUEST
        ///  [FSCTL_CSV_TUNNEL_REQUEST]
        /// </summary>
        /// <remarks>
        ///  Introduced in Windows 7.
        ///  CTL_CODE(FILE_DEVICE_FILE_SYSTEM, 145, METHOD_BUFFERED, FILE_ANY_ACCESS)
        /// </remarks>
        CsvTunnelRequest = 0x00090244,

        /// <summary>
        ///  IS_CSV_FILE
        ///  [FSCTL_IS_CSV_FILE]
        /// </summary>
        /// <remarks>
        ///  Introduced in Windows 7.
        ///  CTL_CODE(FILE_DEVICE_FILE_SYSTEM, 146, METHOD_BUFFERED, FILE_ANY_ACCESS)
        /// </remarks>
        IsCsvFile = 0x00090248,

        /// <summary>
        ///  [FSCTL_QUERY_FILE_SYSTEM_RECOGNITION]
        /// </summary>
        /// <remarks>
        ///  Introduced in Windows 7.
        ///  CTL_CODE(FILE_DEVICE_FILE_SYSTEM, 147, METHOD_BUFFERED, FILE_ANY_ACCESS)
        /// </remarks>
        QueryFileSystemRecognition = 0x0009024c,

        /// <summary>
        ///  [FSCTL_CSV_GET_VOLUME_PATH_NAME]
        /// </summary>
        /// <remarks>
        ///  Introduced in Windows 7.
        ///  CTL_CODE(FILE_DEVICE_FILE_SYSTEM, 148, METHOD_BUFFERED, FILE_ANY_ACCESS)
        /// </remarks>
        CsvGetVolumePathName = 0x00090250,

        /// <summary>
        ///  [FSCTL_CSV_GET_VOLUME_NAME_FOR_VOLUME_MOUNT_POINT]
        /// </summary>
        /// <remarks>
        ///  Introduced in Windows 7.
        ///  CTL_CODE(FILE_DEVICE_FILE_SYSTEM, 149, METHOD_BUFFERED, FILE_ANY_ACCESS)
        /// </remarks>
        CsvGetVolumeNameForVolumeMountPoint = 0x00090254,

        /// <summary>
        ///  [FSCTL_CSV_GET_VOLUME_PATH_NAMES_FOR_VOLUME_NAME]
        /// </summary>
        /// <remarks>
        ///  Introduced in Windows 7.
        ///  CTL_CODE(FILE_DEVICE_FILE_SYSTEM, 150,  METHOD_BUFFERED, FILE_ANY_ACCESS)
        /// </remarks>
        CsvGetVolumePathNamesForVolumeName = 0x00090258,

        /// <summary>
        ///  [FSCTL_IS_FILE_ON_CSV_VOLUME]
        /// </summary>
        /// <remarks>
        ///  Introduced in Windows 7.
        ///  CTL_CODE(FILE_DEVICE_FILE_SYSTEM, 151,  METHOD_BUFFERED, FILE_ANY_ACCESS)
        /// </remarks>
        IsFileOnCsvVolume = 0x0009025c,

        /// <summary>
        ///  [FSCTL_CORRUPTION_HANDLING]
        /// </summary>
        /// <remarks>
        ///  Introduced in Windows 8.
        ///  CTL_CODE(FILE_DEVICE_FILE_SYSTEM, 152, METHOD_BUFFERED, FILE_ANY_ACCESS)
        /// </remarks>
        CorruptionHandling = 0x00090260,

        /// <summary>
        ///  [FSCTL_OFFLOAD_READ]
        /// </summary>
        /// <remarks>
        ///  Introduced in Windows 8.
        ///  CTL_CODE(FILE_DEVICE_FILE_SYSTEM, 153, METHOD_BUFFERED, FILE_READ_ACCESS)
        /// </remarks>
        OffloadRead = 0x00094264,

        /// <summary>
        ///  [FSCTL_OFFLOAD_WRITE]
        /// </summary>
        /// <remarks>
        ///  Introduced in Windows 8.
        ///  CTL_CODE(FILE_DEVICE_FILE_SYSTEM, 154, METHOD_BUFFERED, FILE_WRITE_ACCESS)
        /// </remarks>
        OffloadWrite = 0x00098268,

        /// <summary>
        ///  [FSCTL_CSV_INTERNAL]
        /// </summary>
        /// <remarks>
        ///  Introduced in Windows 7.
        ///  CTL_CODE(FILE_DEVICE_FILE_SYSTEM, 155,  METHOD_BUFFERED, FILE_ANY_ACCESS)
        /// </remarks>
        CsvInternal = 0x0009026c,

        /// <summary>
        ///  [FSCTL_SET_PURGE_FAILURE_MODE]
        /// </summary>
        /// <remarks>
        ///  Introduced in Windows 8.
        ///  CTL_CODE(FILE_DEVICE_FILE_SYSTEM, 156, METHOD_BUFFERED, FILE_ANY_ACCESS)
        /// </remarks>
        SetPurgeFailureMode = 0x00090270,

        /// <summary>
        ///  [FSCTL_QUERY_FILE_LAYOUT]
        /// </summary>
        /// <remarks>
        ///  Introduced in Windows 8.
        ///  CTL_CODE(FILE_DEVICE_FILE_SYSTEM, 157, METHOD_NEITHER, FILE_ANY_ACCESS)
        /// </remarks>
        QueryFileLayout = 0x00090277,

        /// <summary>
        ///  [FSCTL_IS_VOLUME_OWNED_BYCSVFS]
        /// </summary>
        /// <remarks>
        ///  Introduced in Windows 8.
        ///  CTL_CODE(FILE_DEVICE_FILE_SYSTEM, 158,  METHOD_BUFFERED, FILE_ANY_ACCESS)
        /// </remarks>
        IsVolumeOwnedByCsvfs = 0x00090278,

        /// <summary>
        ///  Uses FSCTL_GET_INTEGRITY_INFORMATION_BUFFER.
        ///  [FSCTL_GET_INTEGRITY_INFORMATION]
        /// </summary>
        /// <remarks>
        ///  Introduced in Windows 8.
        ///  CTL_CODE(FILE_DEVICE_FILE_SYSTEM, 159, METHOD_BUFFERED, FILE_ANY_ACCESS)
        /// </remarks>
        GetIntegrityInformation = 0x0009027c,

        /// <summary>
        ///  Uses FSCTL_SET_INTEGRITY_INFORMATION_BUFFER.
        ///  [FSCTL_SET_INTEGRITY_INFORMATION]
        /// </summary>
        /// <remarks>
        ///  Introduced in Windows 8.
        ///  CTL_CODE(FILE_DEVICE_FILE_SYSTEM, 160, METHOD_BUFFERED, FILE_READ_DATA | FILE_WRITE_DATA)
        /// </remarks>
        SetIntegrityInformation = 0x0009c280,

        /// <summary>
        ///  [FSCTL_QUERY_FILE_REGIONS]
        /// </summary>
        /// <remarks>
        ///  Introduced in Windows 8.
        ///  CTL_CODE(FILE_DEVICE_FILE_SYSTEM, 161, METHOD_BUFFERED, FILE_ANY_ACCESS)
        /// </remarks>
        QueryFileRegions = 0x00090284,

        /// <summary>
        ///  [FSCTL_DEDUP_FILE]
        /// </summary>
        /// <remarks>
        ///  Added in Windows 8.
        ///  CTL_CODE(FILE_DEVICE_FILE_SYSTEM, 165, METHOD_BUFFERED, FILE_ANY_ACCESS)
        /// </remarks>
        DedupFile = 0x00090294,

        /// <summary>
        ///  [FSCTL_DEDUP_QUERY_FILE_HASHES]
        /// </summary>
        /// <remarks>
        ///  Added in Windows 8.
        ///  CTL_CODE(FILE_DEVICE_FILE_SYSTEM, 166, METHOD_NEITHER, FILE_READ_DATA)
        /// </remarks>
        DedupQueryFileHashes = 0x0009429b,

        /// <summary>
        ///  Resume key filter. [FSCTL_RKF_INTERNAL]
        /// </summary>
        /// <remarks>
        ///  Added in Windows 8.
        ///  CTL_CODE(FILE_DEVICE_FILE_SYSTEM, 171, METHOD_NEITHER, FILE_ANY_ACCESS)
        /// </remarks>
        ResumeKeyFilterInternal = 0x000902af,

        /// <summary>
        ///  [FSCTL_SCRUB_DATA]
        /// </summary>
        /// <remarks>
        ///  Added in Windows 8.
        ///  CTL_CODE(FILE_DEVICE_FILE_SYSTEM, 172, METHOD_BUFFERED, FILE_ANY_ACCESS)
        /// </remarks>
        ScrubData = 0x000902b0,

        /// <summary>
        ///  [FSCTL_REPAIR_COPIES]
        /// </summary>
        /// <remarks>
        ///  Added in Windows 8.
        ///  CTL_CODE(FILE_DEVICE_FILE_SYSTEM, 173, METHOD_BUFFERED, FILE_READ_DATA | FILE_WRITE_DATA)
        /// </remarks>
        RepairCopies = 0x0009c2b4,

        /// <summary>
        ///  [FSCTL_DISABLE_LOCAL_BUFFERING]
        /// </summary>
        /// <remarks>
        ///  Added in Windows 8.
        ///  CTL_CODE(FILE_DEVICE_FILE_SYSTEM, 174, METHOD_BUFFERED, FILE_ANY_ACCESS)
        /// </remarks>
        DisableLocalBuffering = 0x000902b8,

        /// <summary>
        ///  [FSCTL_CSV_MGMT_LOCK]
        /// </summary>
        /// <remarks>
        ///  Added in Windows 8.
        ///  CTL_CODE(FILE_DEVICE_FILE_SYSTEM, 175, METHOD_BUFFERED, FILE_ANY_ACCESS)
        /// </remarks>
        CsvManagementLock = 0x000902bc,

        /// <summary>
        ///  [FSCTL_CSV_QUERY_DOWN_LEVEL_FILE_SYSTEM_CHARACTERISTICS]
        /// </summary>
        /// <remarks>
        ///  Added in Windows 8.
        ///  CTL_CODE(FILE_DEVICE_FILE_SYSTEM, 176, METHOD_BUFFERED, FILE_ANY_ACCESS)
        /// </remarks>
        CsvQueryDownLevelFileSystemCharacteristics = 0x000902c0,

        /// <summary>
        ///  [FSCTL_ADVANCE_FILE_ID]
        /// </summary>
        /// <remarks>
        ///  Added in Windows 8.
        ///  CTL_CODE(FILE_DEVICE_FILE_SYSTEM, 177, METHOD_BUFFERED, FILE_ANY_ACCESS)
        /// </remarks>
        AdvanceFileId = 0x000902c4,

        /// <summary>
        ///  [FSCTL_CSV_SYNC_TUNNEL_REQUEST]
        /// </summary>
        /// <remarks>
        ///  Added in Windows 8.
        ///  CTL_CODE(FILE_DEVICE_FILE_SYSTEM, 178, METHOD_BUFFERED, FILE_ANY_ACCESS)
        /// </remarks>
        CsvSyncTunnelRequest = 0x000902c8,

        /// <summary>
        ///  [FSCTL_CSV_QUERY_VETO_FILE_DIRECT_IO]
        /// </summary>
        /// <remarks>
        ///  Added in Windows 8.
        ///  CTL_CODE(FILE_DEVICE_FILE_SYSTEM, 179, METHOD_BUFFERED, FILE_ANY_ACCESS)
        /// </remarks>
        CsvQueryVetoFileDirectIo = 0x000902cc,

        /// <summary>
        ///  [FSCTL_WRITE_USN_REASON]
        /// </summary>
        /// <remarks>
        ///  Added in Windows 8.
        ///  CTL_CODE(FILE_DEVICE_FILE_SYSTEM, 180, METHOD_BUFFERED, FILE_ANY_ACCESS)
        /// </remarks>
        WriteUsnReason = 0x000902d0,

        /// <summary>
        ///  [FSCTL_CSV_CONTROL]
        /// </summary>
        /// <remarks>
        ///  Added in Windows 8.
        ///  CTL_CODE(FILE_DEVICE_FILE_SYSTEM, 181, METHOD_BUFFERED, FILE_ANY_ACCESS)
        /// </remarks>
        CsvControl = 0x000902d4,

        /// <summary>
        ///  [FSCTL_GET_REFS_VOLUME_DATA]
        /// </summary>
        /// <remarks>
        ///  Added in Windows 8.
        ///  CTL_CODE(FILE_DEVICE_FILE_SYSTEM, 182, METHOD_BUFFERED, FILE_ANY_ACCESS)
        /// </remarks>
        GetRefsVolumeData = 0x000902d8,

        /// <summary>
        ///  [FSCTL_CSV_H_BREAKING_SYNC_TUNNEL_REQUEST]
        /// </summary>
        /// <remarks>
        ///  Added in Windows 8.
        ///  CTL_CODE(FILE_DEVICE_FILE_SYSTEM, 185, METHOD_BUFFERED, FILE_ANY_ACCESS)
        /// </remarks>
        CsvHBreakingSyncTunnelRequest = 0x000902e4
    }
}