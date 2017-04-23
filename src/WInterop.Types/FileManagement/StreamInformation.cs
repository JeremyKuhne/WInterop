// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.FileManagement.Types
{
    /// <summary>
    /// Basic information about an alternate stream.
    /// </summary>
    public struct StreamInformation
    {
        /// <summary>
        /// Name of the stream
        /// </summary>
        public string Name;

        /// <summary>
        /// Size of the stream
        /// </summary>
        public ulong Size;

        /// <summary>
        /// Allocated size of the stream
        /// </summary>
        public ulong AllocationSize;
    }
}
