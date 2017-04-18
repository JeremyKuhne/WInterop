// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Gdi.DataTypes
{
    // https://msdn.microsoft.com/en-us/library/dd144877.aspx
    public enum DeviceCapability : int
    {
        /// <summary>
        /// Device driver version.
        /// </summary>
        DRIVERVERSION = 0,

        /// <summary>
        /// Device classification.
        /// </summary>
        TECHNOLOGY = 2,

        /// <summary>
        /// Horizontal size in millimeters.
        /// </summary>
        HORZSIZE = 4,

        /// <summary>
        /// Vertical size in millimeters.
        /// </summary>
        VERTSIZE = 6,

        /// <summary>
        /// Horizontal width in pixels.
        /// </summary>
        HORZRES = 8,

        /// <summary>
        /// Vertical height in pixels.
        /// </summary>
        VERTRES = 10,

        /// <summary>
        /// Number of bits per pixel.
        /// </summary>
        BITSPIXEL = 12,
        PLANES = 14,
        NUMBRUSHES = 16,
        NUMPENS = 18,
        NUMMARKERS = 20,
        NUMFONTS = 22,
        NUMCOLORS = 24,
        PDEVICESIZE = 26,
        CURVECAPS = 28,
        LINECAPS = 30,
        POLYGONALCAPS = 32,
        TEXTCAPS = 34,
        CLIPCAPS = 36,
        RASTERCAPS = 38,
        ASPECTX = 40,
        ASPECTY = 42,
        ASPECTXY = 44,
        LOGPIXELSX = 88,
        LOGPIXELSY = 90,
        SIZEPALETTE = 104,
        NUMRESERVED = 106,
        COLORRES = 108,
        PHYSICALWIDTH = 110,
        PHYSICALHEIGHT = 111,
        PHYSICALOFFSETX = 112,
        PHYSICALOFFSETY = 113,
        SCALINGFACTORX = 114,
        SCALINGFACTORY = 115,
        VREFRESH = 116,
        DESKTOPVERTRES = 117,
        DESKTOPHORZRES = 118,
        BLTALIGNMENT = 119,
        SHADEBLENDCAPS = 120,
        COLORMGMTCAPS = 121
    }
}
