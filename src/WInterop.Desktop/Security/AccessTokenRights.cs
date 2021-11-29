// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace WInterop.Security;

/// <summary>
///  Access token rights.
/// <see cref="https://msdn.microsoft.com/en-us/library/windows/desktop/aa374905.aspx"/>
/// </summary>
[Flags]
public enum AccessTokenRights : uint
{
    /// <summary>
    ///  Required to assign the primary access token for a process. Needs <see cref="Privilege.AssignPrimaryToken"/>.
    ///  [TOKEN_ASSIGN_PRIMARY]
    /// </summary>
    AssignPrimary = 0x0001,

    /// <summary>
    ///  Required to duplicate a token.
    ///  [TOKEN_DUPLICATE]
    /// </summary>
    Duplicate = 0x0002,

    /// <summary>
    ///  Required to attach an impersonation access token for a process.
    ///  [TOKEN_IMPERSONATE]
    /// </summary>
    Impersonate = 0x0004,

    /// <summary>
    ///  Required to query an access token.
    ///  [TOKEN_QUERY]
    /// </summary>
    Query = 0x0008,

    /// <summary>
    ///  Required to query the source of an access token.
    ///  [TOKEN_QUERY_SOURCE]
    /// </summary>
    QuerySource = 0x0010,

    /// <summary>
    ///  Required to enable or disable the privileges in an access token.
    ///  [TOKEN_ADJUST_PRIVILEGES]
    /// </summary>
    AdjustPrivileges = 0x0020,

    /// <summary>
    ///  Required to adjust the attributes of the groups in an access token.
    ///  [TOKEN_ADJUST_GROUPS]
    /// </summary>
    AdjustGroups = 0x0040,

    /// <summary>
    ///  Required to change the default owner, primary group, or DACL of an access token.
    ///  [TOKEN_ADJUST_DEFAULT]
    /// </summary>
    AdjustDefault = 0x0080,

    /// <summary>
    ///  Required to adjust the session ID of an access token. The <see cref="Privilege.TrustedComputerBase"/> privilege is required.
    ///  [TOKEN_ADJUST_SESSIONID]
    /// </summary>
    AdjustSessionId = 0x0100,

    /// <summary>
    ///  Special value to attempt to get all valid caller access rights.
    /// </summary>
    MaximumAllowed = 0x02000000,

    /// <summary>
    ///  All rights except AdjustSessionId.
    ///  [TOKEN_ALL_ACCESS_P]
    /// </summary>
    AllAccessP = StandardAccessRights.Required | AssignPrimary | Duplicate | Impersonate
        | Query | QuerySource | AdjustPrivileges | AdjustGroups | AdjustDefault,

    /// <summary>
    ///  All access rights.
    ///  [TOKEN_ALL_ACCESS]
    /// </summary>
    AllAccess = AllAccessP | AdjustSessionId,

    /// <summary>
    ///  Combines <see cref="StandardAccessRights.Read"/> and Query.
    ///  [TOKEN_READ]
    /// </summary>
    Read = StandardAccessRights.Read | Query,

    /// <summary>
    ///  Combines <see cref="StandardAccessRights.Write"/>, AdjustPrivileges, AdjustGroups, and AdjustDefault.
    ///  [TOKEN_WRITE]
    /// </summary>
    Write = StandardAccessRights.Write | AdjustPrivileges | AdjustGroups | AdjustDefault,

    /// <summary>
    ///  Same as <see cref="StandardAccessRights.Execute"/>, which effectively gives Impersonate.
    ///  [TOKEN_EXECUTE]
    /// </summary>
    Execute = StandardAccessRights.Execute
}