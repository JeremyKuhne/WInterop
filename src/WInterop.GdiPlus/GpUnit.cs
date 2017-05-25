// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.GdiPlus
{
    public enum GpUnit
    {
        // https://msdn.microsoft.com/en-us/library/ms534405.aspx

        /// <summary>
        /// Specifies world coordinates, a nonphysical unit.
        /// </summary>
        UnitWorld = 0,

        /// <summary>
        /// Specifies display units. For example, if the display device is a monitor, then the unit is 1 pixel.
        /// </summary>
        UnitDisplay = 1,

        /// <summary>
        /// Specifies that a unit is 1 pixel.
        /// </summary>
        UnitPixel = 2,

        /// <summary>
        /// Specifies that a unit is 1 point or 1/72 inch.
        /// </summary>
        UnitPoint = 3,

        /// <summary>
        /// Specifies that a unit is 1 inch.
        /// </summary>
        UnitInch = 4,

        /// <summary>
        /// Specifies that a unit is 1/300 inch.
        /// </summary>
        UnitDocument = 5,

        /// <summary>
        /// Specifies that a unit is 1 millimeter. 
        /// </summary>
        UnitMillimeter = 6
    }
}
