// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Drawing;

namespace WInterop.Direct2d
{
    /// <summary>
    /// Describes an arc that is defined as part of a path. [D2D1_ARC_SEGMENT]
    /// </summary>
    public struct ArcSegment
    {
        public PointF Point;
        public SizeF Size;
        public float FlotationAngle;
        public SweepDirection SweepDirection;
        public ArcSize ArcSize;
    }
}
