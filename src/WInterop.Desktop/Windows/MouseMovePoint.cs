// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Drawing;

namespace WInterop.Windows
{
    /// <summary>
    /// [MOUSEMOVEPOINT]
    /// </summary>
    /// <remarks><see cref="https://msdn.microsoft.com/en-us/library/windows/desktop/ms645603.aspx"/></remarks>
    public readonly struct MouseMovePoint
    {
        public readonly int x;
        public readonly int y;
        public readonly uint time;
        public readonly IntPtr dwExtraInfo;

        public MouseMovePoint(Point point, uint time = 0)
        {
            x = point.X;
            y = point.Y;
            this.time = time;
            dwExtraInfo = default;
        }
    }
}
