// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using FluentAssertions;
using WInterop.SystemInformation;
using WInterop.SystemInformation.DataTypes;
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

        [Fact]
        public void GetSuiteMask()
        {
            SystemInformationDesktopMethods.GetSuiteMask().Should().HaveFlag(SuiteMask.VER_SUITE_SINGLEUSERTS);
        }
    }
}
