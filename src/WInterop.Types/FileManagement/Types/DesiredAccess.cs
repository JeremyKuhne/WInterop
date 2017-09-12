// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using WInterop.Authorization.Types;

namespace WInterop.FileManagement.Types
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
        // File Access Rights Constants
        // https://msdn.microsoft.com/en-us/library/windows/desktop/gg258116.aspx

        /// <summary>
        /// For a file, the right to read data from the file. (FILE_READ_DATA)
        /// </summary>
        /// <remarks>
        /// Directory version of this flag is FILE_LIST_DIRECTORY.
        /// </remarks>
        ReadData = FileAccessRights.FILE_READ_DATA,

        /// <summary>
        /// For a directory, the right to list the contents. (FILE_LIST_DIRECTORY)
        /// </summary>
        /// <remarks>
        /// File version of this flag is FILE_READ_DATA.
        /// </remarks>
        ListDirectory = DirectoryAccessRights.FILE_LIST_DIRECTORY,

        /// <summary>
        /// For a file, the right to write data to the file. (FILE_WRITE_DATA)
        /// </summary>
        /// <remarks>
        /// Directory version of this flag is FILE_ADD_FILE.
        /// </remarks>
        WriteData = FileAccessRights.FILE_WRITE_DATA,

        /// <summary>
        /// For a directory, the right to create a file in a directory. (FILE_ADD_FILE)
        /// </summary>
        /// <remarks>
        /// File version of this flag is FILE_WRITE_DATA.
        /// </remarks>
        AddFile = DirectoryAccessRights.FILE_ADD_FILE,

        /// <summary>
        /// For a file, the right to append data to a file. FILE_WRITE_DATA is needed
        /// to overwrite existing data. (FILE_APPEND_DATA)
        /// </summary>
        /// <remarks>
        /// Directory version of this flag is FILE_ADD_SUBDIRECTORY.
        /// </remarks>
        AppendData = FileAccessRights.FILE_APPEND_DATA,

        /// <summary>
        /// For a directory, the right to create a subdirectory. (FILE_ADD_SUBDIRECTORY)
        /// </summary>
        /// <remarks>
        /// File version of this flag is FILE_APPEND_DATA.
        /// </remarks>
        AddSubdirectory = DirectoryAccessRights.FILE_ADD_SUBDIRECTORY,

        /// <summary>
        /// For a named pipe, the right to create a pipe instance. (FILE_CREATE_PIPE_INSTANCE)
        /// </summary>
        CreatePipeInstance = 0x0004,

        /// <summary>
        /// The right to read extended attributes. (FILE_READ_EA)
        /// </summary>
        ReadExtendedAttributes = 0x0008,

        /// <summary>
        /// The right to write extended attributes. (FILE_WRITE_EA)
        /// </summary>
        WriteExtendedAttributes = 0x0010,

        /// <summary>
        /// The right to execute the file. (FILE_EXECUTE)
        /// </summary>
        /// <remarks>
        /// Directory version of this flag is FILE_TRAVERSE.
        /// </remarks>
        Execute = FileAccessRights.FILE_EXECUTE,

        /// <summary>
        /// For a directory, the right to traverse the directory. (FILE_TRAVERSE)
        /// </summary>
        /// <remarks>
        /// File version of this flag is FILE_EXECUTE.
        /// </remarks>
        Traverse = DirectoryAccessRights.FILE_TRAVERSE,

        /// <summary>
        /// For a directory, the right to delete a directory and all
        /// the files it contains, including read-only files. (FILE_DELETE_CHILD)
        /// </summary>
        DeleteChild = DirectoryAccessRights.FILE_DELETE_CHILD,

        /// <summary>
        /// The right to read attributes. (FILE_READ_ATTRIBUTES)
        /// </summary>
        ReadAttributes = 0x0080,

        /// <summary>
        /// The right to write attributes. (FILE_WRITE_ATTRIBUTES)
        /// </summary>
        WriteAttributes = 0x0100,

        /// <summary>
        /// All standard and specific rights. (FILE_ALL_ACCESS)
        /// </summary>
        AllAccess = StandardAccessRights.STANDARD_RIGHTS_REQUIRED | StandardAccessRights.SYNCHRONIZE | 0x1FF,

        /// <summary>
        /// The right to delete the object. (DELETE)
        /// </summary>
        Delete = StandardAccessRights.DELETE,

        /// <summary>
        /// The right to read the information in the object's security descriptor.
        /// Doesn't include system access control list info (SACL). (READ_CONTROL)
        /// </summary>
        ReadControl = StandardAccessRights.READ_CONTROL,

        /// <summary>
        /// The right to modify the discretionary access control list (DACL) in the
        /// object's security descriptor. (WRITE_DAC)
        /// </summary>
        WriteDac = StandardAccessRights.WRITE_DAC,

        /// <summary>
        /// The right to change the owner in the object's security descriptor. (WRITE_OWNER)
        /// </summary>
        WriteOwner = StandardAccessRights.WRITE_OWNER,

        /// <summary>
        /// The right to use the object for synchronization. Enables a thread to wait until the object
        /// is in the signaled state. This is required if opening a synchronous handle.(SYNCHRONIZE)
        /// </summary>
        Synchronize = StandardAccessRights.SYNCHRONIZE,

        /// <summary>
        /// Same as READ_CONTROL.
        /// </summary>
        StandardRightsRead = StandardAccessRights.STANDARD_RIGHTS_READ,

        /// <summary>
        /// STANDARD_RIGHTS_WRITE. Same as READ_CONTROL.
        /// </summary>
        StandardRightsWrite = StandardAccessRights.STANDARD_RIGHTS_WRITE,

        /// <summary>
        /// STANDARD_RIGHTS_EXECUTE. Same as READ_CONTROL.
        /// </summary>
        StandardRightsExecute = ReadControl,

        /// <summary>
        /// FILE_GENERIC_READ. Maps internally to FILE_READ_ATTRIBUTES | FILE_READ_DATA | FILE_READ_EA | STANDARD_RIGHTS_READ | SYNCHRONIZE.
        /// (For directories, FILE_READ_ATTRIBUTES | FILE_LIST_DIRECTORY | FILE_READ_EA | STANDARD_RIGHTS_READ | SYNCHRONIZE.)
        /// </summary>
        GenericRead = GenericAccessRights.Read,

        /// <summary>
        /// FILE_GENERIC_WRITE. Maps internally to FILE_APPEND_DATA | FILE_WRITE_ATTRIBUTES | FILE_WRITE_DATA
        ///  | FILE_WRITE_EA | STANDARD_RIGHTS_WRITE | SYNCHRONIZE.
        /// (For directories, FILE_ADD_SUBDIRECTORY | FILE_WRITE_ATTRIBUTES | FILE_ADD_FILE
        ///  | FILE_WRITE_EA | STANDARD_RIGHTS_WRITE | SYNCHRONIZE.)
        /// </summary>
        GenericWrite = GenericAccessRights.Write,

        /// <summary>
        /// FILE_GENERIC_EXECUTE. Maps internally to FILE_EXECUTE | FILE_READ_ATTRIBUTES | STANDARD_RIGHTS_EXECUTE | SYNCHRONIZE.
        /// (For directories, FILE_DELETE_CHILD | FILE_READ_ATTRIBUTES | STANDARD_RIGHTS_EXECUTE | SYNCHRONIZE.)
        /// </summary>
        GenericExecute = GenericAccessRights.Execute,

        /// <summary>
        /// Not technically part of the SDK, for convenience.
        /// </summary>
        GenericReadWrite = GenericRead | GenericWrite
    }
}
