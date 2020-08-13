// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using FluentAssertions;
using System;
using System.IO;
using WInterop.Windows;
using Xunit;

namespace WindowsTests
{
    public class WindowsTests
    {
        [Fact]
        public void BasicGetDesktopWindow()
        {
            Windows.GetDesktopWindow().Should().NotBe(default);
        }

        [Fact]
        public void BasicGetShellWindow()
        {
            Windows.GetShellWindow().Should().NotBe(default);
        }

        [Fact]
        public void IsWindowShell()
        {
            var window = Windows.GetShellWindow();
            Windows.IsWindow(window).Should().BeTrue();
        }

        [Fact]
        public void IsWindowUnicodeShell()
        {
            var window = Windows.GetShellWindow();
            Windows.IsWindowUnicode(window).Should().BeTrue();
        }

        [Fact]
        public void IsWindowVisibleShell()
        {
            var window = Windows.GetShellWindow();
            Windows.IsWindowVisible(window).Should().BeTrue();
        }

        [Fact]
        public void IsWindowVisibleDesktop()
        {
            var window = Windows.GetDesktopWindow();
            Windows.IsWindowVisible(window).Should().BeTrue();
        }

        [Fact]
        public void GetTopWindow()
        {
            Windows.GetTopWindow(default).Should().NotBe(default);
        }

        [Fact]
        public void IsGuiThread()
        {
            // Could be either, make sure we don't choke.
            Windows.IsGuiThread();
        }

        [Fact]
        public void GetClassName()
        {
            Windows.GetClassName(Windows.GetShellWindow()).Should().Be("Progman");
        }

        [Fact]
        public void GetClassName_NullWindow()
        {
            Action action = () => Windows.GetClassName(default);

            // Invalid window handle. (1400)
            action.Should().Throw<IOException>().And.HResult.Should().Be(unchecked((int)0x80070578));
        }

        [Fact]
        public void GetOwner_Desktop()
        {
            var window = Windows.GetWindow(Windows.GetDesktopWindow(), GetWindowOption.Owner);
            window.Should().Be(new WindowHandle());
        }

        [Fact]
        public void GetKeyboardType()
        {
            // Don't know that you can ever get something other than Enhanced?
            Windows.GetKeyboardType().Should().Be(KeyboardType.Enhanced);
        }

        [Fact]
        public void GetKeyboardSubtype()
        {
            int subType = Windows.GetKeyboardSubType();
        }

        [Fact]
        public void GetKeyboardFunctionKeyCount()
        {
            int keyCount = Windows.GetKeyboardFunctionKeyCount();
            keyCount.Should().BeGreaterOrEqualTo(10);
        }

        [Fact]
        public void GetCommonControlsVersion()
        {
            var version = Windows.GetCommonControlsVersion();
            version.MajorVersion.Should().BeGreaterOrEqualTo(5);
        }
    }
}
