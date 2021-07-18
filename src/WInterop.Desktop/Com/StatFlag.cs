// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using WInterop.Com.Native;

namespace WInterop.Com
{
    /// <summary>
    ///  Stat flags for <see cref="IStorage.Stat(out STATSTG, StatFlag))"/>.
    /// </summary>
    /// <docs>https://docs.microsoft.com/windows/win32/api/wtypes/ne-wtypes-statflag</docs>
    public enum StatFlag : uint
    {
        /// <summary>
        ///  Stat includes the name. [STATFLAG_DEFAULT]
        /// </summary>
        Default = 0,

        /// <summary>
        ///  Stat doesn't include the name. [STATFLAG_NONAME]
        /// </summary>
        NoName = 1,

        /// <summary>
        ///  Not implemented. [STATFLAG_NOOPEN]
        /// </summary>
        NoOpen = 2
    }
}