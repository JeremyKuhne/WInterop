// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Drawing;

namespace WInterop.Windows
{
    public class EditControl : Window
    {
        private static readonly EditClass s_editClass = new EditClass();

        public EditControl(
            Rectangle bounds,
            string? windowName = default,
            EditStyles editStyle = EditStyles.Left,
            WindowStyles style = WindowStyles.Overlapped,
            ExtendedWindowStyles extendedStyle = ExtendedWindowStyles.Default,
            bool isMainWindow = false,
            Window? parentWindow = default,
            IntPtr parameters = default,
            MenuHandle menuHandle = default) : base(
                s_editClass,
                bounds,
                windowName,
                style |= (WindowStyles)editStyle,
                extendedStyle,
                isMainWindow,
                parentWindow,
                parameters,
                menuHandle)
        {
        }
    }
}