// ------------------------
//    WInterop Framework
// ------------------------

// Copyright [c] Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Drawing;

namespace WInterop.Windows
{
    /// <summary>
    /// [MSG]
    /// </summary>
    public readonly struct WindowMessage
    {
        public readonly WindowHandle Window;
        public readonly MessageType Type;
        public readonly WPARAM wParam;
        public readonly LPARAM lParam;
        public readonly uint Time;
        public readonly Point Point;

        public WindowMessage(
            WindowHandle window,
            MessageType type,
            WPARAM wParam = default,
            LPARAM lParam = default,
            uint time = 0,
            Point point = default)
        {
            Window = window;
            Type = type;
            this.wParam = wParam;
            this.lParam = lParam;
            Time = time;
            Point = point;
        }
    }
}
