// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Windows
{
    // https://msdn.microsoft.com/en-us/library/windows/desktop/bb787593.aspx
    public enum ScrollWindowFlags : uint
    {
        SW_SCROLLCHILDREN  = 0x0001,
        SW_INVALIDATE      = 0x0002,
        SW_ERASE           = 0x0004,
        SW_SMOOTHSCROLL    = 0x0010
    }
}
