// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Storage
{
    /// <summary>
    /// Used to set the allocation size for a file.
    /// </summary>
    /// <remarks><see cref="https://msdn.microsoft.com/en-us/library/windows/hardware/ff540232.aspx"/></remarks>
    public struct FILE_ALLOCATION_INFORMATION
    {
        public long AllocationSize;
    }
}
