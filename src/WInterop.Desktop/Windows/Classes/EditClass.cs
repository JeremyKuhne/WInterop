// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Drawing;

namespace WInterop.Windows
{
    public class EditClass : WindowClass
    {
        public EditStyles EditStyle { get; }

        public EditClass(EditStyles editStyle)
            : base("Edit")
        {
            EditStyle = editStyle;
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
            style |= (WindowStyles)EditStyle;
            return base.CreateWindow(bounds, windowName, style, extendedStyle, isMainWindow, parentWindow, parameters, menuHandle);
        }
    }
}
