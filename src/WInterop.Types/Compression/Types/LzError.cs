// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Compression.Types
{
    public enum LzError
    {
        /// <summary>
        /// Invalid input handle. [LZERROR_BADINHANDLE]
        /// </summary>
        BadInHandle = -1,

        /// <summary>
        /// Invalid output handle. [LZERROR_BADOUTHANDLE]
        /// </summary>
        BadOutHandle = -2,

        /// <summary>
        /// Corrupt compressed file format. [LZERROR_READ]
        /// </summary>
        Read = -3,

        /// <summary>
        /// Out of space for output file. [LZERROR_WRITE]
        /// </summary>
        Write = -4,

        /// <summary>
        /// Insufficient memory for LZFile struct. [LZERROR_GLOBALLOC]
        /// </summary>
        GlobalAlloc = -5,

        /// <summary>
        /// Bad global handle. [LZERROR_GLOBLOCK]
        /// </summary>
        GlobalLock = -6,

        /// <summary>
        /// Input parameter out of acceptable range. [LZERROR_BADVALUE]
        /// </summary>
        BadValue = -7,

        /// <summary>
        /// Compression algorithm not recognized. [LZERROR_UNKNOWNALG]
        /// </summary>
        UnknownAlgorithm = -8
    }
}
