// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Xunit;
using FluentAssertions;
using WInterop.Globalization;

namespace GlobalizationTests;

public class CasingTests
{
    [Theory,
        InlineData('\0', '\0'),
        InlineData('a', 'A'),
        InlineData('A', 'A'),
        InlineData('ŭ', 'Ŭ')
        ]
    public void CharToUpperInvariant(char value, char expected)
    {
        Globalization.ToUpperInvariant(value).Should().Be(expected);
    }

    [Theory,
        InlineData('\0', '\0'),
        InlineData('B', 'b'),
        InlineData('b', 'b'),
        InlineData('Ĕ', 'ĕ')
        ]
    public void CharToLowerInvariant(char value, char expected)
    {
        Globalization.ToLowerInvariant(value).Should().Be(expected);
    }

    [Theory,
        InlineData(null, null),
        InlineData("", ""),
        InlineData("abc", "ABC"),
        InlineData("a\0c", "A\0C")
        ]
    public void StringToUpperInvariantUnsafe(string value, string expected)
    {
        // Copy the string so we don't run into any interning issues
#pragma warning disable CS0618 // Type or member is obsolete
        value = value == null ? null : string.Copy(value);
#pragma warning restore CS0618 // Type or member is obsolete
        Globalization.ToUpperInvariantUnsafe(value);
        value.Should().Be(expected);
    }

    [Theory,
        InlineData(null, null),
        InlineData("", ""),
        InlineData("DĔF", "dĕf"),
        InlineData("D\0F", "d\0f")
        ]
    public void StringToLowerInvariantUnsafe(string value, string expected)
    {
        // Copy the string so we don't run into any interning issues
#pragma warning disable CS0618 // Type or member is obsolete
        value = value == null ? null : string.Copy(value);
#pragma warning restore CS0618 // Type or member is obsolete
        Globalization.ToLowerInvariantUnsafe(value);
        value.Should().Be(expected);
    }
}
