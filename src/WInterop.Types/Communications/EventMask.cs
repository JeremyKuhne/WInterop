// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace WInterop.Desktop.Communications.Types
{
    // https://msdn.microsoft.com/en-us/library/windows/desktop/aa363479.aspx
    [Flags]
    public enum EventMask : uint
    {
        EV_RXCHAR          = 0x0001,
        EV_RXFLAG          = 0x0002,
        EV_TXEMPTY         = 0x0004,
        EV_CTS             = 0x0008,
        EV_DSR             = 0x0010,
        EV_RLSD            = 0x0020,
        EV_BREAK           = 0x0040,
        EV_ERR             = 0x0080,
        EV_RING            = 0x0100,
        EV_PERR            = 0x0200,
        EV_RX80FULL        = 0x0400,
        EV_EVENT1          = 0x0800,
        EV_EVENT2          = 0x1000
    }
}
