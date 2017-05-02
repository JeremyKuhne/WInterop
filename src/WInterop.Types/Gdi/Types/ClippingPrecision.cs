// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace WInterop.Gdi.Types
{
    // https://msdn.microsoft.com/en-us/library/dd183499.aspx
    [Flags]
    public enum ClippingPrecision : byte
    {
        CLIP_DEFAULT_PRECIS     = 0,
        CLIP_CHARACTER_PRECIS   = 1,
        CLIP_STROKE_PRECIS      = 2,
        CLIP_LH_ANGLES          = (1<<4),
        CLIP_TT_ALWAYS          = (2<<4),
        CLIP_DFA_DISABLE        = (4<<4),
        CLIP_EMBEDDED           = (8<<4)
    }
}
