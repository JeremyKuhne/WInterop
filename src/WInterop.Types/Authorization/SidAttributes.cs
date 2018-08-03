// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace WInterop.Authorization
{
    /// <summary>
    /// <see cref="SID_AND_ATTRIBUTES"/> attributes for group SIDs.
    /// <see cref="https://msdn.microsoft.com/en-us/library/windows/desktop/aa379624.aspx"/>
    /// </summary>
    [Flags]
    public enum SidAttributes : uint
    {
        /// <summary>
        /// The SID cannot have the Enabled attribute cleared by a call to the AdjustTokenGroups function.
        /// However, you can use the CreateRestrictedToken function to convert a mandatory SID to a deny-only SID.
        /// [SE_GROUP_MANDATORY]
        /// </summary>
        Mandatory = 0x00000001,

        /// <summary>
        /// The SID is enabled by default. [SE_GROUP_ENABLED_BY_DEFAULT]
        /// </summary>
        EnabledByDefault = 0x00000002,

        /// <summary>
        /// The SID is enabled for access checks. When the system performs an access check, it checks for
        /// access-allowed and access-denied access control entries (ACEs) that apply to the SID.
        /// [SE_GROUP_ENABLED]
        /// </summary>
        Enabled = 0x00000004,

        /// <summary>
        /// The SID identifies a group account for which the user of the token is the owner of the group,
        /// or the SID can be assigned as the owner of the token or objects.
        /// [SE_GROUP_OWNER]
        /// </summary>
        Owner = 0x00000008,

        /// <summary>
        /// The SID is a deny-only SID in a restricted token. When the system performs an access check, it
        /// checks for access-denied ACEs that apply to the SID; it ignores access-allowed ACEs for the SID.
        /// [SE_GROUP_USE_FOR_DENY_ONLY]
        /// </summary>
        UseForDenyOnly = 0x00000010,

        /// <summary>
        /// The SID is a mandatory integrity SID. [SE_GROUP_INTEGRITY]
        /// </summary>
        GroupIntegrity = 0x00000020,

        /// <summary>
        /// The SID is enabled for mandatory integrity checks. [SE_GROUP_INTEGRITY_ENABLED]
        /// </summary>
        GroupIntegrityEnabled = 0x00000040,

        /// <summary>
        /// The SID is a logon SID that identifies the logon session associated with an access token.
        /// [SE_GROUP_LOGON_ID]
        /// </summary>
        GroupLogonId = 0xC0000000,

        /// <summary>
        /// The SID identifies a domain-local group. [SE_GROUP_RESOURCE]
        /// </summary>
        GroupResource = 0x20000000
    }
}
