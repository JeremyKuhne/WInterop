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
        public PointF Point { get; }
        public SizeF Size { get; }
        public float RotationAngle { get; }
        public SweepDirection SweepDirection { get; }
        public ArcSize ArcSize { get; }

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

        public ArcSegment(
            (float X, float Y) point,
            (float X, float Y) size,
            float rotationAngle = 0.0f,
            SweepDirection sweepDirection = SweepDirection.Clockwise,
            ArcSize arcSize = ArcSize.Small)
        {
            Point = new(point.X, point.Y);
            Size = new(size.X, size.Y);
            RotationAngle = rotationAngle;
            SweepDirection = sweepDirection;
            ArcSize = arcSize;
        }
    }
}
