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
    /// File specific access rights.
    /// </summary>
    [Flags]
    public enum FileAccessRights : uint
    {
        /// <summary>
        /// The right to read data from the file.
        /// </summary>
        FILE_READ_DATA = 0x0001,

        /// <summary>
        /// The right to write data to the file.
        /// </summary>
        FILE_WRITE_DATA = 0x0002,

        /// <summary>
        /// The right to append data to the file. FILE_WRITE_DATA is needed
        /// to overwrite existing data.
        /// </summary>
        FILE_APPEND_DATA = 0x0004,

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
        FILE_EXECUTE = 0x0020,

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
        DELETE = StandardAccessRights.Delete,

        /// <summary>
        /// The right to read the information in the object's security descriptor.
        /// Doesn't include system access control list info (SACL).
        /// </summary>
        READ_CONTROL = StandardAccessRights.ReadControl,

        /// <summary>
        /// The right to modify the discretionary access control list (DACL) in the
        /// object's security descriptor.
        /// </summary>
        WRITE_DAC = StandardAccessRights.WriteDac,

        /// <summary>
        /// The right to change the owner in the object's security descriptor.
        /// </summary>
        WRITE_OWNER = StandardAccessRights.WriteOwner,

        /// <summary>
        /// The right to use the object for synchronization. Enables a thread to wait
        /// until the object is in the signaled state.
        /// </summary>
        SYNCHRONIZE = StandardAccessRights.Synchronize
    }
}
