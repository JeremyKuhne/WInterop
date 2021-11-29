// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using WInterop.Windows;

namespace DirectWriteDemo;

internal static class Program
{
    [STAThread]
    private static void Main()
    {
        Windows.CreateMainWindowAndRun(new HelloWorld(), "DirectWrite Hello World");
        Windows.CreateMainWindowAndRun(new CustomText(), "Custom Text Rendering");
    }
}
