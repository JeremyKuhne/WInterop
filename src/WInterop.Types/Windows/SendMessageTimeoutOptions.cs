// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace WInterop.Windows
{
    [Flags]
    public enum SendMessageTimeoutOptions : uint
    {
        /// <summary>
        /// The calling thread is not prevented from processing other requests
        /// while waiting for the function to return. [SMTO_NORMAL]
        /// </summary>
        Normal = 0x0000,

        /// <summary>
        /// Prevents the calling thread from processing any other requests
        /// until the function returns. [SMTO_BLOCK]
        /// </summary>
        Block = 0x0001,

        /// <summary>
        /// The function returns without waiting for the time-out period to elapse
        /// if the receiving thread appears to not respond or "hangs." [SMTO_ABORTIFHUNG]
        /// </summary>
        AbortIfHung = 0x0002,

        /// <summary>
        /// The function does not enforce the time-out period as long as the receiving
        /// thread is processing messages. [SMTO_NOTIMEOUTIFNOTHUNG]
        /// </summary>
        NoTimeoutIfNotHung = 0x0008,

        /// <summary>
        /// The function should return 0 if the receiving window is destroyed or its
        /// owning thread dies while the message is being processed. [SMTO_ERRORONEXIT]
        /// </summary>
        ErrorOnExit = 0x0020
    }
}
