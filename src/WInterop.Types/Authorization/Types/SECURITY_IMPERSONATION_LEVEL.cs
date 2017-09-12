// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Authorization.Types
{
    /// <summary>
    /// <a href="https://msdn.microsoft.com/en-us/library/windows/desktop/aa379572.aspx">SECURITY_IMPERSONATION_LEVEL</a> enumeration values.
    /// </summary>
    public enum SECURITY_IMPERSONATION_LEVEL : uint
    {
        /// <summary>
        /// The server process cannot obtain identification information about the client and cannot impersonate the client.
        /// </summary>
        SecurityAnonymous,

        /// <summary>
        /// The server process can obtain identification information about the client, but cannot impersonate the client.
        /// </summary>
        SecurityIdentification,

        /// <summary>
        /// The server process can impersonate the client's security context on it's local system.
        /// </summary>
        SecurityImpersonation,

        /// <summary>
        /// The server process can impersonate the client's security context on remote systems.
        /// </summary>
        SecurityDelegation
    }
}
