// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Storage
{
    /// <summary>
    /// [FILE_POSITION_INFORMATION]
    /// </summary>
    // https://msdn.microsoft.com/en-us/library/windows/hardware/ff545848.aspx
    public struct FilePositionInformation
    {
        public long CurrentByteOffset;
    }
}
