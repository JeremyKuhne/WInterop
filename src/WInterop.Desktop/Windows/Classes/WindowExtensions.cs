// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Windows;

public static class WindowExtensions
{
    public static void AddLayoutHandler(this Window window, ILayoutHandler handler)
        => new LayoutBinder(window, handler);
}