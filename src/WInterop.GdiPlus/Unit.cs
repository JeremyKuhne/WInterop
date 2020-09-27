// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.GdiPlus
{
    public enum Unit
    {
        /// <summary>
        ///  Non-physical unit.
        /// </summary>
        World,

        /// <summary>
        ///  Variable.
        /// </summary>
        Display,

        /// <summary>
        ///  Each unit is one device pixel.
        /// </summary>
        Pixel,

        /// <summary>
        ///  Each unity is a printer's point, or 1/72 inch.
        /// </summary>
        Point,

        /// <summary>
        ///  Each unit is one inch.
        /// </summary>
        Inch,

        /// <summary>
        ///  Each unit is 1/300 inch.
        /// </summary>
        Document,

        /// <summary>
        ///  Each unit is 1 millimeter.
        /// </summary>
        Millimeter  // 6 -- Each unit is 1 millimeter.
    }
}
