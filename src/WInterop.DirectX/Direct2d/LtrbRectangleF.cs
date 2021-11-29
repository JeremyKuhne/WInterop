// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Drawing;

namespace WInterop.Direct2d;

// [D2D_RECT_F] Also defined as [D2D1_RECT_F]
// https://docs.microsoft.com/en-us/windows/desktop/api/dcommon/ns-dcommon-d2d_rect_f
public readonly struct LtrbRectangleF
{
    public float Left { get; }
    public float Top { get; }
    public float Right { get; }
    public float Bottom { get; }

    public LtrbRectangleF(RectangleF rectangle)
    {
        Left = rectangle.Left;
        Top = rectangle.Top;
        Right = rectangle.Right;
        Bottom = rectangle.Bottom;
    }

    public LtrbRectangleF(SizeF size)
    {
        Left = 0;
        Top = 0;
        Right = size.Width;
        Bottom = size.Height;
    }

    public static implicit operator RectangleF(LtrbRectangleF rect)
        => RectangleF.FromLTRB(rect.Left, rect.Top, rect.Right, rect.Bottom);

    public static unsafe implicit operator LtrbRectangleF((float Left, float Top, float Right, float Bottom) rectangle)
        => *(LtrbRectangleF*)&rectangle;

    public static implicit operator LtrbRectangleF(RectangleF rectangle) => new(rectangle);

    public static implicit operator LtrbRectangleF(Rectangle rectangle) => new(rectangle);

    public static implicit operator LtrbRectangleF(SizeF size) => new(size);
}
