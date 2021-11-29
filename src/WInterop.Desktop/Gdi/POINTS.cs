// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Drawing;

namespace WInterop.Gdi;

/// <summary>
///  Point with two <see cref="short"/> values. [POINTS]
/// </summary>
/// <docs>https://docs.microsoft.com/windows/win32/api/windef/ns-windef-points</docs>
public struct PointS
{
    public short X;
    public short Y;

    public PointS(short x, short y)
    {
        X = x;
        Y = y;
    }

    public static implicit operator Point(PointS point) => new Point(point.X, point.Y);
}