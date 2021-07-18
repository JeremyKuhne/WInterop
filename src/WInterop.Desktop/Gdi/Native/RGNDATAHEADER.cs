// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Gdi.Native
{
    // https://docs.microsoft.com/windows/win32/api/wingdi/ns-wingdi-rgndataheader
    public struct RGNDATAHEADER
    {
        public uint dwSize;
        public RegionHeaderType iType;
        public uint nCount;
        public uint nRgnSize;
        public Rect rcBound;
    }
}