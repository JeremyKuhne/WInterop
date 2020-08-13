// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Gdi
{
    /// <summary>
    ///  Contains information about the dimensions and color format of a DIB.
    ///  [BITMAPINFOHEADER]
    /// </summary>
    /// <msdn>https://docs.microsoft.com/windows/win32/api/wingdi/ns-wingdi-tagbitmapinfoheader</msdn>
    public struct BitmapInfoHeader
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
    }
}
