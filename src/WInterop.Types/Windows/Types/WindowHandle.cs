// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace WInterop.Windows.Types
{
    /// <summary>
    /// Simple struct to encapsulate a Window handle (HWND).
    /// </summary>
    public struct WindowHandle
    {
        public IntPtr HWND;

        public static WindowHandle Null = new WindowHandle(IntPtr.Zero);

        // Special handles for setting position
        // https://msdn.microsoft.com/en-us/library/windows/desktop/ms633545.aspx

        /// <summary>
        /// For placing windows at the bottom of the Z order.
        /// </summary>
        public static WindowHandle HWND_BOTTOM = new WindowHandle(new IntPtr(1));

        /// <summary>
        /// For placing windows behind all topmost windows (if not already non-topmost).
        /// </summary>
        public static WindowHandle HWND_NOTOPMOST = new WindowHandle(new IntPtr(-2));

        /// <summary>
        /// For placing windows at the top of the Z order.
        /// </summary>
        public static WindowHandle HWND_TOP = new WindowHandle(IntPtr.Zero);

        /// <summary>
        /// For placing windows above all non-topmost windows.
        /// </summary>
        public static WindowHandle HWND_TOPMOST = new WindowHandle(new IntPtr(-1));

        public WindowHandle(IntPtr hwnd)
        {
            HWND = hwnd;
        }

        static public implicit operator IntPtr(WindowHandle handle)
        {
            return handle.HWND;
        }

        public override int GetHashCode()
        {
            return HWND.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public static bool operator ==(WindowHandle x, WindowHandle y)
        {
            return x.HWND == y.HWND;
        }

        public static bool operator !=(WindowHandle x, WindowHandle y)
        {
            return x.HWND != y.HWND;
        }
    }
}
