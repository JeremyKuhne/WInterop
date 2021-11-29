// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Gdi;

public enum RegionType : int
{
    /// <summary>
    ///  An error occurred. [ERROR]
    /// </summary>
    Error = 0,

    /// <summary>
    ///  Region is empty. [NULLREGION]
    /// </summary>
    Null = 1,

    /// <summary>
    ///  Region is a single rectangle. [SIMPLEREGION]
    /// </summary>
    Simple = 2,

    /// <summary>
    ///  Region is more than one rectangle. [COMPLEXREGION]
    /// </summary>
    Complex = 3
}