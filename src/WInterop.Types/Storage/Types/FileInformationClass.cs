// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Storage.Types
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

        /// <summary>
        /// Uses <see cref="FILE_DIRECTORY_INFORMATION"/>.
        /// </summary>
        FileDirectoryInformation = 1,

        /// <summary>
        /// Uses <see cref="FILE_FULL_DIR_INFORMATION"/>.
        /// </summary>
        FileFullDirectoryInformation = 2,

        /// <summary>
        /// Uses <see cref="FILE_BOTH_DIR_INFORMATION"/>.
        /// </summary>
        FileBothDirectoryInformation = 3,

        /// <summary>
        /// Uses <see cref="FILE_BASIC_INFORMATION"/>.
        /// </summary>
        FileBasicInformation = 4,

        /// <summary>
        /// Uses <see cref="FILE_STANDARD_INFORMATION"/>.
        /// </summary>
        FileStandardInformation = 5,

        /// <summary>
        /// Uses <see cref="FILE_INTERNAL_INFORMATION"/>.
        /// </summary>
        FileInternalInformation = 6,

        /// <summary>
        /// Uses <see cref="FILE_EA_INFORMATION"/>.
        /// </summary>
        FileEaInformation = 7,

        /// <summary>
        /// Uses <see cref="FILE_ACCESS_INFORMATION"/>.
        /// </summary>
        FileAccessInformation = 8,

        /// <summary>
        /// Uses <see cref="FILE_NAME_INFORMATION"/>.
        /// </summary>
        FileNameInformation = 9,

        /// <summary>
        /// Uses <see cref="FILE_RENAME_INFORMATION"/>.
        /// </summary>
        FileRenameInformation = 10,

        /// <summary>
        /// Uses <see cref="FILE_LINK_INFORMATION"/>.
        /// </summary>
        FileLinkInformation = 11,

        /// <summary>
        /// Used to query names of files in a directory.
        /// Uses <see cref="FILE_NAMES_INFORMATION"/>.
        /// </summary>
        FileNamesInformation = 12,

        /// <summary>
        /// Allows marking a file handle for deletion. Handle must have been opened with DesiredAccess.Delete.
        /// You cannot change the state of a handle opened with DeleteOnClose. Use with <see cref="FILE_DISPOSITION_INFORMATION"/>.
        /// </summary>
        FileDispositionInformation = 13,

        /// <summary>
        /// Uses <see cref="FILE_POSITION_INFORMATION"/>.
        /// </summary>
        FilePositionInformation = 14,

        /// <summary>
        /// Uses <see cref="FILE_FULL_EA_INFORMATION"/>.
        /// </summary>
        FileFullEaInformation = 15,

        /// <summary>
        /// Gets the file access mode (cannot set). Used with <see cref="FILE_MODE_INFORMATION"/>.
        /// </summary>
        /// <remarks>
        /// Sadly not available via GetFileInformationByHandle yet.
        /// </remarks>
        FileModeInformation = 16,

        /// <summary>
        /// Uses <see cref="FILE_ALIGNMENT_INFORMATION"/>.
        /// </summary>
        FileAlignmentInformation = 17,

        /// <summary>
        /// Used with <see cref="FILE_ALL_INFORMATION"/>.
        /// </summary>
        /// <remarks>
        /// Aggregate of FileBasicInformation, FileStandardInformation, FileInternalInformation,
        /// FileEaInformation, FilePositionInformation, & FileNameInformation.
        /// 
        /// https://msdn.microsoft.com/en-us/library/ff469436.aspx
        /// </remarks>
        FileAllInformation = 18,

        /// <summary>
        /// Used to set the allocation size for a file. Uses <see cref="FILE_ALLOCATION_INFORMATION"/>.
        /// </summary>
        /// <remarks>
        /// <see cref="https://msdn.microsoft.com/en-us/library/cc232076.aspx"/>
        /// </remarks>
        FileAllocationInformation = 19,

        /// <summary>
        /// Used to set the end of file position. Uses <see cref="FILE_END_OF_FILE_INFORMATION"/>.
        /// </summary>
        /// <remarks>
        /// <see cref="https://msdn.microsoft.com/en-us/library/cc232067.aspx"/>
        /// </remarks>
        FileEndOfFileInformation = 20,

        /// <summary>
        /// Used to query the 8.3 filename for a file. Uses <see cref="FILE_NAME_INFORMATION"/>.
        /// </summary>
        /// <remarks>
        /// <see cref="https://msdn.microsoft.com/en-us/library/cc232089.aspx"/>
        /// </remarks>
        FileAlternateNameInformation = 21,

        /// <summary>
        /// Used to get information about streams in a file. Uses <see cref="FILE_STREAM_INFORMATION"/>.
        /// </summary>
        FileStreamInformation = 22,

        /// <summary>
        /// Used to query or set information that isn't specific to one end of a pipe.
        /// Uses <see cref="FILE_PIPE_INFORMATION"/>.
        /// </summary>
        /// <remarks>
        /// <see cref="https://msdn.microsoft.com/en-us/library/cc232082.aspx"/>
        /// </remarks>
        FilePipeInformation = 23,

        /// <summary>
        /// Used to get information on a named pipe that is associated with the end of the pipe
        /// that is being queried. Uses <see cref="FILE_PIPE_LOCAL_INFORMATION"/>.
        /// </summary>
        /// <remarks>
        /// <see cref="https://msdn.microsoft.com/en-us/library/cc232083.aspx"/>
        /// </remarks>
        FilePipeLocalInformation = 24,

        /// <summary>
        /// Used to query information on a named pipe that is associated with the client end of the
        /// pipe that is being queried. Remote information is not available for local pipes or for
        /// the server end of a remote pipe. Therefore, this information class is usable only by the
        /// client to retrieve information associated with its end of the pipe.
        /// 
        /// Uses <see cref="FILE_PIPE_REMOTE_INFORMATION"/>.
        /// </summary>
        /// <remarks>
        /// <see cref="https://msdn.microsoft.com/en-us/library/cc232120.aspx"/>
        /// </remarks>
        FilePipeRemoteInformation = 25,

        /// <summary>
        /// Uses <see cref=""/>.
        /// </summary>
        FileMailslotQueryInformation = 26,

        /// <summary>
        /// Uses <see cref=""/>.
        /// </summary>
        FileMailslotSetInformation = 27,

        /// <summary>
        /// Uses <see cref=""/>.
        /// </summary>
        FileCompressionInformation = 28,

        /// <summary>
        /// Uses <see cref=""/>.
        /// </summary>
        FileObjectIdInformation = 29,

        /// <summary>
        /// Uses <see cref=""/>.
        /// </summary>
        FileCompletionInformation = 30,

        /// <summary>
        /// Uses <see cref=""/>.
        /// </summary>
        FileMoveClusterInformation = 31,

        /// <summary>
        /// Uses <see cref=""/>.
        /// </summary>
        FileQuotaInformation = 32,

        /// <summary>
        /// Returns the reparse point information in <see cref="FILE_REPARSE_POINT_INFORMATION"/>.
        /// </summary>
        FileReparsePointInformation = 33,

        /// <summary>
        /// Uses <see cref=""/>.
        /// </summary>
        FileNetworkOpenInformation = 34,

        /// <summary>
        /// Uses <see cref=""/>.
        /// </summary>
        FileAttributeTagInformation = 35,

        /// <summary>
        /// Uses <see cref=""/>.
        /// </summary>
        FileTrackingInformation = 36,

        /// <summary>
        /// Uses <see cref=""/>.
        /// </summary>
        FileIdBothDirectoryInformation = 37,

        /// <summary>
        /// Uses <see cref=""/>.
        /// </summary>
        FileIdFullDirectoryInformation = 38,

        /// <summary>
        /// Uses <see cref=""/>.
        /// </summary>
        FileValidDataLengthInformation = 39,

        /// <summary>
        /// This is used for setting, not getting the short name.
        /// </summary>
        FileShortNameInformation = 40,

        /// <summary>
        /// Uses <see cref=""/>.
        /// </summary>
        FileIoCompletionNotificationInformation = 41,

        /// <summary>
        /// Uses <see cref=""/>.
        /// </summary>
        FileIoStatusBlockRangeInformation = 42,

        /// <summary>
        /// Uses <see cref=""/>.
        /// </summary>
        FileIoPriorityHintInformation = 43,

        /// <summary>
        /// Uses <see cref=""/>.
        /// </summary>
        FileSfioReserveInformation = 44,

        /// <summary>
        /// Uses <see cref=""/>.
        /// </summary>
        FileSfioVolumeInformation = 45,

        /// <summary>
        /// Uses <see cref=""/>.
        /// </summary>
        FileHardLinkInformation = 46,

        /// <summary>
        /// Returns the process ids (other than the current process) that have an active handle
        /// to the same file. Uses <see cref="FILE_PROCESS_IDS_USING_FILE_INFORMATION"/>.
        /// </summary>
        /// <remarks>
        /// Introduced in Windows Vista.
        /// </remarks>
        FileProcessIdsUsingFileInformation = 47,

        /// <summary>
        /// Uses <see cref=""/>.
        /// </summary>
        FileNormalizedNameInformation = 48,

        /// <summary>
        /// Uses <see cref=""/>.
        /// </summary>
        FileNetworkPhysicalNameInformation = 49,

        /// <summary>
        /// Uses <see cref=""/>.
        /// </summary>
        FileIdGlobalTxDirectoryInformation = 50,

        /// <summary>
        /// Uses <see cref=""/>.
        /// </summary>
        FileIsRemoteDeviceInformation = 51,

        /// <summary>
        /// Uses <see cref=""/>.
        /// </summary>
        FileUnusedInformation = 52,

        /// <summary>
        /// Uses <see cref=""/>.
        /// </summary>
        FileNumaNodeInformation = 53,

        /// <summary>
        /// Uses <see cref=""/>.
        /// </summary>
        FileStandardLinkInformation = 54,

        /// <summary>
        /// Uses <see cref=""/>.
        /// </summary>
        FileRemoteProtocolInformation = 55,

        // The following are available starting with Windows 10

        /// <summary>
        /// Uses <see cref=""/>.
        /// </summary>
        FileRenameInformationBypassAccessCheck = 56,

        /// <summary>
        /// Uses <see cref=""/>.
        /// </summary>
        FileLinkInformationBypassAccessCheck = 57,

        /// <summary>
        /// Uses <see cref=""/>.
        /// </summary>
        FileVolumeNameInformation = 58,

        /// <summary>
        /// Uses <see cref=""/>.
        /// </summary>
        FileIdInformation = 59,

        /// <summary>
        /// Uses <see cref=""/>.
        /// </summary>
        FileIdExtdDirectoryInformation = 60,

        /// <summary>
        /// Uses <see cref=""/>.
        /// </summary>
        FileReplaceCompletionInformation = 61, // Windows 8.1

        /// <summary>
        /// Uses <see cref=""/>.
        /// </summary>
        FileHardLinkFullIdInformation = 62,

        /// <summary>
        /// Uses <see cref=""/>.
        /// </summary>
        FileIdExtdBothDirectoryInformation = 63,

        /// <summary>
        /// Uses <see cref=""/>.
        /// </summary>
        FileDispositionInformationEx = 64,

        /// <summary>
        /// Uses <see cref=""/>.
        /// </summary>
        FileRenameInformationEx = 65,

        /// <summary>
        /// Uses <see cref=""/>.
        /// </summary>
        FileRenameInformationExBypassAccessCheck = 66,

        /// <summary>
        /// Uses <see cref=""/>.
        /// </summary>
        FileDesiredStorageClassInformation = 67,

        /// <summary>
        /// Uses <see cref=""/>.
        /// </summary>
        FileStatInformation = 68

        // This is always set to the last value
        // FileMaximumInformation
    }
}
