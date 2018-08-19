// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.DirectWrite
{
    /// <summary>
    /// Geometry enclosing of text positions. [DWRITE_HIT_TEST_METRICS]
    /// </summary>
    public readonly struct HitTestMetrics
    {
        /// <summary>
        /// First text position within the geometry.
        /// </summary>
        public readonly uint TextPosition;

        /// <summary>
        /// Number of text positions within the geometry.
        /// </summary>
        public readonly uint Length;

        /// <summary>
        /// Left position of the top-left coordinate of the geometry.
        /// </summary>
        public readonly float Left;

        /// <summary>
        /// Top position of the top-left coordinate of the geometry.
        /// </summary>
        public readonly float Top;

        /// <summary>
        /// Geometry's width.
        /// </summary>
        public readonly float Width;

        /// <summary>
        /// Geometry's height.
        /// </summary>
        public readonly float Height;

        /// <summary>
        /// Bidi level of text positions enclosed within the geometry.
        /// </summary>
        public readonly uint BidiLevel;

        /// <summary>
        /// Geometry encloses text?
        /// </summary>
        public readonly BOOL IsText;

        /// <summary>
        /// Range is trimmed.
        /// </summary>
        public readonly BOOL IsTrimmed;
    }
}
