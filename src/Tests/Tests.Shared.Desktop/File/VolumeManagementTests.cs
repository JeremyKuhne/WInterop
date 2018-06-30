// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using FluentAssertions;
using System.Linq;
using WInterop.ErrorHandling.Types;
using WInterop.File;
using Xunit;

namespace DesktopTests
{
    public class VolumeManagementTests
    {
        [Fact]
        public void QueryDosDevice_All()
        {
            var names = FileMethods.QueryDosDevice(null).ToArray();
            names.Should().Contain("Global");
            names.Should().Contain("C:");
            names[names.Length - 1].Should().NotBeNullOrEmpty("we split correctly");
        }

        [Fact]
        public void QueryDosDevice_C()
        {
            var names = FileMethods.QueryDosDevice("C:").ToArray();
            names.Length.Should().Be(1);
            names[0].Should().StartWith(@"\Device\HarddiskVolume");
        }

        [Fact]
        public void BasicLogicalDriveStrings()
        {
            var driveStrings = FileMethods.GetLogicalDriveStrings();
            driveStrings.Should().NotBeEmpty();
            driveStrings.Should().OnlyContain(t => t.Length == 3 && t[0] >= 'A' && t[0] <= 'Z' && t[1] == ':' && t[2] == '\\');
        }

        [Fact]
        public void BasicGetVolumeInformation()
        {
            foreach (string drive in FileMethods.GetLogicalDriveStrings())
            {
                try
                {
                    var info = FileMethods.GetVolumeInformation(drive);
                    info.RootPathName.Should().Be(drive);
                }
                catch (DriveNotReadyException)
                {
                }
                catch (DriveLockedException)
                {
                }
            }
        }

        [Fact]
        public void BasicGetDriveType()
        {
            foreach (string drive in FileMethods.GetLogicalDriveStrings())
            {
                var type = FileMethods.GetDriveType(drive);
            }
        }

        [Fact]
        public void GetVolumeNames()
        {
            var volumeNames = FileMethods.GetVolumes().ToArray();
            volumeNames.Length.Should().BeGreaterThan(0);
            volumeNames[0].Should().StartWith(@"\\?\Volume{");
        }

        [Fact(Skip = "Needs admin access and the API this is calling appears buggy.")]
        public void GetVolumeMountPoints()
        {
            foreach (string volumeName in FileMethods.GetVolumes())
            {
                var mountPoints = FileMethods.GetVolumeMountPoints(volumeName).ToArray();
            }
        }

        [Fact]
        public void GetAllVolumeMountPoints()
        {
            foreach (string volumeName in FileMethods.GetVolumes())
            {
                foreach (string mountPoint in FileMethods.GetVolumePathNamesForVolumeName(volumeName))
                {
                    mountPoint.Should().NotBeNullOrWhiteSpace();
                }
            }
        }
    }
}
