// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Direct2d;

/// <summary>
///  Describes the sequence of dashes and gaps in a stroke.
///  [D2D1_DASH_STYLE]
/// </summary>
public enum DashStyle : uint
{
    /// <summary>
    ///  [D2D1_DASH_STYLE_SOLID]
    /// </summary>
    Solid = D2D1_DASH_STYLE.D2D1_DASH_STYLE_SOLID,

    /// <summary>
    ///  [D2D1_DASH_STYLE_DASH]
    /// </summary>
    Dash = D2D1_DASH_STYLE.D2D1_DASH_STYLE_DASH,

    /// <summary>
    ///  [D2D1_DASH_STYLE_DOT]
    /// </summary>
    Dot = D2D1_DASH_STYLE.D2D1_DASH_STYLE_DOT,

    /// <summary>
    ///  [D2D1_DASH_STYLE_DASH_DOT]
    /// </summary>
    DashDot = D2D1_DASH_STYLE.D2D1_DASH_STYLE_DASH_DOT,

    /// <summary>
    ///  [D2D1_DASH_STYLE_DASH_DOT_DOT]
    /// </summary>
    DashDotDot = D2D1_DASH_STYLE.D2D1_DASH_STYLE_DASH_DOT_DOT,

    /// <summary>
    ///  [D2D1_DASH_STYLE_CUSTOM]
    /// </summary>
    Custom = D2D1_DASH_STYLE.D2D1_DASH_STYLE_CUSTOM
}
