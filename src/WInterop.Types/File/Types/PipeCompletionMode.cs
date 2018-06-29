// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.File.Types
{
    /// <summary>
    /// The completion mode of the pipe.
    /// </summary>
    /// <remarks>
    /// <see cref="https://msdn.microsoft.com/en-us/library/windows/hardware/ff728845.aspx"/>
    /// <see cref="https://msdn.microsoft.com/en-us/library/cc232082.aspx"/>
    /// </remarks>
    public enum PipeCompletionMode : uint
    {
        /// <summary>
        /// Blocking mode. When the pipe is being connected, read to, or written from, the operation
        /// is not completed until there is data to read, all data is written, or a client is
        /// connected. Use of this mode can result in the server waiting indefinitely for a client
        /// process to perform an action. [FILE_PIPE_QUEUE_OPERATION]
        /// </summary>
        QueueOperation = 0x0,

        /// <summary>
        /// Non-blocking mode. When the pipe is being connected, read to, or written from, the
        /// operation completes immediately. [FILE_PIPE_COMPLETE_OPERATION]
        /// </summary>
        CompleteOperation = 0x1
    }
}
