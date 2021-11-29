// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Direct2d;

/// <summary>
///  Enum which describes the manner in which we render edges of non-text primitives. [D2D1_ANTIALIAS_MODE]
/// </summary>
public enum AntialiasMode : uint
{
    /// <summary>
    ///  The edges of each primitive are antialiased sequentially. [D2D1_ANTIALIAS_MODE_PER_PRIMITIVE]
    /// </summary>
    PerPrimitive = D2D1_ANTIALIAS_MODE.D2D1_ANTIALIAS_MODE_PER_PRIMITIVE,

    /// <summary>
    ///  Each pixel is rendered if its pixel center is contained by the geometry. [D2D1_ANTIALIAS_MODE_ALIASED]
    /// </summary>
    Aliased = D2D1_ANTIALIAS_MODE.D2D1_ANTIALIAS_MODE_ALIASED
}
