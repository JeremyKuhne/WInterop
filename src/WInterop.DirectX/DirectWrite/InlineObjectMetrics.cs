// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.DirectWrite
{
    /// <summary>
    /// Properties describing the geometric measurement of an
    /// application-defined inline object.
    /// </summary>
    public struct InlineObjectMetrics
    {
        /// <summary>
        /// Width of the inline object.
        /// </summary>
        public readonly float Width;

        /// <summary>
        /// Height of the inline object as measured from top to bottom.
        /// </summary>
        public readonly float Height;

        /// <summary>
        /// Distance from the top of the object to the baseline where it is lined up with the adjacent text.
        /// If the baseline is at the bottom, baseline simply equals height.
        /// </summary>
        public readonly float Baseline;

        /// <summary>
        /// Flag indicating whether the object is to be placed upright or alongside the text baseline
        /// for vertical text.
        /// </summary>
        public readonly BOOL SupportsSideways;
    }
}
