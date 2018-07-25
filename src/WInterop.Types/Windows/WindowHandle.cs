﻿// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace WInterop.Windows
{
    /// <summary>
    /// Simple struct to encapsulate a Window handle (HWND).
    /// </summary>
    public readonly struct WindowHandle
    {
        public IntPtr HWND { get; }

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

        public WindowHandle(IntPtr hwnd) => HWND = hwnd;

        public static implicit operator IntPtr(WindowHandle handle) => handle.HWND;
        public static implicit operator WindowHandle(IntPtr handle) => new WindowHandle(handle);

        public override int GetHashCode() => HWND.GetHashCode();

        public override bool Equals(object obj)
        {
            return obj is WindowHandle other
                ? other.HWND == HWND
                : false;
        }

        public bool Equals(WindowHandle other) => other.HWND == HWND;

        public static bool operator ==(WindowHandle a, WindowHandle b) => a.HWND == b.HWND;

        public static bool operator !=(WindowHandle a, WindowHandle b) => a.HWND != b.HWND;

        public bool IsValid => HWND != IntPtr.Zero && HWND != (IntPtr)(-1);
    }
}
