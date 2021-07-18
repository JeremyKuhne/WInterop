// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace WInterop.Communications
{
    // https://msdn.microsoft.com/en-us/library/windows/desktop/aa363189.aspx
    [Flags]
    public enum SettableStopAndParityBits : ushort
    {
        StopBits10       = 0x0001,
        StopBits15       = 0x0002,
        StopBits20       = 0x0004,
        ParityNone       = 0x0100,
        ParityOdd        = 0x0200,
        ParityEven       = 0x0400,
        ParityMark       = 0x0800,
        ParitySpace      = 0x1000
    }
}