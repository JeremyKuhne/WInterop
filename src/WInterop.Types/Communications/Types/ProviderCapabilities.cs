// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace WInterop.Desktop.Communications.Types
{
    // https://msdn.microsoft.com/en-us/library/windows/desktop/aa363189.aspx
    [Flags]
    public enum ProviderCapabilities : uint
    {
        PCF_DTRDSR        = 0x0001,
        PCF_RTSCTS        = 0x0002,
        PCF_RLSD          = 0x0004,
        PCF_PARITY_CHECK  = 0x0008,
        PCF_XONXOFF       = 0x0010,
        PCF_SETXCHAR      = 0x0020,
        PCF_TOTALTIMEOUTS = 0x0040,
        PCF_INTTIMEOUTS   = 0x0080,
        PCF_SPECIALCHARS  = 0x0100,
        PCF_16BITMODE     = 0x0200
    }
}
