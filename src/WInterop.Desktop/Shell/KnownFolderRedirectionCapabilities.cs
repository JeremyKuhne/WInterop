// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace WInterop.Shell
{
    /// <summary>
    ///  [KF_REDIRECTION_CAPABILITIES]
    /// </summary>
    // https://msdn.microsoft.com/en-us/library/windows/desktop/bb762514.aspx
    [Flags]
    public enum KnownFolderRedirectionCapabilities : uint
    {
        AllowAll = 0x000000FF,
        Redirectable = 0x00000001,
        DenyAll = 0x000FFF00,
        DenyPolicyRedirected = 0x00000100,
        DenyPolicy = 0x00000200,
        DenyPermissions = 0x00000400
    }
}