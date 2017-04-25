// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using FluentAssertions;
using System;
using System.IO;
using WInterop.Windows;
using WInterop.Windows.Types;
using Xunit;

namespace DesktopTests.Windows
{
    public class WindowsTests
    {
        [Fact]
        public void BasicGetDesktopWindow()
        {
            WindowMethods.GetDesktopWindow().Should().NotBe(WindowHandle.NullWindowHandle);
        }

        [Fact]
        public void BasicGetShellWindow()
        {
            WindowMethods.GetShellWindow().Should().NotBe(WindowHandle.NullWindowHandle);
        }

        [Fact]
        public void IsWindowShell()
        {
            var window = WindowMethods.GetShellWindow();
            WindowMethods.IsWindow(window).Should().BeTrue();
        }

        [Fact]
        public void IsWindowUnicodeShell()
        {
            var window = WindowMethods.GetShellWindow();
            WindowMethods.IsWindowUnicode(window).Should().BeTrue();
        }

        [Fact]
        public void IsWindowVisibleShell()
        {
            var window = WindowMethods.GetShellWindow();
            WindowMethods.IsWindowVisible(window).Should().BeTrue();
        }

        [Fact]
        public void IsWindowVisibleDesktop()
        {
            var window = WindowMethods.GetDesktopWindow();
            WindowMethods.IsWindowVisible(window).Should().BeTrue();
        }

        [Fact]
        public void GetTopWindow()
        {
            WindowMethods.GetTopWindow(WindowHandle.NullWindowHandle).Should().NotBe(WindowHandle.NullWindowHandle);
        }

        [Fact]
        public void IsGuiThread()
        {
            // Could be either, make sure we don't choke.
            WindowMethods.IsGuiThread();
        }

        [Fact]
        public void GetClassName()
        {
            WindowMethods.GetClassName(WindowMethods.GetShellWindow()).Should().Be("Progman");
        }

        [Fact]
        public void GetClassName_NullWindow()
        {
            Action action = () => WindowMethods.GetClassName(WindowHandle.NullWindowHandle);

            // Invalid window handle. (1400)
            action.ShouldThrow<IOException>().And.HResult.Should().Be(unchecked((int)0x80070578));
        }

        [Fact]
        public void GetOwner_Desktop()
        {
            var window = WindowMethods.GetWindow(WindowMethods.GetDesktopWindow(), GetWindowOption.GW_OWNER);
            window.Should().Be(WindowHandle.NullWindowHandle);
        }
    }
}
