// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace WInterop.Windows
{
    // https://msdn.microsoft.com/en-us/library/windows/desktop/ms644943.aspx
    [Flags]
    public enum PeekMessageOptions : uint
    {
        /// <summary>
        ///  Messages are not removed from the queue after processing by PeekMessage. [PM_NOREMOVE]
        /// </summary>
        NoRemove = 0x0000,

        /// <summary>
        ///  Messages are removed from the queue after processing by PeekMessage. [PM_REMOVE]
        /// </summary>
        Remove = 0x0001,

        /// <summary>
        ///  Prevents the system from releasing any thread that is waiting for the caller to go idle.
        ///  Combine this value with either <see cref="Remove"/> or <see cref="NoRemove"/>.
        ///  [PM_NOYIELD]
        /// </summary>
        NoYield = 0x0002,

        /// <summary>
        ///  Process mouse and keyboard messages. [PM_QS_INPUT]
        /// </summary>
        Input = QueueStatus.Input << 16,

        /// <summary>
        ///  Process all posted messages, including timers and hotkeys. [PM_QS_POSTMESSAGE]
        /// </summary>
        PostMessage = (QueueStatus.PostMessage | QueueStatus.HotKey | QueueStatus.Timer) << 16,

        /// <summary>
        ///  Process paint messages. [PM_QS_PAINT]
        /// </summary>
        Paint = QueueStatus.Paint << 16,

        /// <summary>
        ///  Process all sent messages. [PM_QS_SENDMESSAGE]
        /// </summary>
        SendMessage = QueueStatus.SendMessage << 16
    }
}
