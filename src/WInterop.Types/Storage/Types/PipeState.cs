// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Storage
{
    /// <summary>
    /// Named pipe connection status.
    /// </summary>
    /// <remarks>
    /// <see cref="https://msdn.microsoft.com/en-us/library/windows/hardware/ff728846.aspx"/>
    /// </remarks>
    public enum PipeState : uint
    {
        /// <summary>
        /// The pipe is disconnected. [FILE_PIPE_DISCONNECTED_STATE].
        /// </summary>
        Disconnected = 0x0,

        /// <summary>
        /// The pipe is waiting to establish a connection. [FILE_PIPE_LISTENING_STATE]
        /// </summary>
        Listening = 0x2,

        /// <summary>
        /// The pipe is connected. [FILE_PIPE_CONNECTED_STATE]
        /// </summary>
        Connected = 0x3,

        /// <summary>
        /// The pipe is closing. [FILE_PIPE_CLOSING_STATE]
        /// </summary>
        Closing = 0x4
    }
}
