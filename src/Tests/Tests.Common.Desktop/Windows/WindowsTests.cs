// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using FluentAssertions;
using System;
using System.IO;
using WInterop.Windows;
using WInterop.Windows.DataTypes;
using Xunit;

namespace DesktopTests.Windows
{
    public class WindowsTests
    {
        [Fact]
        public void BasicGetDesktopWindow()
        {
            WindowsMethods.GetDesktopWindow().Should().NotBe(WindowHandle.NullWindowHandle);
        }

        [Fact]
        public void BasicGetShellWindow()
        {
            WindowsMethods.GetShellWindow().Should().NotBe(WindowHandle.NullWindowHandle);
        }

        [Fact]
        public void IsWindowShell()
        {
            var window = WindowsMethods.GetShellWindow();
            WindowsMethods.IsWindow(window).Should().BeTrue();
        }

        [Fact]
        public void IsWindowUnicodeShell()
        {
            var window = WindowsMethods.GetShellWindow();
            WindowsMethods.IsWindowUnicode(window).Should().BeTrue();
        }

        [Fact]
        public void IsWindowVisibleShell()
        {
            var window = WindowsMethods.GetShellWindow();
            WindowsMethods.IsWindowVisible(window).Should().BeTrue();
        }

        [Fact]
        public void IsWindowVisibleDesktop()
        {
            var window = WindowsMethods.GetDesktopWindow();
            WindowsMethods.IsWindowVisible(window).Should().BeTrue();
        }

        [Fact]
        public void GetTopWindow()
        {
            WindowsMethods.GetTopWindow(WindowHandle.NullWindowHandle).Should().NotBe(WindowHandle.NullWindowHandle);
        }

        [Fact]
        public void IsGuiThread()
        {
            // Could be either, make sure we don't choke.
            WindowsMethods.IsGuiThread();
        }

        [Fact]
        public void GetClassName()
        {
            WindowsMethods.GetClassName(WindowsMethods.GetShellWindow()).Should().Be("Progman");
        }

        [Fact]
        public void GetClassName_NullWindow()
        {
            Action action = () => WindowsMethods.GetClassName(WindowHandle.NullWindowHandle);

            // Invalid window handle. (1400)
            action.ShouldThrow<IOException>().And.HResult.Should().Be(unchecked((int)0x80070578));
        }

        [Fact]
        public void GetOwner_Desktop()
        {
            var window = WindowsMethods.GetWindow(WindowsMethods.GetDesktopWindow(), GetWindowOptions.GW_OWNER);
            window.Should().Be(WindowHandle.NullWindowHandle);
        }
    }
}
