// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace WInterop.Gdi.Types
{
    // https://msdn.microsoft.com/en-us/library/dd145132.aspx
    [Flags]
    public enum PitchAndFamily : byte
    {
        TMPF_FIXED_PITCH    = 0x01,
        TMPF_VECTOR         = 0x02,
        TMPF_DEVICE         = 0x08,
        TMPF_TRUETYPE       = 0x04,
        FF_DONTCARE         = 0 << 4,
        FF_ROMAN            = 1 << 4,
        FF_SWISS            = 2 << 4,
        FF_MODERN           = 3 << 4,
        FF_SCRIPT           = 4 << 4,
        FF_DECORATIVE       = 5 << 4
    }
}
