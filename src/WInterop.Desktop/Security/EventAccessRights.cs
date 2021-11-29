// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Security;

// https://msdn.microsoft.com/en-us/library/windows/desktop/ms686670.aspx
[Flags]
public enum EventAccessRights : uint
{
    /// <summary>
    ///  Modify state access, which is required for the SetEvent, ResetEvent and PulseEvent functions.
    ///  [EVENT_MODIFY_STATE]
    /// </summary>
    ModifyState = 0x0002,

    /// <summary>
    ///  All possible rights for an event object. [EVENT_ALL_ACCESS]
    /// </summary>
    AllAccess = StandardAccessRights.All | 0x3,

    /// <summary>
    ///  The right to delete the object. [DELETE]
    /// </summary>
    Delete = StandardAccessRights.Delete,

    /// <summary>
    ///  The right to read the information in the object's security descriptor.
    ///  Doesn't include system access control list info (SACL). [READ_CONTROL]
    /// </summary>
    ReadControl = StandardAccessRights.ReadControl,

    /// <summary>
    ///  The right to modify the discretionary access control list (DACL) in the
    ///  object's security descriptor. [WRITE_DAC]
    /// </summary>
    WriteDac = StandardAccessRights.WriteDac,

    /// <summary>
    ///  The right to change the owner in the object's security descriptor. [WRITE_OWNER]
    /// </summary>
    WriteOwner = StandardAccessRights.WriteOwner,

    /// <summary>
    ///  The right to use the object for synchronization. Enables a thread to wait
    ///  until the object is in the signaled state.
    /// </summary>
    Synchronize = StandardAccessRights.Synchronize,

    /// <summary>
    ///  Internally maps to KEY_READ.
    /// </summary>
    GenericRead = GenericAccessRights.Read,

    /// <summary>
    ///  Internally maps to KEY_WRITE.
    /// </summary>
    GenericWrite = GenericAccessRights.Write,

    /// <summary>
    ///  Internally maps to KEY_EXECUTE | KEY_CREATE_LINK.
    /// </summary>
    GenericExecute = GenericAccessRights.Execute,

    /// <summary>
    ///  Internally maps to KEY_ALL_ACCESS.
    /// </summary>
    GenericAll = GenericAccessRights.All
}