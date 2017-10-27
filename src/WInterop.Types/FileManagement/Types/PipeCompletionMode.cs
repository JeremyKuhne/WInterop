// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.FileManagement.Types
{
    public enum PipeCompletionMode : uint
    {
        // https://msdn.microsoft.com/en-us/library/windows/hardware/ff728845.aspx

        /// <summary>
        /// Blocking mode. [FILE_PIPE_QUEUE_OPERATION]
        /// </summary>
        QueueOperation = 0x0,

        /// <summary>
        /// Non-blocking mode. [FILE_PIPE_COMPLETE_OPERATION]
        /// </summary>
        CompleteOperation = 0x1
    }
}
