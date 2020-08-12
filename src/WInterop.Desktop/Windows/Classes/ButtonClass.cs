// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Drawing;

namespace WInterop.Windows
{
    public class ButtonClass : WindowClass
    {
        public ButtonStyles ButtonStyle { get; }

        public ButtonClass(ButtonStyles buttonStyle)
            : base("Button")
        {
            ButtonStyle = buttonStyle;
        }

        public override WindowHandle CreateWindow(
            Rectangle bounds,
            string? windowName = null,
            WindowStyles style = WindowStyles.Overlapped,
            ExtendedWindowStyles extendedStyle = ExtendedWindowStyles.Default,
            bool isMainWindow = false,
            WindowHandle parentWindow = default,
            IntPtr parameters = default,
            MenuHandle menuHandle = default)
        {
            style |= (WindowStyles)ButtonStyle;
            return base.CreateWindow(bounds, windowName, style, extendedStyle, isMainWindow, parentWindow, parameters, menuHandle);
        }
    }
}
