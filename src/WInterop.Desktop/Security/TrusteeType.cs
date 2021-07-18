// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Security
{
    /// <summary>
    ///  [TRUSTEE_TYPE]
    /// </summary>
    public enum TrusteeType
    {
        /// <summary>
        ///  [TRUSTEE_IS_UNKNOWN]
        /// </summary>
        Unknown,

        /// <summary>
        ///  [TRUSTEE_IS_USER]
        /// </summary>
        User,

        /// <summary>
        ///  [TRUSTEE_IS_GROUP]
        /// </summary>
        Group,

        /// <summary>
        ///  [TRUSTEE_IS_DOMAIN]
        /// </summary>
        Domain,

        /// <summary>
        ///  [TRUSTEE_IS_ALIAS]
        /// </summary>
        Alias,

        /// <summary>
        ///  [TRUSTEE_IS_WELL_KNOWN_GROUP]
        /// </summary>
        WellKnownGroup,

        /// <summary>
        ///  Deleted account. [TRUSTEE_IS_DELETED]
        /// </summary>
        Deleted,

        /// <summary>
        ///  [TRUSTEE_IS_INVALID]
        /// </summary>
        Invalid,

        /// <summary>
        ///  [TRUSTEE_IS_COMPUTER]
        /// </summary>
        Computer
    }
}