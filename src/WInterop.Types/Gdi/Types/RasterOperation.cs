// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Gdi.Types
{
    /// <summary>
    /// Mix modes for raster operations (ROPs)
    /// </summary>
    public enum RasterOperation : int
    {
        R2_BLACK           = 1,
        R2_NOTMERGEPEN     = 2,
        R2_MASKNOTPEN      = 3,
        R2_NOTCOPYPEN      = 4,
        R2_MASKPENNOT      = 5,
        R2_NOT             = 6,
        R2_XORPEN          = 7,
        R2_NOTMASKPEN      = 8,
        R2_MASKPEN         = 9,
        R2_NOTXORPEN       = 10,
        R2_NOP             = 11,
        R2_MERGENOTPEN     = 12,
        R2_COPYPEN         = 13,
        R2_MERGEPENNOT     = 14,
        R2_MERGEPEN        = 15,
        R2_WHITE           = 16
    }
}
