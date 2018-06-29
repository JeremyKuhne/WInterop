// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace WInterop.FileManagement.Types
{
    [Flags]
    public enum FileFlags : uint
    {
        /// <summary>
        /// Not technically a defined attribute, added here for convenience.
        /// </summary>
        None = 0x0,

        /// <summary>
        /// Requests an atomic opportunistic lock and file open operation.
        /// 
        /// Valid for CreateFile2 only, CreateFile combines security QoS flags with file flags-
        /// this is a QoS flag value in CreateFile.
        /// 
        /// [FILE_FLAG_OPEN_REQUIRING_OPLOCK]
        /// </summary>
        /// <remarks>
        /// Flag details (CREATEFILE2_EXTENDED_PARAMETERS):
        /// https://msdn.microsoft.com/en-us/library/windows/desktop/hh449426.aspx
        /// 
        /// Documentation on Opportunistic Locks:
        /// https://msdn.microsoft.com/en-us/library/windows/desktop/aa365433.aspx
        /// 
        /// This flag is functionally equivalent to NtCreateFile's FILE_OPEN_REQUIRING_OPLOCK.
        /// </remarks>
        OpenRequiringOpLock = 0x00040000,

        /// <summary>
        /// If you attempt to create multiple instances of a pipe with this flag, creation of
        /// the first instance succeeds, but creation of the next instance fails with
        /// ERROR_ACCESS_DENIED. [FILE_FLAG_FIRST_PIPE_INSTANCE]
        /// </summary>
        FirstPipeInstance = 0x00080000,

        /// <summary>
        /// Gets file state without changing the file's location. (Doesn't make a remote
        /// file local) [FILE_FLAG_OPEN_NO_RECALL]
        /// </summary>
        /// <remarks>
        /// This flag is functionally equivalent to NtCreateFile's FILE_OPEN_NO_RECALL.
        /// </remarks>
        OpenNoRecall = 0x00100000,

        /// <summary>
        /// Opens the actual reparse point rather than reparsing. [FILE_FLAG_OPEN_REPARSE_POINT]
        /// </summary>
        /// <remarks>
        /// Reparse points:
        /// https://msdn.microsoft.com/en-us/library/windows/desktop/aa365503.aspx
        /// 
        /// This flag is functionally equivalent to NtCreateFile's FILE_OPEN_REPARSE_POINT.
        /// </remarks>
        OpenReparsePoint = 0x00200000,

        /// <remarks>
        /// This flag is functionally equivalent to NtCreateFile's FILE_SESSION_AWARE.
        /// [FILE_FLAG_SESSION_AWARE]
        /// </remarks>
        SessionAware = 0x00800000,

        /// <summary>
        /// Allows accessing via POSIX semantics, notably not ignoring case.
        /// [FILE_FLAG_POSIX_SEMANTICS]
        /// </summary>
        PosixSemantics = 0x01000000,

        /// <summary>
        /// [FILE_FLAG_BACKUP_SEMANTICS]
        /// </summary>
        /// <remarks>
        /// If this isn't set the call to NtCreateFile will include FILE_NON_DIRECTORY_FILE.
        /// 
        /// This flag is functionally equivalent to NtCreateFile's FILE_OPEN_FOR_BACKUP_INTENT.
        /// </remarks>
        BackupSemantics = 0x02000000,

        /// <summary>
        /// Flags the file to be deleted when the final handle is closed. Setting
        /// this flag will implicitly set DELETE for DesiredAccess. [FILE_FLAG_DELETE_ON_CLOSE]
        /// </summary>
        /// <remarks>
        /// This flag is functionally equivalent to NtCreateFile's FILE_DELETE_ON_CLOSE.
        /// </remarks>
        DeleteOnClose = 0x04000000,

        /// <summary>
        /// Hints that the access for a file will be mostly sequential to optimize caching behavior.
        /// [FILE_FLAG_SEQUENTIAL_SCAN]
        /// </summary>
        /// <remarks>
        /// This flag is functionally equivalent to NtCreateFile's FILE_SEQUENTIAL_ONLY.
        /// </remarks>
        SequentialScan = 0x08000000,

        /// <summary>
        /// Hints that the access for a file will be mostly random to optimize caching behavior.
        /// Notably that the system shouldn't read ahead in anticipation of further reads.
        /// [FILE_FLAG_RANDOM_ACCESS]
        /// </summary>
        /// <remarks>
        /// This flag is functionally equivalent to NtCreateFile's FILE_RANDOM_ACCESS.
        /// </remarks>
        RandomAccess = 0x10000000,

        /// <summary>
        /// Causes data writes to skip the system cache and go straight to disk.
        /// [FILE_FLAG_NO_BUFFERING]
        /// </summary>
        /// <remarks>
        /// Using this flag requires reading and writing aligned on physical sectors
        /// and in sector sized chunks.
        /// https://msdn.microsoft.com/en-us/library/windows/desktop/cc644950.aspx
        /// 
        /// File metadata is always cached, so it must be flushed either explicitly
        /// or through the FILE_FLAG_WRITE_THROUGH flag.
        /// 
        /// This flag is functionally equivalent to NtCreateFile's FILE_NO_INTERMEDIATE_BUFFERING.
        /// </remarks>
        NoBuffering = 0x20000000,

        /// <summary>
        /// Indicates that operations should potentially be asynchronous. If data is pending
        /// Read/WriteFile will return ERROR_IO_PENDING and will signal the event specified
        /// in the OVERLAPPED structure when complete. [FILE_FLAG_OVERLAPPED]
        /// </summary>
        /// <remarks>
        /// In this state the system will not maintain a file pointer, you must pass the
        /// offset in the OVERLAPPED structure.
        /// 
        /// When this flag is *not* set, the file mode FILE_SYNCHRONOUS_IO_NONALERT is set,
        /// which is the file handle object flag FO_SYNCHRONOUS_IO without FO_ALERTABLE_IO.
        /// FILE_SYNCHRONOUS_IO_ALERT is FO_SYNCHRONOUS_IO with FO_ALTERTABLE_IO.
        /// 
        /// So when neither FILE_SYNCHRONOUS_IO_NONALERT or FILE_SYNCHRONOUS_IO_ALERT is set
        /// the object is overlapped (e.g. async is the "default" state).
        /// 
        /// This flag is functionally the inverse of NtCreateFile's FILE_SYNCHRONOUS_IO_NONALERT.
        /// </remarks>
        Overlapped = 0x40000000,

        /// <summary>
        /// Causes writes to be immediately flushed to disk (e.g. FlushFileBuffers)
        /// https://msdn.microsoft.com/en-us/library/windows/desktop/aa364218.aspx
        /// [FILE_FLAG_WRITE_THROUGH]
        /// </summary>
        /// <remarks>
        /// This flag is functionally equivalent to NtCreateFile's FILE_WRITE_THROUGH.
        /// </remarks>
        WriteThrough = 0x80000000
    }
}
