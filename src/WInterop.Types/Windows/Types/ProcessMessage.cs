// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Windows.Types
{
    // https://msdn.microsoft.com/en-us/library/windows/desktop/ms644943.aspx
    public enum ProcessMessage : uint
    {
        PM_NOREMOVE         = 0x0000,
        PM_REMOVE           = 0x0001,
        PM_NOYIELD          = 0x0002,
        PM_QS_INPUT         = QueueStatus.QS_INPUT << 16,
        PM_QS_POSTMESSAGE   = (QueueStatus.QS_POSTMESSAGE | QueueStatus.QS_HOTKEY | QueueStatus.QS_TIMER) << 16,
        PM_QS_PAINT         = QueueStatus.QS_PAINT << 16,
        PM_QS_SENDMESSAGE   = QueueStatus.QS_SENDMESSAGE << 16
    }
}
