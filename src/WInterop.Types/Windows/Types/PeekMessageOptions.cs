// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace WInterop.Windows.Types
{
    // https://msdn.microsoft.com/en-us/library/windows/desktop/ms644943.aspx
    [Flags]
    public enum PeekMessageOptions : uint
    {
        /// <summary>
        /// Messages are not removed from the queue after processing by PeekMessage.
        /// </summary>
        PM_NOREMOVE = 0x0000,

        /// <summary>
        /// Messages are removed from the queue after processing by PeekMessage.
        /// </summary>
        PM_REMOVE = 0x0001,

        /// <summary>
        /// Prevents the system from releasing any thread that is waiting for the caller to go idle.
        /// Combine this value with either PM_NOREMOVE or PM_REMOVE.
        /// </summary>
        PM_NOYIELD = 0x0002,

        /// <summary>
        /// Process mouse and keyboard messages.
        /// </summary>
        PM_QS_INPUT = QueueStatus.QS_INPUT << 16,

        /// <summary>
        /// Process all posted messages, including timers and hotkeys.
        /// </summary>
        PM_QS_POSTMESSAGE = (QueueStatus.QS_POSTMESSAGE | QueueStatus.QS_HOTKEY | QueueStatus.QS_TIMER) << 16,

        /// <summary>
        /// Process paint messages.
        /// </summary>
        PM_QS_PAINT = QueueStatus.QS_PAINT << 16,

        /// <summary>
        /// Process all sent messages.
        /// </summary>
        PM_QS_SENDMESSAGE = QueueStatus.QS_SENDMESSAGE << 16
    }
}
