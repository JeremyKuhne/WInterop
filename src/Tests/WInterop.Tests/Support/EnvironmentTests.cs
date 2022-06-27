// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using WInterop.WindowsStore;

namespace SupportTests;

public class EnvironmentTests
{
    [Fact]
    public void IsWindowsStore()
    {
        bool isWindowsStore = WindowsStore.IsWindowsStoreApplication();
#if WINRT
            isWindowsStore.Should().BeTrue();
#else
        isWindowsStore.Should().BeFalse();
#endif
    }
}
