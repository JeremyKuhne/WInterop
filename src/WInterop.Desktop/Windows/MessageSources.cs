// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace WInterop.Windows
{
    [Flags]
    public enum MessageSources : uint
    {
        // https://msdn.microsoft.com/en-us/library/windows/desktop/ms644942.aspx

        /// <summary>
        ///  The message was sent using the SendMessage or SendMessageTimeout function.
        ///  If Replied [ISMEX_REPLIED] is not set, the thread that sent the message is blocked.
        ///  [ISMEX_SEND]
        /// </summary>
        Send = 0x00000001,

        /// <summary>
        ///  The message was sent using the SendNotifyMessage function.
        ///  The thread that sent the message is not blocked. [ISMEX_NOTIFY]
        /// </summary>
        Notify = 0x00000002,

        /// <summary>
        ///  The message was sent using the SendMessageCallback function.
        ///  The thread that sent the message is not blocked. [ISMEX_CALLBACK]
        /// </summary>
        Callback = 0x00000004,

        /// <summary>
        ///  The window procedure has processed the message.
        ///  The thread that sent the message is no longer blocked. [ISMEX_REPLIED]
        /// </summary>
        Replied = 0x00000008
    }
}