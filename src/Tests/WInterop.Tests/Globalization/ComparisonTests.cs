// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Xunit;
using FluentAssertions;
using WInterop.Globalization;

namespace GlobalizationTests
{
    public class ComparisonTests
    {
        [Theory,
            InlineData(null, null, true, 0),
            InlineData(null, null, false, 0),
            InlineData("A", null, true, 1),
            InlineData("A", null, false, 1),
            InlineData(null, "B", true, -1),
            InlineData(null, "B", false, -1),
            InlineData("", "", true, 0),
            InlineData("", "", false, 0),
            InlineData("A", "", true, 1),
            InlineData("A", "", false, 1),
            InlineData("", "B", true, -1),
            InlineData("", "B", false, -1),
            InlineData("A", "B", true, -1),
            InlineData("A", "B", false, -1),
            InlineData("B", "A", true, 1),
            InlineData("B", "A", false, 1),
            InlineData("A", "b", true, -1),
            InlineData("A", "b", false, -1),
            InlineData("a", "B", true, -1),
            InlineData("a", "B", false, 1),
            InlineData("a", "aa", true, -1),
            InlineData("a", "aa", false, -1),
            InlineData("bb", "b", true, 1),
            InlineData("bb", "b", false, 1),
            ]
        public void StringCompareInvariant(string first, string second, bool ignoreCase, int expected)
        {
            Globalization.CompareStringOrdinal(first, second, ignoreCase).Should().Be(expected);
        }
    }
}
