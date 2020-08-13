// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace WInterop.Windows
{
    /// <summary>
    ///  [KEYBDINPUT]
    /// </summary>
    /// <remarks><see cref="https://msdn.microsoft.com/en-us/library/windows/desktop/ms646271.aspx"/></remarks>
    public struct KeyboardInput
    {
        public VirtualKey VirtualKey;
        public ushort Scan;
        public KeyEvent Flags;
        public uint Time;
        public IntPtr ExtraInfo;
    }
}
