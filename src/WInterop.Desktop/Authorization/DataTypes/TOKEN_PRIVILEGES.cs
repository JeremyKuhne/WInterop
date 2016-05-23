// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Runtime.InteropServices;

namespace WInterop.Authorization.Desktop
{
    /// <summary>
    /// <a href="https://msdn.microsoft.com/en-us/library/windows/desktop/aa379630.aspx">TOKEN_PRIVILEGES</a> structure.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct TOKEN_PRIVILEGES
    {
        public uint PrivilegeCount;

        [MarshalAs(UnmanagedType.ByValArray)]
        public LUID_AND_ATTRIBUTES[] Privileges;
    }
}
