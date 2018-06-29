// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.


namespace WInterop.FileManagement.Types
{
    // https://msdn.microsoft.com/en-us/library/windows/desktop/aa363866.aspx
    public enum SYMBOLIC_LINK_FLAG : uint
    {
        SYMBOLIC_LINK_FLAG_FILE = 0x0,
        SYMBOLIC_LINK_FLAG_DIRECTORY = 0x1
    }
}
