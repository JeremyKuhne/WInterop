// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Storage.Types
{
    /// <summary>
    /// Used to set the end of file position.
    /// </summary>
    /// <remarks><see cref="https://msdn.microsoft.com/en-us/library/windows/hardware/ff545780(v=vs.85).aspx"/></remarks>
    public struct FILE_END_OF_FILE_INFORMATION
    {
        /// <summary>
        /// The absolute new end of file position as a byte offset from the start of the file.
        /// </summary>
        public long EndOfFile;
    }
}
