// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using FluentAssertions;
using WInterop.Support;
using Xunit;

namespace SupportTests;

public class DelegatesTests
{
    private delegate bool SpanToBool(ReadOnlySpan<char> value);

    [Fact]
    public void SimpleGetDelegate()
    {
        var beginsWithDirectorySeparator = Delegates.CreateDelegate<SpanToBool>(@"WInterop.Support.Paths, " + Delegates.DesktopLibrary, "BeginsWithDirectorySeparator");
        beginsWithDirectorySeparator.Should().NotBeNull();
        beginsWithDirectorySeparator(@"a").Should().BeFalse();
        beginsWithDirectorySeparator(@"\a").Should().BeTrue();
    }
}
