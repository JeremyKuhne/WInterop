// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Network
{
    public enum UserPrivilege : uint
    {
        /// <summary>
        /// [USER_PRIV_GUEST]
        /// </summary>
        Guest = 0,

        /// <summary>
        /// [USER_PRIV_USER]
        /// </summary>
        User = 1,

        /// <summary>
        /// [USER_PRIV_ADMIN]
        /// </summary>
        Administrator = 2
    }
}
