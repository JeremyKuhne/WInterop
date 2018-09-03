// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using FluentAssertions;
using System;
using WInterop.Devices;
using WInterop.Errors;
using WInterop.Storage;
using Xunit;

namespace DevicesTests
{
    public class Basic
    {
        [Fact]
        public void QueryDeviceName()
        {
            // Need to open the handle with no rights (desiredAccess: 0) to avoid needing to run as admin
            using (var handle = Storage.CreateFile(@"\\.\C:", CreationDisposition.OpenExisting, desiredAccess: 0))
            {
                Devices.QueryDeviceName(handle).Should().StartWith(@"\Device\HarddiskVolume");
            }
        }

        [Fact]
        public void QuerySuggestedLinkName()
        {
            // Need to open the handle with no rights (desiredAccess: 0) to avoid needing to run as admin
            using (var handle = Storage.CreateFile(@"\\.\C:", CreationDisposition.OpenExisting, desiredAccess: 0))
            {
                Action action = () => WInterop.Devices.Devices.QuerySuggestedLinkName(handle);
                action.Should().Throw<WInteropIOException>("this is an optional query, not aware of which drivers support this").
                    And.HResult.Should().Be((int)WindowsError.ERROR_NOT_FOUND.ToHResult());
            }
        }

        [Fact]
        public void QueryUniqueId()
        {
            // Need to open the handle with no rights (desiredAccess: 0) to avoid needing to run as admin
            using (var handle = Storage.CreateFile(@"\\.\C:", CreationDisposition.OpenExisting, desiredAccess: 0))
            {
                Devices.QueryUniqueId(handle).Should().NotBeEmpty();
            }
        }

        [Fact]
        public void QueryStableGuid()
        {
            // Need to open the handle with no rights (desiredAccess: 0) to avoid needing to run as admin
            using (var handle = Storage.CreateFile(@"\\.\C:", CreationDisposition.OpenExisting, desiredAccess: 0))
            {
                Devices.QueryStableGuid(handle).Should().NotBe(Guid.Empty);
            }
        }

        [Fact]
        public void QueryInterfaceName()
        {
            // TODO: Need to conditionalize this on RS1

            // Need to open the handle with no rights (desiredAccess: 0) to avoid needing to run as admin
            using (var handle = Storage.CreateFile(@"\\.\C:", CreationDisposition.OpenExisting, desiredAccess: 0))
            {
                Devices.QueryInterfacename(handle).Should().StartWith(@"\\?\STORAGE#Volume#{");
            }
        }
    }
}
