// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace WInterop.Com
{
    /// <summary>
    ///  Storage commit options. [STGC]
    /// <see cref="https://msdn.microsoft.com/en-us/library/windows/desktop/aa380320.aspx"/>
    /// </summary>
    [Flags]
    public enum StorageCommit : uint
    {
        /// <summary>
        ///  Default options. [STGC_DEFAULT]
        /// </summary>
        Default = 0,

        /// <summary>
        ///  The commit operation can overwrite existing data to reduce overall space requirements.
        ///  Not as robust. [STGC_OVERWRITE]
        /// </summary>
        Overwrite = 1,

        /// <summary>
        ///  Commit operation occurs only if there have been no changes to the saved storage object
        ///  by other users. [STGC_ONLYIFCURRENT]
        /// </summary>
        OnlyIfCurrent = 2,

        /// <summary>
        ///  Writes to the disk cache without explicitly saving the cache.
        ///  [STGC_DANGEROUSLYCOMMITMERELYTODISKCACHE]
        /// </summary>
        DangerouslyCommitMerelyToDiskCache = 4,

        /// <summary>
        ///  Valid for root storage object opened in transacted mode. Consolidates storage after
        ///  commit. [STGC_CONSOLIDATE]
        /// </summary>
        Consolodate = 8
    }
}
