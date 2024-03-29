﻿// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using WInterop.Windows;

namespace ResourceTests;

public class Menus
{
    private static LResult CallDefaultProcedure(WindowHandle window, MessageType message, WParam wParam, LParam lParam)
    {
        return window.DefaultWindowProcedure(message, wParam, lParam);
    }

    [Fact]
    public void CreateMenu()
    {
        using MenuHandle menu = Windows.CreateMenu();
        menu.IsInvalid.Should().BeFalse();
    }

    [Fact]
    public void AppendMenu()
    {
        using MenuHandle menu = Windows.CreateMenu();
        menu.IsInvalid.Should().BeFalse();
        Windows.AppendMenu(menu, "&File", 1000);
    }
}
