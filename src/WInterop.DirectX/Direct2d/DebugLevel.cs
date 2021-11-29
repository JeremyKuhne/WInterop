// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Direct2d;

/// <summary>
///  [D2D1_DEBUG_LEVEL]
/// </summary>
/// <remarks><see cref="https://docs.microsoft.com/en-us/windows/desktop/api/d2d1/ne-d2d1-d2d1_debug_level"/></remarks>
public enum DebugLevel : uint
{
    /// <summary>
    ///  [D2D1_DEBUG_LEVEL_NONE]
    /// </summary>
    None = D2D1_DEBUG_LEVEL.D2D1_DEBUG_LEVEL_NONE,

    /// <summary>
    ///  [D2D1_DEBUG_LEVEL_ERROR]
    /// </summary>
    Error = D2D1_DEBUG_LEVEL.D2D1_DEBUG_LEVEL_ERROR,

    /// <summary>
    ///  [D2D1_DEBUG_LEVEL_WARNING]
    /// </summary>
    Warning = D2D1_DEBUG_LEVEL.D2D1_DEBUG_LEVEL_WARNING,

    /// <summary>
    ///  [D2D1_DEBUG_LEVEL_INFORMATION]
    /// </summary>
    Information = D2D1_DEBUG_LEVEL.D2D1_DEBUG_LEVEL_INFORMATION
}
