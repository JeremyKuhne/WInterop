// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using FluentAssertions;
using WInterop.Support;
using Xunit;

namespace SupportTests
{
    public class PathsTests
    {
        [Theory,
            InlineData(null, null),
            InlineData("", ""),
            InlineData(@"\", ""),
            InlineData(@"C:\foo", "foo"),
            InlineData(@"C:\foo\", "foo"),
            InlineData(@"a", "a"),
            ]
        public void GetLastSegment(string value, string expected)
        {
            Paths.GetLastSegment(value).Should().Be(expected);
        }

        [Theory,
            InlineData(null, null),
            InlineData("", ""),
            InlineData(@"\", ""),
            InlineData(@"C:\foo", @"C:"),
            InlineData(@"C:\foo\", @"C:"),
            InlineData(@"C:\foo\bar", @"C:\foo"),
            InlineData(@"a", ""),
            ]
        public void TrimLastSegment(string value, string expected)
        {
            Paths.TrimLastSegment(value).Should().Be(expected);
        }

        [Theory,
            InlineData(new string[] { null }, ""),
            InlineData(new string[] { null, null }, ""),
            InlineData(new string[] { null, @"foo" }, @"foo"),
            InlineData(new string[] { @"foo", null }, @"foo"),
            InlineData(new string[] { @"foo", "bar" }, @"foo\bar"),
            InlineData(new string[] { @"foo", null, null, "bar" }, @"foo\bar"),
            InlineData(new string[] { @"foo\", @"bar" }, @"foo\bar"),
            InlineData(new string[] { @"foo", @"\bar" }, @"foo\bar"),
            InlineData(new string[] { @"foo\", @"\bar" }, @"foo\bar"),
            InlineData(new string[] { @"a\", @"\b\", @"\c" }, @"a\b\c"),
            InlineData(new string[] { @"\", @"\", @"\" }, @""),
            InlineData(new string[] { @"\\?\" }, @"\\?\"),
            InlineData(new string[] { null, @"\\?\" }, @"\\?\"),
            InlineData(new string[] { @"", @"\\?\" }, @"\\?\"),
            InlineData(new string[] { @"\", @"\\?\" }, @"\\?\"),
            ]
        public void Combine(string[] paths, string expected)
        {
            Paths.Combine(paths).Should().Be(expected);
        }
    }
}
