// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using FluentAssertions;
using WInterop.DiskManagement;
using Xunit;

namespace DesktopTests.DiskManagementTests
{
    public class Methods
    {
        [Fact]
        public void GetDiskFreeSpaceForCurrentDrive()
        {
            var freeSpace = DiskMethods.GetDiskFreeSpace(null);
            freeSpace.NumberOfFreeClusters.Should().BeLessOrEqualTo(freeSpace.TotalNumberOfClusters);
            var extendedFreeSpace = DiskMethods.GetDiskFreeSpaceExtended(null);
            extendedFreeSpace.FreeBytesAvailable.Should().BeLessOrEqualTo(extendedFreeSpace.TotalNumberOfBytes);
            extendedFreeSpace.FreeBytesAvailable.Should().BeLessOrEqualTo(extendedFreeSpace.TotalNumberOfFreeBytes);
            extendedFreeSpace.TotalNumberOfBytes.Should().Be(freeSpace.SectorsPerCluster * (ulong)freeSpace.BytesPerSector * freeSpace.TotalNumberOfClusters);
        }
    }
}
