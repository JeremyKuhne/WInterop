// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using WInterop.Authorization.Types;

namespace WInterop.FileManagement.Types
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
        /// The right to list the directory contents.
        /// </summary>
        FILE_LIST_DIRECTORY = 0x0001,

        /// <summary>
        /// The right to create a file in the directory.
        /// </summary>
        FILE_ADD_FILE = 0x0002,

        /// <summary>
        /// The right to create a subdirectory.
        /// </summary>
        FILE_ADD_SUBDIRECTORY = 0x0004,

        /// <summary>
        /// The right to read extended attributes.
        /// </summary>
        FILE_READ_EA = 0x0008,

        /// <summary>
        /// The right to write extended attributes.
        /// </summary>
        FILE_WRITE_EA = 0x0010,

        /// <summary>
        /// The right to traverse the directory. (The right to walk through the
        /// directory to the desired sub-object.)
        /// </summary>
        /// <remarks>
        /// Normally all users are granted the BYPASS_TRAVERSE_CHECKING right.
        /// </remarks>
        FILE_TRAVERSE = 0x0020,

        /// <summary>
        /// The right to delete the directory and all the files it contains,
        /// including read-only files.
        /// </summary>
        FILE_DELETE_CHILD = 0x0040,

        /// <summary>
        /// The right to read attributes.
        /// </summary>
        FILE_READ_ATTRIBUTES = 0x0080,

        /// <summary>
        /// The right to write attributes.
        /// </summary>
        FILE_WRITE_ATTRIBUTES = 0x0100,

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
        SYNCHRONIZE = StandardAccessRights.SYNCHRONIZE
    }
}
