// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Drawing;

namespace WInterop.Windows;

public class VerticalLayout : ILayoutHandler
{
    private readonly (float Percent, ILayoutHandler Handler)[] _handlers;

    public VerticalLayout(params (float Percent, ILayoutHandler Handler)[] handlers)
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
        int left = bounds.Left;

        for (int i = 0; i < last; i++)
        {
            int currentWidth = (int)(bounds.Width * _handlers[i].Percent);
            _handlers[i].Handler.Layout(new Rectangle(left, bounds.Y, currentWidth, bounds.Height));
            left += currentWidth;
        }

        _handlers[last].Handler.Layout(new Rectangle(left, bounds.Y, bounds.Width - left, bounds.Height));
    }
}