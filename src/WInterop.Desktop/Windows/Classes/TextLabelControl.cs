// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Drawing;
using WInterop.Gdi;

namespace WInterop.Windows.Classes
{
    public class TextLabelControl : Window
    {
        private static readonly WindowClass s_textLabelClass = new WindowClass(
            className: "TextLabelClass");

        private TextFormat _textFormat;

        public TextLabelControl(
            Rectangle bounds,
            TextFormat textFormat = TextFormat.Center | TextFormat.VerticallyCenter,
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
            _textFormat = textFormat;
        }

        protected override LResult WindowProcedure(WindowHandle window, MessageType message, WParam wParam, LParam lParam)
        {
            switch (message)
            {
                case MessageType.Paint:
                    {
                        using var deviceContext = window.BeginPaint(out Rectangle paintBounds);
                        deviceContext.DrawText(Text, paintBounds, _textFormat);
                        break;
                    }
            }

            return base.WindowProcedure(window, message, wParam, lParam);
        }

        public TextFormat TextFormat
        {
            get => _textFormat;
            set
            {
                if (value == _textFormat)
                {
                    return;
                }

                _textFormat = value;
                Handle.Invalidate();
            }
        }
    }
}
