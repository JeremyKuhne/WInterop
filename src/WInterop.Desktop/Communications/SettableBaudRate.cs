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
    public enum SettableBaudRate : uint
    {
        BAUD_075         = 0x00000001,
        BAUD_110         = 0x00000002,
        BAUD_134_5       = 0x00000004,
        BAUD_150         = 0x00000008,
        BAUD_300         = 0x00000010,
        BAUD_600         = 0x00000020,
        BAUD_1200        = 0x00000040,
        BAUD_1800        = 0x00000080,
        BAUD_2400        = 0x00000100,
        BAUD_4800        = 0x00000200,
        BAUD_7200        = 0x00000400,
        BAUD_9600        = 0x00000800,
        BAUD_14400       = 0x00001000,
        BAUD_19200       = 0x00002000,
        BAUD_38400       = 0x00004000,
        BAUD_56K         = 0x00008000,
        BAUD_128K        = 0x00010000,
        BAUD_115200      = 0x00020000,
        BAUD_57600       = 0x00040000,
        BAUD_USER        = 0x10000000
    }
}
