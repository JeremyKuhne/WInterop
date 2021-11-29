// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Security;

[Flags]
public enum PrivilegeAttributes : uint
{
    // Values aren't defined in MSDN, taken from winnt.h.

    /// <summary>
    ///  [SE_PRIVILEGE_ENABLED_BY_DEFAULT]
    /// </summary>
    EnabledByDefault = 0x00000001,

    /// <summary>
    ///  [SE_PRIVILEGE_ENABLED]
    /// </summary>
    Enabled = 0x00000002,

    /// <summary>
    ///  [SE_PRIVILEGE_REMOVED]
    /// </summary>
    Removed = 0x00000004,

    /// <summary>
    ///  [SE_PRIVILEGE_USED_FOR_ACCESS]
    /// </summary>
    UsedForAccess = 0x80000000
}