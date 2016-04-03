// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.FileManagement
{
    using System;
    using WInterop.Authorization;

    /// <summary>
    /// <a href="https://msdn.microsoft.com/en-us/library/windows/desktop/gg258117.aspx">File attribute constants</a>. This is a superset of what is defined
    /// in System.IO.
    /// </summary>
    [Flags]
    public enum FileAttributes : uint
    {
        /// <summary>
        /// Not technically a defined attribute, added here for convenience.
        /// </summary>
        NONE = 0x0,

        FILE_ATTRIBUTE_READONLY = 0x00000001,
        FILE_ATTRIBUTE_HIDDEN = 0x00000002,
        FILE_ATTRIBUTE_SYSTEM = 0x00000004,
        FILE_ATTRIBUTE_DIRECTORY = 0x00000010,
        FILE_ATTRIBUTE_ARCHIVE = 0x00000020,
        FILE_ATTRIBUTE_DEVICE = 0x00000040,
        FILE_ATTRIBUTE_NORMAL = 0x00000080,
        FILE_ATTRIBUTE_TEMPORARY = 0x00000100,
        FILE_ATTRIBUTE_SPARSE_FILE = 0x00000200,
        FILE_ATTRIBUTE_REPARSE_POINT = 0x00000400,
        FILE_ATTRIBUTE_COMPRESSED = 0x00000800,
        FILE_ATTRIBUTE_OFFLINE = 0x00001000,
        FILE_ATTRIBUTE_NOT_CONTENT_INDEXED = 0x00002000,
        FILE_ATTRIBUTE_ENCRYPTED = 0x00004000,
        FILE_ATTRIBUTE_INTEGRITY_STREAM = 0x00008000,
        FILE_ATTRIBUTE_VIRTUAL = 0x00010000,
        FILE_ATTRIBUTE_NO_SCRUB_DATA = 0x00020000,
        FILE_ATTRIBUTE_EA = 0x00040000,
        FILE_FLAG_FIRST_PIPE_INSTANCE = 0x00080000,
        FILE_FLAG_OPEN_NO_RECALL = 0x00100000,
        FILE_FLAG_OPEN_REPARSE_POINT = 0x00200000,
        FILE_FLAG_SESSION_AWARE = 0x00800000,
        FILE_FLAG_POSIX_SEMANTICS = 0x01000000,
        FILE_FLAG_BACKUP_SEMANTICS = 0x02000000,
        FILE_FLAG_DELETE_ON_CLOSE = 0x04000000,
        FILE_FLAG_SEQUENTIAL_SCAN = 0x08000000,
        FILE_FLAG_RANDOM_ACCESS = 0x10000000,
        FILE_FLAG_NO_BUFFERING = 0x20000000,
        FILE_FLAG_OVERLAPPED = 0x40000000,
        FILE_FLAG_WRITE_THROUGH = 0x80000000,
        SECURITY_SQOS_PRESENT = 0x00100000,
        SECURITY_ANONYMOUS = (SECURITY_IMPERSONATION_LEVEL.SecurityAnonymous << 16),
        SECURITY_IDENTIFICATION = (SECURITY_IMPERSONATION_LEVEL.SecurityIdentification << 16),
        SECURITY_IMPERSONATION = (SECURITY_IMPERSONATION_LEVEL.SecurityImpersonation << 16),
        SECURITY_DELEGATION = (SECURITY_IMPERSONATION_LEVEL.SecurityDelegation << 16),
        SECURITY_CONTEXT_TRACKING = 0x00040000,
        SECURITY_EFFECTIVE_ONLY = 0x00080000,

        /// <summary>
        /// Be careful with this, check for invalid <i>before</i> checking other flags.
        /// </summary>
        INVALID_FILE_ATTRIBUTES = unchecked((uint)(-1))
}
}
