// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Communications;

// https://msdn.microsoft.com/en-us/library/windows/desktop/aa363479.aspx
[Flags]
public enum EventMask : uint
{
    RxChar = 0x0001,
    RxFlag = 0x0002,
    TxEmtp = 0x0004,
    Cts = 0x0008,
    Dsr = 0x0010,
    Rlsd = 0x0020,
    Break = 0x0040,
    Err = 0x0080,
    Ring = 0x0100,
    PErr = 0x0200,
    Rx80Full = 0x0400,
    Event1 = 0x0800,
    Event2 = 0x1000
}