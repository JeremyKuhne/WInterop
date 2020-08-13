// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Drawing;

namespace WInterop.Direct2d
{
    /// <summary>
    ///  Describes an arc that is defined as part of a path. [D2D1_ARC_SEGMENT]
    /// </summary>
    public readonly struct ArcSegment
    {
        public readonly PointF Point;
        public readonly SizeF Size;
        public readonly float RotationAngle;
        public readonly SweepDirection SweepDirection;
        public readonly ArcSize ArcSize;

        public ArcSegment(
            PointF point,
            SizeF size,
            float rotationAngle = 0.0f,
            SweepDirection sweepDirection = SweepDirection.Clockwise,
            ArcSize arcSize = ArcSize.Small)
        {
            Point = point;
            Size = size;
            RotationAngle = rotationAngle;
            SweepDirection = sweepDirection;
            ArcSize = arcSize;
        }
    }
}
