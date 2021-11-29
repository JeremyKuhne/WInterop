// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Drawing;

namespace WInterop.Gdi;

/// <summary>
///  Win32 rectangle [RECT] [RECTL]
/// </summary>
/// <remarks>
///  <see cref="https://docs.microsoft.com/windows/win32/api/windef/ns-windef-rect"/>
/// </remarks>
public readonly struct Rect
{
    public readonly int Left;
    public readonly int Top;
    public readonly int Right;
    public readonly int Bottom;

    public Rect(int left, int top, int right, int bottom)
    {
        Left = left;
        Top = top;
        Right = right;
        Bottom = bottom;
    }

    public bool PointIsInside(Point point)
        => (point.X >= Left) && (point.X < Right) && (point.Y >= Top) && (point.Y < Bottom);

    public Rect Offset(int dx, int dy)
        => new(Left + dx, Top + dy, Right + dx, Bottom + dy);

    public Rect Inflate(int dx, int dy)
        => new(Left - dx, Top + dy, Right + dx, Bottom - dy);

    public bool Intersects(Rect rect)
        => !Intersect(rect).IsEmpty();

    public Rect Intersect(Rect rect)
    {
        int maxLeft = Left > rect.Left ? Left : rect.Left;
        int minRight = Right < rect.Right ? Right : rect.Right;

        if (maxLeft < minRight)
        {
            int maxTop = Top > rect.Top ? Top : rect.Top;
            int minBottom = Bottom < rect.Bottom ? Bottom : rect.Bottom;

            if (maxTop < minBottom)
                return new Rect(maxLeft, maxTop, minRight, minBottom);
        }

        return default;
    }

    /// <summary>
    ///  Empty rect is one that has no area.
    /// </summary>
    public bool IsEmpty() => (Left >= Right) || (Top >= Bottom);

    public static implicit operator Rectangle(Rect rect)
        => Rectangle.FromLTRB(rect.Left, rect.Top, rect.Right, rect.Bottom);

    public static implicit operator Rect(Rectangle rectangle)
        => new(rectangle.Left, rectangle.Top, rectangle.Right, rectangle.Bottom);
}