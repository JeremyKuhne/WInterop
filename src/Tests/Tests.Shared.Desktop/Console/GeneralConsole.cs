// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using FluentAssertions;
using WInterop.Console;
using WInterop.Windows;
using Xunit;

namespace ConsoleTests
{
    public class GeneralConsole
    {
        [Fact]
        public void GetConsoleWindow()
        {
            // Tests run with an attached console. (This changed recently...)
            WindowHandle window = Console.GetConsoleWindow();
            window.IsInvalid.Should().BeFalse();
            window = Windows.GetActiveWindow();
        }
    }
}
