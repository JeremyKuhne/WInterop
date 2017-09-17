// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using WInterop.Windows;
using WInterop.Windows.Types;
using Xunit;
using FluentAssertions;
using WInterop.Resources.Types;
using WInterop.Resources;

namespace DesktopTests.Resources
{
    public class MenuTests
    {
        static LRESULT CallDefaultProcedure(WindowHandle window, WindowMessage message, WPARAM wParam, LPARAM lParam)
        {
            return WindowMethods.DefaultWindowProcedure(window, message, wParam, lParam);
        }

        [Fact]
        public void CreateMenu()
        {
            using (MenuHandle menu = ResourceMethods.CreateMenu())
            {
                menu.IsInvalid.Should().BeFalse();
            }
        }

        [Fact]
        public void AppendMenu()
        {
            using (MenuHandle menu = ResourceMethods.CreateMenu())
            {
                menu.IsInvalid.Should().BeFalse();
                ResourceMethods.AppendMenu(menu, "&File", 1000);
            }
        }
    }
}
