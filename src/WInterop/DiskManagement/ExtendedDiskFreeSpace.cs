// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.DiskManagement
{
    public struct ExtendedDiskFreeSpace
    {
        /// <summary>
        /// Total number of free bytes available to the current user
        /// </summary>
        public ulong FreeBytesAvailable;

        /// <summary>
        /// Total number of bytes available to the user
        /// </summary>
        public ulong TotalNumberOfBytes;

        /// <summary>
        /// Total number of free bytes
        /// </summary>
        public ulong TotalNumberOfFreeBytes;
    }
}
