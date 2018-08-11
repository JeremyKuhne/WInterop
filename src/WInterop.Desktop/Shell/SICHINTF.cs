// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace WInterop.Shell
{
    // https://msdn.microsoft.com/en-us/library/windows/desktop/bb762543.aspx
    [Flags]
    public enum SICHINTF : uint
    {
        DISPLAY = 0,
        ALLFIELDS = 0x80000000,
        CANONICAL = 0x10000000,
        TEST_FILESYSPATH_IF_NOT_EQUAL = 0x20000000
    }
}
