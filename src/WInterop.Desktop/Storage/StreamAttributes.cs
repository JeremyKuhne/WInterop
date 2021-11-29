// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace WInterop.Storage;

/// <summary>
///  The attributes returned in WIN32_STREAM_ID dwStreamAttributes. Defines taken from winnt.h.
///  See <a href="https://msdn.microsoft.com/en-us/library/dd305136.aspx">[MS-BKUP]</a> specification.
/// </summary>
[Flags]
public enum StreamAttributes : uint
{
    /// <summary>
    ///  This backup stream has no special attributes. [STREAM_NORMAL_ATTRIBUTE]
    /// </summary>
    Normal = 0x00000000,

    /// <summary>
    ///  [STREAM_MODIFIED_WHEN_READ]
    /// </summary>
    ModifiedWhenRead = 0x00000001,

    /// <summary>
    ///  The backup stream contains security information. This attribute applies only to backup stream of type SECURITY_DATA.
    ///  [STREAM_CONTAINS_SECURITY]
    /// </summary>
    ContainsSecurity = 0x00000002,

    /// <summary>
    ///  [STREAM_CONTAINS_PROPERTIES]
    /// </summary>
    ContainsProperties = 0x00000004,

    /// <summary>
    ///  The backup stream is part of a sparse file stream. This attribute applies only to backup stream of type DATA, ALTERNATE_DATA, and SPARSE_BLOCK.
    ///  [STREAM_SPARSE_ATTRIBUTE]
    /// </summary>
    Sparse = 0x00000008
}