// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace WInterop.Storage;

/// <summary>
///  Flags used by the <a href="https://msdn.microsoft.com/en-us/library/windows/desktop/aa364962.aspx">GetFinalPathNameByHandle</a> API.
/// </summary>
[Flags]
public enum GetFinalPathNameByHandleFlags : uint
{
    /// <summary>
    ///  Return the normalized drive name. This is the default. [FILE_NAME_NORMALIZED]
    /// </summary>
    FileNameNormalized = 0x0,

    /// <summary>
    ///  Return the path with the drive letter. This is the default. [VOLUME_NAME_DOS]
    /// </summary>
    VolumeNameDos = 0x0,

    /// <summary>
    ///  Return the path with a volume GUID path instead of the drive name. [VOLUME_NAME_GUID]
    /// </summary>
    VolumeNameGuid = 0x1,

    /// <summary>
    ///  Return the path with the volume device path. [VOLUME_NAME_NT]
    /// </summary>
    VolumeNameNt = 0x2,

    /// <summary>
    ///  Return the path with no drive information. [VOLUME_NAME_NONE]
    /// </summary>
    VolumeNameNone = 0x4,

    /// <summary>
    ///  Return the opened file name (not normalized). [FILE_NAME_OPENED]
    /// </summary>
    FileNameOpened = 0x8,
}