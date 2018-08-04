﻿// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Desktop.Communications.Types
{
    // https://msdn.microsoft.com/en-us/library/windows/desktop/aa363214.aspx
    public enum DtrControl : byte
    {
        DTR_CONTROL_DISABLE = 0x00,
        DTR_CONTROL_ENABLE = 0x01,
        DTR_CONTROL_HANDSHAKE = 0x02
    }
}
