// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace WInterop.Windows.Types
{
    [Flags]
    public enum TrackMouseEvents : uint
    {
        TME_HOVER       = 0x00000001,
        TME_LEAVE       = 0x00000002,
        TME_NONCLIENT   = 0x00000010,
        TME_QUERY       = 0x40000000,
        TME_CANCEL      = 0x80000000
    }
}
