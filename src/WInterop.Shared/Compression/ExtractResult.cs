// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Runtime.InteropServices;

namespace WInterop.Compression
{
    /// <summary>
    /// [ERF]
    /// </summary>
    // https://msdn.microsoft.com/en-us/library/bb432257.aspx
    public struct ExtractResult
    {
        /// <summary>
        /// [erfOper]
        /// </summary>
        public Error OperationResult;

        /// <summary>
        /// Optional error value, filled in by FCI/FDI. For FCI, this is usually the CRT errno. [erfType]
        /// </summary>
        public int Type;

        /// <summary>
        /// True if an error is present.
        /// </summary>
        public BOOL ErrorPresent;

        [StructLayout(LayoutKind.Explicit)]
        public struct Error
        {
            [FieldOffset(0)]
            public FdiError FdiError;
            [FieldOffset(0)]
            public FciError FciError;
        }
    }
}
