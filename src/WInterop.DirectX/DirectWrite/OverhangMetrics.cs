// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.DirectWrite
{
    /// <summary>
    ///  The <see cref="OverhangMetrics"/> structure holds how much any visible pixels
    ///  (in DIPs) overshoot each side of the layout or inline objects. [DWRITE_OVERHANG_METRICS]
    /// </summary>
    /// <remarks>
    ///  Positive overhangs indicate that the visible area extends outside the layout
    ///  box or inline object, while negative values mean there is whitespace inside.
    ///  The returned values are unaffected by rendering transforms or pixel snapping.
    ///  Additionally, they may not exactly match final target's pixel bounds after
    ///  applying grid fitting and hinting.
    /// </remarks>
    public readonly struct OverhangMetrics
    {
        /// <summary>
        ///  The distance from the left-most visible DIP to its left alignment edge.
        /// </summary>
        public readonly float Left;

        /// <summary>
        ///  The distance from the top-most visible DIP to its top alignment edge.
        /// </summary>
        public readonly float Top;

        /// <summary>
        ///  The distance from the right-most visible DIP to its right alignment edge.
        /// </summary>
        public readonly float Right;

        /// <summary>
        ///  The distance from the bottom-most visible DIP to its bottom alignment edge.
        /// </summary>
        public readonly float Bottom;
    }
}
