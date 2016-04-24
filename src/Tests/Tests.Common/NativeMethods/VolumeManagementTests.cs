// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Tests.NativeMethodTests
{
    using ErrorHandling;
    using FluentAssertions;
    using System;
    using System.IO;
    using Xunit;

#if DESKTOP
    public class VolumeManagementTests
    {
        [Fact]
        public void BasicLogicalDriveStrings()
        {
            var driveStrings = NativeMethods.VolumeManagement.GetLogicalDriveStrings();
            driveStrings.Should().NotBeEmpty();
            driveStrings.Should().OnlyContain(t => t.Length == 3 && t[0] >= 'A' && t[0] <= 'Z' && t[1] == ':' && t[2] == '\\');
        }

        [Fact]
        public void BasicGetVolumeInformation()
        {
            foreach (string drive in NativeMethods.VolumeManagement.GetLogicalDriveStrings())
            {
                try
                {
                    var info = NativeMethods.VolumeManagement.GetVolumeInformation(drive);
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
            foreach (string drive in NativeMethods.VolumeManagement.GetLogicalDriveStrings())
            {
                var type = NativeMethods.VolumeManagement.GetDriveType(drive);
            }
        }
    }
#endif
}
