// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using FluentAssertions;
using System;
using System.IO;
using WInterop.Windows;
using Xunit;

namespace WindowsTests;

public class WindowTests
{
    private static readonly WindowClass s_windowClass = new WindowClass(className: "WindowTests");

    [Fact]
    public void Window_GetDefaultText()
    {
        using Window window = new(s_windowClass, Windows.DefaultBounds);
        window.Text.Should().BeNull();
        string text = window.GetWindowText();
        text.Should().BeEmpty();
    }

    [Fact]
    public void Window_GetSetText()
    {
        using Window window = new(s_windowClass, Windows.DefaultBounds, "Clear Windows");
        string text = window.GetWindowText();
        text.Should().Be("Clear Windows");
        window.Text.Should().Be("Clear Windows");
        window.SetWindowText("Opaque Windows");
        window.Text.Should().Be("Opaque Windows");

        window.Text = null;
        window.Text.Should().BeNull();
    }

    [Fact]
    public void Window_SelfIsNotChild()
    {
        using Window window = new(s_windowClass, Windows.DefaultBounds);
        window.IsChild(window).Should().BeFalse();
    }
}
