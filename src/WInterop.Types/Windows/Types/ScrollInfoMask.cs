// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace WInterop.Windows.Types
{
    // https://msdn.microsoft.com/en-us/library/windows/desktop/bb787537.aspx
    [Flags]
    public enum ScrollInfoMask : uint
    {
        SIF_RANGE           = 0x0001,
        SIF_PAGE            = 0x0002,
        SIF_POS             = 0x0004,
        SIF_DISABLENOSCROLL = 0x0008,
        SIF_TRACKPOS        = 0x0010,
        SIF_ALL             = (SIF_RANGE | SIF_PAGE | SIF_POS | SIF_TRACKPOS)
    }
}
