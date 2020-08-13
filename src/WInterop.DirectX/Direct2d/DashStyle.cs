// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Direct2d
{
    /// <summary>
    ///  Describes the sequence of dashes and gaps in a stroke.
    ///  [D2D1_DASH_STYLE]
    /// </summary>
    public enum DashStyle : uint
    {
        /// <summary>
        ///  [D2D1_DASH_STYLE_SOLID]
        /// </summary>
        Solid = 0,

        /// <summary>
        ///  [D2D1_DASH_STYLE_DASH]
        /// </summary>
        Dash = 1,

        /// <summary>
        ///  [D2D1_DASH_STYLE_DOT]
        /// </summary>
        Dot = 2,

        /// <summary>
        ///  [D2D1_DASH_STYLE_DASH_DOT]
        /// </summary>
        DashDot = 3,

        /// <summary>
        ///  [D2D1_DASH_STYLE_DASH_DOT_DOT]
        /// </summary>
        DashDotDot = 4,

        /// <summary>
        ///  [D2D1_DASH_STYLE_CUSTOM]
        /// </summary>
        Custom = 5
    }
}
