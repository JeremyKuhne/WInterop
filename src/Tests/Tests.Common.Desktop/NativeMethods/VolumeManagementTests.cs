// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using FluentAssertions;
using WInterop.ErrorHandling;
using Xunit;

namespace WInterop.DesktopTests.NativeMethodTests
{
    public class VolumeManagementTests
    {
        [Fact]
        public void BasicLogicalDriveStrings()
        {
            var driveStrings = VolumeManagement.Desktop.NativeMethods.GetLogicalDriveStrings();
            driveStrings.Should().NotBeEmpty();
            driveStrings.Should().OnlyContain(t => t.Length == 3 && t[0] >= 'A' && t[0] <= 'Z' && t[1] == ':' && t[2] == '\\');
        }

        [Fact]
        public void BasicGetVolumeInformation()
        {
            foreach (string drive in VolumeManagement.Desktop.NativeMethods.GetLogicalDriveStrings())
            {
                try
                {
                    var info = VolumeManagement.Desktop.NativeMethods.GetVolumeInformation(drive);
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
            foreach (string drive in VolumeManagement.Desktop.NativeMethods.GetLogicalDriveStrings())
            {
                var type = VolumeManagement.Desktop.NativeMethods.GetDriveType(drive);
            }
        }
    }
}
