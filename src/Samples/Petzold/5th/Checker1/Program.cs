// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using WInterop.Windows;

namespace Checker
{
    /// <summary>
    ///  Sample from Programming Windows, 5th Edition.
    ///  Original (c) Charles Petzold, 1998
    /// </summary>
    internal static class Program
    {
        [STAThread]
        private static void Main()
        {
            // Drawn in main Window (Figure 7-4, Pages 288-290.)
            Windows.CreateMainWindowAndRun(new Checker1(), "Checker1 Mouse Hit-Test Demo");

            // With keyboard control (Figure 7-6, Pages 293-296.)
            Windows.CreateMainWindowAndRun(new Checker2(), "Checker2 Mouse Hit-Test Demo");

            // With child windows (Figure 7-7, Pages 299-301.)
            Windows.CreateMainWindowAndRun(new Checker3(), "Checker3 Mouse Hit-Test Demo");

            // Child windows with keyboard control. (Figure 7-8, Pages 303-308.)
            Windows.CreateMainWindowAndRun(new Checker4(), "Checker4 Mouse Hit-Test Demo");
        }
    }
}
