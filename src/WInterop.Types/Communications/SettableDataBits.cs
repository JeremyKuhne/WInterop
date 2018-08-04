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
    public enum SettableDataBits : ushort
    {
        DATABITS_5       = 0x0001,
        DATABITS_6       = 0x0002,
        DATABITS_7       = 0x0004,
        DATABITS_8       = 0x0008,
        DATABITS_16      = 0x0010,
        DATABITS_16X     = 0x0020
    }
}
