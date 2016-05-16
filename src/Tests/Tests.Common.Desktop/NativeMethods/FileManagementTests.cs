// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using FluentAssertions;
using System.Linq;
using WInterop.FileManagement;
using WInterop.FileManagement.Desktop;
using WInterop.Tests.Support;
using WInterop.Utility;
using Xunit;

namespace WInterop.DesktopTests.NativeMethodTests
{
    public partial class FileManagementTests
    {
        [Fact]
        public void GetShortPathBasic()
        {
            string tempPath = NativeMethods.GetTempPath();
            DesktopNativeMethods.GetShortPathName(tempPath).Should().NotBeNullOrWhiteSpace();
        }

        [Fact]
        public void GetLongPathBasic()
        {
            string tempPath = NativeMethods.GetTempPath();
            DesktopNativeMethods.GetLongPathName(tempPath).Should().NotBeNullOrWhiteSpace();
        }

        [Fact]
        public void FinalPathNameVolumeNameBehavior()
        {
            // This test is asserting that the original volume name has nothing to do with the volume GetFinalPathNameByHandle returns
            using (var cleaner = new TestFileCleaner())
            {
                string filePath = cleaner.CreateTestFile("FinalPathNameVolumeNameBehavior");

                using (var handle = NativeMethods.CreateFile(filePath.ToLower(), DesiredAccess.GENERIC_READ, ShareMode.FILE_SHARE_READWRITE, CreationDisposition.OPEN_EXISTING))
                {
                    handle.IsInvalid.Should().BeFalse();

                    string extendedPath = @"\\?\" + filePath;
                    DesktopNativeMethods.GetFinalPathNameByHandle(handle, GetFinalPathNameByHandleFlags.FILE_NAME_NORMALIZED)
                        .Should().Be(extendedPath);
                    DesktopNativeMethods.GetFinalPathNameByHandle(handle, GetFinalPathNameByHandleFlags.FILE_NAME_OPENED)
                        .Should().Be(extendedPath);
                    DesktopNativeMethods.GetFinalPathNameByHandle(handle, GetFinalPathNameByHandleFlags.VOLUME_NAME_DOS)
                        .Should().Be(extendedPath);
                    DesktopNativeMethods.GetFinalPathNameByHandle(handle, GetFinalPathNameByHandleFlags.VOLUME_NAME_GUID)
                        .Should().StartWith(@"\\?\Volume");
                    DesktopNativeMethods.GetFinalPathNameByHandle(handle, GetFinalPathNameByHandleFlags.VOLUME_NAME_NT)
                        .Should().StartWith(@"\Device\");
                    DesktopNativeMethods.GetFinalPathNameByHandle(handle, GetFinalPathNameByHandleFlags.VOLUME_NAME_NONE)
                        .Should().Be(filePath.Substring(2));
                }
            }
        }
    }
}
