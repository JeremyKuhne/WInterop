// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Drawing;

namespace WInterop.Windows;

public class FillLayout : ILayoutHandler
{
    private readonly ILayoutHandler _handler;

    public FillLayout(ILayoutHandler handler) => _handler = handler;

    public void Layout(Rectangle bounds) => _handler.Layout(bounds);
}