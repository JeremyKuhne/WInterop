// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using FluentAssertions;
using WInterop.SystemInformation;
using Xunit;

namespace DesktopTests.SystemInformation
{
    public class SystemInformationTests
    {
        [Fact]
        public void GetCurrentUserName()
        {
            SystemInformationDesktopMethods.GetUserName().Should().NotBeNullOrWhiteSpace();
        }
    }
}
