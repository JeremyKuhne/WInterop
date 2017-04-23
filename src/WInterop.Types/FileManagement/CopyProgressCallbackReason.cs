// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.FileManagement.Types
{
    /// <summary>
    /// Reason for the <a href="https://msdn.microsoft.com/en-us/library/windows/desktop/aa363854.aspx">CopyProgressRoutine</a> callback.
    /// </summary>
    public enum CopyProgressCallbackReason : uint
    {
        /// <summary>
        /// Another part of the data file was copied.
        /// </summary>
        CALLBACK_CHUNK_FINISHED = 0x00000000,

        /// <summary>
        /// Another stream was created and is about to be copied. This is the callback reason given when the callback routine is first invoked.
        /// </summary>
        CALLBACK_STREAM_SWITCH = 0x00000001
    }
}
