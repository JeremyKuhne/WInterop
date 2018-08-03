// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Storage
{
    public struct DiskFreeSpace
    {
        /// <summary>
        /// Number of sectors per cluster.
        /// </summary>
        public uint SectorsPerCluster;

        /// <summary>
        /// Number of bytes per sector.
        /// </summary>
        public uint BytesPerSector;

        /// <summary>
        /// Number of free clusters available to the user on the calling thread.
        /// </summary>
        public uint NumberOfFreeClusters;

        /// <summary>
        /// Total number of free clusters available to the user on the calling thread.
        /// </summary>
        public uint TotalNumberOfClusters;
    }
}
