// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace WInterop.Shell
{
    // https://msdn.microsoft.com/en-us/library/windows/desktop/bb762514.aspx
    [Flags]
    public enum KF_REDIRECTION_CAPABILITIES : uint
    {
        ALLOW_ALL = 0x000000FF,
        REDIRECTABLE = 0x00000001,
        DENY_ALL = 0x000FFF00,
        DENY_POLICY_REDIRECTED = 0x00000100,
        DENY_POLICY = 0x00000200,
        DENY_PERMISSIONS = 0x00000400
    }
}
