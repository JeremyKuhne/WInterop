// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using FluentAssertions;
using System;
using WInterop;
using Xunit;

namespace Tests.Support
{
    public class CharSpanExtensionTests
    {
        [Theory,
            InlineData(new char[] { }, null, false),
            InlineData(new char[] { }, "", true),
            InlineData(new char[] { }, "a", false),
            InlineData(new char[] { '\0' }, null, false),
            InlineData(new char[] { '\0' }, "", true),
            InlineData(new char[] { '\0' }, "a", false),
            InlineData(new char[] { 'a' }, "a", true),
            InlineData(new char[] { 'a' }, "A", false),
            InlineData(new char[] { 'a' }, "b", false),
            InlineData(new char[] { 'a', 'b' }, "a", false),
            InlineData(new char[] { 'a', '\0', 'b' }, "a", true),
            InlineData(new char[] { 'a', 'b' }, "ab", true)
            ]
        public void EqualsOrdinal(char[] buffer, string compareTo, bool expected)
        {
            ReadOnlySpan<char> span = new ReadOnlySpan<char>(buffer);
            span.EqualsOrdinal(compareTo).Should().Be(expected);
        }

        [Theory,
            InlineData(new char[] { }, ""),
            InlineData(new char[] { '\0' }, ""),
            InlineData(new char[] { 'a' }, "a"),
            InlineData(new char[] { 'a', '\0' }, "a"),
            InlineData(new char[] { 'a', '\0', 'b' }, "a"),
            InlineData(new char[] { 'a', 'b' }, "ab"),
            InlineData(new char[] { 'a', 'b', '\0' }, "ab")
            ]
        public void CreateString(char[] buffer, string expected)
        {
            ReadOnlySpan<char> span = new ReadOnlySpan<char>(buffer);
            span.CreateString().Should().Be(expected);
        }

        [Fact]
        public void NoSpaceForNullThrows()
        {
            Action action = () => { new Span<char>(new char[0]).CopyFrom(""); };
            action.Should().Throw<ArgumentException>().And.ParamName.Should().Be("nullTerminate");
        }

        [Theory,
            InlineData(0, null, false, new char[] { }),
            InlineData(1, null, false, new char[] { '\0' }),
            InlineData(1, "a", false, new char[] { 'a' }),
            InlineData(1, "a", true, new char[] { '\0' }),
            InlineData(3, "a\0b", false, new char[] { 'a', '\0', 'b' }),
            InlineData(3, "a\0b", true, new char[] { 'a', '\0', '\0' }),
            InlineData(3, "abba", true, new char[] { 'a', 'b', '\0' }),
            InlineData(3, "abba", false, new char[] { 'a', 'b', 'b' })
            ]
        public void CopyFromString(int bufferSize, string source, bool nullTerminate, char[] expected)
        {
            char[] buffer = new char[bufferSize];
            Span<char> span = new Span<char>(buffer);
            span.CopyFrom(source, nullTerminate);
            buffer.Should().Equal(expected);
        }

        [Theory,
            InlineData("", "", true, true),
            InlineData("", "", false, true),
            InlineData("", "a", true, false),
            InlineData("", "a", false, false),
            InlineData("a", "", true, true),
            InlineData("a", "", false, true),
            InlineData("a", "a", false, true),
            InlineData("a", "a", true, true),
            InlineData("a", "A", false, false),
            InlineData("a", "A", true, true),
            InlineData("a", "ab", false, false),
            InlineData("a", "ab", true, false),
            InlineData("abbacab", "cab", false, true),
            InlineData("abbacab", "aca", true, false)
            ]
        public void EndsWith(string buffer, string value, bool ignoreCase, bool expected)
        {
            buffer.AsSpan().EndsWithOrdinal(value.AsSpan(), ignoreCase).Should().Be(expected);
        }
    }
}
