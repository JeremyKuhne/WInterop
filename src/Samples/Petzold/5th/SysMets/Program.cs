// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using WInterop.Windows;

namespace SysMets;

/// <summary>
///  Sample from Programming Windows, 5th Edition.
///  Original (c) Charles Petzold, 1998
/// </summary>
internal static class Program
{
    [STAThread]
    private static void Main()
    {
        // Figure 4-5, Pages 91-93.
        Windows.CreateMainWindowAndRun(new SysMets1(), "System Metrics");

        // Figure 4-10, Pages 103-106.
        Windows.CreateMainWindowAndRun(new SysMets2(), "System Metrics with Scrollbar");

        // Figure 4-11, Pages 112-117.
        Windows.CreateMainWindowAndRun(new SysMets3(), "System metrics with both Scrollbars");

        // Figure 6-2, Pages 224-231.
        Windows.CreateMainWindowAndRun(new SysMets4(), "System metrics with scrollbars and keyboard support");

        // Figure 7-12, Pages 318-325.
        Windows.CreateMainWindowAndRun(new SysMets(), "Get system metrics with scroll wheel support");
    }
}
