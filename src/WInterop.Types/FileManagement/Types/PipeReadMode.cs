// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.FileManagement.Types
{
    public enum PipeReadMode : uint
    {
        // https://msdn.microsoft.com/en-us/library/windows/hardware/ff728845.aspx

        /// <summary>
        /// Data is read from the pipe as a stream of bytes. [FILE_PIPE_BYTE_STREAM_MODE]
        /// </summary>
        StreamMode = 0x0,

        /// <summary>
        /// Data is read from the pipe as a stream of messages. [FILE_PIPE_MESSAGE_MODE]
        /// </summary>
        MessageMode = 0x1
    }
}
