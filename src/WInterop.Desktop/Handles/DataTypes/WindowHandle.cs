// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace WInterop.Handles.DataTypes
{
    /// <summary>
    /// Simple struct to encapsulate a Window handle (HWND).
    /// </summary>
    public struct WindowHandle
    {
        public IntPtr HWND;

        public static WindowHandle NullWindowHandle = new WindowHandle(IntPtr.Zero);

        public WindowHandle(IntPtr hwnd)
        {
            HWND = hwnd;
        }

        static public implicit operator IntPtr(WindowHandle handle)
        {
            return handle.HWND;
        }
    }
}
