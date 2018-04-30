// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Buffers;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace WInterop.Support.Buffers
{
    public ref struct ValueBuffer<T> where T : struct
    {
        private Span<T> _span;
        private byte[] _buffer;

        public ValueBuffer(Span<T> span)
        {
            _span = span;
            _buffer = null;
        }

        public Span<T> Span => _span;

        public int Length => _span.Length;

        public unsafe void EnsureCapacity(int capacity, bool copy = false)
        {
            if (capacity <= _span.Length)
                return;

            // We want to align to highest power of 2 less than the size/ up to
            // 128 bits (16 bytes), which should handle all alignment cases.
            int sizeOfT = Unsafe.SizeOf<T>();
            int alignTo = (sizeOfT & 16) != 0 ? 16
                : (sizeOfT & 8) != 0 ? 8
                : (sizeOfT & 4) != 0 ? 4
                : (sizeOfT & 2) != 0 ? 2
                : 1;

            // Get extra for possible realignment
            int byteCapacity = capacity * sizeOfT + alignTo;

            byte[] oldBuffer = _buffer;
            byte[] newBuffer = null;

            newBuffer = ArrayPool<byte>.Shared.Rent(byteCapacity);

            // Align if necessary
            byte* p = (byte*)Unsafe.AsPointer(ref newBuffer[0]);
            int offset = (int)((ulong)p % (uint)alignTo);
            if (offset > 0)
            {
                offset = alignTo - offset;
                p += offset;
            }

            Debug.Assert(((int)((ulong)p % (uint)alignTo)) == 0);

            Span<T> newSpan = new Span<T>(p, (newBuffer.Length - offset) / sizeOfT);
            _span.CopyTo(newSpan);
            Dispose();
            _buffer = newBuffer;
            _span = newSpan;
        }

        public ref T this[int index] => ref _span[index];

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Dispose()
        {
            byte[] buffer = _buffer;
            this = default;
            if (buffer != null)
                ArrayPool<byte>.Shared.Return(buffer);
        }
    }
}
