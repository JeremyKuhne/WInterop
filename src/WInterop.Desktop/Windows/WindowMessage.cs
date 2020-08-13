// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Drawing;

namespace WInterop.Windows
{
    /// <summary>
    ///  Windows Message. [MSG]
    /// </summary>
    public readonly struct WindowMessage
    {
        public readonly WindowHandle Window;
        public readonly MessageType Type;
        public readonly WParam WParam;
        public readonly LParam LParam;
        public readonly uint Time;
        public readonly Point Point;

        public WindowMessage(
            WindowHandle window,
            MessageType type,
            WParam wParam = default,
            LParam lParam = default,
            uint time = 0,
            Point point = default)
        {
            Window = window;
            Type = type;
            WParam = wParam;
            LParam = lParam;
            Time = time;
            Point = point;
        }
    }
}
