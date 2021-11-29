// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Com;

/// <summary>
///  [DROPEFFECT]
/// </summary>
[Flags]
public enum DropEffect : uint
{
    /// <summary>
    ///  [DROPEFFECT_NONE]
    /// </summary>
    None = 0,

    /// <summary>
    ///  [DROPEFFECT_COPY]
    /// </summary>
    Copy = 1,

    /// <summary>
    ///  [DROPEFFECT_MOVE]
    /// </summary>
    Move = 2,

    /// <summary>
    ///  [DROPEFFECT_LINK]
    /// </summary>
    Link = 4,

    /// <summary>
    ///  [DROPEFFECT_LINK]
    /// </summary>
    Scroll = 0x80000000
}