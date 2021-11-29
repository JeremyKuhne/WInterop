// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Direct2d
{
    /// <summary>
    ///  Enum which describes the drawing of the ends of a line.
    ///  [D2D1_CAP_STYLE]
    /// </summary>
    public enum CapStyle : uint
    {
        /// <summary>
        ///  Flat line cap. [D2D1_CAP_STYLE_FLAT]
        /// </summary>
        Flat = D2D1_CAP_STYLE.D2D1_CAP_STYLE_FLAT,

        /// <summary>
        ///  Square line cap. [D2D1_CAP_STYLE_SQUARE]
        /// </summary>
        Square = D2D1_CAP_STYLE.D2D1_CAP_STYLE_SQUARE,

        /// <summary>
        ///  Round line cap. [D2D1_CAP_STYLE_ROUND]
        /// </summary>
        Round = D2D1_CAP_STYLE.D2D1_CAP_STYLE_ROUND,

        /// <summary>
        ///  Triangle line cap. [D2D1_CAP_STYLE_TRIANGLE]
        /// </summary>
        Triangle = D2D1_CAP_STYLE.D2D1_CAP_STYLE_TRIANGLE
    }
}
