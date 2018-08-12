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
    public struct BezierSegment
    {
        public PointF Point1;
        public PointF Point2;
        public PointF Point3;
    }
}
