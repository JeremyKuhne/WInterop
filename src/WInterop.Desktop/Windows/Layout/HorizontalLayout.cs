// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Drawing;

namespace WInterop.Windows
{
    public class HorizontalLayout : ILayoutHandler
    {
        private readonly (float Percent, ILayoutHandler Handler)[] _handlers;

        public HorizontalLayout(params (float Percent, ILayoutHandler Handler)[] handlers)
        {
            float totalPercent = 0f;
            foreach (var handler in handlers)
            {
                totalPercent += handler.Percent;
            }

            if (totalPercent != 1.0f)
                throw new ArgumentOutOfRangeException($"Total percentage must be 1.0f.");

            _handlers = handlers;
        }

        public void Layout(Rectangle bounds)
        {
            int last = _handlers.Length - 1;
            int top = bounds.Top;

            for (int i = 0; i < last; i++)
            {
                int currentHeight = (int)(bounds.Height * _handlers[i].Percent);
                _handlers[i].Handler.Layout(new Rectangle(bounds.X, top, bounds.Width, currentHeight));
                top += currentHeight;
            }

            _handlers[last].Handler.Layout(new Rectangle(bounds.X, top, bounds.Width, bounds.Height - top));
        }
    }
}