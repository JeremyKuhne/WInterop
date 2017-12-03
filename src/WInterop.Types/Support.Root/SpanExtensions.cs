// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Runtime.CompilerServices;

namespace WInterop
{
    public static class SpanExtensions
    {
        internal static int IndexOfAnyHelper<T>(ref T buffer, T first, T second, int length)
            where T : struct, IEquatable<T>
        {
            if (length < 1)
                return -1;

            if (first.Equals(buffer) || second.Equals(buffer))
                return 0;

            // Unsafe.Add(IntPtr) is more efficient on 64 bit
            IntPtr index = (IntPtr)1;
            while (length > 1)
            {
                T current = Unsafe.Add(ref buffer, index);
                if (first.Equals(current) || second.Equals(current))
                    return (int)index;

                index = index + 1;
                length--;
            }

            return -1;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int IndexOfAny<T>(this ReadOnlySpan<T> buffer, T first, T second)
            where T : struct, IEquatable<T>
        {
            return IndexOfAnyHelper<T>(ref buffer.DangerousGetPinnableReference(), first, second, buffer.Length);
        }

        internal static int IndexOfAnyHelper<T>(ref T buffer, T first, T second, T third, int length)
            where T : struct, IEquatable<T>
        {
            if (length < 1)
                return -1;

            if (first.Equals(buffer) || second.Equals(buffer) || third.Equals(buffer))
                return 0;

            // Unsafe.Add(IntPtr) is more efficient on 64 bit
            IntPtr index = (IntPtr)1;
            while (length > 1)
            {
                T current = Unsafe.Add(ref buffer, index);
                if (first.Equals(current) || second.Equals(current) || third.Equals(buffer))
                    return (int)index;

                index = index + 1;
                length--;
            }

            return -1;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public unsafe static int IndexOfAny<T>(this ReadOnlySpan<T> buffer, T first, T second, T third)
            where T : struct, IEquatable<T>
        {
            return IndexOfAnyHelper<T>(ref buffer.DangerousGetPinnableReference(), first, second, third, buffer.Length);
        }
    }
}
