// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace WInterop.Authorization.DataTypes
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
        /// The right to delete the object.
        /// </summary>
        DELETE = 0x00010000,

        /// <summary>
        /// The right to read the information in the object's security descriptor.
        /// Doesn't include system access control list info (SACL).
        /// </summary>
        READ_CONTROL = 0x00020000,

        /// <summary>
        /// The right to modify the discretionary access control list (DACL) in the
        /// object's security descriptor.
        /// </summary>
        WRITE_DAC = 0x00040000,

        /// <summary>
        /// The right to change the owner in the object's security descriptor.
        /// </summary>
        WRITE_OWNER = 0x00080000,

        /// <summary>
        /// The right to use the object for synchronization. Enables a thread to wait
        /// until the object is in the signaled state. Some object types do not support
        /// this access right.
        /// </summary>
        SYNCHRONIZE = 0x00100000,

        // Yes, these three are all READ_CONTROL
        STANDARD_RIGHTS_READ = READ_CONTROL,
        STANDARD_RIGHTS_WRITE = READ_CONTROL,
        STANDARD_RIGHTS_EXECUTE = READ_CONTROL,

        STANDARD_RIGHTS_REQUIRED = DELETE | READ_CONTROL | WRITE_DAC | WRITE_OWNER,
        STANDARD_RIGHTS_ALL = STANDARD_RIGHTS_REQUIRED | SYNCHRONIZE,

        SPECIFIC_RIGHTS_ALL = 0x0000FFFF
    }
}
