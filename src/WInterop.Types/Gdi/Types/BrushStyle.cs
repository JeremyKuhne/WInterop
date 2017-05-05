// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Gdi.Types
{
    public enum BrushStyle : uint
    {
        BS_SOLID           = 0,
        BS_NULL            = 1,
        BS_HOLLOW          = BS_NULL,
        BS_HATCHED         = 2,
        BS_PATTERN         = 3,
        BS_INDEXED         = 4,
        BS_DIBPATTERN      = 5,
        BS_DIBPATTERNPT    = 6,
        BS_PATTERN8X8      = 7,
        BS_DIBPATTERN8X8   = 8,
        BS_MONOPATTERN     = 9
    }
}
