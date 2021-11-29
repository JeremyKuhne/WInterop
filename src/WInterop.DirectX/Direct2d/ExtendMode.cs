// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Direct2d;

/// <summary>
///  Enum which describes how to sample from a source outside its base tile. [D2D1_EXTEND_MODE]
/// </summary>
public enum ExtendMode : uint
{
    /// <summary>
    ///  Extend the edges of the source out by clamping sample points outside the source
    ///  to the edges. [D2D1_EXTEND_MODE_CLAMP]
    /// </summary>
    Clamp = D2D1_EXTEND_MODE.D2D1_EXTEND_MODE_CLAMP,

    /// <summary>
    ///  The base tile is drawn untransformed and the remainder are filled by repeating
    ///  the base tile. [D2D1_EXTEND_MODE_WRAP]
    /// </summary>
    Wrap = D2D1_EXTEND_MODE.D2D1_EXTEND_MODE_WRAP,

    /// <summary>
    ///  The same as wrap, but alternate tiles are flipped  The base tile is drawn
    ///  untransformed. [D2D1_EXTEND_MODE_MIRROR]
    /// </summary>
    Mirror = D2D1_EXTEND_MODE.D2D1_EXTEND_MODE_MIRROR
}
