// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace WInterop.Storage
{
    [Flags]
    public enum ReplaceFileFlags : uint
    {
        /// <summary>
        /// Not supported. [REPLACEFILE_WRITE_THROUGH]
        /// </summary>
        WriteThrough = 0x00000001,

        /// <summary>
        /// Ignores errors while merging metadata (such as attributes and ACLs).
        /// [REPLACEFILE_IGNORE_MERGE_ERRORS]
        /// </summary>
        IgnoreMergeErrors = 0x00000002,

        /// <summary>
        /// Ignores errors while merging ACL information. [REPLACEFILE_IGNORE_ACL_ERRORS]
        /// </summary>
        IgnoreAclErrors = 0x00000004
    }
}
