﻿// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Storage
{
    /// <summary>
    /// <a href="https://msdn.microsoft.com/en-us/library/windows/desktop/aa364415.aspx">FINDEX_INFO_LEVELS</a>.
    /// </summary>
    public enum FindExtendedInfoLevels : uint
    {
        /// <summary>
        ///  [FindExInfoStandard]
        /// </summary>
        Standard = 0,

        /// <summary>
        ///  FindExInfoBasic
        /// </summary>
        Basic = 1
    }
}