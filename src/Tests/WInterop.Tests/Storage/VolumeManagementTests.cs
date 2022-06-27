// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using WInterop.Errors;
using WInterop.Storage;

namespace StorageTests;

public class Volumes
{
    [Fact]
    public void QueryDosDevice_All()
    {
        var names = Storage.QueryDosDevice(null).ToArray();
        names.Should().Contain("Global");
        names.Should().Contain("C:");
        names[^1].Should().NotBeNullOrEmpty("we split correctly");
    }

    [Fact]
    public void QueryDosDevice_C()
    {
        var names = Storage.QueryDosDevice("C:").ToArray();
        names.Length.Should().Be(1);
        names[0].Should().StartWith(@"\Device\HarddiskVolume");
    }

    [Fact]
    public void BasicLogicalDriveStrings()
    {
        var driveStrings = Storage.GetLogicalDriveStrings();
        driveStrings.Should().NotBeEmpty();
        driveStrings.Should().OnlyContain(t => t.Length == 3 && t[0] >= 'A' && t[0] <= 'Z' && t[1] == ':' && t[2] == '\\');
    }

    [Fact]
    public void BasicGetVolumeInformation()
    {
        foreach (string drive in Storage.GetLogicalDriveStrings())
        {
            try
            {
                var info = Storage.GetVolumeInformation(drive);
                info.RootPathName.Should().Be(drive);
            }
            catch (DriveNotReadyException)
            {
            }
            catch (DriveLockedException)
            {
            }
            catch (WInteropIOException e) when (
                (HResult)e.HResult == WindowsError.ERROR_FT_READ_FAILURE.ToHResult()
                || (HResult)e.HResult == WindowsError.ERROR_UNRECOGNIZED_VOLUME.ToHResult())
            {
                // Had this happen with Storage Spaces not being online
            }
        }
    }

    [Fact]
    public void BasicGetDriveType()
    {
        // Null is the current directory's drive (likely Fixed)
        var type = Storage.GetDriveType(null);
        type.Should().NotBe(WInterop.Storage.DriveType.Unknown);

        foreach (string drive in Storage.GetLogicalDriveStrings())
        {
            type = Storage.GetDriveType(drive);
        }
    }

    [Fact]
    public void GetVolumeNames()
    {
        var volumeNames = Storage.GetVolumes().ToArray();
        volumeNames.Length.Should().BeGreaterThan(0);
        volumeNames.Should().OnlyContain(x => x.StartsWith(@"\\?\Volume{"));
    }

    [Fact(Skip = "Needs admin access and the API this is calling appears buggy.")]
    public void GetVolumeMountPoints()
    {
        foreach (string volumeName in Storage.GetVolumes())
        {
            _ = Storage.GetVolumeMountPoints(volumeName).ToArray();
        }
    }

    [Fact]
    public void GetAllVolumeMountPoints()
    {
        foreach (string volumeName in Storage.GetVolumes())
        {
            foreach (string mountPoint in Storage.GetVolumePathNamesForVolumeName(volumeName))
            {
                mountPoint.Should().NotBeNullOrWhiteSpace();
            }
        }
    }
}
