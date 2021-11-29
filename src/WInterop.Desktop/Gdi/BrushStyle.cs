// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Gdi;

public enum BrushStyle : uint
{
    /// <summary>
    ///  [BS_SOLID]
    /// </summary>
    Solid = 0,

    /// <summary>
    ///  [BS_NULL]
    /// </summary>
    Null = 1,

    /// <summary>
    ///  [BS_HOLLOW]
    /// </summary>
    Hollow = Null,

    /// <summary>
    ///  [BS_HATCHED]
    /// </summary>
    Hatched = 2,

    /// <summary>
    ///  [BS_PATTERN]
    /// </summary>
    Pattern = 3,

    /// <summary>
    ///  [BS_INDEXED]
    /// </summary>
    Indexed = 4,

    /// <summary>
    ///  [BS_DIBPATTERN]
    /// </summary>
    DibPattern = 5,

    /// <summary>
    ///  [BS_DIBPATTERNPT]
    /// </summary>
    DibPatternPointer = 6,

    /// <summary>
    ///  [BS_PATTERN8X8]
    /// </summary>
    Pattern8x8 = 7,

    /// <summary>
    ///  [BS_DIBPATTERN8X8]
    /// </summary>
    DibPattern8x8 = 8,

    /// <summary>
    ///  [BS_MONOPATTERN]
    /// </summary>
    MonoPattern = 9
}