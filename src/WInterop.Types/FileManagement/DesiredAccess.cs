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
    /// <remarks>
    /// File Security and Access Rights
    /// https://msdn.microsoft.com/en-us/library/windows/desktop/aa364399.aspx
    /// </remarks>
    [Flags]
    public enum DesiredAccess : uint
    {
        NONE = 0,

        // File Access Rights Constants
        // https://msdn.microsoft.com/en-us/library/windows/desktop/gg258116.aspx

        /// <summary>
        /// For a file, the right to read data from the file.
        /// </summary>
        /// <remarks>
        /// Directory version of this flag is FILE_LIST_DIRECTORY.
        /// </remarks>
        FILE_READ_DATA = FileAccessRights.FILE_READ_DATA,

        /// <summary>
        /// For a directory, the right to list the contents.
        /// </summary>
        /// <remarks>
        /// File version of this flag is FILE_READ_DATA.
        /// </remarks>
        FILE_LIST_DIRECTORY = DirectoryAccessRights.FILE_LIST_DIRECTORY,

        /// <summary>
        /// For a file, the right to write data to the file.
        /// </summary>
        /// <remarks>
        /// Directory version of this flag is FILE_ADD_FILE.
        /// </remarks>
        FILE_WRITE_DATA = FileAccessRights.FILE_WRITE_DATA,

        /// <summary>
        /// For a directory, the right to create a file in a directory.
        /// </summary>
        /// <remarks>
        /// File version of this flag is FILE_WRITE_DATA.
        /// </remarks>
        FILE_ADD_FILE = DirectoryAccessRights.FILE_ADD_FILE,

        /// <summary>
        /// For a file, the right to append data to a file. FILE_WRITE_DATA is needed
        /// to overwrite existing data.
        /// </summary>
        /// <remarks>
        /// Directory version of this flag is FILE_ADD_SUBDIRECTORY.
        /// </remarks>
        FILE_APPEND_DATA = FileAccessRights.FILE_APPEND_DATA,

        /// <summary>
        /// For a directory, the right to create a subdirectory.
        /// </summary>
        /// <remarks>
        /// File version of this flag is FILE_APPEND_DATA.
        /// </remarks>
        FILE_ADD_SUBDIRECTORY = DirectoryAccessRights.FILE_ADD_SUBDIRECTORY,

        /// <summary>
        /// For a named pipe, the right to create a pipe instance.
        /// </summary>
        FILE_CREATE_PIPE_INSTANCE = 0x0004,

        /// <summary>
        /// The right to read extended attributes.
        /// </summary>
        FILE_READ_EA = 0x0008,

        /// <summary>
        /// The right to write extended attributes.
        /// </summary>
        FILE_WRITE_EA = 0x0010,

        /// <summary>
        /// The right to execute the file.
        /// </summary>
        /// <remarks>
        /// Directory version of this flag is FILE_TRAVERSE.
        /// </remarks>
        FILE_EXECUTE = FileAccessRights.FILE_EXECUTE,

        /// <summary>
        /// For a directory, the right to traverse the directory.
        /// </summary>
        /// <remarks>
        /// File version of this flag is FILE_EXECUTE.
        /// </remarks>
        FILE_TRAVERSE = DirectoryAccessRights.FILE_TRAVERSE,

        /// <summary>
        /// For a directory, the right to delete a directory and all
        /// the files it contains, including read-only files.
        /// </summary>
        FILE_DELETE_CHILD = DirectoryAccessRights.FILE_DELETE_CHILD,

        /// <summary>
        /// The right to read attributes.
        /// </summary>
        FILE_READ_ATTRIBUTES = 0x0080,

        /// <summary>
        /// The right to write attributes.
        /// </summary>
        FILE_WRITE_ATTRIBUTES = 0x0100,

        /// <summary>
        /// All standard and specific rights.
        /// </summary>
        FILE_ALL_ACCESS = StandardAccessRights.STANDARD_RIGHTS_REQUIRED | StandardAccessRights.SYNCHRONIZE | 0x1FF,

        /// <summary>
        /// The right to delete the object.
        /// </summary>
        DELETE = StandardAccessRights.DELETE,

        /// <summary>
        /// The right to read the information in the object's security descriptor.
        /// Doesn't include system access control list info (SACL).
        /// </summary>
        READ_CONTROL = StandardAccessRights.READ_CONTROL,

        /// <summary>
        /// The right to modify the discretionary access control list (DACL) in the
        /// object's security descriptor.
        /// </summary>
        WRITE_DAC = StandardAccessRights.WRITE_DAC,

        /// <summary>
        /// The right to change the owner in the object's security descriptor.
        /// </summary>
        WRITE_OWNER = StandardAccessRights.WRITE_OWNER,

        /// <summary>
        /// The right to use the object for synchronization. Enables a thread to wait
        /// until the object is in the signaled state.
        /// </summary>
        SYNCHRONIZE = StandardAccessRights.SYNCHRONIZE,

        /// <summary>
        /// Same as READ_CONTROL.
        /// </summary>
        STANDARD_RIGHTS_READ = StandardAccessRights.STANDARD_RIGHTS_READ,

        /// <summary>
        /// Same as READ_CONTROL.
        /// </summary>
        STANDARD_RIGHTS_WRITE = StandardAccessRights.STANDARD_RIGHTS_WRITE,

        /// <summary>
        /// Same as READ_CONTROL.
        /// </summary>
        STANDARD_RIGHTS_EXECUTE = READ_CONTROL,

        /// <summary>
        /// Maps internally to FILE_READ_ATTRIBUTES | FILE_READ_DATA | FILE_READ_EA | STANDARD_RIGHTS_READ | SYNCHRONIZE.
        /// (For directories, FILE_READ_ATTRIBUTES | FILE_LIST_DIRECTORY | FILE_READ_EA | STANDARD_RIGHTS_READ | SYNCHRONIZE.)
        /// </summary>
        FILE_GENERIC_READ = GenericAccessRights.GENERIC_READ,

        /// <summary>
        /// Maps internally to FILE_APPEND_DATA | FILE_WRITE_ATTRIBUTES | FILE_WRITE_DATA
        ///  | FILE_WRITE_EA | STANDARD_RIGHTS_WRITE | SYNCHRONIZE.
        /// (For directories, FILE_ADD_SUBDIRECTORY | FILE_WRITE_ATTRIBUTES | FILE_ADD_FILE
        ///  | FILE_WRITE_EA | STANDARD_RIGHTS_WRITE | SYNCHRONIZE.)
        /// </summary>
        FILE_GENERIC_WRITE = GenericAccessRights.GENERIC_WRITE,

        /// <summary>
        /// Maps internally to FILE_EXECUTE | FILE_READ_ATTRIBUTES | STANDARD_RIGHTS_EXECUTE | SYNCHRONIZE.
        /// (For directories, FILE_DELETE_CHILD | FILE_READ_ATTRIBUTES | STANDARD_RIGHTS_EXECUTE | SYNCHRONIZE.)
        /// </summary>
        FILE_GENERIC_EXECUTE = GenericAccessRights.GENERIC_EXECUTE,

        /// <summary>
        /// For convenience.
        /// </summary>
        FILE_GENERIC_READWRITE = FILE_GENERIC_READ | FILE_GENERIC_WRITE
    }
}
