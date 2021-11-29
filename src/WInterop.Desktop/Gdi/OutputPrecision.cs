// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Gdi;

// https://msdn.microsoft.com/en-us/library/cc250405.aspx
// https://msdn.microsoft.com/en-us/library/dd183499.aspx
public enum OutputPrecision : byte
{
    /// <summary>
    ///  Default. (OUT_DEFAULT_PRECIS)
    /// </summary>
    Default = 0,

    /// <summary>
    ///  Returned when enumerating raster fonts. (OUT_STRING_PRECIS)
    /// </summary>
    String = 1,

    /// <summary>
    ///  Not used. (OUT_CHARACTER_PRECIS)
    /// </summary>
    Character = 2,

    /// <summary>
    ///  Returned when enumerating vector fonts. (OUT_STROKE_PRECIS)
    /// </summary>
    Stroke = 3,

    /// <summary>
    ///  Choose a TrueType font when there are multiple fonts of the same name. (OUT_TT_PRECIS)
    /// </summary>
    TrueType = 4,

    /// <summary>
    ///  Choose a Device font when there are multiple fonts of the same name. (OUT_DEVICE_PRECIS)
    /// </summary>
    Device = 5,

    /// <summary>
    ///  Choose a raster font when there are multiple fonts of the same name. (OUT_RASTER_PRECIS)
    /// </summary>
    Raster = 6,

    /// <summary>
    ///  Choose from TrueType fonts only (if any are installed). (OUT_TT_ONLY_PRECIS)
    /// </summary>
    TrueTypeOnly = 7,

    /// <summary>
    ///  Choose from TrueType and other outline based fonts. (OUT_OUTLINE_PRECIS)
    /// </summary>
    Outline = 8,

    /// <summary>
    ///  Prefer TrueType and other outline based fonts. (OUT_SCREEN_OUTLINE_PRECIS)
    /// </summary>
    ScreenOutline = 9,

    /// <summary>
    ///  Choose from PostScript fonts only (if any are installed). (OUT_PS_ONLY_PRECIS)
    /// </summary>
    PostScriptOnly = 10
}