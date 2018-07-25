// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Gdi
{
    // https://msdn.microsoft.com/en-us/library/dd162941.aspx
    public struct RGNDATAHEADER
    {
        public uint dwSize;
        public RegionHeaderType iType;
        public uint nCount;
        public uint nRgnSize;
        public RECT rcBound;
    }
}
