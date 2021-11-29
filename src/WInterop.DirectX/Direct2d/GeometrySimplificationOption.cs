// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Direct2d;

/// <summary>
///  Specifies how simple the output of a simplified geometry sink should be.
///  [D2D1_GEOMETRY_SIMPLIFICATION_OPTION]
/// </summary>
public enum GeometrySimplificationOption : uint
{
    /// <summary>
    ///  [D2D1_GEOMETRY_SIMPLIFICATION_OPTION_CUBICS_AND_LINES]
    /// </summary>
    CubicsAndLines = 0,

    /// <summary>
    ///  [D2D1_GEOMETRY_SIMPLIFICATION_OPTION_LINES]
    /// </summary>
    Lines = 1
}
