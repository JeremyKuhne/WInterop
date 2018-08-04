// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Runtime.InteropServices;

namespace WInterop.Compression.Types
{
    // https://msdn.microsoft.com/en-us/library/bb432257.aspx
    public struct ERF
    {
        public Error erfOper;
        public int erfType;
        public BOOL fError;

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
