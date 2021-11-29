// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Direct2d;

/// <summary>
///  Enum which describes the drawing of the corners on the line. [D2D1_LINE_JOIN]
/// </summary>
public enum LineJoin : uint
{
    /// <summary>
    ///  Miter join. [D2D1_LINE_JOIN_MITER]
    /// </summary>
    Miter = D2D1_LINE_JOIN.D2D1_LINE_JOIN_MITER,

    /// <summary>
    ///  Bevel join. [D2D1_LINE_JOIN_BEVEL]
    /// </summary>
    Bevel = D2D1_LINE_JOIN.D2D1_LINE_JOIN_BEVEL,

    /// <summary>
    ///  Round join. [D2D1_LINE_JOIN_ROUND]
    /// </summary>
    Round = D2D1_LINE_JOIN.D2D1_LINE_JOIN_ROUND,

    /// <summary>
    ///  Miter/Bevel join. [D2D1_LINE_JOIN_MITER_OR_BEVEL]
    /// </summary>
    MiterOrBevel = D2D1_LINE_JOIN.D2D1_LINE_JOIN_MITER_OR_BEVEL
}
