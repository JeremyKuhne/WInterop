// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Authorization
{
    /// <summary>
    /// <a href="https://msdn.microsoft.com/en-us/library/windows/desktop/aa379572.aspx">SECURITY_IMPERSONATION_LEVEL</a> enumeration values.
    /// [SECURITY_IMPERSONATION_LEVEL]
    /// </summary>
    public enum ImpersonationLevel : uint
    {
        /// <summary>
        /// The server process cannot obtain identification information about the client and cannot impersonate the client.
        /// [SecurityAnonymous]
        /// </summary>
        Anonymous,

        /// <summary>
        /// The server process can obtain identification information about the client, but cannot impersonate the client.
        /// [SecurityIdentification]
        /// </summary>
        Identification,

        /// <summary>
        /// The server process can impersonate the client's security context on it's local system.
        /// [SecurityImpersonation]
        /// </summary>
        Impersonation,

        /// <summary>
        /// The server process can impersonate the client's security context on remote systems.
        /// [SecurityDelegation]
        /// </summary>
        Delegation
    }
}
