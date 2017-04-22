// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.ColorSystem
{
    // https://msdn.microsoft.com/en-us/library/dd371828.aspx
    public struct CIEXYZ
    {
        // FXPT2DOT30: Fixed-point notation 2 bit integer and 30 bit fractional
        public uint ciexyzX;
        public uint ciexyzY;
        public uint ciexyzZ;
    }
}
