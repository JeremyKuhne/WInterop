// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Com.Types
{
    /// <summary>
    /// Stat flags for <see cref="IStorage.Stat(out STATSTG, StatFlag)"/>.
    /// <see cref="https://msdn.microsoft.com/en-us/library/windows/desktop/aa380316.aspx"/>
    /// </summary>
    public enum StatFlag : uint
    {
        /// <summary>
        /// Stat includes the name. [STATFLAG_DEFAULT]
        /// </summary>
        Default = 0,

        /// <summary>
        /// Stat doesn't include the name. [STATFLAG_NONAME]
        /// </summary>
        NoName = 1,

        /// <summary>
        /// Not implemented. [STATFLAG_NOOPEN]
        /// </summary>
        NoOpen = 2
    }
}
