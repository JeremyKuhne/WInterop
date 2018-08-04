// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace WInterop.Communications
{
    // https://msdn.microsoft.com/en-us/library/windows/desktop/aa363189.aspx
    [Flags]
    public enum SettableStopAndParityBits : ushort
    {
        STOPBITS_10      = 0x0001,
        STOPBITS_15      = 0x0002,
        STOPBITS_20      = 0x0004,
        PARITY_NONE      = 0x0100,
        PARITY_ODD       = 0x0200,
        PARITY_EVEN      = 0x0400,
        PARITY_MARK      = 0x0800,
        PARITY_SPACE     = 0x1000
    }
}
