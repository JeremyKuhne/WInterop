// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using Xunit;
using FluentAssertions;
using WInterop.ProcessAndThreads;
using WInterop.ProcessAndThreads.Types;

namespace DesktopTests.ProcessAndThreads
{
    public class ProcessTests
    {
        [Fact]
        public void GetEnvironmentVariable_GetNullStringThrows()
        {
            Action action = () => ProcessMethods.GetEnvironmentVariable(null);
            action.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void SetEnvironmentVariable_SetNullStringThrows()
        {
            Action action = () => ProcessMethods.SetEnvironmentVariable(null, "invalid");
            action.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void SetEnvironmentVariable_SetEmptyStringNotValid()
        {
            Action action = () => ProcessMethods.SetEnvironmentVariable("", "invalid");
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
            Action action = () => ProcessMethods.SetEnvironmentVariable(name, "invalid");
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
            ProcessMethods.SetEnvironmentVariable(name, "BasicGetSetEnvironmentVariable");
            ProcessMethods.GetEnvironmentVariable(name).Should().Be("BasicGetSetEnvironmentVariable");
            ProcessMethods.SetEnvironmentVariable(name, null);
            ProcessMethods.GetEnvironmentVariable(name).Should().BeNull();
        }

        [Theory,
            InlineData(""),
            // Make the variable hidden (from CMD)
            InlineData("="),
            ]
        public void ListEnvironmentVariables_Basic(string prefix)
        {
            string name = prefix + System.IO.Path.GetRandomFileName();
            ProcessMethods.SetEnvironmentVariable(name, "test");
            ProcessMethods.GetEnvironmentVariable(name).Should().Be("test");
            var variables = ProcessMethods.GetEnvironmentVariables();
            variables.Should().ContainKey(name);
            variables[name].Should().Be("test");
            ProcessMethods.SetEnvironmentVariable(name, null);
            ProcessMethods.GetEnvironmentVariable(name).Should().BeNull();
            variables = ProcessMethods.GetEnvironmentVariables();
            variables.Should().NotContainKey(name);
        }

        [Fact]
        public void GetProcessMemoryInfo_Basic()
        {
            var info = ProcessMethods.GetProcessMemoryInfo();

            // PagefileUsage is 0 on Win7, otherwise it is equal to PrivateUsage
            if (info.PagefileUsage.ToUInt64() != 0)
                info.PagefileUsage.ToUInt64().Should().Be(info.PrivateUsage.ToUInt64());
        }

        [Fact]
        public void GetCurrentProcessIdViaFakeHandle()
        {
            uint id = ProcessMethods.GetCurrentProcessId();
            var handle = ProcessMethods.GetCurrentProcess();
            ProcessMethods.GetProcessId(handle).Should().Be(id);
        }

        [Fact]
        public void GetCurrentProcessIdViaRealHandle()
        {
            uint id = ProcessMethods.GetCurrentProcessId();
            using (var handle = ProcessMethods.OpenProcess(id, ProcessAccessRights.QueryLimitedInfomration))
            {
                ProcessMethods.GetProcessId(handle).Should().Be(id);
            }
        }
    }
}
