// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Desktop.Communications.DataTypes
{
    // https://msdn.microsoft.com/en-us/library/windows/desktop/aa363214.aspx
    public enum Parity : byte
    {
        NOPARITY = 0,
        ODDPARITY = 1,
        EVENPARITY = 2,
        MARKPARITY = 3,
        SPACEPARITY = 4
    }
}
