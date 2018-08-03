// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Storage
{
    /// <summary>
    /// Used to get the reference number for a file.
    /// </summary>
    /// <remarks>
    /// <see cref="https://msdn.microsoft.com/en-us/library/windows/hardware/ff540318.aspx"/>
    /// </remarks>
    public struct FILE_INTERNAL_INFORMATION
    {
        /// <summary>
        /// Reference number for the file.
        /// </summary>
        public long IndexNumber;
    }
}
