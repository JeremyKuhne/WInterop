// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using WInterop.Windows.Native;

namespace WInterop.Windows
{
    /// <docs>
    ///  Note that the docs has the field ordering messed up.
    ///  https://docs.microsoft.com/windows/win32/api/winuser/ns-winuser-windowpos
    /// </docs>
    public struct WindowPosition
    {
        public WindowHandle Handle;

        /// <summary>
        ///  The window behind this Window or <see cref="WindowHandle.Top"/>, <see cref="WindowHandle.TopMost"/>,
        ///  <see cref="WindowHandle.NoTopMost"/>, or <see cref="WindowHandle.Bottom"/>;
        /// </summary>
        public WindowHandle InsertAfter;
        public int X;
        public int Y;
        public int Width;
        public int Height;
        public WindowPositionFlags Flags;
    }
}