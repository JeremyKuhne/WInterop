// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using Xunit;
using FluentAssertions;
using WInterop.Registry;
using WInterop.Registry.Types;

namespace DesktopTests.Registry
{
    public class RegistryTests
    {
        [Fact]
        public void OpenUserKey()
        {
            using (var key = RegistryMethods.OpenKey(RegistryKeyHandle.HKEY_CURRENT_USER, null))
            {
                key.IsInvalid.Should().BeFalse();
            }
        }

        [Fact]
        public void OpenUserSubKey()
        {
            using (var key = RegistryMethods.OpenKey(
                RegistryKeyHandle.HKEY_CURRENT_USER, @"SOFTWARE\Microsoft\Windows NT\CurrentVersion\Winlogon"))
            {
                key.IsInvalid.Should().BeFalse();
            }
        }

        [Fact]
        public void IsLocalHandle()
        {
            using (var key = RegistryMethods.OpenKey(RegistryKeyHandle.HKEY_CURRENT_USER, null))
            {
                key.IsLocalKey.Should().BeFalse();
            }
        }

        [Fact]
        public void IsSpecialKey()
        {
            using (var key = RegistryMethods.OpenKey(RegistryKeyHandle.HKEY_CLASSES_ROOT, null))
            {
                key.IsInvalid.Should().BeFalse();
                key.IsSpecialKey.Should().BeFalse();
            }
        }

        [Fact]
        public void QueryValueExists()
        {
            using (var key = RegistryMethods.OpenKey(
                RegistryKeyHandle.HKEY_CURRENT_USER, @"SOFTWARE\Microsoft\Windows NT\CurrentVersion\Winlogon"))
            {
                RegistryMethods.QueryValueExists(key, "BuildNumber").Should().BeTrue();
            }
        }

        [Fact]
        public void QueryValueExists_False()
        {
            using (var key = RegistryMethods.OpenKey(
                RegistryKeyHandle.HKEY_CURRENT_USER, @"SOFTWARE\Microsoft\Windows NT\CurrentVersion\Winlogon"))
            {
                RegistryMethods.QueryValueExists(key, "Fizzlewig").Should().BeFalse();
            }
        }

        [Fact]
        public void QueryValueType()
        {
            using (var key = RegistryMethods.OpenKey(
                RegistryKeyHandle.HKEY_CURRENT_USER, @"SOFTWARE\Microsoft\Windows NT\CurrentVersion\Winlogon"))
            {
                RegistryMethods.QueryValueType(key, "BuildNumber").Should().Be(RegistryValueType.REG_DWORD);
            }
        }

        [Fact]
        public void GetValueNames()
        {
            using (var key = RegistryMethods.OpenKey(
                RegistryKeyHandle.HKEY_CURRENT_USER, @"SOFTWARE\Microsoft\Windows NT\CurrentVersion\Winlogon"))
            {
                var names = RegistryMethods.GetValueNames(key);
                names.Should().Contain("BuildNumber");
            }
        }

        [Fact]
        public void GetValueNames_PerformanceData()
        {
            var names = RegistryMethods.GetValueNames(RegistryKeyHandle.HKEY_PERFORMANCE_DATA);
            names.Should().ContainInOrder("Global", "Costly");
        }

        [Fact]
        public void GetValueNames_PerformanceText()
        {
            var names = RegistryMethods.GetValueNames(RegistryKeyHandle.HKEY_PERFORMANCE_TEXT);
            names.Should().ContainInOrder("Counter", "Help");
        }

        [Fact]
        public void GetValueNames_PerformanceNlsText()
        {
            var names = RegistryMethods.GetValueNames(RegistryKeyHandle.HKEY_PERFORMANCE_NLSTEXT);
            names.Should().ContainInOrder("Counter", "Help");
        }

        [Fact]
        public void GetValueNamesDirect()
        {
            using (var key = RegistryMethods.OpenKey(
                RegistryKeyHandle.HKEY_CURRENT_USER, @"SOFTWARE\Microsoft\Windows NT\CurrentVersion\Winlogon"))
            {
                var names = RegistryMethods.GetValueNamesDirect(key);
                names.Should().Contain("BuildNumber");
            }
        }

        [Fact]
        public void GetValueDataDirect()
        {
            using (var key = RegistryMethods.OpenKey(
                RegistryKeyHandle.HKEY_LOCAL_MACHINE, @"SOFTWARE\Microsoft\Windows NT\CurrentVersion"))
            {
                var data = RegistryMethods.GetValueDataDirect(key);
                data.Should().NotBeEmpty();
            }
        }

        [Fact]
        public void QueryValue_Uint()
        {
            using (var key = RegistryMethods.OpenKey(
                RegistryKeyHandle.HKEY_CURRENT_USER, @"SOFTWARE\Microsoft\Windows NT\CurrentVersion\Winlogon"))
            {
                var buildNumber = RegistryMethods.QueryValue(key, "BuildNumber");
                buildNumber.Should().BeOfType<uint>();
            }
        }

        [Fact]
        public void QueryValue_String()
        {
            using (var key = RegistryMethods.OpenKey(
                RegistryKeyHandle.HKEY_LOCAL_MACHINE, @"SOFTWARE\Microsoft\Windows NT\CurrentVersion"))
            {
                var productName = RegistryMethods.QueryValue(key, "ProductName");
                productName.Should().BeOfType<string>();
                ((string)productName).Should().StartWith("Windows");
            }
        }
    }
}
