// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Diagnostics;
using TerraFX.Interop.Windows;
using WInterop.Support;

namespace WInterop.Windows;

/// <summary>
///  Simple struct to encapsulate a Window handle (HWND).
/// </summary>
[DebuggerDisplay("{HWND}")]
public readonly struct WindowHandle : IHandle<WindowHandle>
{
    public HWND HWND { get; }

    // Special handles for setting position
    // https://docs.microsoft.com/windows/win32/api/winuser/nf-winuser-setwindowpos

    /// <summary>
    ///  For placing windows at the top of the Z order. [HWND_TOP]
    /// </summary>
    public static readonly WindowHandle Top = HWND.HWND_TOP;

    /// <summary>
    ///  For placing windows at the bottom of the Z order. [HWND_BOTTOM]
    /// </summary>
    public static readonly WindowHandle Bottom = HWND.HWND_BOTTOM;

    /// <summary>
    ///  For placing windows behind all topmost windows (if not already non-topmost). [HWND_NOTOPMOST]
    /// </summary>
    public static readonly WindowHandle NoTopMost = HWND.HWND_NOTOPMOST;

    /// <summary>
    ///  For placing windows above all non-topmost windows. [HWND_TOPMOST]
    /// </summary>
    public static readonly WindowHandle TopMost = HWND.HWND_NOTOPMOST;

    /// <summary>
    ///  Used to create and enumerate message only windows. [HWND_MESSAGE]
    /// </summary>
    public static readonly WindowHandle Message = HWND.HWND_MESSAGE;

    /// <summary>
    ///  Used for sending broadcast messages. [HWND_BROADCAST]
    /// </summary>
    public static readonly WindowHandle Broadcast = HWND.HWND_BROADCAST;

    public static readonly WindowHandle Null = new(default);

    public WindowHandle(HWND hwnd) => HWND = hwnd;

    public static implicit operator HWND(WindowHandle handle) => handle.HWND;
    public static implicit operator WindowHandle(HWND handle) => new(handle);

    public override int GetHashCode() => HWND.GetHashCode();

    public override bool Equals(object? obj) => obj is WindowHandle other && other.HWND == HWND;

    public bool Equals(WindowHandle other) => other.HWND == HWND;

    public static bool operator ==(WindowHandle a, WindowHandle b) => a.HWND == b.HWND;

    public static bool operator !=(WindowHandle a, WindowHandle b) => a.HWND != b.HWND;

    public bool IsInvalid => HWND == HWND.INVALID_VALUE;

    public bool IsNull => HWND == HWND.NULL;

    public WindowHandle Handle => this;
}