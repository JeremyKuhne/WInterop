// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Gdi;

/// <summary>
///  Device context capability query options. <see cref="Gdi.GetDeviceCapability(in DeviceContext, DeviceCapability)" />
/// </summary>
/// <remarks>
///  https://docs.microsoft.com/windows/desktop/api/wingdi/nf-wingdi-getdevicecaps
/// </remarks>
public enum DeviceCapability : int
{
    /// <summary>
    ///  Device driver version.
    /// </summary>
    DriverVersion = 0,

    /// <summary>
    ///  Device technology.
    /// </summary>
    Technology = 2,

    /// <summary>
    ///  Horizontal size of the physical screen in millimeters. [HORSIZE]
    /// </summary>
    HorizontalSize = 4,

    /// <summary>
    ///  Vertical size of the physical screen in millimeters. [VERTSIZE]
    /// </summary>
    VerticalSize = 6,

    /// <summary>
    ///  Horizontal width in pixels. [HORZRES]
    /// </summary>
    HorzontalResolution = 8,

    /// <summary>
    ///  Vertical height in pixels. [VERTRES]
    /// </summary>
    VerticalResolution = 10,

    /// <summary>
    ///  Number of bits per pixel. [BITSPIXEL]
    /// </summary>
    BitsPerPixel = 12,

    /// <summary>
    ///  Number of color planes. [PLANES]
    /// </summary>
    Planes = 14,

    /// <summary>
    ///  Number of device-specific brushes. [NUMBRUSHES]
    /// </summary>
    NumberOfBrushes = 16,

    /// <summary>
    ///  Number of device-specific pens. [NUMPENS]
    /// </summary>
    NumberOfPens = 18,

    NumberOfMarkers = 20,

    /// <summary>
    ///  Number of device-specific fonts. [NUMFONTS]
    /// </summary>
    NumberOfFonts = 22,

    /// <summary>
    ///  Number of entries in the device's color table, if the device has a color depth
    ///  of no more than 8 bits per pixel. Otherwise returns 1. [NUMCOLORS]
    /// </summary>
    NumberOfColors = 24,

    // Reserved
    // PDeviceSize = 26,

    /// <summary>
    ///  [CURVECAPS]
    /// </summary>
    CurveCapabilities = 28,

    /// <summary>
    ///  [LINECAPS]
    /// </summary>
    LineCapabilities = 30,

    /// <summary>
    ///  [POLYGONALCAPS]
    /// </summary>
    PolygonalCapabilities = 32,

    /// <summary>
    ///  [TEXTCAPS]
    /// </summary>
    TextCapabilities = 34,

    /// <summary>
    ///  [CLIPCAPS]
    /// </summary>
    ClipCapabilities = 36,

    /// <summary>
    ///  [RASTERCAPS]
    /// </summary>
    RasterCapabilities = 38,

    /// <summary>
    ///  [ASPECTX]
    /// </summary>
    AspectX = 40,

    /// <summary>
    ///  [ASPECTY]
    /// </summary>
    AspectY = 42,

    /// <summary>
    ///  [ASPECTXY]
    /// </summary>
    AspectXY = 44,

    /// <summary>
    ///  Number of pixels per logical inch in width. [LOGPIXELSX]
    /// </summary>
    LogicalPixelsX = 88,

    /// <summary>
    ///  Number of pixels per logical inch in height. [LOGPIXELSY]
    /// </summary>
    LogicalPixelsY = 90,

    /// <summary>
    ///  Number of entries in physical palette. [SIZEPALETTE]
    /// </summary>
    SizePalette = 104,

    /// <summary>
    ///  Number of reserved entries in palette. [NUMRESERVED]
    /// </summary>
    NumberOfReserved = 106,

    /// <summary>
    ///  Actual color resolution. [COLORRES]
    /// </summary>
    ColorResolution = 108,

    /// <summary>
    ///  Printer physical width in device units. [PHYSICALWIDTH]
    /// </summary>
    PhysicalWidth = 110,

    /// <summary>
    ///  Printer physical height in device units. [PHYSICALHEIGHT]
    /// </summary>
    PhysicalHeight = 111,

    /// <summary>
    ///  Printer printable area offset. [PHYSICALOFFSETX]
    /// </summary>
    PhysicalOffsetX = 112,

    /// <summary>
    ///  Printer printable area offset. [PHYSICALOFFSETY]
    /// </summary>
    PhysicalOffsetY = 113,

    /// <summary>
    ///  Printer X scaling factor. [SCALINGFACTORX]
    /// </summary>
    ScalingFactorX = 114,

    /// <summary>
    ///  Printer Y scaling factor. [SCALINGFACTORY]
    /// </summary>
    ScalingFactorY = 115,

    /// <summary>
    ///  [VREFRESH]
    /// </summary>
    VerticalRefresh = 116,

    /// <summary>
    ///  Width of the entire desktop in pixels. [DESKTOPVERTRES]
    /// </summary>
    DesktopVerticalResolution = 117,

    /// <summary>
    ///  Height of the entire desktop in pixels. [DESKTOPHORZRES]
    /// </summary>
    DesktopHorizontalResolution = 118,

    /// <summary>
    ///  Preferred Blitting alignment. 1 if hardware accelerated. [BLTALIGNMENT]
    /// </summary>
    BlitAlignment = 119,

    /// <summary>
    ///  [SHADEBLENDCAPS]
    /// </summary>
    ShadeBlendCapabilities = 120,

    /// <summary>
    ///  [COLORMGMTCAPS]
    /// </summary>
    ColorManagementCapabilities = 121
}