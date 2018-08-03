// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Storage
{
    // https://msdn.microsoft.com/en-us/library/windows/desktop/hh449417.aspx
    public enum COPYFILE2_MESSAGE_ACTION
    {
        COPYFILE2_PROGRESS_CONTINUE = 0,
        COPYFILE2_PROGRESS_CANCEL = 1,
        COPYFILE2_PROGRESS_STOP = 2,
        COPYFILE2_PROGRESS_QUIET = 3,
        COPYFILE2_PROGRESS_PAUSE = 4
    }
}
