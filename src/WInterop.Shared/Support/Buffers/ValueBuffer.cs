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
        // We need to pin our backing byte array so our reinterpreted spans don't get mistracked
        // if the backing byte[] array gets moved by the GC.
        private GCHandle _handle;

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
            GCHandle newHandle = GCHandle.Alloc(newBuffer, GCHandleType.Pinned);

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
            Span.CopyTo(newSpan);
            Dispose();
            _handle = newHandle;
            Span = newSpan;
        }

        public ref T this[int index] => ref Span[index];

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Dispose()
        {
            if (_handle.IsAllocated)
            {
                ArrayPool<byte>.Shared.Return((byte[])_handle.Target);
                _handle.Free();
            }
        }
    }
}
