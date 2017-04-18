// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace WInterop.FileManagement.DataTypes
{
    [Flags]
    public enum ReplaceFileFlags : uint
    {
        /// <summary>
        /// Not supported.
        /// </summary>
        REPLACEFILE_WRITE_THROUGH = 0x00000001,

        /// <summary>
        /// Ignores errors while merging metadata (such as attributes and ACLs)
        /// </summary>
        REPLACEFILE_IGNORE_MERGE_ERRORS = 0x00000002,

        /// <summary>
        /// Ignores errors while merging ACL information
        /// </summary>
        REPLACEFILE_IGNORE_ACL_ERRORS = 0x00000004
    }
}
