// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Drawing;

namespace WInterop.Direct2d
{
    /// <summary>
    ///  [D2D_POINT_U]
    /// </summary>
    public readonly struct PointU
    {
        public readonly uint X;
        public readonly uint Y;

        public PointU(uint x, uint y)
        {
            X = x;
            Y = y;
        }

        public static implicit operator Point(PointU point) => new Point((int)point.X, (int)point.Y);
        public static implicit operator PointU(Point point) => new PointU((uint)point.X, (uint)point.Y);
    }
}
