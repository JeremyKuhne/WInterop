// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Direct2d;

/// <summary>
///  This enumeration describes the type of combine operation to be performed.
///  [D2D1_COMBINE_MODE]
/// </summary>
public enum CombineMode : uint
{
    /// <summary>
    ///  Produce a geometry representing the set of points contained in either the first
    ///  or the second geometry. [D2D1_COMBINE_MODE_UNION]
    /// </summary>
    Union = 0,

    /// <summary>
    ///  Produce a geometry representing the set of points common to the first and the
    ///  second geometries. [D2D1_COMBINE_MODE_INTERSECT]
    /// </summary>
    Intersect = 1,

    /// <summary>
    ///  Produce a geometry representing the set of points contained in the first
    ///  geometry or the second geometry, but not both. [D2D1_COMBINE_MODE_XOR]
    /// </summary>
    XOr = 2,

    /// <summary>
    ///  Produce a geometry representing the set of points contained in the first
    ///  geometry but not the second geometry. [D2D1_COMBINE_MODE_EXCLUDE]
    /// </summary>
    Exclude = 3,
}
