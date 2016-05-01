// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

#if DESKTOP
using System;
using Xunit;
using FluentAssertions;

namespace WInterop.Tests.NativeMethodTests
{
    public class ProcessAndThreadTests
    {
        [Fact]
        public void GetNullStringThrows()
        {
            Action action = () => NativeMethods.ProcessAndThreads.Desktop.GetEnvironmentVariable(null);
            action.ShouldThrow<ArgumentNullException>();
        }

        [Fact]
        public void SetNullStringThrows()
        {
            Action action = () => NativeMethods.ProcessAndThreads.Desktop.SetEnvironmentVariable(null, "invalid");
            action.ShouldThrow<ArgumentNullException>();
        }

        [Fact]
        public void SetEmptyStringNotValid()
        {
            Action action = () => NativeMethods.ProcessAndThreads.Desktop.SetEnvironmentVariable("", "invalid");
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
            Action action = () => NativeMethods.ProcessAndThreads.Desktop.SetEnvironmentVariable(name, "invalid");
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
            NativeMethods.ProcessAndThreads.Desktop.SetEnvironmentVariable(name, "BasicGetSetEnvironmentVariable");
            NativeMethods.ProcessAndThreads.Desktop.GetEnvironmentVariable(name).Should().Be("BasicGetSetEnvironmentVariable");
            NativeMethods.ProcessAndThreads.Desktop.SetEnvironmentVariable(name, null);
            NativeMethods.ProcessAndThreads.Desktop.GetEnvironmentVariable(name).Should().BeNull();
        }

        [Theory
            InlineData("")
            // Make the variable hidden (from CMD)
            InlineData("=")
            ]
        public void BasicListEnvironmentVariables(string prefix)
        {
            string name = System.IO.Path.GetRandomFileName();
            NativeMethods.ProcessAndThreads.Desktop.SetEnvironmentVariable(name, "test");
            NativeMethods.ProcessAndThreads.Desktop.GetEnvironmentVariable(name).Should().Be("test");
            var variables = NativeMethods.ProcessAndThreads.Desktop.GetEnvironmentVariables();
            variables.Should().ContainKey(name);
            variables[name].Should().Be("test");
            NativeMethods.ProcessAndThreads.Desktop.SetEnvironmentVariable(name, null);
            NativeMethods.ProcessAndThreads.Desktop.GetEnvironmentVariable(name).Should().BeNull();
            variables = NativeMethods.ProcessAndThreads.Desktop.GetEnvironmentVariables();
            variables.Should().NotContainKey(name);
        }
    }
}
#endif
