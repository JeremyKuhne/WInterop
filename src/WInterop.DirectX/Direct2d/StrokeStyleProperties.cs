// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Direct2d
{
    /// <summary>
    /// Properties, aside from the width, that allow geometric penning to be specified.
    /// [D2D1_STROKE_STYLE_PROPERTIES]
    /// </summary>
    public readonly struct StrokeStyleProperties
    {
        public readonly CapStyle StartCap;
        public readonly CapStyle EndCap;
        public readonly CapStyle DashCap;
        public readonly LineJoin LineJoin;
        public readonly float MiterLimit;
        public readonly DashStyle DashStyle;
        public readonly float DashOffset;
    }
}
