// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace WInterop.FileManagement.Types
{
    /// <summary>
    /// File identification information.
    /// </summary>
    /// <remarks>
    /// Available in Windows 10 / Server 2012.
    /// </remarks>
    public struct FILE_ID_INFORMATION
    {
        // https://msdn.microsoft.com/en-us/library/windows/desktop/hh802691.aspx

        /// <summary>
        /// Serial number of the volume that contains this file.
        /// </summary>
        public ulong VolumeSerialNumber;

        /// <summary>
        /// The file identifier.
        /// </summary>
        public FILE_ID_128 FileId;
    }
}
