// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using WInterop.Authorization.Types;

namespace WInterop.File.Types
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
        /// For a file, the right to read data from the file. [FILE_READ_DATA]
        /// </summary>
        /// <remarks>
        /// Directory version of this flag is FILE_LIST_DIRECTORY.
        /// </remarks>
        ReadData = FileAccessRights.ReadData,

        /// <summary>
        /// For a directory, the right to list the contents. [FILE_LIST_DIRECTORY]
        /// </summary>
        /// <remarks>
        /// File version of this flag is FILE_READ_DATA.
        /// </remarks>
        ListDirectory = DirectoryAccessRights.ListDirectory,

        /// <summary>
        /// For a file, the right to write data to the file. [FILE_WRITE_DATA]
        /// </summary>
        /// <remarks>
        /// Directory version of this flag is FILE_ADD_FILE.
        /// </remarks>
        WriteData = FileAccessRights.WriteData,

        /// <summary>
        /// For a directory, the right to create a file in a directory. [FILE_ADD_FILE]
        /// </summary>
        /// <remarks>
        /// File version of this flag is FILE_WRITE_DATA.
        /// </remarks>
        AddFile = DirectoryAccessRights.AddFile,

        /// <summary>
        /// For a file, the right to append data to a file. FILE_WRITE_DATA is needed
        /// to overwrite existing data. [FILE_APPEND_DATA]
        /// </summary>
        /// <remarks>
        /// Directory version of this flag is FILE_ADD_SUBDIRECTORY.
        /// </remarks>
        AppendData = FileAccessRights.AppendData,

        /// <summary>
        /// For a directory, the right to create a subdirectory. [FILE_ADD_SUBDIRECTORY]
        /// </summary>
        /// <remarks>
        /// File version of this flag is FILE_APPEND_DATA.
        /// </remarks>
        AddSubdirectory = DirectoryAccessRights.AddSubdirectory,

        /// <summary>
        /// For a named pipe, the right to create a pipe instance. [FILE_CREATE_PIPE_INSTANCE]
        /// </summary>
        CreatePipeInstance = 0x0004,

        /// <summary>
        /// The right to read extended attributes. [FILE_READ_EA]
        /// </summary>
        ReadExtendedAttributes = 0x0008,

        /// <summary>
        /// The right to write extended attributes. [FILE_WRITE_EA]
        /// </summary>
        WriteExtendedAttributes = 0x0010,

        /// <summary>
        /// The right to execute the file. [FILE_EXECUTE]
        /// </summary>
        /// <remarks>
        /// Directory version of this flag is FILE_TRAVERSE.
        /// </remarks>
        Execute = FileAccessRights.Execute,

        /// <summary>
        /// For a directory, the right to traverse the directory. [FILE_TRAVERSE]
        /// </summary>
        /// <remarks>
        /// File version of this flag is FILE_EXECUTE.
        /// </remarks>
        Traverse = DirectoryAccessRights.Traverse,

        /// <summary>
        /// For a directory, the right to delete a directory and all
        /// the files it contains, including read-only files. [FILE_DELETE_CHILD]
        /// </summary>
        DeleteChild = DirectoryAccessRights.DeleteChild,

        /// <summary>
        /// The right to read attributes. [FILE_READ_ATTRIBUTES]
        /// </summary>
        ReadAttributes = 0x0080,

        /// <summary>
        /// The right to write attributes. [FILE_WRITE_ATTRIBUTES]
        /// </summary>
        WriteAttributes = 0x0100,

        /// <summary>
        /// All standard and specific rights. [FILE_ALL_ACCESS]
        /// </summary>
        AllAccess = StandardAccessRights.Required | StandardAccessRights.Synchronize | 0x1FF,

        /// <summary>
        /// The right to delete the object. [DELETE]
        /// </summary>
        Delete = StandardAccessRights.Delete,

        /// <summary>
        /// The right to read the information in the object's security descriptor.
        /// Doesn't include system access control list info (SACL). [READ_CONTROL]
        /// </summary>
        ReadControl = StandardAccessRights.ReadControl,

        /// <summary>
        /// The right to modify the discretionary access control list (DACL) in the
        /// object's security descriptor. [WRITE_DAC]
        /// </summary>
        WriteDac = StandardAccessRights.WriteDac,

        /// <summary>
        /// The right to change the owner in the object's security descriptor. [WRITE_OWNER]
        /// </summary>
        WriteOwner = StandardAccessRights.WriteOwner,

        /// <summary>
        /// The right to use the object for synchronization. Enables a thread to wait until the object
        /// is in the signaled state. This is required if opening a synchronous handle. [SYNCHRONIZE]
        /// </summary>
        Synchronize = StandardAccessRights.Synchronize,

        /// <summary>
        /// Same as ReadControl. [STANDARD_RIGHTS_READ]
        /// </summary>
        StandardRightsRead = StandardAccessRights.Read,

        /// <summary>
        /// Same as ReadControl. [STANDARD_RIGHTS_WRITE]
        /// </summary>
        StandardRightsWrite = StandardAccessRights.Write,

        /// <summary>
        /// Same as ReadControl. [STANDARD_RIGHTS_EXECUTE]
        /// </summary>
        StandardRightsExecute = StandardAccessRights.Execute,

        /// <summary>
        /// Maps internally to ReadAttributes | ReadData | ReadExtendedAttributes | StandardRightsRead | Synchronize.
        /// (For directories, ReadAttributes | ListDirectory | ReadExtendedAttributes | StandardRightsRead | Synchronize.)
        /// [FILE_GENERIC_READ]
        /// </summary>
        GenericRead = GenericAccessRights.Read,

        /// <summary>
        /// Maps internally to AppendData | WriteAttributes | WriteData | WriteExtendedAttributes | StandardRightsWrite | Synchronize.
        /// (For directories, AddSubdirectory | WriteAttributes | AddFile | WriteExtendedAttributes | StandardRightsWrite | Synchronize.)
        /// [FILE_GENERIC_WRITE]
        /// </summary>
        GenericWrite = GenericAccessRights.Write,

        /// <summary>
        /// Maps internally to Execute | ReadAttributes | StandardRightsExecute | Synchronize.
        /// (For directories, DeleteChild | ReadAttributes | StandardRightsExecute | Synchronize.)
        /// [FILE_GENERIC_EXECUTE]
        /// </summary>
        GenericExecute = GenericAccessRights.Execute,

        /// <summary>
        /// Not technically part of the SDK, for convenience.
        /// </summary>
        GenericReadWrite = GenericRead | GenericWrite
    }
}
