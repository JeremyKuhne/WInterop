// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Storage
{
    /// <summary>
    /// File information type for GetFileInformationByHandle() [FILE_INFO_BY_HANDLE_CLASS]
    /// </summary>
    public enum FileInfoClass : uint
    {
        // https://msdn.microsoft.com/en-us/library/windows/desktop/aa364228.aspx
        // Note that some OS support is gleaned from documentation & headers for the structures.

        /// <summary>
        /// Returns basic information (timestamps and attributes) for a file in FILE_BASIC_INFO.
        /// </summary>
        /// <remarks>
        /// Thunks to NtQueryInformationFile & FileBasicInformation. Windows 7 and up.
        /// </remarks>
        FileBasicInfo,

        /// <summary>
        /// Returns file size, link count, pending delete status, and if it is a directory in FILE_STANDARD_INFO.
        /// </summary>
        /// <remarks>
        /// Thunks to NtQueryInformationFile & FileStandardInformation. Windows 7 and up.
        /// </remarks>
        FileStandardInfo,

        /// <summary>
        /// Gets the file name in FILE_NAME_INFO.
        /// </summary>
        /// <remarks>
        /// Thunks to NtQueryInformationFile & FileNameInformation. Windows 7 and up.
        /// </remarks>
        FileNameInfo,

        /// <summary>
        /// Allows renaming a file without having to specify a full path, if you have
        /// a handle to the directory the file resides in.
        /// </summary>
        /// <remarks>
        /// Only valid for SetFileInformationByHandle. Thunks to NtSetInformationFile.
        /// Windows 7 and up. MoveFileEx is effectively the same API.
        /// </remarks>
        FileRenameInfo,

        /// <summary>
        /// Allows marking a file handle for deletion. Handle must have been opened with DesiredAccess.Delete.
        /// You cannot change the state of a handle opened with DeleteOnClose.
        /// </summary>
        /// <remarks>
        /// Only valid for SetFileInformationByHandle. Thunks to NtSetInformationFile.
        /// Windows 7 and up. DeleteFile is effectively the same API.
        /// </remarks>
        FileDispositionInfo,

        /// <summary>
        /// Allows setting the allocated size of the file.
        /// </summary>
        /// <remarks>
        /// Only valid for SetFileInformationByHandle. Thunks to NtSetInformationFile. Windows 7 and up.
        /// SetEndOfFile sets this after setting the logical end of file to the current position via FileEndOfFileInfo.
        /// </remarks>
        FileAllocationInfo,

        /// <summary>
        /// Allows setting the end of file.
        /// </summary>
        /// <remarks>
        /// Only valid for SetFileInformationByHandle. Thunks to NtSetInformationFile. Windows 7 and up.
        /// SetEndOfFile calls this to set the logical end of file to whatever the current position is.
        /// </remarks>
        FileEndOfFileInfo,

        /// <summary>
        /// Gets stream information for the file.
        /// </summary>
        /// <remarks>
        /// Thunks to NtQueryInformationFile & FileStreamInformation. Windows 7 and up.
        /// </remarks>
        FileStreamInfo,

        /// <summary>
        /// Gets compression information for the file.
        /// </summary>
        /// <remarks>
        /// Thunks to NtQueryInformationFile & FileCompressionInformation. Windows 7 and up.
        /// </remarks>
        FileCompressionInfo,

        /// <summary>
        /// Gets the file attributes and reparse tag.
        /// </summary>
        /// <remarks>
        /// Thunks to NtQueryInformationFile & FileAttributeTagInformation. Windows 7 and up.
        /// </remarks>
        FileAttributeTagInfo,

        /// <summary>
        /// Starts a query for for file information in a directory.
        /// </summary>
        /// <remarks>
        /// Thunks to NtQueryDirectoryFile & FileIdBothDirectoryInformation with RestartScan
        /// set to false. Windows 7 and up.
        /// </remarks>
        FileIdBothDirectoryInfo,

        /// <summary>
        /// Resumes a query for file information in a directory.
        /// </summary>
        /// <remarks>
        /// Thunks to NtQueryDirectoryFile & FileIdBothDirectoryInformation with RestartScan
        /// set to true. Windows 7 and up.
        /// </remarks>
        FileIdBothDirectoryRestartInfo,

        /// <summary>
        /// Allows setting the priority hint for a file.
        /// </summary>
        /// <remarks>
        /// Only valid for SetFileInformationByHandle. Thunks to NtSetInformationFile.
        /// Windows 7 and up.
        /// </remarks>
        FileIoPriorityHintInfo,

        /// <summary>
        /// Gets the file remote protocol information.
        /// </summary>
        /// <remarks>
        /// Thunks to NtQueryInformationFile & FileRemoteProtocolInformation. Windows 7 and up.
        /// </remarks>
        FileRemoteProtocolInfo,

        /// <summary>
        /// Starts a query for for file information in a directory.
        /// </summary>
        /// <remarks>
        /// Thunks to NtQueryDirectoryFile & FileFullDirectoryInformation with RestartScan
        /// set to false. Windows 8 and up.
        /// </remarks>
        FileFullDirectoryInfo,

        /// <summary>
        /// Resumes a query for file information in a directory.
        /// </summary>
        /// <remarks>
        /// Thunks to NtQueryDirectoryFile & FileFullDirectoryInformation with RestartScan
        /// set to true. Windows 8 and up.
        /// </remarks>
        FileFullDirectoryRestartInfo,

        /// <summary>
        /// Gets detailed information about how data is stored on the containing volume.
        /// </summary>
        /// <remarks>
        /// Thunks to NtQueryVolumeInformationFile & FileFsSectorSizeInformation. Windows 8 and up.
        /// </remarks>
        FileStorageInfo,

        /// <summary>
        /// Gets the alignment information for the file.
        /// </summary>
        /// <remarks>
        /// Thunks to NtQueryInformationFile & FileAlignmentInformation. Windows 8 and up.
        /// </remarks>
        FileAlignmentInfo,

        /// <summary>
        /// Gets the volume serial number and file id for the file.
        /// </summary>
        /// <remarks>
        /// Thunks to NtQueryInformationFile & FileIdInformation. Windows 8 and up.
        /// </remarks>
        FileIdInfo,

        /// <summary>
        /// 
        /// </summary>
        /// <remarks>
        /// Thunks to NtQueryDirectoryFile & FileIdExtdDirectoryInformation with RestartScan
        /// set to false. Windows 8 and up.
        /// </remarks>
        FileIdExtdDirectoryInfo,

        /// <summary>
        /// 
        /// </summary>
        /// <remarks>
        /// Thunks to NtQueryDirectoryFile & FileIdExtdDirectoryInformation with RestartScan
        /// set to true. Windows 8 and up.
        /// </remarks>
        FileIdExtdDirectoryRestartInfo,

        /// <summary>
        /// 
        /// </summary>
        /// <remarks>
        /// Only valid for SetFileInformationByHandle. Thunks to NtSetInformationFile.
        /// Windows 10 Anniversary Update (RS1) and up.
        /// </remarks>
        FileDispositionInfoEx,

        /// <summary>
        /// Allows renaming with POSIX semantics.
        /// </summary>
        /// <remarks>
        /// Replaces the BOOLEAN in FILE_RENAME_INFORMATION with a union of BOOLEAN and uint flags.
        /// Only valid for SetFileInformationByHandle. Thunks to NtSetInformationFile.
        /// Windows 10 Anniversary Update (RS1) and up.
        /// </remarks>
        FileRenameInfoEx

        // Always follows the final enum value.
        // MaximumFileInfoByHandlesClass
    }
}
