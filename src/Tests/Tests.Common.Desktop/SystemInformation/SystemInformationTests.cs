// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using FluentAssertions;
using WInterop.SystemInformation;
using WInterop.SystemInformation.DataTypes;
using Xunit;
using System.Diagnostics;

namespace DesktopTests.SystemInformation
{
    public class SystemInformationTests
    {
        [Fact]
        public void GetCurrentUserName()
        {
            SystemInformationDesktopMethods.GetUserName().Should().Be(Environment.GetEnvironmentVariable("USERNAME"));
        }

        [Fact]
        public void GetSuiteMask()
        {
            SystemInformationDesktopMethods.GetSuiteMask().Should().HaveFlag(SuiteMask.VER_SUITE_SINGLEUSERTS);
        }

        [Fact]
        public void GetUserNameSam()
        {
            // Should be DOMAIN\user or COMPUTER\user
            SystemInformationDesktopMethods.GetUserName(EXTENDED_NAME_FORMAT.NameSamCompatible)
                .Should().Be($"{Environment.GetEnvironmentVariable("USERDOMAIN")}\\{Environment.GetEnvironmentVariable("USERNAME")}");
        }

        [Fact]
        public void GetUserNameDisplay()
        {
            SystemInformationDesktopMethods.GetUserName(EXTENDED_NAME_FORMAT.NameDisplay).Should().NotBeNullOrWhiteSpace();
        }

        // [Fact]
        public void DumpUserNameFormats()
        {
            foreach (EXTENDED_NAME_FORMAT format in Enum.GetValues(typeof(EXTENDED_NAME_FORMAT)))
            {
                try
                {
                    string name = SystemInformationDesktopMethods.GetUserName(format) ?? "<null>";
                    Debug.WriteLine($"{format}: '{name}'");
                }
                catch (Exception e)
                {
                    Debug.WriteLine($"{format}: FAILED- {e.Message}");
                }
            }
        }

        [Fact]
        public void GetComputerName()
        {
            SystemInformationDesktopMethods.GetComputerName().Should().Be(Environment.GetEnvironmentVariable("COMPUTERNAME"));
        }

        [Fact]
        public void GetComputerName_NetBIOS()
        {
            SystemInformationDesktopMethods.GetComputerName(COMPUTER_NAME_FORMAT.ComputerNameNetBIOS)
                .Should().Be(SystemInformationDesktopMethods.GetComputerName());
        }

        // [Fact]
        public void DumpComputerNameFormats()
        {
            foreach (COMPUTER_NAME_FORMAT format in Enum.GetValues(typeof(COMPUTER_NAME_FORMAT)))
            {
                try
                {
                    string name = SystemInformationDesktopMethods.GetComputerName(format) ?? "<null>";
                    Debug.WriteLine($"{format}: '{name}'");
                }
                catch (Exception e)
                {
                    Debug.WriteLine($"{format}: FAILED- {e.Message}");
                }
            }
        }
    }
}
