// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using Xunit;
using FluentAssertions;
using WInterop.ProcessAndThreads;

namespace DesktopTests.ProcessAndThread
{
    public class ProcessTests
    {
        [Fact]
        public void GetEnvironmentVariable_GetNullStringThrows()
        {
            Action action = () => ProcessDesktopMethods.GetEnvironmentVariable(null);
            action.ShouldThrow<ArgumentNullException>();
        }

        [Fact]
        public void SetEnvironmentVariable_SetNullStringThrows()
        {
            Action action = () => ProcessDesktopMethods.SetEnvironmentVariable(null, "invalid");
            action.ShouldThrow<ArgumentNullException>();
        }

        [Fact]
        public void SetEnvironmentVariable_SetEmptyStringNotValid()
        {
            Action action = () => ProcessDesktopMethods.SetEnvironmentVariable("", "invalid");
            action.ShouldThrow<ArgumentException>();
        }

        [Theory,
            InlineData("=="),
            InlineData("a=")
            ]
        public void SetEnvironmentVariable_EqualsNotValidPastFirstCharacter(string name)
        {
            // Anything past the first character in the name isn't allowed to be an equals character.
            // If this does change we'd need to change the logic in GetEnvironmentVariables().
            Action action = () => ProcessDesktopMethods.SetEnvironmentVariable(name, "invalid");
            action.ShouldThrow<ArgumentException>();
        }

        [Theory,
            InlineData(""),
            // Make the variable hidden (from CMD)
            InlineData("="),
            ]
        public void BasicGetSetEnvironmentVariable(string prefix)
        {
            string name = prefix + System.IO.Path.GetRandomFileName();
            ProcessDesktopMethods.SetEnvironmentVariable(name, "BasicGetSetEnvironmentVariable");
            ProcessDesktopMethods.GetEnvironmentVariable(name).Should().Be("BasicGetSetEnvironmentVariable");
            ProcessDesktopMethods.SetEnvironmentVariable(name, null);
            ProcessDesktopMethods.GetEnvironmentVariable(name).Should().BeNull();
        }

        [Theory,
            InlineData(""),
            // Make the variable hidden (from CMD)
            InlineData("="),
            ]
        public void ListEnvironmentVariables_Basic(string prefix)
        {
            string name = System.IO.Path.GetRandomFileName();
            ProcessDesktopMethods.SetEnvironmentVariable(name, "test");
            ProcessDesktopMethods.GetEnvironmentVariable(name).Should().Be("test");
            var variables = ProcessDesktopMethods.GetEnvironmentVariables();
            variables.Should().ContainKey(name);
            variables[name].Should().Be("test");
            ProcessDesktopMethods.SetEnvironmentVariable(name, null);
            ProcessDesktopMethods.GetEnvironmentVariable(name).Should().BeNull();
            variables = ProcessDesktopMethods.GetEnvironmentVariables();
            variables.Should().NotContainKey(name);
        }

        [Fact]
        public void GetProcessMemoryInfo_Basic()
        {
            var info = ProcessDesktopMethods.GetProcessMemoryInfo();

            // PagefileUsage is 0 on Win7, otherwise it is equal to PrivateUsage
            if (info.PagefileUsage.ToUInt64() != 0)
                info.PagefileUsage.ToUInt64().Should().Be(info.PrivateUsage.ToUInt64());
        }
    }
}
