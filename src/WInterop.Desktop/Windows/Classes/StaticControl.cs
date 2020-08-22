// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Drawing;

namespace WInterop.Windows
{
    public class StaticControl : Window
    {
        private static readonly StaticClass s_buttonClass = new StaticClass();

        public StaticControl(
            Rectangle bounds,
            string? text = default,
            StaticStyles staticStyle = StaticStyles.Center | StaticStyles.EditControl,
            WindowStyles style = WindowStyles.Overlapped,
            ExtendedWindowStyles extendedStyle = ExtendedWindowStyles.Default,
            bool isMainWindow = false,
            Window? parentWindow = default,
            IntPtr parameters = default,
            MenuHandle menuHandle = default) : base(
                s_buttonClass,
                bounds,
                text,
                style |= (WindowStyles)staticStyle,
                extendedStyle,
                isMainWindow,
                parentWindow,
                parameters,
                menuHandle)
        {
        }
    }
}
