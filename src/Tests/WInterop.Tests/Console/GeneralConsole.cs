// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using FluentAssertions;
using WInterop.Windows;
using Xunit;

namespace ConsoleTests;

public class GeneralConsole
{
    [Fact]
    public void GetConsoleWindow()
    {
        // Xunit tests in core don't have an attached console window
        WindowHandle window = WInterop.Console.Console.GetConsoleWindow();
        window.IsInvalid.Should().BeTrue();
        _ = Windows.GetActiveWindow();
    }
}
