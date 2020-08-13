// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace WInterop.Communications
{
    // https://msdn.microsoft.com/en-us/library/windows/desktop/aa363189.aspx
    [Flags]
    public enum SettableDataBits : ushort
    {
        DataBits5       = 0x0001,
        DataBits6       = 0x0002,
        DataBits7       = 0x0004,
        DataBits8       = 0x0008,
        DataBits16      = 0x0010,
        DataBits16x     = 0x0020
    }
}
