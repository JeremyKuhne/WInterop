// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace WInterop.Windows
{
    // https://msdn.microsoft.com/en-us/library/windows/desktop/ms646271.aspx
    [Flags]
    public enum KeyEvent : uint
    {
        /// <summary>
        ///  [KEYEVENTF_EXTENDEDKEY]
        /// </summary>
        ExtendedKey = 0x0001,

        /// <summary>
        ///  [KEYEVENTF_KEYUP]
        /// </summary>
        KeyUp = 0x0002,

        /// <summary>
        ///  [KEYEVENTF_UNICODE]
        /// </summary>
        Unicode = 0x0004,

        /// <summary>
        ///  [KEYEVENTF_SCANCODE]
        /// </summary>
        ScanCode = 0x0008
    }
}