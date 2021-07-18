// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Storage
{
    // https://msdn.microsoft.com/en-us/library/windows/desktop/aa365542.aspx
    public enum MoveMethod : uint
    {
        /// <summary>
        ///  Seek from the beginning of the file. [FILE_BEGIN]
        /// </summary>
        Begin = 0,

        /// <summary>
        ///  Seek relative to the current position. [FILE_CURRENT]
        /// </summary>
        Current = 1,

        /// <summary>
        ///  Seek from the end of the file. [FILE_END]
        /// </summary>
        End = 2
    }
}