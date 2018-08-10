// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using FluentAssertions;
using WInterop.Windows;
using Xunit;

namespace DesktopTests.Resources
{
    public class MenuTests
    {
        static LRESULT CallDefaultProcedure(WindowHandle window, MessageType message, WPARAM wParam, LPARAM lParam)
        {
            return window.DefaultWindowProcedure(message, wParam, lParam);
        }

        [Fact]
        public void CreateMenu()
        {
            using (MenuHandle menu = Windows.CreateMenu())
            {
                menu.IsInvalid.Should().BeFalse();
            }
        }

        [Fact]
        public void AppendMenu()
        {
            using (MenuHandle menu = Windows.CreateMenu())
            {
                menu.IsInvalid.Should().BeFalse();
                Windows.AppendMenu(menu, "&File", 1000);
            }
        }
    }
}
