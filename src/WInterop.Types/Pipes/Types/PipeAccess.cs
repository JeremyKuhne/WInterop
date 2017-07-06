// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Pipes.Types
{
    public enum PipeAccess : uint
    {
        /// <summary>
        /// Inbound [PIPE_ACCESS_INBOUND]
        /// </summary>
        Inbound = 0x00000001,

        /// <summary>
        /// Outbound [PIPE_ACCESS_OUTBOUND]
        /// </summary>
        Outbound = 0x00000002,

        /// <summary>
        /// Duplex [PIPE_ACCESS_DUPLEX]
        /// </summary>
        Duplex = 0x00000003
    }
}
