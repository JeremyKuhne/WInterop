// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Drawing;

namespace WInterop.Windows.Types
{
    // https://msdn.microsoft.com/en-us/library/windows/desktop/ms644958.aspx
    public struct MSG
    {
        public WindowHandle hwnd;
        public WindowMessage message;
        public WPARAM wParam;
        public LPARAM lParam;
        public uint time;
        public Point pt;
    }
}
