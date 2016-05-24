// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.FileManagement.DataTypes
{
    // https://msdn.microsoft.com/en-us/library/windows/desktop/aa365542.aspx
    public enum MoveMethod : uint
    {
        FILE_BEGIN = 0,
        FILE_CURRENT = 1,
        FILE_END = 2
    }
}
