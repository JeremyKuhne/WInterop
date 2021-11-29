// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using WInterop.Windows;

namespace Direct2dDemo
{
    internal static class Program
    {
        [STAThread]
        private static void Main()
        {
            Windows.CreateMainWindowAndRun(new CombineGeometries(), "Combine geometries");
            Windows.CreateMainWindowAndRun(new Direct2dDemo(), "Direct2d Sample App");
            Windows.CreateMainWindowAndRun(new DrawEllipse(), "Drawing ellipses");
            Windows.CreateMainWindowAndRun(new PathGeometries(), "Path geometries");
        }
    }
}
