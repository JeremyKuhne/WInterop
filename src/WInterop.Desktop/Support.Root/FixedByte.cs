// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Runtime.CompilerServices;

namespace WInterop
{
    public static unsafe class FixedByte
    {
        // These structs can't be marked as readonly (yet) due to the fixed buffer.

        public struct Size6
        {
            private const int Size = 6;
            private fixed byte _buffer[Size];

            public Span<byte> Buffer
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get { fixed (byte* c = _buffer) return new Span<byte>(c, Size); }
            }
        }

        public struct Size16
        {
            private const int Size = 16;
            private fixed byte _buffer[Size];

            public Span<byte> Buffer
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get { fixed (byte* c = _buffer) return new Span<byte>(c, Size); }
            }
        }

        public struct Size32
        {
            private const int Size = 32;
            private fixed byte _buffer[Size];

            public Span<byte> Buffer
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get { fixed (byte* c = _buffer) return new Span<byte>(c, Size); }
            }
        }

        public struct Size48
        {
            private const int Size = 48;
            private fixed byte _buffer[Size];

            public Span<byte> Buffer
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get { fixed (byte* c = _buffer) return new Span<byte>(c, Size); }
            }
        }

        public struct Size128
        {
            private const int Size = 128;
            private fixed byte _buffer[Size];

            public Span<byte> Buffer
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get { fixed (byte* c = _buffer) return new Span<byte>(c, Size); }
            }
        }
    }
}
