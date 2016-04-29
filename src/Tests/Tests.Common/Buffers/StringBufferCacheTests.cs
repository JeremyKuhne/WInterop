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
    }
}
