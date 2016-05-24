// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using FluentAssertions;
using WInterop.Handles.DataTypes;
using WInterop.Windows;
using Xunit;

namespace DesktopTests.WindowsTests
{
    public class Methods
    {
        [Fact]
        public void BasicGetDesktopWindow()
        {
            WindowsDesktopMethods.GetDesktopWindow().Should().NotBe(WindowHandle.NullWindowHandle);
        }

        [Fact]
        public void BasicGetShellWindow()
        {
            WindowsDesktopMethods.GetShellWindow().Should().NotBe(WindowHandle.NullWindowHandle);
        }

        [Fact]
        public void IsWindowShell()
        {
            var window = WindowsDesktopMethods.GetShellWindow();
            WindowsDesktopMethods.IsWindow(window).Should().BeTrue();
        }

        [Fact]
        public void IsWindowUnicodeShell()
        {
            var window = WindowsDesktopMethods.GetShellWindow();
            WindowsDesktopMethods.IsWindowUnicode(window).Should().BeTrue();
        }

        [Fact]
        public void IsWindowVisibleShell()
        {
            var window = WindowsDesktopMethods.GetShellWindow();
            WindowsDesktopMethods.IsWindowVisible(window).Should().BeTrue();
        }

        [Fact]
        public void IsWindowVisibleDesktop()
        {
            var window = WindowsDesktopMethods.GetDesktopWindow();
            WindowsDesktopMethods.IsWindowVisible(window).Should().BeTrue();
        }

        [Fact]
        public void GetTopWindow()
        {
            WindowsDesktopMethods.GetTopWindow(WindowHandle.NullWindowHandle).Should().NotBe(WindowHandle.NullWindowHandle);
        }

        [Fact]
        public void IsGuiThread()
        {
            // Could be either, make sure we don't choke.
            WindowsDesktopMethods.IsGuiThread();
        }
    }
}
