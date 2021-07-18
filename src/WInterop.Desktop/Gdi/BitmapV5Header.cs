// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Gdi
{
    /// <summary>
    ///  [BITMAPV5HEADER]
    /// </summary>
    /// <msdn>https://docs.microsoft.com/en-us/windows/desktop/api/wingdi/ns-wingdi-bitmapv5header</msdn>
    public struct BitmapV5Header
    {
        public uint Size;
        public int Width;
        public int Height;
        public ushort Planes;
        public ushort BitCount;
        public BitmapCompression Compression;
        public uint SizeImage;
        public int XPixelsPerMeter;
        public int YPixelsPerMeter;
        public uint ColorsUsed;
        public uint ColorsImportant;
        public uint RedMask;
        public uint GreenMask;
        public uint BlueMask;
        public uint AlphaMask;
        public LogicalColorSpace ColorSpaceType;
        public CieXyzTriple Endpoints;
        public uint GammaRed;
        public uint GammaGreen;
        public uint GammaBlue;
        public BitmapIntent Intent;
        public uint ProfileData;
        public uint ProfileSize;
        public uint Reserved;
    }
}