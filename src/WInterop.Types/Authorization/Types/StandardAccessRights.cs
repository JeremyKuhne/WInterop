// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace WInterop.Authorization.Types
{
    /// <summary>
    /// Standard rights that apply to most securable objects (in addition to
    /// object specific rights).
    /// </summary>
    /// <remarks>
    /// https://msdn.microsoft.com/en-us/library/windows/desktop/aa379607.aspx
    /// </remarks>
    [Flags]
    public enum StandardAccessRights : uint
    {
        /// <summary>
        /// The right to delete the object. [DELETE]
        /// </summary>
        Delete = 0x00010000,

        /// <summary>
        /// The right to read the information in the object's security descriptor.
        /// Doesn't include system access control list info (SACL). [READ_CONTROL]
        /// </summary>
        ReadControl = 0x00020000,

        /// <summary>
        /// The right to modify the discretionary access control list (DACL) in the
        /// object's security descriptor. [WRITE_DAC]
        /// </summary>
        WriteDac = 0x00040000,

        /// <summary>
        /// The right to change the owner in the object's security descriptor. [WRITE_OWNER]
        /// </summary>
        WriteOwner = 0x00080000,

        /// <summary>
        /// The right to use the object for synchronization. Enables a thread to wait
        /// until the object is in the signaled state. Some object types do not support
        /// this access right. [SYNCHRONIZE]
        /// </summary>
        Synchronize = 0x00100000,

        // Yes, these three are all READ_CONTROL

        /// <summary>
        /// [STANDARD_RIGHTS_READ]
        /// </summary>
        Read = ReadControl,

        /// <summary>
        /// [STANDARD_RIGHTS_WRITE]
        /// </summary>
        Write = ReadControl,

        /// <summary>
        /// [STANDARD_RIGHTS_EXECUTE]
        /// </summary>
        Execute = ReadControl,

        /// <summary>
        /// [STANDARD_RIGHTS_REQUIRED]
        /// </summary>
        Required = Delete | ReadControl | WriteDac | WriteOwner,

        /// <summary>
        /// [STANDARD_RIGHTS_ALL]
        /// </summary>
        All = Required | Synchronize,

        /// <summary>
        /// Rights filter for object type specific rights. [SPECIFIC_RIGHTS_ALL]
        /// </summary>
        SpecificRights = 0x0000FFFF
    }
}
