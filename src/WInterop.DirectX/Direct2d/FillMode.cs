// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Direct2d;

/// <summary>
///  Specifies how the intersecting areas of geometries or figures are combined to
///  form the area of the composite geometry. [D2D1_FILL_MODE]
/// </summary>
public enum FillMode : uint
{
    /// <summary>
    ///  [D2D1_FILL_MODE_ALTERNATE]
    /// </summary>
    Alternate = D2D1_FILL_MODE.D2D1_FILL_MODE_ALTERNATE,

    /// <summary>
    ///  [D2D1_FILL_MODE_WINDING]
    /// </summary>
    Winding = D2D1_FILL_MODE.D2D1_FILL_MODE_WINDING
}
