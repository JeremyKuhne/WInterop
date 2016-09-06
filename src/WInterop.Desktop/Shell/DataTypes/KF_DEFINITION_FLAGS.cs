// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace WInterop.Shell.DataTypes
{
    // https://msdn.microsoft.com/en-us/library/windows/desktop/bb762513.aspx
    [Flags]
    public enum KF_DEFINITION_FLAGS : uint
    {
        KFDF_LOCAL_REDIRECT_ONLY = 0x00000002,
        KFDF_ROAMABLE = 0x00000004,
        KFDF_PRECREATE = 0x00000008,
        KFDF_STREAM = 0x00000010,
        KFDF_PUBLISHEXPANDEDPATH = 0x00000020,
        KFDF_NO_REDIRECT_UI = 0x00000040
    }
}
