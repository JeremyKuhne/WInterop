// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.UtilityTests
{
    using FluentAssertions;
    using Xunit;

    public class EnvironmentTests
    {
        [Fact]
        public void IsWindowsStore()
        {
            bool isWindowsStore = Utility.Environment.IsWindowsStoreApplication();
#if WINRT
            isWindowsStore.Should().BeTrue();
#else
            isWindowsStore.Should().BeFalse();
#endif
        }
    }
}
