// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Authorization.Types
{
    // https://msdn.microsoft.com/en-us/library/cc234317.aspx
    public enum SECURITY_CONTEXT_TRACKING_MODE : byte
    {
        /// <summary>
        /// The server is given a snapshot of the client's security context.
        /// </summary>
        SECURITY_STATIC_TRACKING = 0x00,

        /// <summary>
        /// The server is continually updated with changes.
        /// </summary>
        SECURITY_DYNAMIC_TRACKING = 0x01
    }
}
