// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Buffers;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace WInterop.Support.Buffers
{
    /// <summary>
    /// Growable Span(T) buffer wrapper.
    /// </summary>
    public ref struct ValueBuffer<T> where T : unmanaged
    {
        private byte[] _buffer;

        /// <summary>
        /// Create the buffer with an initial span.
        /// </summary>
        /// <remarks>
        /// This is particularly useful when you have an initial stack allocated
        /// buffer that you want to use.
        /// </remarks>
        public ValueBuffer(Span<T> span)
        {
            Span = span;
            _buffer = null;
        }

        public ValueBuffer(int initialCapacity)
        {
            Span = default;
            _buffer = null;
            EnsureCapacity(initialCapacity);
        }

        public Span<T> Span { get; private set; }

        public int Length => Span.Length;

        /// <summary>
        /// Ensure that the buffer has enough space for <paramref name="capacity"/>
        /// number of elements.
        /// </summary>
        /// <param name="copy">True to copy the existing elements when new space is allocated.</param>
        public unsafe void EnsureCapacity(int capacity, bool copy = false)
        {
            if (capacity <= Span.Length)
                return;

            // We want to align to highest power of 2 less than the size/ up to
            // 128 bits (16 bytes), which should handle all alignment cases.
            int sizeOfT = Unsafe.SizeOf<T>();
            int alignTo = sizeOfT >= 16 ? 16
                : (sizeOfT & 8) != 0 ? 8
                : (sizeOfT & 4) != 0 ? 4
                : (sizeOfT & 2) != 0 ? 2
                : 1;

            // Get extra for possible realignment
            int byteCapacity = capacity * sizeOfT + alignTo;

            byte[] newBuffer = ArrayPool<byte>.Shared.Rent(byteCapacity);
            fixed (byte* b = newBuffer)
            {
                byte* p = b;

                // Align if necessary
                int offset = (int)((ulong)b % (uint)alignTo);
                if (offset > 0)
                {
                    offset = alignTo - offset;
                    p += offset;
                }

                Debug.Assert(((int)((ulong)p % (uint)alignTo)) == 0);

                Span<T> newSpan = new Span<T>(p, (newBuffer.Length - offset) / sizeOfT);

                if (copy)
                {
                    Span.CopyTo(newSpan);
                }

                if (_buffer != null)
                {
                    ArrayPool<byte>.Shared.Return(_buffer);
                }
                _buffer = newBuffer;
                Span = newSpan;
            }
        }

        public ref T this[int index] => ref Span[index];

        public ref T GetPinnableReference() => ref MemoryMarshal.GetReference(Span);

        public void Dispose()
        {
            if (_buffer != null)
            {
                ArrayPool<byte>.Shared.Return(_buffer);
                _buffer = null;
            }
        }
    }
}
