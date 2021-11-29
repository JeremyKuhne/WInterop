// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Drawing;

namespace WInterop.Windows;

/// <summary>
///  Delegate for handling Windows messages. Used to forward recieved messages for a window.
/// </summary>
/// <param name="sender">The class invoking the delegate.</param>
/// <returns>
///  The result of processing the message. Return null to indicate that the message has not been handled.
/// </returns>
public delegate LResult? WindowsMessageEvent(
    object sender,
    WindowHandle window,
    MessageType message,
    WParam wParam,
    LParam lParam);

public delegate void MouseMessageEvent(
    object sender,
    WindowHandle window,
    Point position,
    Button button,
    MouseKey mouseState);