// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Xunit;
using WInterop.Globalization;

namespace GlobalizationTests;

public class LocaleInfoTests
{
    [Fact]
    public void GetIs24Clock_Default()
    {
        LocaleInfo.Instance.GetIs24HourClock();
    }

    [Fact]
    public void GetHoursHaveLeadingZeros_Default()
    {
        LocaleInfo.Instance.GetHoursHaveLeadingZeros();
    }
}
