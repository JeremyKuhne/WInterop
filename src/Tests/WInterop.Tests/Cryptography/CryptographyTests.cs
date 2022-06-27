// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using WInterop.Cryptography;

namespace CryptographyTests;

public class Basic
{
    [Theory,
        InlineData(StoreName.TrustedPeople),
        InlineData(StoreName.CA)
        ]
    public void OpenSystemStore(StoreName store)
    {
        WInterop.Cryptography.Cryptography.OpenSystemStore(store).IsInvalid.Should().BeFalse();
    }

    [Fact]
    public void BasicEnumerateStores()
    {
        string localMachineName =
#if DESKTOP
                @"\\" + Environment.MachineName;
#else
            // Haven't found an easy way to find the local machine name in a portable assembly. You can get the environment variable,
            // but that only works if you run on desktop, not as a Windows Store App. All of the suggested alternatives for WinRT aren't available
            // in the PCL surface area. Manually putting the machine name in works.
            null;
#endif

        var localMachine = WInterop.Cryptography.Cryptography.EnumerateSystemStores(SystemStoreLocation.LocalMachine);
        localMachine.Should().NotBeEmpty();

        var localMachineByName = WInterop.Cryptography.Cryptography.EnumerateSystemStores(SystemStoreLocation.LocalMachine, localMachineName);

        if (localMachineName != null)
            localMachineByName.Should().OnlyContain(x => x.Name.StartsWith(localMachineName, StringComparison.Ordinal), "when specifying the machine name they should come back with the name");

        localMachineByName.Should().Equal(localMachine, (s1, s2) => s1.Name.EndsWith(s2.Name, StringComparison.Ordinal), "names should be the same whether or not we get local by name");
    }

    [Fact]
    public void BasicEnumerateLocations()
    {
        string[] knownLocations = { "CurrentUser", "LocalMachine", "CurrentService", "Services", "Users", "CurrentUserGroupPolicy", "LocalMachineGroupPolicy", "LocalMachineEnterprise" };
        var locations = WInterop.Cryptography.Cryptography.EnumerateSystemStoreLocations();
        knownLocations.Should().BeSubsetOf(locations);
    }

    [Fact]
    public void BasicEnumeratePhysical()
    {
        string[] knownPhysical = { ".Default", ".AuthRoot", ".GroupPolicy", ".Enterprise" };
        var physical = WInterop.Cryptography.Cryptography.EnumeratePhysicalStores(SystemStoreLocation.LocalMachine, "Root");
        knownPhysical.Should().BeSubsetOf(physical.Select(p => p.PhysicalStoreName));
    }
}
