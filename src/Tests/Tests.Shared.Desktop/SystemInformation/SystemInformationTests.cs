// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using FluentAssertions;
using System;
using System.Diagnostics;
using WInterop.SystemInformation;
using WInterop.SystemInformation.Types;
using Xunit;

namespace DesktopTests.SystemInformation
{
    public class SystemInformationTests
    {
        [Fact]
        public void GetCurrentUserName()
        {
            SystemInformationMethods.GetUserName().Should().Be(Environment.GetEnvironmentVariable("USERNAME"));
        }

        [Fact]
        public void GetSuiteMask()
        {
            SystemInformationMethods.GetSuiteMask().Should().HaveFlag(SuiteMask.VER_SUITE_SINGLEUSERTS);
        }

        [Fact]
        public void GetUserNameSam()
        {
            // Should be DOMAIN\user or COMPUTER\user
            SystemInformationMethods.GetUserName(EXTENDED_NAME_FORMAT.NameSamCompatible)
                .Should().Be($"{Environment.GetEnvironmentVariable("USERDOMAIN")}\\{Environment.GetEnvironmentVariable("USERNAME")}");
        }

        [Fact]
        public void GetUserNameDisplay()
        {
            SystemInformationMethods.GetUserName(EXTENDED_NAME_FORMAT.NameDisplay).Should().NotBeNullOrWhiteSpace();
        }

        // [Fact]
        public void DumpUserNameFormats()
        {
            foreach (EXTENDED_NAME_FORMAT format in Enum.GetValues(typeof(EXTENDED_NAME_FORMAT)))
            {
                try
                {
                    string name = SystemInformationMethods.GetUserName(format) ?? "<null>";
                    Debug.WriteLine($"{format}: '{name}'");
                }
                catch (Exception e)
                {
                    Debug.WriteLine($"{format}: FAILED- {e.Message}");
                }
            }
        }

        [Fact]
        public void GetComputerName_NetBIOS()
        {
            SystemInformationMethods.GetComputerName(COMPUTER_NAME_FORMAT.ComputerNameNetBIOS)
                .Should().Be(SystemInformationMethods.GetComputerName());
        }

        // [Fact]
        public void DumpComputerNameFormats()
        {
            foreach (COMPUTER_NAME_FORMAT format in Enum.GetValues(typeof(COMPUTER_NAME_FORMAT)))
            {
                try
                {
                    string name = SystemInformationMethods.GetComputerName(format) ?? "<null>";
                    Debug.WriteLine($"{format}: '{name}'");
                }
                catch (Exception e)
                {
                    Debug.WriteLine($"{format}: FAILED- {e.Message}");
                }
            }
        }

        [Fact]
        public void ExpandEnvironmentVariables_Existing()
        {
            SystemInformationMethods.ExpandEnvironmentVariables(@"%COMPUTERNAME%").
                Should().Be(Environment.GetEnvironmentVariable("COMPUTERNAME"));
        }

        [Fact]
        public void ExpandEnvironmentVariables_Long()
        {
            string longValue = new string('a', 100);
            try
            {
                Environment.SetEnvironmentVariable("ExpandEnvironmentVariables_Long", longValue);
                SystemInformationMethods.ExpandEnvironmentVariables(@"%ExpandEnvironmentVariables_Long%").
                    Should().Be(longValue);
            }
            finally
            {
                Environment.SetEnvironmentVariable("ExpandEnvironmentVariables_Long", null);
            }
        }
    }
}
