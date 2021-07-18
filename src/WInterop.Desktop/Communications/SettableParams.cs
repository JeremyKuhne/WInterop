// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace WInterop.Communications
{
    // https://msdn.microsoft.com/en-us/library/windows/desktop/aa363189.aspx
    [Flags]
    public enum SettableParams : uint
    {
        Parity         = 0x0001,
        Baud           = 0x0002,
        DataBits       = 0x0004,
        StopBits       = 0x0008,
        HandShaking    = 0x0010,
        ParityCheck    = 0x0020,
        Rlsd           = 0x0040
    }
}