// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using FluentAssertions;
using WInterop.Storage;
using Xunit;

namespace StorageTests
{
    public partial class DiskTests
    {
        [Fact]
        public void GetDiskFreeSpaceForCurrentDrive()
        {
            var freeSpace = Storage.GetDiskFreeSpace(null);
            freeSpace.NumberOfFreeClusters.Should().BeLessOrEqualTo(freeSpace.TotalNumberOfClusters);
            var extendedFreeSpace = Storage.GetDiskFreeSpaceExtended(null);
            extendedFreeSpace.FreeBytesAvailable.Should().BeLessOrEqualTo(extendedFreeSpace.TotalNumberOfBytes);
            extendedFreeSpace.FreeBytesAvailable.Should().BeLessOrEqualTo(extendedFreeSpace.TotalNumberOfFreeBytes);
            extendedFreeSpace.TotalNumberOfBytes.Should().Be(freeSpace.SectorsPerCluster * (ulong)freeSpace.BytesPerSector * freeSpace.TotalNumberOfClusters);
        }
    }
}
