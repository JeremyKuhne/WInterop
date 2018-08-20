// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Direct3d
{
    /// <summary>
    /// [D3D_DRIVER_TYPE]
    /// </summary>
    public enum DriverType
    {
        Unknown = 0,
        Hardware = Unknown + 1,
        Reference = Hardware + 1,
        Null = Reference + 1,
        Software = Null + 1,
        Warp = Software + 1
    }
}
