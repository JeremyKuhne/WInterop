// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Communications
{
    // https://msdn.microsoft.com/en-us/library/windows/desktop/aa363189.aspx
    public enum ProviderSubType : uint
    {
        PST_UNSPECIFIED     = 0x00000000,
        PST_RS232           = 0x00000001,
        PST_PARALLELPORT    = 0x00000002,
        PST_RS422           = 0x00000003,
        PST_RS423           = 0x00000004,
        PST_RS449           = 0x00000005,
        PST_MODEM           = 0x00000006,
        PST_FAX             = 0x00000021,
        PST_SCANNER         = 0x00000022,
        PST_NETWORK_BRIDGE  = 0x00000100,
        PST_LAT             = 0x00000101,
        PST_TCPIP_TELNET    = 0x00000102,
        PST_X25             = 0x00000103
    }
}
