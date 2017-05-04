// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using WInterop.Windows;

namespace HelloMsg
{
    /// <summary>
    /// Sample from Programming Windows, 5th Edition.
    /// Original (c) Charles Petzold, 1998
    /// Page 14.
    /// </summary>
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Windows.MessageBox("Hello, Windows 98!", "HelloMsg", 0);
        }
    }
}
