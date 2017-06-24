// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace WInterop.Support
{
    public unsafe struct FixedByte
    {
        private const int BaseSize = 1;
        private unsafe byte _buffer;

        private byte[] GetBytes(int size)
        {
            byte[] bytes = new byte[size];
            fixed (void* destination = bytes)
            fixed (void* source = &_buffer)
            {
                Buffer.MemoryCopy(source, destination, size, size);
            }
            return bytes;
        }

        public struct Size6
        {
            private const int Size = 6;
            private FixedByte _buffer;
            private unsafe fixed char _bufferExtension[Size - BaseSize];

            public byte[] GetBytes() => _buffer.GetBytes(Size);
        }
    }
}
