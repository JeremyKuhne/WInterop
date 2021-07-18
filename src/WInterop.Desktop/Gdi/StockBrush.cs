// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Gdi
{
    // https://msdn.microsoft.com/en-us/library/dd183533.aspx
    public enum StockBrush : int
    {
        /// <summary>
        ///  White brush. (WHITE_BRUSH)
        /// </summary>
        White = 0,

        /// <summary>
        ///  Light gray brush. (LTGRAY_BRUSH)
        /// </summary>
        LightGray = 1,

        /// <summary>
        ///  Gray brush. (GRAY_BRUSH)
        /// </summary>
        Gray = 2,

        /// <summary>
        ///  Dark gray brush. (DKGRAY_BRUSH)
        /// </summary>
        DarkGray = 3,

        /// <summary>
        ///  Black brush. (BLACK_BRUSH)
        /// </summary>
        Black = 4,

        /// <summary>
        ///  Null (hollow) brush. (NULL_BRUSH)
        /// </summary>
        Null = 5,

        /// <summary>
        ///  Hollow (null) brush. (HOLLOW_BRUSH)
        /// </summary>
        Hollow = Null,

        /// <summary>
        ///  Device context brush. Color is changed via Get/SetDeviceContextBrushColor (DC_BRUSH)
        /// </summary>
        DeviceContextBrush = 18
    }
}