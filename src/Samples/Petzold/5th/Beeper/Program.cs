// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using WInterop.Windows;

namespace Beeper
{
    /// <summary>
    ///  Samples from Programming Windows, 5th Edition.
    ///  Original (c) Charles Petzold, 1998
    /// </summary>
    internal static class Program
    {
        [STAThread]
        private static void Main()
        {
            // Figure 8-1, Pages 331-333.
            Windows.CreateMainWindowAndRun(new Beeper1(), "Timer on Message Loop");

            // Figure 8-2, Pages 335-337.
            Windows.CreateMainWindowAndRun(new Beeper2(), "Timer Procedure");
        }
    }
}
