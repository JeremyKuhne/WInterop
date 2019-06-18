// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Direct2d
{
    /// <summary>
    /// This specifies the precision that should be used in buffers allocated by D2D.
    /// [D2D1_BUFFER_PRECISION]
    /// </summary>
    public enum BufferPrecision : uint
    {
        Unknown = 0,
        NormalizedInteger8 = 1,
        NormalizedInteger8Rgb = 2,
        NormalizedInteger16 = 3,
        Float16 = 4,
        Float32 = 5,
    }
}
