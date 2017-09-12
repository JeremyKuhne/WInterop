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
    public struct FILE_ID_INFO
    {
        // https://msdn.microsoft.com/en-us/library/windows/desktop/hh802691.aspx

        public ulong VolumeSerialNumber;
        public Guid FileId;
    }
}
