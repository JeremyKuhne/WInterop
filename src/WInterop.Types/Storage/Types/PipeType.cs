// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Storage
{
    /// <summary>
    /// Named pipe type.
    /// </summary>
    /// <remarks>
    /// <see cref="https://msdn.microsoft.com/en-us/library/windows/hardware/ff728846.aspx"/>
    /// </remarks>
    public enum PipeType : uint
    {
        /// <summary>
        /// Data is read from the pipe as a stream of bytes. [FILE_PIPE_BYTE_STREAM_TYPE]
        /// </summary>
        /// <remarks>
        /// To use this type, <see cref="PipeReadMode"/> must not be <see cref="PipeReadMode.MessageMode"/>.
        /// </remarks>
        ByteStreamType = 0x0,

        /// <summary>
        /// Data is read from the pipe as a stream of messages. [FILE_PIPE_MESSAGE_TYPE]
        /// </summary>
        PipeMessageType = 0x1
    }
}
