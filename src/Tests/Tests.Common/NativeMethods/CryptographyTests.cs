// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using FluentAssertions;
using System;
using Xunit;
using System.Linq;
using WInterop.Cryptography;

namespace WInterop.Tests.NativeMethodTests
{
    public class CryptographyTests
    {
        [Theory
            InlineData(StoreName.TrustedPeople)
            InlineData(StoreName.CA)
            ]
        public void OpenSystemStore(StoreName store)
        {
            Cryptography.NativeMethods.OpenSystemStore(store).IsInvalid.Should().BeFalse();
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

            var localMachine = Cryptography.NativeMethods.EnumerateSystemStores(SystemStoreLocation.CERT_SYSTEM_STORE_LOCAL_MACHINE);
            localMachine.Should().NotBeEmpty();

            var localMachineByName = Cryptography.NativeMethods.EnumerateSystemStores(SystemStoreLocation.CERT_SYSTEM_STORE_LOCAL_MACHINE, localMachineName);

            if (localMachineName != null)
                localMachineByName.Should().OnlyContain(x => x.Name.StartsWith(localMachineName, StringComparison.Ordinal), "when specifying the machine name they should come back with the name");

            localMachineByName.Should().Equal(localMachine, (s1, s2) => s1.Name.EndsWith(s2.Name, StringComparison.Ordinal), "names should be the same whether or not we get local by name");
        }

        [Fact]
        public void BasicEnumerateLocations()
        {
            string[] knownLocations = { "CurrentUser", "LocalMachine", "CurrentService", "Services", "Users", "CurrentUserGroupPolicy", "LocalMachineGroupPolicy", "LocalMachineEnterprise" };
            var locations = Cryptography.NativeMethods.EnumerateSystemStoreLocations();
            knownLocations.Should().BeSubsetOf(locations);
        }

        [Fact]
        public void BasicEnumeratePhysical()
        {
            string[] knownPhysical = { ".Default", ".AuthRoot", ".GroupPolicy", ".Enterprise" };
            var physical = Cryptography.NativeMethods.EnumeratePhysicalStores(SystemStoreLocation.CERT_SYSTEM_STORE_LOCAL_MACHINE, "Root");
            knownPhysical.Should().BeSubsetOf(physical.Select(p => p.PhysicalStoreName));
        }
    }
}
