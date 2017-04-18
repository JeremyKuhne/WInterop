// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace WInterop.Desktop.Communications.DataTypes
{
    // https://msdn.microsoft.com/en-us/library/windows/desktop/aa363189.aspx
    [Flags]
    public enum SettableParams : uint
    {
        SP_PARITY         = 0x0001,
        SP_BAUD           = 0x0002,
        SP_DATABITS       = 0x0004,
        SP_STOPBITS       = 0x0008,
        SP_HANDSHAKING    = 0x0010,
        SP_PARITY_CHECK   = 0x0020,
        SP_RLSD           = 0x0040
    }
}
