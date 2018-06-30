// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace WInterop.Storage.Types
{
    // https://msdn.microsoft.com/en-us/library/windows/desktop/aa364419.aspx
    [Flags]
    public enum FindFirstFileExFlags : uint
    {
        FIND_FIRST_EX_CASE_SENSITIVE = 1,
        FIND_FIRST_EX_LARGE_FETCH = 2
    }
}
