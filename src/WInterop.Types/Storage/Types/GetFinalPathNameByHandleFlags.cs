// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace WInterop.Storage.Types
{
    /// <summary>
    /// Flags used by the <a href="https://msdn.microsoft.com/en-us/library/windows/desktop/aa364962.aspx">GetFinalPathNameByHandle</a> API.
    /// </summary>
    [Flags]
    public enum GetFinalPathNameByHandleFlags : uint
    {
        /// <summary>
        /// Return the normalized drive name. This is the default.
        /// </summary>
        FILE_NAME_NORMALIZED = 0x0,
        /// <summary>
        /// Return the path with the drive letter. This is the default.
        /// </summary>
        VOLUME_NAME_DOS = 0x0,
        /// <summary>
        /// Return the path with a volume GUID path instead of the drive name.
        /// </summary>
        VOLUME_NAME_GUID = 0x1,
        /// <summary>
        /// Return the path with the volume device path.
        /// </summary>
        VOLUME_NAME_NT = 0x2,
        /// <summary>
        /// Return the path with no drive information.
        /// </summary>
        VOLUME_NAME_NONE = 0x4,
        /// <summary>
        /// Return the opened file name (not normalized).
        /// </summary>
        FILE_NAME_OPENED = 0x8,
    }
}
