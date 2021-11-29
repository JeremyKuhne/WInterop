// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Gdi;

/// <summary>
///  Page-space to device-space transformation.
/// </summary>
// https://msdn.microsoft.com/en-us/library/dd145045.aspx
public enum MappingMode
{
    /// <summary>
    ///  Pixel mapping. Each unit in page space is one pixel in device space.
    ///  X increases left to right. Y increases from top to bottom. (MM_TEXT)
    /// </summary>
    Text = 1,

    /// <summary>
    ///  Each unit in page space is 0.1 mm in device space.
    ///  X increases left to right. Y increases from bottom to top. (MM_LOMETRIC)
    /// </summary>
    LowMetric = 2,

    /// <summary>
    ///  Each unit in page space is 0.01 mm in device space.
    ///  X increases left to right. Y increases from bottom to top. (MM_HIMETRIC)
    /// </summary>
    HighMetric = 3,

    /// <summary>
    ///  Each unit in page space is 0.01 inch in device space.
    ///  X increases left to right. Y increases from bottom to top. (MM_LOENGLISH)
    /// </summary>
    LowEnglish = 4,

    /// <summary>
    ///  Each unit in page space is 0.001 inch in device space.
    ///  X increases left to right. Y increases from bottom to top. (MM_HIENGLISH)
    /// </summary>
    HighEnglish = 5,

    /// <summary>
    ///  Each unit in page space is a twip in device space (1/1440 inch).
    ///  X increases left to right. Y increases from bottom to top. (MM_TWIPS)
    /// </summary>
    Twips = 6,

    /// <summary>
    ///  Application defined via SetWindow/ViewPortExtents.
    ///  Both axis are equally scaled. (MM_ISOTROPIC)
    /// </summary>
    Isotropic = 7,

    /// <summary>
    ///  Application defined via SetWindow/ViewPortExtents.
    ///  Axis may not be equally scaled. (MM_ANISOTROPIC)
    /// </summary>
    Anisotropic = 8
}