// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using FluentAssertions;
using WInterop;
using Xunit;

namespace SupportTests
{
    public class UlongTests
    {
        [Theory,
            InlineData(0, 0, 0),
            InlineData(0xDEAD, 0xBEEF, 0x0000DEAD0000BEEF),
            InlineData(0x00ABACAB, 0xDEADBEEF, 0x00ABACABDEADBEEF),
            InlineData(0xFFFFFFFF, 0xFFFFFFFF, 0xFFFFFFFFFFFFFFFF)
            ]
        public void HighLowUlongValues(uint high, uint low, ulong expected)
        {
            HighLowUlong hl = new HighLowUlong { High = high, Low = low };
            ((ulong)hl).Should().Be(expected, "high low converts correctly");
            LowHighUlong lh = new LowHighUlong { High = high, Low = low };
            ((ulong)lh).Should().Be(expected, "low high converts correctly");
            hl = expected;
            ((ulong)hl).Should().Be(expected, "high low roundtrips");
            hl.High.Should().Be(high, "high low high is expected");
            hl.Low.Should().Be(low, "high low low is expected");
            lh = expected;
            ((ulong)lh).Should().Be(expected, "low high roundtrips");
            lh.High.Should().Be(high, "low high high is expected");
            lh.Low.Should().Be(low, "low high low is expected");
        }
    }
}
