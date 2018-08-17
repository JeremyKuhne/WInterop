// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Gdi
{
    /// <summary>
    /// [BITMAPCOREHEADER]
    /// </summary>
    /// <msdn>https://docs.microsoft.com/en-us/windows/desktop/api/wingdi/ns-wingdi-tagbitmapcoreheader</msdn>
    public struct BitmapCoreHeader
    {
        public uint Size;
        public ushort Width;
        public ushort Height;
        public ushort Planes;
        public ushort BitCount;
    }
}
