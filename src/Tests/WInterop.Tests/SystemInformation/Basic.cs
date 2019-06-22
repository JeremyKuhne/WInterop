// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using FluentAssertions;
using System;
using System.Diagnostics;
using WInterop.SystemInformation;
using Xunit;

namespace SystemInformationTests
{
    public partial class Basic
    {
        [Fact]
        public void GetCurrentUserName()
        {
            SystemInformation.GetUserName().Should().Be(Environment.GetEnvironmentVariable("USERNAME"));
        }

        [Fact]
        public void GetSuiteMask()
        {
            SystemInformation.GetSuiteMask().Should().HaveFlag(SuiteMask.SingleUserTerminalServices);
        }

        [Fact]
        public void GetUserNameSam()
        {
            // Should be DOMAIN\user or COMPUTER\user
            SystemInformation.GetUserName(ExtendedNameFormat.SamCompatible)
                .Should().Be($"{Environment.GetEnvironmentVariable("USERDOMAIN")}\\{Environment.GetEnvironmentVariable("USERNAME")}");
        }

        [Fact]
        public void GetUserNameDisplay()
        {
            SystemInformation.GetUserName(ExtendedNameFormat.Display).Should().NotBeNullOrWhiteSpace();
        }

        // [Fact]
        private void DumpUserNameFormats()
        {
            foreach (ExtendedNameFormat format in Enum.GetValues(typeof(ExtendedNameFormat)))
            {
                try
                {
                    string name = SystemInformation.GetUserName(format) ?? "<null>";
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
            SystemInformation.GetComputerName(ComputerNameFormat.NetBIOS)
                .Should().Be(SystemInformation.GetComputerName());
        }

        // [Fact]
        private void DumpComputerNameFormats()
        {
            foreach (ComputerNameFormat format in Enum.GetValues(typeof(ComputerNameFormat)))
            {
                try
                {
                    string name = SystemInformation.GetComputerName(format) ?? "<null>";
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
            SystemInformation.ExpandEnvironmentVariables(@"%COMPUTERNAME%").
                Should().Be(Environment.GetEnvironmentVariable("COMPUTERNAME"));
        }

        [Fact]
        public void ExpandEnvironmentVariables_Long()
        {
            string longValue = new string('a', 100);
            try
            {
                Environment.SetEnvironmentVariable("ExpandEnvironmentVariables_Long", longValue);
                SystemInformation.ExpandEnvironmentVariables(@"%ExpandEnvironmentVariables_Long%").
                    Should().Be(longValue);
            }
            finally
            {
                Environment.SetEnvironmentVariable("ExpandEnvironmentVariables_Long", null);
            }
        }
    }
}
