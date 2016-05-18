// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using Xunit;
using FluentAssertions;

namespace WInterop.DesktopTests
{
    public class ProcessAndThreadTests
    {
        [Fact]
        public void GetNullStringThrows()
        {
            Action action = () => ProcessAndThreads.ProcessAndThreadDesktopMethods.GetEnvironmentVariable(null);
            action.ShouldThrow<ArgumentNullException>();
        }

        [Fact]
        public void SetNullStringThrows()
        {
            Action action = () => ProcessAndThreads.ProcessAndThreadDesktopMethods.SetEnvironmentVariable(null, "invalid");
            action.ShouldThrow<ArgumentNullException>();
        }

        [Fact]
        public void SetEmptyStringNotValid()
        {
            Action action = () => ProcessAndThreads.ProcessAndThreadDesktopMethods.SetEnvironmentVariable("", "invalid");
            action.ShouldThrow<ArgumentException>();
        }

        [Theory
            InlineData("==")
            InlineData("a=")
            ]
        public void EqualsNotValidPastFirstCharacter(string name)
        {
            // Anything past the first character in the name isn't allowed to be an equals character.
            // If this does change we'd need to change the logic in GetEnvironmentVariables().
            Action action = () => ProcessAndThreads.ProcessAndThreadDesktopMethods.SetEnvironmentVariable(name, "invalid");
            action.ShouldThrow<ArgumentException>();
        }

        [Theory
            InlineData("")
            // Make the variable hidden (from CMD)
            InlineData("=")
            ]
        public void BasicGetSetEnvironmentVariable(string prefix)
        {
            string name = prefix + System.IO.Path.GetRandomFileName();
            ProcessAndThreads.ProcessAndThreadDesktopMethods.SetEnvironmentVariable(name, "BasicGetSetEnvironmentVariable");
            ProcessAndThreads.ProcessAndThreadDesktopMethods.GetEnvironmentVariable(name).Should().Be("BasicGetSetEnvironmentVariable");
            ProcessAndThreads.ProcessAndThreadDesktopMethods.SetEnvironmentVariable(name, null);
            ProcessAndThreads.ProcessAndThreadDesktopMethods.GetEnvironmentVariable(name).Should().BeNull();
        }

        [Theory
            InlineData("")
            // Make the variable hidden (from CMD)
            InlineData("=")
            ]
        public void BasicListEnvironmentVariables(string prefix)
        {
            string name = System.IO.Path.GetRandomFileName();
            ProcessAndThreads.ProcessAndThreadDesktopMethods.SetEnvironmentVariable(name, "test");
            ProcessAndThreads.ProcessAndThreadDesktopMethods.GetEnvironmentVariable(name).Should().Be("test");
            var variables = ProcessAndThreads.ProcessAndThreadDesktopMethods.GetEnvironmentVariables();
            variables.Should().ContainKey(name);
            variables[name].Should().Be("test");
            ProcessAndThreads.ProcessAndThreadDesktopMethods.SetEnvironmentVariable(name, null);
            ProcessAndThreads.ProcessAndThreadDesktopMethods.GetEnvironmentVariable(name).Should().BeNull();
            variables = ProcessAndThreads.ProcessAndThreadDesktopMethods.GetEnvironmentVariables();
            variables.Should().NotContainKey(name);
        }
    }
}
