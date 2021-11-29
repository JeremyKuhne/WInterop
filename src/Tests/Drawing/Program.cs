// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using WInterop.Windows;

namespace Windows101;

internal class Program
{
    [STAThread]
    private static void Main()
    {
        var context = Windows.SetThreadDpiAwarenessContext(DpiAwarenessContext.PerMonitorV2);
        Windows.CreateMainWindowAndRun(new DrawLines(), windowTitle: "Draw Lines");
        Windows.CreateMainWindowAndRun(new DrawRectangle(), windowTitle: "Draw Rectangles");
    }
}
