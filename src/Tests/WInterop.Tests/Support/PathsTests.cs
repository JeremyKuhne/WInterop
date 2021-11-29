// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using FluentAssertions;
using System.Text;
using WInterop.Support;
using Xunit;

namespace SupportTests;

public class PathsTests
{
    [Theory,
        InlineData(null, null),
        InlineData("", ""),
        InlineData(@"\", ""),
        InlineData(@"C:\foo", "foo"),
        InlineData(@"C:\foo\", "foo"),
        InlineData(@"a", "a")]
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
        InlineData(@"a", "")]
    public void TrimLastSegment(string value, string expected)
    {
        Paths.TrimLastSegment(value).Should().Be(expected);
    }

    [Theory,
        InlineData(null, false),
        InlineData("", false),
        InlineData("\\", true),
        InlineData("a\\", false),
        InlineData("/", true),
        InlineData("a/", false)]
    public void BeginsWithDirectorySeparator(string value, bool expected)
    {
        Paths.BeginsWithDirectorySeparator(value).Should().Be(expected);
    }

    [Theory,
        InlineData(null, false),
        InlineData("", false),
        InlineData("\\", true),
        InlineData("a\\", true),
        InlineData("\\a", false),
        InlineData("/", true),
        InlineData("a/", true),
        InlineData("/a", false)]
    public void EndsInDirectorySeparator(string value, bool expected)
    {
        Paths.EndsInDirectorySeparator(value == null ? null : new StringBuilder(value))
            .Should().Be(expected);
    }

    [Theory,
        InlineData(null, null),
        InlineData(@"", @"\"),
        InlineData(@"\", @"\"),
        InlineData(@"/", @"/")]
    public void AddTrailingSeparator(string value, string expected)
    {
        Paths.AddTrailingSeparator(value).Should().Be(expected);
    }

    [Theory,
        InlineData(null, null),
        InlineData(@"", @""),
        InlineData(@"\", @""),
        InlineData(@"\a", @"\a"),
        InlineData(@"\\", @""),
        InlineData(@"/", @""),
        InlineData(@"/a", @"/a"),
        InlineData(@"//", @"")]
    public void TrimTrailingSeparators(string value, string expected)
    {
        Paths.TrimTrailingSeparators(value).Should().Be(expected);
    }

    [Theory,
        InlineData(null, false),
        InlineData(@"\\", false),
        InlineData(@"\\.", false),
        InlineData(@"\\.\", true),
        InlineData(@"//./", true),
        InlineData(@"\\?", false),
        InlineData(@"\\?\", true),
        InlineData(@"//?/", true),
        InlineData(@"\??", false),
        InlineData(@"\??\", true),
        InlineData(@"/??/", true)]
    public void IsDosDevicePath(string value, bool expected)
    {
        Paths.IsDosDevicePath(value).Should().Be(expected);
    }

    [Theory,
        InlineData(null, false),
        InlineData(@"\\", false),
        InlineData(@"\\.", false),
        InlineData(@"\\.\", false),
        InlineData(@"//./", false),
        InlineData(@"\\?", false),
        InlineData(@"\\?\", true),
        InlineData(@"//?/", false),
        InlineData(@"\??", false),
        InlineData(@"\??\", true),
        InlineData(@"/??/", false)]
    public void IsExtendedDosDevicePath(string value, bool expected)
    {
        Paths.IsExtendedDosDevicePath(value).Should().Be(expected);
    }

    [Theory,
        InlineData(new string[] { @"foo", null }, @"foo"),
        InlineData(new string[] { @"foo", "bar" }, @"foo\bar"),
        InlineData(new string[] { @"foo\", @"bar" }, @"foo\bar"),
        InlineData(new string[] { @"foo", @"\bar" }, @"foo\bar"),
        InlineData(new string[] { @"foo\", @"\bar" }, @"foo\\bar"),
        InlineData(new string[] { @"", @"\\?\" }, @"\\?\"),
        ]
    public void Join(string[] paths, string expected)
    {
        StringBuilder builder = new(paths[0]);
        Paths.Join(builder, paths[1]);
        builder.ToString().Should().Be(expected);
    }
}
