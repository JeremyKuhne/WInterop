// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Text;
using WInterop.Support;

namespace SupportTests;

public class StringsTests
{
    [Theory]
    [InlineData("foo", 0, 'b', "boo")]
    [InlineData("foo", 2, 'r', "for")]
    [InlineData("foo", 1, 'l', "flo")]
    public void ReplaceChar_SingleChar(string source, int index, char newChar, string expected)
    {
        Strings.ReplaceChar(source, index, newChar).Should().Be(expected);
    }

    [Theory]
    [InlineData("", 0)]
    [InlineData("f", 1)]
    [InlineData("f", -1)]
    public void ReplaceChar_ArgumentOutOfRange(string source, int index)
    {
        Action action = () => Strings.ReplaceChar(source, index, '☺');
        action.Should().Throw<ArgumentOutOfRangeException>();
    }

    [Theory]
    [InlineData(null)]
    public void ReplaceChar_ArgumentNull(string source)
    {
        Action action = () => Strings.ReplaceChar(source, 0, '☺');
        action.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public void FromNullTerminatedAsciiString()
    {
        const string TestString = "Foo";
        Span<byte> testData = stackalloc byte[TestString.Length + 1];
        Encoding.ASCII.GetBytes(TestString, testData);
        Strings.FromNullTerminatedAsciiString(testData).Should().Be("Foo");
        testData[2] = 0;
        Strings.FromNullTerminatedAsciiString(testData).Should().Be("Fo");
        testData[0] = 0;
        Strings.FromNullTerminatedAsciiString(testData).Should().Be(string.Empty);
        Strings.FromNullTerminatedAsciiString(ReadOnlySpan<byte>.Empty).Should().Be(string.Empty);
    }

    [Fact]
    public void ToNullTerminatedAsciiString()
    {
        ReadOnlySpan<byte> bytes = stackalloc byte[] { (byte)'F', (byte)'o', (byte)'o', 0 };
        Strings.ToNullTerminatedAsciiString("Foo").SequenceEqual(bytes).Should().BeTrue();

        bytes = stackalloc byte[] { 0 };
        Strings.ToNullTerminatedAsciiString(string.Empty).SequenceEqual(bytes).Should().BeTrue();
    }
}
