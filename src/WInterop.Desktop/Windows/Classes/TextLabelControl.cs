// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WInterop.Gdi;

namespace WInterop.Windows.Classes
{
    public class TextLabelControl : Window
    {
        private static readonly WindowClass s_textLabelClass = new WindowClass(
            className: "TextLabelClass");

        private HorizontalAlignment _horizontalAlignment;
        private VerticalAlignment _verticalAlignment;

        public TextLabelControl(
            Rectangle bounds,
            HorizontalAlignment horizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment verticalAlignment = VerticalAlignment.Center,
            string? text = default,
            WindowStyles style = default,
            ExtendedWindowStyles extendedStyle = ExtendedWindowStyles.Default,
            bool isMainWindow = false,
            Window? parentWindow = default,
            IntPtr parameters = default,
            MenuHandle menuHandle = default) : base(
                s_textLabelClass,
                bounds,
                text,
                style,
                extendedStyle,
                isMainWindow,
                parentWindow,
                parameters,
                menuHandle)
        {
            _horizontalAlignment = horizontalAlignment;
            _verticalAlignment = verticalAlignment;
        }

        protected override LResult WindowProcedure(WindowHandle window, MessageType message, WParam wParam, LParam lParam)
        {
            switch (message)
            {
                case MessageType.Paint:
                    {
                        using var deviceContext = window.BeginPaint();
                        break;
                    }
            }

            return base.WindowProcedure(window, message, wParam, lParam);
        }
    }
}
