// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Tests.Support;
using WInterop.Storage;

namespace StorageTests;

public partial class DiskTests
{
    [Fact]
    public void GetDiskFreeSpaceExtendedForCurrentDrive()
    {
        StoreHelper.ValidateStoreGetsUnauthorizedAccess(() =>
        {
            var freeSpace = Storage.GetDiskFreeSpaceExtended(null);
            freeSpace.FreeBytesAvailable.Should().BeLessOrEqualTo(freeSpace.TotalNumberOfBytes);
            freeSpace.FreeBytesAvailable.Should().BeLessOrEqualTo(freeSpace.TotalNumberOfFreeBytes);
        });
    }

    [Fact]
    public void GetDiskFreeSpaceForTempDirectory()
    {
        string tempPath = Storage.GetTempPath();
        var freeSpace = Storage.GetDiskFreeSpaceExtended(tempPath);
        freeSpace.FreeBytesAvailable.Should().BeLessOrEqualTo(freeSpace.TotalNumberOfBytes);
        freeSpace.FreeBytesAvailable.Should().BeLessOrEqualTo(freeSpace.TotalNumberOfFreeBytes);
    }
}
