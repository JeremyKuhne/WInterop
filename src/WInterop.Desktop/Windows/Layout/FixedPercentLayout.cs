// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Drawing;

namespace WInterop.Windows
{
    public class FixedPercentLayout : ILayoutHandler
    {
        private readonly ILayoutHandler _handler;
        private readonly SizeF _percent;
        private readonly VerticalAlignment _verticalAlignment;
        private readonly HorizontalAlignment _horizontalAlignment;

        public FixedPercentLayout(
            ILayoutHandler handler,
            SizeF percent,
            VerticalAlignment verticalAlignment = VerticalAlignment.Center,
            HorizontalAlignment horizontalAlignment = HorizontalAlignment.Center)
        {
            _handler = handler;
            _percent = percent;
            _verticalAlignment = verticalAlignment;
            _horizontalAlignment = horizontalAlignment;
        }

        public void Layout(Rectangle bounds)
        {
            Size size = new((int)(bounds.Width * _percent.Width), (int)(bounds.Height * _percent.Height));

            int x = _horizontalAlignment switch
            {
                HorizontalAlignment.Left => bounds.Left,
                HorizontalAlignment.Right => bounds.Right - size.Width,
                HorizontalAlignment.Center => bounds.X + ((bounds.Width - size.Width) / 2),
                _ => bounds.Left,
            };

            int y = _verticalAlignment switch
            {
                VerticalAlignment.Top => bounds.Top,
                VerticalAlignment.Bottom => bounds.Bottom - size.Height,
                VerticalAlignment.Center => bounds.Y + ((bounds.Height - size.Height) / 2),
                _ => bounds.Top,
            };

            _handler.Layout(new Rectangle(new Point(x, y), size));
        }
    }
}