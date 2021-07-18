// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace WInterop.Windows
{
    // https://msdn.microsoft.com/en-us/library/windows/desktop/ms646273.aspx
    [Flags]
    public enum MouseEvent : uint
    {
        /// <summary>
        ///  Mouse move. [MOUSEEVENTF_MOVE]
        /// </summary>
        Move = 0x0001,

        /// <summary>
        ///  Left button down. [MOUSEEVENTF_LEFTDOWN]
        /// </summary>
        LeftDown = 0x0002,

        /// <summary>
        ///  Left button up. [MOUSEEVENTF_LEFTUP]
        /// </summary>
        LeftUp = 0x0004,

        /// <summary>
        ///  Right button down. [MOUSEEVENTF_RIGHTDOWN]
        /// </summary>
        RightDown = 0x0008,

        /// <summary>
        ///  Right button up. [MOUSEEVENTF_RIGHTUP]
        /// </summary>
        RightUp = 0x0010,

        /// <summary>
        ///  Middle button down. [MOUSEEVENTF_MIDDLEDOWN]
        /// </summary>
        MiddleDown = 0x0020,

        /// <summary>
        ///  Middle button up. [MOUSEEVENTF_MIDDLEUP]
        /// </summary>
        MiddleUp = 0x0040,

        /// <summary>
        ///  X button down. [MOUSEEVENTF_XDOWN]
        /// </summary>
        XDown = 0x0080,

        /// <summary>
        ///  X button up. [MOUSEEVENTF_XUP]
        /// </summary>
        XUp = 0x0100,

        /// <summary>
        ///  Wheel move. [MOUSEEVENTF_WHEEL]
        /// </summary>
        Wheel = 0x0800,

        /// <summary>
        ///  Horizontal wheel move. [MOUSEEVENTF_HWHEEL]
        /// </summary>
        HWheel = 0x1000,

        /// <summary>
        ///  Do not coalesce mouse moves. [MOUSEEVENTF_MOVE_NOCOALESCE]
        /// </summary>
        NoCoalesce = 0x2000,

        /// <summary>
        ///  Map to entire virtual desktop. [MOUSEEVENTF_VIRTUALDESK]
        /// </summary>
        VirtualDesk = 0x4000,

        /// <summary>
        ///  Absolute move. [MOUSEEVENTF_ABSOLUTE]
        /// </summary>
        Absolute = 0x8000
    }
}