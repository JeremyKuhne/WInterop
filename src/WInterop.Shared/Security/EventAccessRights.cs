// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace WInterop.Security
{
    // https://msdn.microsoft.com/en-us/library/windows/desktop/ms686670.aspx
    [Flags]
    public enum EventAccessRights : uint
    {
        /// <summary>
        /// Modify state access, which is required for the SetEvent, ResetEvent and PulseEvent functions.
        /// </summary>
        EVENT_MODIFY_STATE = 0x0002,

        /// <summary>
        /// All possible rights for an event object.
        /// </summary>
        EVENT_ALL_ACCESS = StandardAccessRights.All | 0x3,

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
        SYNCHRONIZE = StandardAccessRights.Synchronize,

        /// <summary>
        /// Internally maps to KEY_READ.
        /// </summary>
        GENERIC_READ = GenericAccessRights.Read,

        /// <summary>
        /// Internally maps to KEY_WRITE.
        /// </summary>
        GENERIC_WRITE = GenericAccessRights.Write,

        /// <summary>
        /// Internally maps to KEY_EXECUTE | KEY_CREATE_LINK.
        /// </summary>
        GENERIC_EXECUTE = GenericAccessRights.Execute,

        /// <summary>
        /// Internally maps to KEY_ALL_ACCESS.
        /// </summary>
        GENERIC_ALL = GenericAccessRights.All
    }
}
