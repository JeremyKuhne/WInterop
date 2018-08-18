// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Drawing;

namespace WInterop.Direct2d
{
    /// <summary>
    /// Describes a cubic bezier in a path. [D2D1_BEZIER_SEGMENT]
    /// </summary>
    public readonly struct BezierSegment
    {
        public readonly PointF Point1;
        public readonly PointF Point2;
        public readonly PointF Point3;

        public BezierSegment(PointF point1, PointF point2, PointF point3)
        {
            Point1 = point1;
            Point2 = point2;
            Point3 = point3;
        }

        public BezierSegment((float X, float Y) point1, (float X, float Y) point2, (float X, float Y) point3)
        {
            Point1 = new PointF(point1.X, point1.Y);
            Point2 = new PointF(point2.X, point2.Y);
            Point3 = new PointF(point3.X, point3.Y);
        }
    }
}
