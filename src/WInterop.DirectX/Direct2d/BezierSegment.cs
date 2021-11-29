// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Drawing;

namespace WInterop.Direct2d
{
    /// <summary>
    ///  Describes a cubic bezier in a path. [D2D1_BEZIER_SEGMENT]
    /// </summary>
    public readonly struct BezierSegment
    {
        public PointF Point1 { get; }
        public PointF Point2 { get; }
        public PointF Point3 { get; }

        public BezierSegment(PointF point1, PointF point2, PointF point3)
        {
            Point1 = point1;
            Point2 = point2;
            Point3 = point3;
        }

        public BezierSegment((float X, float Y) point1, (float X, float Y) point2, (float X, float Y) point3)
        {
            Point1 = new(point1.X, point1.Y);
            Point2 = new(point2.X, point2.Y);
            Point3 = new(point3.X, point3.Y);
        }
    }
}
