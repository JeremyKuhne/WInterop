// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Storage
{
    /// <summary>
    /// Options for creating/opening files with NtCreateFile.
    /// </summary>
    public enum CreateOptions : uint
    {
        /// <summary>
        /// File being created or opened must be a directory file. Disposition must be <see cref="CreateDisposition.Create"/>,
        /// <see cref="CreateDisposition.Open"/>, or <see cref="CreateDisposition.OpenIf"/>.
        /// [FILE_DIRECTORY_FILE]
        /// </summary>
        /// <remarks>
        /// Can only be used with <see cref="SynchronousIoAlert"/>, <see cref="SynchronousIoNonalert"/>, <see cref="WriteThrough"/>,
        /// <see cref="OpenForBackupIntent"/>, and <see cref="FileOpenByFileId"/> flags.
        /// </remarks>
        DirectoryFile = 0x00000001,

        /// <summary>
        /// Applications that write data to the file must actually transfer the data into
        /// the file before any requested write operation is considered complete. This flag
        /// is set automatically if <see cref="NoIntermediateBuffering"/> is set.
        /// [FILE_WRITE_THROUGH]
        /// </summary>
        WriteThrough = 0x00000002,

        /// <summary>
        /// All accesses to the file are sequential. [FILE_SEQUENTIAL_ONLY]
        /// </summary>
        SequentialOnly = 0x00000004,

        /// <summary>
        /// File cannot be cached in driver buffers. Cannot use with <see cref="DesiredAccess.AppendData"/>.
        /// [FILE_NO_INTERMEDIATE_BUFFERING]
        /// </summary>
        NoIntermediateBuffering = 0x00000008,

        /// <summary>
        /// All operations are performed synchronously. Any wait on behalf of the caller is
        /// subject to premature termination from alerts. [FILE_SYNCHRONOUS_IO_ALERT]
        /// </summary>
        /// <remarks>
        /// Cannot be used with <see cref="SynchronousIoNonalert"/>.
        /// Synchronous DesiredAccess flag is required. I/O system will maintain file position context.
        /// </remarks>
        SynchronousIoAlert = 0x00000010,

        /// <summary>
        /// All operations are performed synchronously. Waits in the system to synchronize I/O queuing
        /// and completion are not subject to alerts. [FILE_SYNCHRONOUS_IO_NONALERT]
        /// </summary>
        /// <remarks>
        /// Cannot be used with <see cref="SynchronousIoAlert"/>.
        /// Requires <see cref="DesiredAccess.Synchronize"/>. I/O system will maintain file position context.
        /// </remarks>
        SynchronousIoNonalert = 0x00000020,

        /// <summary>
        /// File being created or opened must not be a directory file. Can be a data file, device,
        /// or volume. [FILE_NON_DIRECTORY_FILE]
        /// </summary>
        NonDirectoryFile = 0x00000040,

        /// <summary>
        /// Create a tree connection for this file in order to open it over the network.
        /// [FILE_CREATE_TREE_CONNECTION]
        /// </summary>
        /// <remarks>
        /// Not used by device and intermediate drivers.
        /// </remarks>
        CreateTreeConnection = 0x00000080,

        /// <summary>
        /// Complete the operation immediately with a success code of STATUS_OPLOCK_BREAK_IN_PROGRESS if
        /// the target file is oplocked. [FILE_COMPLETE_IF_OPLOCKED]
        /// </summary>
        /// <remarks>
        /// Not compatible with <see cref="ReserveOpfilter"/> or <see cref="OpenRequiringOplock"/>.
        /// Not used by device and intermediate drivers.
        /// </remarks>
        CompleteIfOplocked = 0x00000100,

        /// <summary>
        /// If the extended attributes on an existing file being opened indicate that the caller must
        /// understand extended attributes to properly interpret the file, fail the request.
        /// [FILE_NO_EA_KNOWLEDGE]
        /// </summary>
        /// <remarks>
        /// Not used by device and intermediate drivers.
        /// </remarks>
        NoEaKnowledge = 0x00000200,

        /// <summary>
        /// [FILE_OPEN_REMOTE_INSTANCE]
        /// </summary>
        OpenRemoteInstance = 0x00000400,

        /// <summary>
        /// Accesses to the file can be random, so no sequential read-ahead operations should be performed
        /// on the file by FSDs or the system. [FILE_RANDOM_ACCESS]
        /// </summary>
        RandomAccess = 0x00000800,

        /// <summary>
        /// Delete the file when the last handle to it is passed to NtClose. Requires <see cref="DesiredAccess.Delete"/>.
        /// [FILE_DELETE_ON_CLOSE]
        /// </summary>
        DeleteOnClose = 0x00001000,

        /// <summary>
        /// Open the file by reference number or object ID. The file name that is specified by the ObjectAttributes
        /// name parameter includes the 8 or 16 byte file reference number or ID for the file in the ObjectAttributes
        /// name field. The device name can optionally be prefixed. [FILE_OPEN_BY_FILE_ID]
        /// </summary>
        /// <remarks>
        /// NTFS supports both reference numbers and object IDs. 16 byte reference numbers are 8 byte numbers padded
        /// with zeros. ReFS only supports reference numbers (not object IDs). 8 byte and 16 byte reference numbers
        /// are not related. Note that as the UNICODE_STRING will contain raw byte data, it may not be a "valid" string.
        /// Not used by device and intermediate drivers.
        /// </remarks>
        /// <example>
        /// \??\C:\{8 bytes of binary FileID}
        /// \device\HardDiskVolume1\{16 bytes of binary ObjectID}
        /// {8 bytes of binary FileID}
        /// </example>
        OpenByFileId = 0x00002000,

        /// <summary>
        /// The file is being opened for backup intent. Therefore, the system should check for certain access rights
        /// and grant the caller the appropriate access to the file before checking the DesiredAccess parameter
        /// against the file's security descriptor. [FILE_OPEN_FOR_BACKUP_INTENT]
        /// </summary>
        /// <remarks>
        /// Not used by device and intermediate drivers.
        /// </remarks>
        OpenForBackupIntent = 0x00004000,

        /// <summary>
        /// When creating a file, specifies that it should not inherit the compression bit from the parent directory.
        /// [FILE_NO_COMPRESSION]
        /// </summary>
        NoCompression = 0x00008000,

        /// <summary>
        /// The file is being opened and an opportunistic lock (oplock) on the file is being requested as a single atomic
        /// operation. [FILE_OPEN_REQUIRING_OPLOCK]
        /// </summary>
        /// <remarks>
        /// The file system checks for oplocks before it performs the create operation and will fail the create with a
        /// return code of STATUS_CANNOT_BREAK_OPLOCK if the result would be to break an existing oplock.
        /// Not compatible with CompleteIfOplocked or ReserveOpFilter. Windows 7 and up.
        /// </remarks>
        OpenRequiringOplock = 0x00010000,

        /// <summary>
        /// CreateFile2 uses this flag to prevent opening a file that you don't have access to without specifying
        /// <see cref="ShareModes.Read"/>. (Preventing users that can only read a file from denying access to other readers.)
        /// [FILE_DISALLOW_EXCLUSIVE]
        /// </summary>
        /// <remarks>
        /// Windows 7 and up.
        /// </remarks>
        DisallowExclusive = 0x00020000,

        /// <summary>
        /// The client opening the file or device is session aware and per session access is validated if necessary.
        /// [FILE_SESSION_AWARE]
        /// </summary>
        /// <remarks>
        /// Windows 8 and up.
        /// </remarks>
        SessionAware = 0x00040000,

        /// <summary>
        /// This flag allows an application to request a filter opportunistic lock (oplock) to prevent other applications
        /// from getting share violations. [FILE_RESERVE_OPFILTER]
        /// </summary>
        /// <remarks>
        /// Not compatible with <see cref="CompleteIfOplocked"/> or <see cref="OpenRequiringOplock"/>.
        /// If there are already open handles, the create request will fail with STATUS_OPLOCK_NOT_GRANTED.
        /// </remarks>
        ReserveOpfilter = 0x00100000,

        /// <summary>
        /// Open a file with a reparse point attribute, bypassing the normal reparse point processing.
        /// [FILE_OPEN_REPARSE_POINT]
        /// </summary>
        OpenReparsePoint = 0x00200000,

        /// <summary>
        /// Causes files that are marked with the <see cref="AllFileAttributes.Offline"/> attribute are not to be recalled
        /// from remote storage. [FILE_OPEN_NO_RECALL]
        /// </summary>
        /// <remarks>
        /// More details can be found in Remote Storage documentation (see Basic Concepts).
        /// https://technet.microsoft.com/en-us/library/cc938459.aspx
        /// </remarks>
        OpenNoRecall = 0x00400000,

        /// <summary>
        /// [FILE_OPEN_FOR_FREE_SPACE_QUERY]
        /// </summary>
        OpenForFreeSpaceQuery = 0x00800000
    }
}
