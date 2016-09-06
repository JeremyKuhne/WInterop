// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace WInterop.Shell.DataTypes
{
    // https://msdn.microsoft.com/en-us/library/windows/desktop/bb762515.aspx
    [Flags]
    public enum KF_REDIRECT_FLAGS : uint
    {
        USER_EXCLUSIVE = 0x00000001,
        COPY_SOURCE_DACL = 0x00000002,
        OWNER_USER = 0x00000004,
        SET_OWNER_EXPLICIT = 0x00000008,
        CHECK_ONLY = 0x00000010,
        WITH_UI = 0x00000020,
        UNPIN = 0x00000040,
        PIN = 0x00000080,
        COPY_CONTENTS = 0x00000200,
        DEL_SOURCE_CONTENTS = 0x00000400,
        EXCLUDE_ALL_KNOWN_SUBFOLDERS = 0x00000800
    }
}
