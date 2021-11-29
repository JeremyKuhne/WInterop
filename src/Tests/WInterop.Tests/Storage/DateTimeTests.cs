// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using FluentAssertions;
using WInterop.Storage;
using Xunit;

namespace StorageTests;

public class DateTimeTests
{
    [Theory,
        InlineData(0, 0, 0x0701cdd41453c000),    // January 1, 1601
        InlineData(0, 1, 0x0701cdd41453c001),
        InlineData(0, -1, 0x0701cdd51453bfff)    // If we don't cast to uint this would throw an exception
        ]
    public void FromFileTimeTest(int high, int low, long expectedTicks)
    {
        FileTime fileTime = new FileTime((uint)low, (uint)high);
        fileTime.ToDateTimeUtc().ToLocalTime().Ticks.Should().Be(expectedTicks);
    }
}
