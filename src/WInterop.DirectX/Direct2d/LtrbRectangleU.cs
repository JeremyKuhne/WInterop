// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Drawing;

namespace WInterop.Direct2d
{
    // [D2D_RECT_U]
    public readonly struct LtrbRectangleU
    {
        public readonly uint Left;
        public readonly uint Top;
        public readonly uint Right;
        public readonly uint Bottom;

        public LtrbRectangleU(Rectangle rectangle)
        {
            Left = (uint)rectangle.Left;
            Top = (uint)rectangle.Top;
            Right = (uint)rectangle.Right;
            Bottom = (uint)rectangle.Bottom;
        }

        public static implicit operator Rectangle(LtrbRectangleU rect)
            => Rectangle.FromLTRB((int)rect.Left, (int)rect.Top, (int)rect.Right, (int)rect.Bottom);
    }
}
