// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.FileManagement.Types
{
    // https://msdn.microsoft.com/en-us/library/windows/desktop/hh449420.aspx
    public enum COPYFILE2_MESSAGE_TYPE
    {
        COPYFILE2_CALLBACK_NONE = 0,
        COPYFILE2_CALLBACK_CHUNK_STARTED = 1,
        COPYFILE2_CALLBACK_CHUNK_FINISHED = 2,
        COPYFILE2_CALLBACK_STREAM_STARTED = 3,
        COPYFILE2_CALLBACK_STREAM_FINISHED = 4,
        COPYFILE2_CALLBACK_POLL_CONTINUE = 5,
        COPYFILE2_CALLBACK_ERROR = 6,
        COPYFILE2_CALLBACK_MAX
    }
}
