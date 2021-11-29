// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace WInterop.Gdi;

[Flags]
public enum BoundsState : uint
{
    /// <summary>
    ///  Bounding rectangle is empty. [DCB_RESET]
    /// </summary>
    Reset = 0x0001,

    /// <summary>
    ///  [DCB_ACCUMULATE]
    /// </summary>
    Accumulate = 0x0002,

    /// <summary>
    ///  [DCB_DIRTY]
    /// </summary>
    Dirty = Accumulate,

    /// <summary>
    ///  Bounding rectangle is not empty. [DCB_SET]
    /// </summary>
    Set = Reset | Accumulate,

    /// <summary>
    ///  Boundary accumulation is on. [DCB_ENABLE]
    /// </summary>
    Enable = 0x0004,

    /// <summary>
    ///  Boundary accumulation is off. [DCB_DISABLE]
    /// </summary>
    Disable = 0x0008
}