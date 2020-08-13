// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace WInterop.Communications
{
    // https://msdn.microsoft.com/en-us/library/windows/desktop/aa363189.aspx
    [Flags]
    public enum ProviderCapabilities : uint
    {
        DtrDsr        = 0x0001,
        RtsCts        = 0x0002,
        Rlsd          = 0x0004,
        ParityCheck   = 0x0008,
        XOnXOff       = 0x0010,
        SetXChar      = 0x0020,
        TotalTimeouts = 0x0040,
        IntTimeouts   = 0x0080,
        SpecialChars  = 0x0100,
        Mode16Bit     = 0x0200
    }
}
