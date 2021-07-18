// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Gdi.Native
{
    // https://devblogs.microsoft.com/oldnewthing/20101018-00/?p=12513
    // https://devblogs.microsoft.com/oldnewthing/20120720-00/?p=7083
    public struct ICONDIRENTRY
    {
        public byte bWidth;
        public byte bHeight;
        public byte bColorCount;
        public byte bReserved;
        public ushort wPlanes;
        public ushort wBitCount;
        public uint dwBytesInRes;
        public uint dwImageOffset;
    }
}