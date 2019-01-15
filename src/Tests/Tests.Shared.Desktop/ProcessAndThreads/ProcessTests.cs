// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using Xunit;
using FluentAssertions;
using WInterop.ProcessAndThreads;

namespace ProcessAndThreadTests
{
    public partial class ProcessTests
    {
        [Fact]
        public void GetEnvironmentVariable_GetNullStringThrows()
        {
            Action action = () => Processes.GetEnvironmentVariable(null);
            action.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void SetEnvironmentVariable_SetNullStringThrows()
        {
            Action action = () => Processes.SetEnvironmentVariable(null, "invalid");
            action.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void SetEnvironmentVariable_SetEmptyStringNotValid()
        {
            Action action = () => Processes.SetEnvironmentVariable("", "invalid");
            action.Should().Throw<ArgumentException>();
        }

        [Theory,
            InlineData("=="),
            InlineData("a=")
            ]
        public void SetEnvironmentVariable_EqualsNotValidPastFirstCharacter(string name)
        {
            // Anything past the first character in the name isn't allowed to be an equals character.
            // If this does change we'd need to change the logic in GetEnvironmentVariables().
            Action action = () => Processes.SetEnvironmentVariable(name, "invalid");
            action.Should().Throw<ArgumentException>();
        }

        [Theory,
            InlineData(""),
            // Make the variable hidden (from CMD)
            InlineData("="),
            ]
        public void BasicGetSetEnvironmentVariable(string prefix)
        {
            string name = prefix + System.IO.Path.GetRandomFileName();
            Processes.SetEnvironmentVariable(name, "BasicGetSetEnvironmentVariable");
            Processes.GetEnvironmentVariable(name).Should().Be("BasicGetSetEnvironmentVariable");
            Processes.SetEnvironmentVariable(name, null);
            Processes.GetEnvironmentVariable(name).Should().BeNull();
        }

        [Theory,
            InlineData(""),
            // Make the variable hidden (from CMD)
            InlineData("="),
            ]
        public void ListEnvironmentVariables_Basic(string prefix)
        {
            string name = prefix + System.IO.Path.GetRandomFileName();
            Processes.SetEnvironmentVariable(name, "test");
            Processes.GetEnvironmentVariable(name).Should().Be("test");
            var variables = Processes.GetEnvironmentVariables();
            variables.Should().ContainKey(name);
            variables[name].Should().Be("test");
            Processes.SetEnvironmentVariable(name, null);
            Processes.GetEnvironmentVariable(name).Should().BeNull();
            variables = Processes.GetEnvironmentVariables();
            variables.Should().NotContainKey(name);
        }

        [Fact]
        public void GetProcessMemoryInfo_Basic()
        {
            var info = Processes.GetProcessMemoryInfo();

            // PagefileUsage is 0 on Win7, otherwise it is equal to PrivateUsage
            if (info.PagefileUsage.ToUInt64() != 0)
                info.PagefileUsage.ToUInt64().Should().Be(info.PrivateUsage.ToUInt64());
        }

        [Fact]
        public void GetCurrentProcessIdViaFakeHandle()
        {
            uint id = Processes.GetCurrentProcessId();
            var handle = Processes.GetCurrentProcess();
            Processes.GetProcessId(handle).Should().Be(id);
        }

        [Fact]
        public void GetCurrentProcessIdViaRealHandle()
        {
            uint id = Processes.GetCurrentProcessId();
            using (var handle = Processes.OpenProcess(id, ProcessAccessRights.QueryLimitedInfomration))
            {
                Processes.GetProcessId(handle).Should().Be(id);
            }
        }
    }
}
