// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using Xunit;
using FluentAssertions;
using WInterop.Desktop.RemoteDesktop;
using WInterop.ProcessAndThreads;

namespace DesktopTests.RemoteDesktop
{
    public class RemoteDesktopTests
    {
        [Fact]
        public void GetCurrentSessionId()
        {
            // This, of course, won't necessarily be true if remotely connected to a full Terminal Server
            // or as an administrator to a Windows server via Remote Desktop for Administration.
            RemoteDesktopMethods.ProcessIdToSessionId(ProcessMethods.GetCurrentProcessId()).Should().Be(1);
        }

        [Fact]
        public void GetActiveConsoleId()
        {
            // This, of course, won't necessarily be true if remotely connected to a full Terminal Server
            // or as an administrator to a Windows server via Remote Desktop for Administration.
            RemoteDesktopMethods.GetActiveConsoleSessionId().Should().Be(
                RemoteDesktopMethods.ProcessIdToSessionId(ProcessMethods.GetCurrentProcessId()));
        }
    }
}
