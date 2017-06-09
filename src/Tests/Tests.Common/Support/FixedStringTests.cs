// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using FluentAssertions;
using System;
using System.Runtime.InteropServices;
using WInterop.Support;
using Xunit;

namespace Tests.Support
{
    public class FixedStringTests
    {
        [Fact]
        public unsafe void Size12_Size()
        {
            sizeof(FixedString.Size12).Should().Be(12 * sizeof(char));
        }

        [Fact]
        public unsafe void Size12_ShouldBeEmpty()
        {
            new FixedString.Size12().Value.Should().Be(string.Empty);
        }

        [Theory,
            InlineData(""),
            InlineData("a"),
            InlineData("fizzlestick")
            ]
        public unsafe void Size12_RoundTrip(string value)
        {
            FixedString.Size12 s = new FixedString.Size12()
            {
                Value = value
            };
            s.Value.Should().Be(value);
        }

        [Fact]
        public unsafe void Size12_SetOver()
        {
            FixedString.Size12 s = new FixedString.Size12()
            {
                Value = "Fizzlesticks"
            };
            s.Value.Should().Be("Fizzlestick");
        }

        [Fact]
        public unsafe void Size12_NoNullOnBuffer()
        {
            FixedString.Size12 s = new FixedString.Size12();
            char* f = (char*)&s;
            string value = "fizzlesticks";
            fixed (char* c = value)
            {
                Buffer.MemoryCopy(c, f, 12 * sizeof(char), 12 * sizeof(char));
            }
            s.Value.Should().Be("fizzlesticks");
        }

        [Fact]
        public unsafe void Size16_Size()
        {
            sizeof(FixedString.Size16).Should().Be(16 * sizeof(char));
        }

        [Fact]
        public unsafe void Size32_Size()
        {
            sizeof(FixedString.Size32).Should().Be(32 * sizeof(char));
        }

        [Fact]
        public unsafe void Size64_Size()
        {
            sizeof(FixedString.Size64).Should().Be(64 * sizeof(char));
        }

        [Fact]
        public unsafe void Size128_Size()
        {
            sizeof(FixedString.Size128).Should().Be(128 * sizeof(char));
        }

        [Fact]
        public unsafe void Size256_Size()
        {
            sizeof(FixedString.Size256).Should().Be(256 * sizeof(char));
        }

        [Fact]
        public unsafe void Size260_Size()
        {
            sizeof(FixedString.Size260).Should().Be(260 * sizeof(char));
        }
    }
}
