// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using WInterop.Windows;

namespace ConsoleTests;

public class GeneralConsole
{
    [Fact]
    public void GetConsoleWindow()
    {
        // There may or may not be a console window, more recently this is coming back valid in the test harness.
        WindowHandle window = WInterop.Console.Console.GetConsoleWindow();
        _ = Windows.GetActiveWindow();
    }
}
