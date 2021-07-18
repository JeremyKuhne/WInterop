// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Security
{
    /// <native>[ACCESS_MODE]</native>
    /// <docs>https://docs.microsoft.com/en-us/windows/desktop/api/accctrl/ne-accctrl-_access_mode</docs>
    public enum AccessMode
    {
        /// <summary>
        ///  Value isn't used. [NOT_USED_ACCESS]
        /// </summary>
        NotUsed,

        /// <summary>
        ///  Combines specified rights with existing. [GRANT_ACCESS]
        /// </summary>
        Grant,

        /// <summary>
        ///  Sets specified rights, replacing all existing. [SET_ACCESS]
        /// </summary>
        Set,

        /// <summary>
        ///  Denies specified rights. [DENY_ACCESS]
        /// </summary>
        Deny,

        /// <summary>
        ///  Removes all rights (other than denied). [REVOKE_ACCESS]
        /// </summary>
        Revoke,

        /// <summary>
        ///  [SET_AUDIT_SUCCESS]
        /// </summary>
        AuditSuccess,

        /// <summary>
        ///  [SET_AUDIT_FAILURE]
        /// </summary>
        AuditFailure
    }
}