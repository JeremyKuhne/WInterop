// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using FluentAssertions;
using System;
using WInterop.Buffers;
using Xunit;

namespace WInterop.Tests.Buffers
{
    public class StringBufferCacheTests
    {
        [Fact]
        public void ReleasedBufferLengthIsCleared()
        {
            var buffer = StringBufferCache.Instance.Acquire();
            buffer.Length.Should().Be(0);
            buffer.Append("foo");
            buffer.Length.Should().Be(3);
            StringBufferCache.Instance.Release(buffer);
            buffer.Length.Should().Be(0);
        }

        [Fact]
        public void BufferOverMaxSizeIsDisposed()
        {
            var cache = new StringBufferCache(1, 10);
            var buffer = cache.Acquire();
            buffer.EnsureCharCapacity(11);
            cache.Release(buffer);
            buffer.ByteCapacity.Should().Be(0);
            buffer.CharCapacity.Should().Be(0);
            buffer.Length.Should().Be(0);
        }

        [Fact]
        public void BufferUnderMaxSizeIsNotDisposed()
        {
            var buffer = new StringBuffer();
            buffer.EnsureCharCapacity(10);
            var cache = new StringBufferCache(1, buffer.CharCapacity);
            cache.Release(buffer);
            buffer.ByteCapacity.Should().NotBe(0);
            buffer.CharCapacity.Should().NotBe(0);
        }
    }
}
