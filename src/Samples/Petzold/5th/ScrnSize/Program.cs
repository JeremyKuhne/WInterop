// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using WInterop.Windows;

namespace ScrnSize;

/// <summary>
///  Sample from Programming Windows, 5th Edition.
///  Original (c) Charles Petzold, 1998
///  Figure 2-3, Pages 37-38.
/// </summary>
internal static class Program
{
    [STAThread]
    private static void Main()
    {
        int cxScreen, cyScreen;

        cxScreen = Windows.GetSystemMetrics(SystemMetric.ScreenWidth);
        cyScreen = Windows.GetSystemMetrics(SystemMetric.ScreenHeight);

        Windows.MessageBox($"The screen is {cxScreen} pixels wide by {cyScreen} pixels high.", "ScrnSize");
    }
}
