// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using Xunit;
using FluentAssertions;
using WInterop.ProcessAndThreads;

namespace Tests.ProcessAndThreads
{
    public class ProcessTests
    {
        [Fact]
        public void GetCurrentProcessId()
        {
            uint id = Processes.GetCurrentProcessId();
            id.Should().NotBe(0);
        }

        [Fact]
        public void GetCurrentProcess()
        {
            ProcessHandle handle = Processes.GetCurrentProcess();
            handle.HANDLE.Should().Be(new IntPtr(-1));
        }

        [Fact]
        public void OpenCurrentProcess()
        {
            using (SafeProcessHandle handle = Processes.OpenProcess(Processes.GetCurrentProcessId(), ProcessAccessRights.QueryLimitedInfomration))
            {
                handle.IsInvalid.Should().BeFalse();
            }
        }
    }
}
