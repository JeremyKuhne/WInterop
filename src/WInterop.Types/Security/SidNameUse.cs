// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Security
{
    /// <summary>
    /// Defines the type of sid. [SID_NAME_USE]
    /// </summary>
    /// <remarks>
    /// <see cref="https://msdn.microsoft.com/en-us/library/windows/desktop/aa379601.aspx"/>
    /// </remarks>
    public enum SidNameUse
    {
        /// <summary>
        /// A user SID. [SidTypeUser]
        /// </summary>
        User = 1,

        /// <summary>
        /// A group SID. [SidTypeGroup]
        /// </summary>
        Group,

        /// <summary>
        /// A domain SID. [SidTypeDomain]
        /// </summary>
        Domain,

        /// <summary>
        /// An alias SID. [SidTypeAlias]
        /// </summary>
        Alias,

        /// <summary>
        /// A well known group SID. [SidTypeWellKnownGroup]
        /// </summary>
        WellKnownGroup,

        /// <summary>
        /// A deleted account SID. [SidTypeDeletedAccount]
        /// </summary>
        DeletedAccount,

        /// <summary>
        /// Invalid SID. [SidTypeInvalid]
        /// </summary>
        Invalid,

        /// <summary>
        /// Unknown SID type. [SidTypeUnknown]
        /// </summary>
        Unknown,

        /// <summary>
        /// A computer SID. [SidTypeComputer]
        /// </summary>
        Computer,

        /// <summary>
        /// Mandatory integrity label SID. [SidTypeLabel]
        /// </summary>
        Label
    }
}
