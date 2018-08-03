// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Storage
{
    /// <summary>
    /// Named pipe configuration.
    /// </summary>
    /// <remarks>
    /// <see cref="https://msdn.microsoft.com/en-us/library/windows/hardware/ff728846.aspx"/>
    /// </remarks>
    public enum PipeConfiguration : uint
    {
        /// <summary>
        /// The flow of data in the pipe goes from client to server only.
        /// [FILE_PIPE_INBOUND]
        /// </summary>
        Inbound = 0x0,

        /// <summary>
        /// The flow of data in the pipe goes from server to client only.
        /// [FILE_PIPE_OUTBOUND]
        /// </summary>
        Outbound = 0x1,

        /// <summary>
        /// The pipe is bidirectional; both server and client processes can read from
        /// and write to the pipe. [FILE_PIPE_FULL_DUPLEX]
        /// </summary>
        FullDuplex = 0x2
    }
}
