// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Gdi;

/// <summary>
///  [ORIENTATION_PREFERENCE]
/// </summary>
/// <msdn><see cref="https://msdn.microsoft.com/en-us/library/dn376360.aspx"/></msdn>
public enum OrientationPreference
{
    /// <summary>
    ///  [ORIENTATION_PREFERENCE_NONE]
    /// </summary>
    None = 0x0,

    /// <summary>
    ///  [ORIENTATION_PREFERENCE_LANDSCAPE]
    /// </summary>
    Landscape = 0x1,

    /// <summary>
    ///  [ORIENTATION_PREFERENCE_PORTRAIT]
    /// </summary>
    Portrait = 0x2,

    /// <summary>
    ///  [ORIENTATION_PREFERENCE_LANDSCAPE_FLIPPED]
    /// </summary>
    LandscapeFlipped = 0x4,

    /// <summary>
    ///  [ORIENTATION_PREFERENCE_PORTRAIT_FLIPPED]
    /// </summary>
    PortraitFlipped = 0x8
}