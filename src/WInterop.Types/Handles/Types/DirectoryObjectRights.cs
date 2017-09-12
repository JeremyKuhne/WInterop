// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using WInterop.Authorization.Types;

namespace WInterop.Handles.Types
{
    // https://msdn.microsoft.com/en-us/library/bb470234.aspx
    [Flags]
    public enum DirectoryObjectRights : uint
    {
        /// <summary>
        /// Query access to the directory object.
        /// </summary>
        DIRECTORY_QUERY = 0x0001,

        /// <summary>
        /// Name-lookup access to the directory object.
        /// </summary>
        DIRECTORY_TRAVERSE = 0x0002,

        /// <summary>
        /// Name-creation access to the directory object.
        /// </summary>
        DIRECTORY_CREATE_OBJECT = 0x0004,

        /// <summary>
        /// Subdirectory-creation access to the directory object.
        /// </summary>
        DIRECTORY_CREATE_SUBDIRECTORY = 0x0008,

        DIRECTORY_ALL_ACCESS = StandardAccessRights.STANDARD_RIGHTS_REQUIRED | DIRECTORY_QUERY | DIRECTORY_TRAVERSE
            | DIRECTORY_CREATE_OBJECT | DIRECTORY_CREATE_SUBDIRECTORY,

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
        /// Maps to STANDARD_RIGHTS_READ | DIRECTORY_QUERY | DIRECTORY_TRAVERSE.
        /// </summary>
        GENERIC_READ = GenericAccessRights.Read,

        /// <summary>
        /// Maps to STANDARD_RIGHTS_WRITE | DIRECTORY_CREATE_OBJECT | DIRECTORY_CREATE_SUBDIRECTORY.
        /// </summary>
        GENERIC_WRITE = GenericAccessRights.Write,

        /// <summary>
        /// Maps to STANDARD_RIGHTS_EXECUTE | DIRECTORY_QUERY | DIRECTORY_TRAVERSE.
        /// </summary>
        GENERIC_EXECUTE = GenericAccessRights.Execute,

        /// <summary>
        /// Maps to DIRECTORY_ALL_ACCESS.
        /// </summary>
        GENERIC_ALL = GenericAccessRights.All
    }
}
