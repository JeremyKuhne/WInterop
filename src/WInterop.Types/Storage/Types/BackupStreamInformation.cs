// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Storage
{
    /// <summary>
    /// Basic information about a stream
    /// </summary>
    public struct BackupStreamInformation
    {
        /// <summary>
        /// Name of the alternate stream
        /// </summary>
        public string Name;

        /// <summary>
        /// Size of the alternate stream
        /// </summary>
        public long Size;

        /// <summary>
        /// Stream type.
        /// </summary>
        public BackupStreamType StreamType;
    }
}
