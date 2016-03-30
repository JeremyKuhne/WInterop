// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Authorization
{
    using System;

    [Flags]
    public enum PrivilegeState : uint
    {
        EnabledByDefault = NativeMethods.Authorization.SE_PRIVILEGE_ENABLED_BY_DEFAULT,
        Enabled = NativeMethods.Authorization.SE_PRIVILEGE_ENABLED,
        Removed = NativeMethods.Authorization.SE_PRIVILEGE_REMOVED,
        UsedForAccess = NativeMethods.Authorization.SE_PRIVILEGE_USED_FOR_ACCESS
    }
}
