// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.ErrorHandling
{
    using System;

    [Flags]
    public enum MessageBeepType : uint
    {
        MB_OK = 0x00000000,
        MB_ICONERROR = 0x00000010,
        MB_ICONQUESTION = 0x00000020,
        MB_ICONWARNING = 0x00000030,
        MB_ICONASTERISK = 0x00000040,
        SimpleBeep = 0xFFFFFFFF
    }
}
