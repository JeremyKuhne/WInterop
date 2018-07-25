// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace WInterop.Gdi.Types
{
    // https://msdn.microsoft.com/en-us/library/dd162741.aspx
    [Flags]
    public enum TextMetricFlags : uint
    {
        NTM_ITALIC          = 0x00000001,
        NTM_BOLD            = 0x00000020,
        NTM_REGULAR         = 0x00000040,
        NTM_NONNEGATIVE_AC  = 0x00010000,
        NTM_PS_OPENTYPE     = 0x00020000,
        NTM_TT_OPENTYPE     = 0x00040000,
        NTM_MULTIPLEMASTER  = 0x00080000,
        NTM_TYPE1           = 0x00100000,
        NTM_DSIG            = 0x00200000
    }
}
