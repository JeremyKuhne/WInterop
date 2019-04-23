// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Security
{
    /// <summary>
    /// [SECURITY_CONTEXT_TRACKING_MODE]
    /// </summary>
    /// <remarks>
    /// <see cref="https://msdn.microsoft.com/en-us/library/cc234317.aspx"/>
    /// </remarks>
    public enum ContextTrackingMode : byte
    {
        /// <summary>
        /// The server is given a snapshot of the client's security context.
        /// [SECURITY_STATIC_TRACKING]
        /// </summary>
        Static = 0x00,

        /// <summary>
        /// The server is continually updated with changes.
        /// [SECURITY_DYNAMIC_TRACKING]
        /// </summary>
        Dynamic = 0x01
    }
}
