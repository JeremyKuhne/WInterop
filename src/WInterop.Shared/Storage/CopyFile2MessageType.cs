// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Storage
{
    /// <summary>
    /// [COPYFILE2_MESSAGE_TYPE]
    /// </summary>
    // https://msdn.microsoft.com/en-us/library/windows/desktop/hh449420.aspx
    public enum CopyFile2MessageType
    {
        /// <summary>
        /// [COPYFILE2_CALLBACK_NONE]
        /// </summary>
        None = 0,

        /// <summary>
        /// [COPYFILE2_CALLBACK_CHUNK_STARTED]
        /// </summary>
        ChunkStarted = 1,

        /// <summary>
        /// [COPYFILE2_CALLBACK_CHUNK_FINISHED]
        /// </summary>
        ChunkFinished = 2,

        /// <summary>
        /// [COPYFILE2_CALLBACK_STREAM_STARTED]
        /// </summary>
        StreamStarted = 3,

        /// <summary>
        /// [COPYFILE2_CALLBACK_STREAM_FINISHED]
        /// </summary>
        StreamFinished = 4,

        /// <summary>
        /// [COPYFILE2_CALLBACK_POLL_CONTINUE]
        /// </summary>
        PollContinue = 5,

        /// <summary>
        /// [COPYFILE2_CALLBACK_ERROR]
        /// </summary>
        Error = 6
    }
}
