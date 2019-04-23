// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Storage
{
    /// <summary>
    /// [COPYFILE2_MESSAGE_ACTION]
    /// </summary>
    // https://msdn.microsoft.com/en-us/library/windows/desktop/hh449417.aspx
    public enum CopyFile2MessageAction
    {
        /// <summary>
        /// [COPYFILE2_PROGRESS_CONTINUE]
        /// </summary>
        Continue = 0,

        /// <summary>
        /// [COPYFILE2_PROGRESS_CANCEL]
        /// </summary>
        Cancel = 1,

        /// <summary>
        /// [COPYFILE2_PROGRESS_STOP]
        /// </summary>
        Stop = 2,

        /// <summary>
        /// [COPYFILE2_PROGRESS_QUIET]
        /// </summary>
        Quiet = 3,

        /// <summary>
        /// [COPYFILE2_PROGRESS_PAUSE]
        /// </summary>
        Pause = 4
    }
}
