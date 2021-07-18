// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Drawing;

namespace WInterop.Windows
{
    /// <summary>
    ///  [MOUSEMOVEPOINT]
    /// </summary>
    /// <docs>https://docs.microsoft.com/windows/win32/api/winuser/ns-winuser-mousemovepoint</docs>
    public readonly struct MouseMovePoint
    {
        public readonly int X;
        public readonly int Y;
        public readonly uint Time;
        public readonly IntPtr ExtraInfo;

        public MouseMovePoint(Point point, uint time = 0)
        {
            X = point.X;
            Y = point.Y;
            Time = time;
            ExtraInfo = default;
        }
    }
}