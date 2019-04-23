// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Storage
{
    /// <summary>
    /// Results for the <a href="https://msdn.microsoft.com/en-us/library/windows/desktop/aa363854.aspx">CopyProgressRoutine</a> callback.
    /// </summary>
    public enum CopyProgressResult : uint
    {
        /// <summary>
        /// Continue the copy operation. [PROGRESS_CONTINUE]
        /// </summary>
        Continue = 0,

        /// <summary>
        /// Cancel the copy operation and delete the destination file. [PROGRESS_CANCEL]
        /// </summary>
        Cancel = 1,

        /// <summary>
        /// Stop the copy operation. It can be restarted at a later time. [PROGRESS_STOP]
        /// </summary>
        Stop = 2,

        /// <summary>
        /// Continue the copy operation, but stop invoking CopyProgressRoutine to report progress. [PROGRESS_QUIET]
        /// </summary>
        Quiet = 3
    }
}
