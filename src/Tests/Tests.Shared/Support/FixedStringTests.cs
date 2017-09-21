// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using FluentAssertions;
using System;
using WInterop;
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
            new FixedString.Size12().Buffer.CreateString().Should().Be(string.Empty);
        }

        [Theory,
            InlineData(""),
            InlineData("a"),
            InlineData("fizzlestick")
            ]
        public unsafe void Size12_RoundTrip(string value)
        {
            FixedString.Size12 s = new FixedString.Size12();
            s.Buffer.CopyFrom(value);
            s.Buffer.CreateString().Should().Be(value);
        }

        [Fact]
        public unsafe void Size12_SetOver()
        {
            FixedString.Size12 s = new FixedString.Size12();
            s.Buffer.CopyFrom("Fizzlesticks");
            s.Buffer.CreateString().Should().Be("Fizzlestick");
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
            s.Buffer.CreateString().Should().Be("fizzlesticks");
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

        [Theory,
            InlineData("", null, false),
            InlineData("", "", true),
            InlineData("", "a", false),
            InlineData("", "\0", false),
            InlineData("a", "", false),
            InlineData("a", "a", true),
            InlineData("fizzlestick", "fizzlestick", true),
            InlineData("fizzlestick", "fizzlestick\0", false),
            InlineData("fizzlestick", "fizzlestic\0", false),
            InlineData("fizzlesticks", "fizzlesticks", true),
            InlineData("fizzlesticks", "fizzlestick", false),
            InlineData("fizzlesticks", "fizzlesticks!", false),
            InlineData("fizzlesticks", "fizzlesticks\0", false)
            ]
        public unsafe void Size12_Equality(string buffer, string compareTo, bool expected)
        {
            // Manually copy in the buffer to allow testing non-null terminated
            FixedString.Size12 s = new FixedString.Size12();

            fixed (char* c = buffer)
            {
                Buffer.MemoryCopy(c, (char*)&s, sizeof(FixedString.Size12) * sizeof(char), buffer.Length * sizeof(char));
            }

            s.Buffer.EqualsOrdinal(compareTo).Should().Be(expected);
        }
    }
}
