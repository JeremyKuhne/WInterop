// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Drawing;

namespace WInterop.Windows;

public class PaddedLayout : ILayoutHandler
{
    private readonly ILayoutHandler _handler;
    private Padding _margin;

    public PaddedLayout(
        Padding margin,
        ILayoutHandler handler)
    {
        _handler = handler;
        _margin = margin;
    }

    public void Layout(Rectangle bounds)
    {
        int widthMargin = _margin.Left + _margin.Right;
        int remainingWidth = bounds.Width - widthMargin;
        if (remainingWidth < 0)
        {
            // Not enough space to grant full margins
            // TODO: Complicated logic here (evenly distribute what we have available in pixels?)
        }
        else
        {
            bounds.X += _margin.Left;
            bounds.Width -= _margin.Left + _margin.Right;
        }

        int heightMargin = _margin.Top + _margin.Bottom;
        int remainingHeight = bounds.Height - heightMargin;
        if (remainingHeight < 0)
        {
            // Not enough space to grant full margins
            // TODO: Complicated logic here (evenly distribute what we have available in pixels?)
        }
        else
        {
            bounds.Y += _margin.Top;
            bounds.Height -= _margin.Top + _margin.Bottom;
        }

        _handler.Layout(bounds);
    }
}