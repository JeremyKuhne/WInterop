// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Runtime.InteropServices;

namespace WInterop.Compression.Types
{
    public unsafe struct LzxHeader
    {
        public static byte[] LzxSignature = { 0x53, 0x5A, 0x44, 0x44, 0x88, 0xF0, 0x27, 0x33 };

        /// <summary>
        /// Signature, should be "SZDDˆð'3" (0x53, 0x5A, 0x44, 0x44, 0x88, 0xF0, 0x27, 0x33)
        /// </summary>
        public fixed byte signature[8];

        /// <summary>
        /// Always 'A' (0x42)
        /// </summary>
        public byte algorithm;

        /// <summary>
        /// Ascii char for last extension character or 0x00 if unchanged.
        /// </summary>
        public byte extensionChar;

        /// <summary>
        /// The uncompressed size (in little endian order)
        /// </summary>
        public fixed byte uncompressedSize[4];

        public byte[] GetSignature()
        {
            fixed (byte* b = signature)
            {
                var s = new byte[8];
                Marshal.Copy((IntPtr)b, s, 0, 8);
                return s;
            }
        }

        public uint GetUncompressedSize()
        {
            fixed (byte* b = uncompressedSize)
            {
                return  (uint)(b[0] | (b[1] << 8) | (b[2] << 16) | (b[3] << 24));
            }
        }
    }
}
