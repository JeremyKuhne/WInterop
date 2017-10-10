// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.FileManagement.Types
{
    /// <summary>
    /// Classes of information available via NtSetInformationFile and NtQueryInformationFile.
    /// [FILE_INFORMATION_CLASS]
    /// </summary>
    /// <remarks>
    /// A number of these are exposed through the GetFileInformationByHandle Win32 API.
    /// </remarks>
    public enum FileInformationClass : uint
    {
        // https://msdn.microsoft.com/en-us/library/windows/hardware/ff728840.aspx

        FileDirectoryInformation = 1,
        FileFullDirectoryInformation = 2,
        FileBothDirectoryInformation = 3,
        FileBasicInformation = 4,
        FileStandardInformation = 5,
        FileInternalInformation = 6,
        FileEaInformation = 7,
        FileAccessInformation = 8,
        FileNameInformation = 9,
        FileRenameInformation = 10,
        FileLinkInformation = 11,
        FileNamesInformation = 12,

        /// <summary>
        /// Allows marking a file handle for deletion. Handle must have been opened with DesiredAccess.Delete.
        /// You cannot change the state of a handle opened with DeleteOnClose. Use with FILE_DISPOSITION_INFORMATION.
        /// </summary>
        FileDispositionInformation = 13,
        FilePositionInformation = 14,
        FileFullEaInformation = 15,

        /// <summary>
        /// Gets the file access mode (cannot set). Used with [FILE_MODE_INFORMATION].
        /// </summary>
        /// <remarks>
        /// Sadly not available via GetFileInformationByHandle yet.
        /// </remarks>
        FileModeInformation = 16,
        FileAlignmentInformation = 17,

        /// <summary>
        /// Used with FILE_ALL_INFORMATION.
        /// </summary>
        /// <remarks>
        /// Aggregate of FileBasicInformation, FileStandardInformation, FileInternalInformation,
        /// FileEaInformation, FilePositionInformation, & FileNameInformation.
        /// 
        /// https://msdn.microsoft.com/en-us/library/ff469436.aspx
        /// </remarks>
        FileAllInformation = 18,

        FileAllocationInformation = 19,
        FileEndOfFileInformation = 20,
        FileAlternateNameInformation = 21,
        FileStreamInformation = 22,
        FilePipeInformation = 23,
        FilePipeLocalInformation = 24,
        FilePipeRemoteInformation = 25,
        FileMailslotQueryInformation = 26,
        FileMailslotSetInformation = 27,
        FileCompressionInformation = 28,
        FileObjectIdInformation = 29,
        FileCompletionInformation = 30,
        FileMoveClusterInformation = 31,
        FileQuotaInformation = 32,


        FileReparsePointInformation = 33,
        FileNetworkOpenInformation = 34,
        FileAttributeTagInformation = 35,
        FileTrackingInformation = 36,
        FileIdBothDirectoryInformation = 37,
        FileIdFullDirectoryInformation = 38,
        FileValidDataLengthInformation = 39,
        /// <summary>
        /// This is used for setting, not getting the short name
        /// </summary>
        FileShortNameInformation = 40,
        FileIoCompletionNotificationInformation = 41,
        FileIoStatusBlockRangeInformation = 42,
        FileIoPriorityHintInformation = 43,
        FileSfioReserveInformation = 44,
        FileSfioVolumeInformation = 45,
        FileHardLinkInformation = 46,
        FileProcessIdsUsingFileInformation = 47,
        FileNormalizedNameInformation = 48,
        FileNetworkPhysicalNameInformation = 49,
        FileIdGlobalTxDirectoryInformation = 50,
        FileIsRemoteDeviceInformation = 51,
        FileUnusedInformation = 52,
        FileNumaNodeInformation = 53,
        FileStandardLinkInformation = 54,
        FileRemoteProtocolInformation = 55,
        // The following are available starting with Windows 10
        FileRenameInformationBypassAccessCheck = 56,
        FileLinkInformationBypassAccessCheck = 57,
        FileVolumeNameInformation = 58,
        FileIdInformation = 59,
        FileIdExtdDirectoryInformation = 60,
        FileReplaceCompletionInformation = 61, // Windows 8.1
        FileHardLinkFullIdInformation = 62,
        FileIdExtdBothDirectoryInformation = 63,
        FileDispositionInformationEx = 64,
        FileRenameInformationEx = 65,
        FileRenameInformationExBypassAccessCheck = 66,
        FileDesiredStorageClassInformation = 67,
        FileStatInformation= 68

        // This is always set to the last value
        // FileMaximumInformation
    }
}
