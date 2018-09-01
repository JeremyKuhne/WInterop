// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Gdi
{
    // https://msdn.microsoft.com/en-us/library/dd144877.aspx
    public enum DeviceCapability : int
    {
        /// <summary>
        /// Device driver version.
        /// </summary>
        DriverVersion = 0,

        /// <summary>
        /// Device classification.
        /// </summary>
        Technology = 2,

        /// <summary>
        /// Horizontal size in millimeters.
        /// </summary>
        HorizontalSize = 4,

        /// <summary>
        /// Vertical size in millimeters.
        /// </summary>
        VerticalSize = 6,

        /// <summary>
        /// Horizontal width in pixels.
        /// </summary>
        HorzontalResolution = 8,

        /// <summary>
        /// Vertical height in pixels.
        /// </summary>
        VerticalResolution = 10,

        /// <summary>
        /// Number of bits per pixel.
        /// </summary>
        BitsPerPixel = 12,

        Planes = 14,
        NumberOfBrushes = 16,
        NumberOfPens = 18,
        NumberOfMarkers = 20,
        NumberOfFonts = 22,
        NumberOfColors = 24,
        PDeviceSize = 26,
        CurveCapabilities = 28,
        LineCapabilities = 30,
        PolygonalCapabilities = 32,
        TextCapabilities = 34,
        ClipCapabilities = 36,
        RasterCapabilities = 38,
        AspectX = 40,
        AspectY = 42,
        AspectXY = 44,
        LogicalPixelsX = 88,
        LogicalPixelsY = 90,
        SizePalette = 104,
        NumberOfReserved = 106,
        ColorResolution = 108,
        PhysicalWidth = 110,
        PhysicalHeight = 111,
        PhysicalOffsetX = 112,
        PhysicalOffsetY = 113,
        ScalingFactorX = 114,
        ScalingFactorY = 115,
        VerticalRefresh = 116,
        DesktopVerticalResolution = 117,
        DesktopHorizontalResolution = 118,
        BlitAlignment = 119,
        ShadeBlendCapabilities = 120,
        ColorManagementCapabilities = 121
    }
}
