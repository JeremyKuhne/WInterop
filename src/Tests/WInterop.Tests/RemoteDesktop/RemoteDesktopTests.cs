// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Xunit;
using FluentAssertions;
using WInterop.RemoteDesktop;
using WInterop.ProcessAndThreads;

namespace RemoteDesktopTests;

public class Basic
{
    [Fact]
    public void GetCurrentSessionId()
    {
        // This, of course, won't necessarily be true if remotely connected to a full Terminal Server
        // or as an administrator to a Windows server via Remote Desktop for Administration.
        RemoteDesktop.ProcessIdToSessionId(Processes.GetCurrentProcessId()).Should().Be(1);
    }

    [Fact]
    public void GetActiveConsoleId()
    {
        // This, of course, won't necessarily be true if remotely connected to a full Terminal Server
        // or as an administrator to a Windows server via Remote Desktop for Administration.
        RemoteDesktop.GetActiveConsoleSessionId().Should().Be(
            RemoteDesktop.ProcessIdToSessionId(Processes.GetCurrentProcessId()));
    }
}
