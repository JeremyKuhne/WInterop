// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Runtime.CompilerServices;

#pragma warning disable SA1015 // Closing generic brackets should be spaced correctly

namespace WInterop
{
    /// <summary>
    ///  Used for trailing native unsized (ANYSIZE) arrays of <typeparamref name="T"/>. Native example:
    ///  UCHAR  UniqueId[1];
    /// </summary>
    /// <remarks>
    ///  Accessing the values is only safe when you have a pointer to the containing struct in
    ///  a buffer. If you have an actual struct (Foo, not Foo*), the trailing array will have been
    ///  truncated as the values aren't actually part of the struct.
    /// </remarks>
    public readonly struct TrailingArray<T> where T : unmanaged
    {
        private readonly T _firstItem;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe ReadOnlySpan<T> GetBuffer(in T first, uint count, uint offset = 0)
        {
            fixed (void* p = &first)
            {
                return ((TrailingArray<T>*)p)->GetBuffer(count, offset);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe ReadOnlySpan<T> GetBufferInBytes(in T first, uint countInBytes, uint offsetInBytes = 0)
        {
            fixed (void* p = &first)
            {
                return ((TrailingArray<T>*)p)->GetBuffer(countInBytes / (uint)sizeof(T), offsetInBytes / (uint)sizeof(T));
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private unsafe ReadOnlySpan<T> GetBuffer(uint count, uint offset = 0)
        {
            if (count == 0)
                return ReadOnlySpan<T>.Empty;

            fixed (T* t = &_firstItem)
                return new ReadOnlySpan<T>(t + offset, (int)count);
        }
    }
}

#pragma warning restore SA1015 // Closing generic brackets should be spaced correctly
