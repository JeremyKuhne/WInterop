// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using Xunit;
using FluentAssertions;
using WInterop.Desktop.Registry;
using WInterop.Desktop.Registry.DataTypes;

namespace DesktopTests.Registry
{
    public class RegistryTests
    {
        [Fact]
        public void OpenUserKey()
        {
            using (var key = RegistryDesktopMethods.OpenKey(RegistryKeyHandle.HKEY_CURRENT_USER, null))
            {
                key.IsInvalid.Should().BeFalse();
            }
        }

        [Fact]
        public void OpenUserSubKey()
        {
            using (var key = RegistryDesktopMethods.OpenKey(
                RegistryKeyHandle.HKEY_CURRENT_USER, @"SOFTWARE\Microsoft\Windows NT\CurrentVersion\Winlogon"))
            {
                key.IsInvalid.Should().BeFalse();
            }
        }

        [Fact]
        public void IsLocalHandle()
        {
            using (var key = RegistryDesktopMethods.OpenKey(RegistryKeyHandle.HKEY_CURRENT_USER, null))
            {
                key.IsLocalKey.Should().BeTrue();
            }
        }

        [Fact]
        public void IsSpecialKey()
        {
            using (var key = RegistryDesktopMethods.OpenKey(RegistryKeyHandle.HKEY_CLASSES_ROOT, null))
            {
                key.IsInvalid.Should().BeFalse();
                key.IsSpecialKey.Should().BeTrue("the key should be special");
            }
        }

        [Fact]
        public void QueryValueExists()
        {
            using (var key = RegistryDesktopMethods.OpenKey(
                RegistryKeyHandle.HKEY_CURRENT_USER, @"SOFTWARE\Microsoft\Windows NT\CurrentVersion\Winlogon"))
            {
                RegistryDesktopMethods.QueryValueExists(key, "BuildNumber").Should().BeTrue();
            }
        }

        [Fact]
        public void QueryValueExists_False()
        {
            using (var key = RegistryDesktopMethods.OpenKey(
                RegistryKeyHandle.HKEY_CURRENT_USER, @"SOFTWARE\Microsoft\Windows NT\CurrentVersion\Winlogon"))
            {
                RegistryDesktopMethods.QueryValueExists(key, "Fizzlewig").Should().BeFalse();
            }
        }

        [Fact]
        public void QueryValueType()
        {
            using (var key = RegistryDesktopMethods.OpenKey(
                RegistryKeyHandle.HKEY_CURRENT_USER, @"SOFTWARE\Microsoft\Windows NT\CurrentVersion\Winlogon"))
            {
                RegistryDesktopMethods.QueryValueType(key, "BuildNumber").Should().Be(RegistryValueType.REG_DWORD);
            }
        }

        [Fact]
        public void GetValueNames()
        {
            using (var key = RegistryDesktopMethods.OpenKey(
                RegistryKeyHandle.HKEY_CURRENT_USER, @"SOFTWARE\Microsoft\Windows NT\CurrentVersion\Winlogon"))
            {
                var names = RegistryDesktopMethods.GetValueNames(key);
                names.Should().Contain("BuildNumber");
            }
        }

        [Fact]
        public void GetValueNamesDirect()
        {
            using (var key = RegistryDesktopMethods.OpenKey(
                RegistryKeyHandle.HKEY_CURRENT_USER, @"SOFTWARE\Microsoft\Windows NT\CurrentVersion\Winlogon"))
            {
                var names = RegistryDesktopMethods.GetValueNamesDirect(key);
                names.Should().Contain("BuildNumber");
            }
        }

        [Fact]
        public void GetValueDataDirect()
        {
            using (var key = RegistryDesktopMethods.OpenKey(
                RegistryKeyHandle.HKEY_LOCAL_MACHINE, @"SOFTWARE\Microsoft\Windows NT\CurrentVersion"))
            {
                var data = RegistryDesktopMethods.GetValueDataDirect(key);
                data.Should().NotBeEmpty();
            }
        }

        [Fact]
        public void QueryValue_Uint()
        {
            using (var key = RegistryDesktopMethods.OpenKey(
                RegistryKeyHandle.HKEY_CURRENT_USER, @"SOFTWARE\Microsoft\Windows NT\CurrentVersion\Winlogon"))
            {
                var buildNumber = RegistryDesktopMethods.QueryValue(key, "BuildNumber");
                buildNumber.Should().BeOfType<uint>();
            }
        }

        [Fact]
        public void QueryValue_String()
        {
            using (var key = RegistryDesktopMethods.OpenKey(
                RegistryKeyHandle.HKEY_LOCAL_MACHINE, @"SOFTWARE\Microsoft\Windows NT\CurrentVersion"))
            {
                var productName = RegistryDesktopMethods.QueryValue(key, "ProductName");
                productName.Should().BeOfType<string>();
                ((string)productName).Should().StartWith("Windows");
            }
        }
    }
}
