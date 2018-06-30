// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using FluentAssertions;
using Tests.Support;
using WInterop.Storage;
using Xunit;

namespace Tests.DiskManagement
{
    public class DiskTests
    {
        [Fact]
        public void GetDiskFreeSpaceForCurrentDrive()
        {
            StoreHelper.ValidateStoreGetsUnauthorizedAccess(() =>
            {
                var freeSpace = StorageMethods.GetDiskFreeSpaceExtended(null);
                freeSpace.FreeBytesAvailable.Should().BeLessOrEqualTo(freeSpace.TotalNumberOfBytes);
                freeSpace.FreeBytesAvailable.Should().BeLessOrEqualTo(freeSpace.TotalNumberOfFreeBytes);
            });
        }

        [Fact]
        public void GetDiskFreeSpaceForTempDirectory()
        {
            string tempPath = StorageMethods.GetTempPath();
            var freeSpace = StorageMethods.GetDiskFreeSpaceExtended(tempPath);
            freeSpace.FreeBytesAvailable.Should().BeLessOrEqualTo(freeSpace.TotalNumberOfBytes);
            freeSpace.FreeBytesAvailable.Should().BeLessOrEqualTo(freeSpace.TotalNumberOfFreeBytes);
        }
    }
}
