// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.DirectWrite;

/// <summary>
///  Specifies algorithmic style simulations to be applied to the font face.
///  Bold and oblique simulations can be combined via bitwise OR operation.
///  [DWRITE_FONT_SIMULATIONS]
/// </summary>
[Flags]
public enum FontSimulations : uint
{
    /// <summary>
    ///  No simulations are performed.
    /// </summary>
    None = DWRITE_FONT_SIMULATIONS.DWRITE_FONT_SIMULATIONS_NONE,

    /// <summary>
    ///  Algorithmic emboldening is performed.
    /// </summary>
    Bold = DWRITE_FONT_SIMULATIONS.DWRITE_FONT_SIMULATIONS_BOLD,

    /// <summary>
    ///  Algorithmic italicization is performed.
    /// </summary>
    Oblique = DWRITE_FONT_SIMULATIONS.DWRITE_FONT_SIMULATIONS_OBLIQUE
}
