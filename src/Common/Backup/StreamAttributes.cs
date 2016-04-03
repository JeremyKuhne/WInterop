// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Backup
{
    using System;

    /// <summary>
    /// The attributes returned in WIN32_STREAM_ID dwStreamAttributes. Defines taken from winnt.h.
    /// See <a href="https://msdn.microsoft.com/en-us/library/dd305136.aspx">[MS-BKUP]</a> specification.
    /// </summary>
    [Flags]
    public enum StreamAttributes : uint
    {
        /// <summary>
        /// This backup stream has no special attributes.
        /// </summary>
        STREAM_NORMAL_ATTRIBUTE = 0x00000000,

        STREAM_MODIFIED_WHEN_READ = 0x00000001,

        /// <summary>
        /// The backup stream contains security information. This attribute applies only to backup stream of type SECURITY_DATA.
        /// </summary>
        STREAM_CONTAINS_SECURITY = 0x00000002,

        STREAM_CONTAINS_PROPERTIES = 0x00000004,

        /// <summary>
        /// The backup stream is part of a sparse file stream. This attribute applies only to backup stream of type DATA, ALTERNATE_DATA, and SPARSE_BLOCK.
        /// </summary>
        STREAM_SPARSE_ATTRIBUTE = 0x00000008
    }
}
