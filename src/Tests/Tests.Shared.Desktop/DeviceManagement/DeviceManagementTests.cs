// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using FluentAssertions;
using System;
using WInterop.Devices;
using WInterop.ErrorHandling;
using WInterop.Storage;
using Xunit;

namespace DesktopTests.Devices
{
    public class DeviceManagementTests
    {
        [Fact]
        public void QueryDeviceName()
        {
            // Need to open the handle with no rights (desiredAccess: 0) to avoid needing to run as admin
            using (var handle = StorageMethods.CreateFile(@"\\.\C:", CreationDisposition.OpenExisting, desiredAccess: 0))
            {
                DeviceMethods.QueryDeviceName(handle).Should().StartWith(@"\Device\HarddiskVolume");
            }
        }

        [Fact]
        public void QuerySuggestedLinkName()
        {
            // Need to open the handle with no rights (desiredAccess: 0) to avoid needing to run as admin
            using (var handle = StorageMethods.CreateFile(@"\\.\C:", CreationDisposition.OpenExisting, desiredAccess: 0))
            {
                Action action = () => DeviceMethods.QuerySuggestedLinkName(handle);
                action.Should().Throw<WInteropIOException>("this is an optional query, not aware of which drivers support this").
                    And.HResult.Should().Be((int)ErrorMacros.HRESULT_FROM_WIN32(WindowsError.ERROR_NOT_FOUND));
            }
        }

        [Fact]
        public void QueryUniqueId()
        {
            // Need to open the handle with no rights (desiredAccess: 0) to avoid needing to run as admin
            using (var handle = StorageMethods.CreateFile(@"\\.\C:", CreationDisposition.OpenExisting, desiredAccess: 0))
            {
                DeviceMethods.QueryUniqueId(handle).Should().NotBeEmpty();
            }
        }

        [Fact]
        public void QueryStableGuid()
        {
            // Need to open the handle with no rights (desiredAccess: 0) to avoid needing to run as admin
            using (var handle = StorageMethods.CreateFile(@"\\.\C:", CreationDisposition.OpenExisting, desiredAccess: 0))
            {
                DeviceMethods.QueryStableGuid(handle).Should().NotBe(Guid.Empty);
            }
        }

        [Fact]
        public void QueryInterfaceName()
        {
            // TODO: Need to conditionalize this on RS1

            // Need to open the handle with no rights (desiredAccess: 0) to avoid needing to run as admin
            using (var handle = StorageMethods.CreateFile(@"\\.\C:", CreationDisposition.OpenExisting, desiredAccess: 0))
            {
                DeviceMethods.QueryInterfacename(handle).Should().StartWith(@"\\?\STORAGE#Volume#{");
            }
        }
    }
}
