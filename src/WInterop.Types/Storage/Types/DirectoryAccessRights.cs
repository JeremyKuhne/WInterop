// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using WInterop.Authorization;

namespace WInterop.Storage.Types
{
    // File Access Rights Constants
    // https://msdn.microsoft.com/en-us/library/windows/desktop/gg258116.aspx

    /// <summary>
    /// Directory specific access rights.
    /// </summary>
    [Flags]
    public enum DirectoryAccessRights : uint
    {
        /// <summary>
        /// The right to list the directory contents. [FILE_LIST_DIRECTORY]
        /// </summary>
        ListDirectory = 0x0001,

        /// <summary>
        /// The right to create a file in the directory. [FILE_ADD_FILE]
        /// </summary>
        AddFile = 0x0002,

        /// <summary>
        /// The right to create a subdirectory. [FILE_ADD_SUBDIRECTORY]
        /// </summary>
        AddSubdirectory = 0x0004,

        /// <summary>
        /// The right to read extended attributes. [FILE_READ_EA]
        /// </summary>
        ReadExtendedAttributes = 0x0008,

        /// <summary>
        /// The right to write extended attributes. [FILE_WRITE_EA]
        /// </summary>
        WriteExtendedAttributes = 0x0010,

        /// <summary>
        /// The right to traverse the directory. (The right to walk through the
        /// directory to the desired sub-object.) [FILE_TRAVERSE]
        /// </summary>
        /// <remarks>
        /// Normally all users are granted the BYPASS_TRAVERSE_CHECKING right.
        /// </remarks>
        Traverse = 0x0020,

        /// <summary>
        /// The right to delete the directory and all the files it contains,
        /// including read-only files. [FILE_DELETE_CHILD]
        /// </summary>
        DeleteChild = 0x0040,

        /// <summary>
        /// The right to read attributes. [FILE_READ_ATTRIBUTES]
        /// </summary>
        ReadAttributes = 0x0080,

        /// <summary>
        /// The right to write attributes. [FILE_WRITE_ATTRIBUTES]
        /// </summary>
        WriteAttributes = 0x0100,

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
        /// The right to use the object for synchronization. Enables a thread to wait
        /// until the object is in the signaled state. [SYNCHRONIZE]
        /// </summary>
        Synchronize = StandardAccessRights.Synchronize
    }
}
