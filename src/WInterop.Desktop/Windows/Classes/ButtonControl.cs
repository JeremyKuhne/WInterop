// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Drawing;

namespace WInterop.Windows;

public class ButtonControl : Window
{
    private static readonly ButtonClass s_buttonClass = new ButtonClass();

    public ButtonControl(
        Rectangle bounds,
        string? text = default,
        ButtonStyles buttonStyle = ButtonStyles.PushButton,
        WindowStyles style = WindowStyles.Overlapped,
        ExtendedWindowStyles extendedStyle = ExtendedWindowStyles.Default,
        bool isMainWindow = false,
        Window? parentWindow = default,
        IntPtr parameters = default,
        MenuHandle menuHandle = default) : base(
            s_buttonClass,
            bounds,
            text,
            style |= (WindowStyles)buttonStyle,
            extendedStyle,
            isMainWindow,
            parentWindow,
            parameters,
            menuHandle)
    {
    }
}