// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace WInterop.Windows;

/// <summary>
///  [MOUSEINPUT]
/// </summary>
/// <docs>https://docs.microsoft.com/windows/win32/api/winuser/ns-winuser-mouseinput"</docs>
public struct MouseInput
{
    public int X;
    public int Y;

    /// <summary>
    ///  Wheel movement for wheel moves (positive for forward/right). For X button events, which X button (1 or 2)
    /// </summary>
    public int MouseData;

    public MouseEvent Flags;

    /// <summary>
    ///  Time in milliseconds.
    /// </summary>
    public uint Time;

    public IntPtr ExtraInfo;
}