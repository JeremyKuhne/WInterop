// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using FluentAssertions;
using System.Linq;
using WInterop.ErrorHandling.Types;
using WInterop.VolumeManagement;
using Xunit;

namespace DesktopTests
{
    public class VolumeManagementTests
    {
        [Fact]
        public void BasicLogicalDriveStrings()
        {
            var driveStrings = VolumeMethods.GetLogicalDriveStrings();
            driveStrings.Should().NotBeEmpty();
            driveStrings.Should().OnlyContain(t => t.Length == 3 && t[0] >= 'A' && t[0] <= 'Z' && t[1] == ':' && t[2] == '\\');
        }

        [Fact]
        public void BasicGetVolumeInformation()
        {
            foreach (string drive in VolumeMethods.GetLogicalDriveStrings())
            {
                try
                {
                    var info = VolumeMethods.GetVolumeInformation(drive);
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
            foreach (string drive in VolumeMethods.GetLogicalDriveStrings())
            {
                var type = VolumeMethods.GetDriveType(drive);
            }
        }

        [Fact]
        public void GetVolumeNames()
        {
            var volumeNames = VolumeMethods.GetVolumes().ToArray();
            volumeNames.Length.Should().BeGreaterThan(0);
            volumeNames[0].Should().StartWith(@"\\?\Volume{");
        }

        [Fact(Skip = "Needs admin access and the API this is calling appears buggy.")]
        public void GetVolumeMountPoints()
        {
            foreach (string volumeName in VolumeMethods.GetVolumes())
            {
                var mountPoints = VolumeMethods.GetVolumeMountPoints(volumeName).ToArray();
            }
        }

        [Fact]
        public void GetAllVolumeMountPoints()
        {
            foreach (string volumeName in VolumeMethods.GetVolumes())
            {
                foreach (string mountPoint in VolumeMethods.GetVolumePathNamesForVolumeName(volumeName))
                {
                    mountPoint.Should().NotBeNullOrWhiteSpace();
                }
            }
        }
    }
}
