// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Drawing;

namespace WInterop.Direct2d
{
    // [D2D_RECT_F] Also defined as [D2D1_RECT_F]
    // https://docs.microsoft.com/en-us/windows/desktop/api/dcommon/ns-dcommon-d2d_rect_f
    public readonly struct LtrbRectangleF
    {
        public readonly float Left;
        public readonly float Top;
        public readonly float Right;
        public readonly float Bottom;

        public LtrbRectangleF(RectangleF rectangle)
        {
            Left = rectangle.Left;
            Top = rectangle.Top;
            Right = rectangle.Right;
            Bottom = rectangle.Bottom;
        }

        public static implicit operator RectangleF(LtrbRectangleF rect)
            => RectangleF.FromLTRB(rect.Left, rect.Top, rect.Right, rect.Bottom);

        public static implicit operator LtrbRectangleF(RectangleF rectangle)
            => new LtrbRectangleF(rectangle);

        public static implicit operator LtrbRectangleF(Rectangle rectangle)
            => new LtrbRectangleF(rectangle);
    }
}
