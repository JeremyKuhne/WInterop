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
        Baud_075         = 0x00000001,
        Baud_110         = 0x00000002,
        Baud_134_5       = 0x00000004,
        Baud_150         = 0x00000008,
        Baud_300         = 0x00000010,
        Baud_600         = 0x00000020,
        Baud_1200        = 0x00000040,
        Baud_1800        = 0x00000080,
        Baud_2400        = 0x00000100,
        Baud_4800        = 0x00000200,
        Baud_7200        = 0x00000400,
        Baud_9600        = 0x00000800,
        Baud_14400       = 0x00001000,
        Baud_19200       = 0x00002000,
        Baud_38400       = 0x00004000,
        Baud_56K         = 0x00008000,
        Baud_128K        = 0x00010000,
        Baud_115200      = 0x00020000,
        Baud_57600       = 0x00040000,
        Baud_User        = 0x10000000
    }
}
