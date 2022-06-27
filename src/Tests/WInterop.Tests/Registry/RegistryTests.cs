// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using WInterop.Registry;

namespace RegistryTests;

public class Basic
{
    [Fact]
    public void OpenUserKey()
    {
        using var key = Registry.OpenKey(RegistryKeyHandle.HKEY_CURRENT_USER, null);
        key.IsInvalid.Should().BeFalse();
    }

    [Fact]
    public void OpenUserSubKey()
    {
        using var key = Registry.OpenKey(
            RegistryKeyHandle.HKEY_CURRENT_USER, @"SOFTWARE\Microsoft\Windows NT\CurrentVersion\Winlogon");
        key.IsInvalid.Should().BeFalse();
    }

    [Fact]
    public void IsLocalHandle()
    {
        using var key = Registry.OpenKey(RegistryKeyHandle.HKEY_CURRENT_USER, null);
        key.IsLocalKey.Should().BeFalse();
    }

    [Fact]
    public void IsSpecialKey()
    {
        using var key = Registry.OpenKey(RegistryKeyHandle.HKEY_CLASSES_ROOT, null);
        key.IsInvalid.Should().BeFalse();
        key.IsSpecialKey.Should().BeFalse();
    }

    [Fact]
    public void QueryValueExists()
    {
        using var key = Registry.OpenKey(
            RegistryKeyHandle.HKEY_CURRENT_USER, @"SOFTWARE\Microsoft\Windows NT\CurrentVersion\Winlogon");
        Registry.QueryValueExists(key, "BuildNumber").Should().BeTrue();
    }

    [Fact]
    public void QueryValueExists_False()
    {
        using var key = Registry.OpenKey(
            RegistryKeyHandle.HKEY_CURRENT_USER, @"SOFTWARE\Microsoft\Windows NT\CurrentVersion\Winlogon");
        Registry.QueryValueExists(key, "Fizzlewig").Should().BeFalse();
    }

    [Fact]
    public void QueryValueType()
    {
        using var key = Registry.OpenKey(
            RegistryKeyHandle.HKEY_CURRENT_USER, @"SOFTWARE\Microsoft\Windows NT\CurrentVersion\Winlogon");
        Registry.QueryValueType(key, "BuildNumber").Should().Be(RegistryValueType.Unsigned32BitInteger);
    }

    [Fact]
    public void GetValueNames()
    {
        using var key = Registry.OpenKey(
            RegistryKeyHandle.HKEY_CURRENT_USER, @"SOFTWARE\Microsoft\Windows NT\CurrentVersion\Winlogon");
        var names = Registry.GetValueNames(key);
        names.Should().Contain("BuildNumber");
    }

    [Fact]
    public void GetValueNames_PerformanceData()
    {
        var names = Registry.GetValueNames(RegistryKeyHandle.HKEY_PERFORMANCE_DATA);
        names.Should().ContainInOrder("Global", "Costly");
    }

    [Fact]
    public void GetValueNames_PerformanceText()
    {
        var names = Registry.GetValueNames(RegistryKeyHandle.HKEY_PERFORMANCE_TEXT);
        names.Should().ContainInOrder("Counter", "Help");
    }

    [Fact]
    public void GetValueNames_PerformanceNlsText()
    {
        var names = Registry.GetValueNames(RegistryKeyHandle.HKEY_PERFORMANCE_NLSTEXT);
        names.Should().ContainInOrder("Counter", "Help");
    }

    [Fact]
    public void GetValueNamesDirect()
    {
        using var key = Registry.OpenKey(
            RegistryKeyHandle.HKEY_CURRENT_USER, @"SOFTWARE\Microsoft\Windows NT\CurrentVersion\Winlogon");
        var names = Registry.GetValueNamesDirect(key);
        names.Should().Contain("BuildNumber");
    }

    [Fact]
    public void GetValueDataDirect()
    {
        using var key = Registry.OpenKey(
            RegistryKeyHandle.HKEY_LOCAL_MACHINE, @"SOFTWARE\Microsoft\Windows NT\CurrentVersion");
        var data = Registry.GetValueDataDirect(key);
        data.Should().NotBeEmpty();
    }

    [Fact]
    public void QueryValue_Uint()
    {
        using var key = Registry.OpenKey(
            RegistryKeyHandle.HKEY_CURRENT_USER, @"SOFTWARE\Microsoft\Windows NT\CurrentVersion\Winlogon");
        var buildNumber = Registry.QueryValue(key, "BuildNumber");
        buildNumber.Should().BeOfType<uint>();
    }

    [Fact]
    public void QueryValue_String()
    {
        using var key = Registry.OpenKey(
            RegistryKeyHandle.HKEY_LOCAL_MACHINE, @"SOFTWARE\Microsoft\Windows NT\CurrentVersion");
        var productName = Registry.QueryValue(key, "ProductName");
        productName.Should().BeOfType<string>();
        ((string)productName).Should().StartWith("Windows");
    }
}
