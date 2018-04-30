// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using FluentAssertions;
using System.Runtime.CompilerServices;
using WInterop.Authorization.Types;
using WInterop.Support.Buffers;
using Xunit;

namespace Tests.Support.Buffers
{
    public class ValueBufferTests
    {
        [Fact]
        public void CheckCharAlignment()
        {
            CheckAlignment<char>(sizeof(char));
        }

        [Fact]
        public void CheckIntAlignment()
        {
            CheckAlignment<int>(sizeof(int));
            CheckAlignment<uint>(sizeof(uint));
        }

        [Fact]
        public void CheckLongAlignment()
        {
            CheckAlignment<long>(sizeof(long));
            CheckAlignment<ulong>(sizeof(ulong));
        }

        [Fact]
        public void CheckSidAligment()
        {
            // Max alignment is 16 bytes (128 bit)
            CheckAlignment<SID>(16);
        }

        private unsafe void CheckAlignment<T>(int alignment) where T : struct
        {
            ValueBuffer<T> t = new ValueBuffer<T>();
            try
            {
                t.EnsureCapacity(42);
                void* p = Unsafe.AsPointer(ref t.Span[0]);
                ulong address = (ulong)p;
                int sizeOfT = Unsafe.SizeOf<T>();
                int offset = (int)(address % (ulong)alignment);
                offset.Should().Be(0);
            }
            finally
            {
                t.Dispose();
            }
        }
    }
}
