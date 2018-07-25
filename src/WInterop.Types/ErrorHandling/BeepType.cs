// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace WInterop.ErrorHandling.Types
{
    // https://msdn.microsoft.com/en-us/library/windows/desktop/ms680356.aspx
    [Flags]
    public enum BeepType : uint
    {
        /// <summary>
        /// Default beep sound. (MB_OK)
        /// </summary>
        Ok = 0x00000000,

        /// <summary>
        /// Critical stop sound. (MB_ICONERROR)
        /// </summary>
        Error = 0x00000010,

        /// <summary>
        /// Question sound. (MB_ICONQUESTION)
        /// </summary>
        Question = 0x00000020,

        /// <summary>
        /// Exclamation sound. (MB_ICONWARNING)
        /// </summary>
        Warning = 0x00000030,

        /// <summary>
        /// Information sound. (MB_ICONINFORMATION)
        /// </summary>
        Information = 0x00000040,

        /// <summary>
        /// Simple beep. If no sound card is available, will use speaker.
        /// </summary>
        SimpleBeep = 0xFFFFFFFF
    }
}
