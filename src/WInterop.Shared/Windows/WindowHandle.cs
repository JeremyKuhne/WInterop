// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using WInterop.Windows.Unsafe;

namespace WInterop.Windows
{
    /// <summary>
    /// Simple struct to encapsulate a Window handle (HWND).
    /// </summary>
    public readonly struct WindowHandle
    {
        public HWND HWND { get; }

        // Special handles for setting position
        // https://msdn.microsoft.com/en-us/library/windows/desktop/ms633545.aspx

        /// <summary>
        /// For placing windows at the top of the Z order. [HWND_TOP]
        /// </summary>
        public static WindowHandle Top = new WindowHandle(default);

        /// <summary>
        /// For placing windows at the bottom of the Z order. [HWND_BOTTOM]
        /// </summary>
        public static WindowHandle Bottom = new WindowHandle(new HWND(new IntPtr(1)));

        /// <summary>
        /// For placing windows behind all topmost windows (if not already non-topmost). [HWND_NOTOPMOST]
        /// </summary>
        public static WindowHandle NoTopMost = new WindowHandle(new HWND(new IntPtr(-2)));

        /// <summary>
        /// For placing windows above all non-topmost windows. [HWND_TOPMOST]
        /// </summary>
        public static WindowHandle TopMost = new WindowHandle(new HWND(new IntPtr(-1)));

        /// <summary>
        /// Used to create and enumerate message only windows. [HWND_MESSAGE]
        /// </summary>
        public static WindowHandle Message = new WindowHandle(new HWND(new IntPtr(-3)));

        /// <summary>
        /// Used for sending broadcast messages. [HWND_BROADCAST]
        /// </summary>
        public static WindowHandle Broadcast = new WindowHandle(new HWND(new IntPtr(0xffff)));

        public WindowHandle(HWND hwnd) => HWND = hwnd;

        public static implicit operator HWND(WindowHandle handle) => handle.HWND;
        public static implicit operator WindowHandle(HWND handle) => new WindowHandle(handle);

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

        public bool IsInvalid => HWND.IsInvalid;

        public bool IsNull => HWND.Value == IntPtr.Zero;
    }
}
