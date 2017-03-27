// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using WInterop.Authorization.DataTypes;

namespace WInterop.FileManagement.DataTypes
{
    /// <summary>
    /// System.IO.FileAccess looks up these values when creating handles
    /// </summary>
    [Flags]
    public enum DesiredAccess : uint
    {
        NONE = 0,

        DELETE = AccessTypes.DELETE,

        // File Security and Access Rights
        // https://msdn.microsoft.com/en-us/library/windows/desktop/aa364399.aspx

        // https://msdn.microsoft.com/en-us/library/windows/desktop/aa446632.aspx
        GENERIC_READ = AccessTypes.GENERIC_READ,
        GENERIC_WRITE = AccessTypes.GENERIC_WRITE,
        GENERIC_EXECUTE = AccessTypes.GENERIC_EXECUTE,
        GENERIC_READWRITE = GENERIC_READ | GENERIC_WRITE,

        // File Access Rights Constants
        // https://msdn.microsoft.com/en-us/library/windows/desktop/gg258116.aspx

        FILE_READ_DATA = 0x0001,            // file & pipe
        FILE_LIST_DIRECTORY = 0x0001,       // directory
        FILE_WRITE_DATA = 0x0002,           // file & pipe
        FILE_ADD_FILE = 0x0002,             // directory
        FILE_APPEND_DATA = 0x0004,          // file
        FILE_ADD_SUBDIRECTORY = 0x0004,     // directory
        FILE_CREATE_PIPE_INSTANCE = 0x0004, // named pipe
        FILE_READ_EA = 0x0008,              // file & directory
        FILE_WRITE_EA = 0x0010,             // file & directory
        FILE_EXECUTE = 0x0020,              // file
        FILE_TRAVERSE = 0x0020,             // directory
        FILE_DELETE_CHILD = 0x0040,         // directory
        FILE_READ_ATTRIBUTES = 0x0080,      // all
        FILE_WRITE_ATTRIBUTES = 0x0100,     // all
        FILE_ALL_ACCESS = AccessTypes.STANDARD_RIGHTS_REQUIRED | AccessTypes.SYNCHRONIZE | 0x1FF,
        FILE_GENERIC_READ = AccessTypes.STANDARD_RIGHTS_READ | FILE_READ_DATA | FILE_READ_ATTRIBUTES | FILE_READ_EA | AccessTypes.SYNCHRONIZE,
        FILE_GENERIC_WRITE = AccessTypes.STANDARD_RIGHTS_WRITE | FILE_WRITE_DATA | FILE_WRITE_ATTRIBUTES | FILE_WRITE_EA | FILE_APPEND_DATA | AccessTypes.SYNCHRONIZE,
        FILE_GENERIC_READWRITE = FILE_GENERIC_READ | FILE_GENERIC_WRITE,
        FILE_GENERIC_EXECUTE = AccessTypes.STANDARD_RIGHTS_EXECUTE | FILE_READ_ATTRIBUTES | FILE_EXECUTE | AccessTypes.SYNCHRONIZE
    }
}
