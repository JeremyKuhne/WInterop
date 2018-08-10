// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace WInterop.Windows
{
    /// <summary>
    /// [MOUSEINPUT]
    /// </summary>
    /// <remarks><see cref="https://msdn.microsoft.com/en-us/library/windows/desktop/ms646273.aspx"/></remarks>
    public struct MouseInput
    {
        public int dx;
        public int dy;

        /// <summary>
        /// Wheel movement for wheel moves (positive for forward/right).
        /// For X button events, which X button (1 or 2)
        /// </summary>
        public int mouseData;

        public MouseEvent dwFlags;

        /// <summary>
        /// Time in milliseconds.
        /// </summary>
        public uint time;
        public IntPtr dwExtraInfo;
    }
}
