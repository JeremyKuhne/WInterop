// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using FluentAssertions;
using System;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using WInterop.Buffers;
using WInterop.Utility;
using Xunit;

namespace WInterop.Tests.NativeMethodsTests
{
    public class HandlesTests
    {
        [Fact]
        public void GetHandleTypeBasic()
        {
            string tempPath = NativeMethods.FileManagement.GetTempPath();
            using (var directory = NativeMethods.FileManagement.CreateFile(tempPath, FileAccess.Read, FileShare.ReadWrite, FileMode.Open,
                FileManagement.FileAttributes.NONE, FileManagement.FileFlags.FILE_FLAG_BACKUP_SEMANTICS))
            {
                string name = NativeMethods.Handles.GetObjectTypeName(directory);
                name.Should().Be("File");
            }
        }

        [Fact]
        public void GetHandleNameBasic()
        {
            string tempPath = NativeMethods.FileManagement.GetTempPath();
            using (var directory = NativeMethods.FileManagement.CreateFile(tempPath, FileAccess.Read, FileShare.ReadWrite, FileMode.Open,
                FileManagement.FileAttributes.NONE, FileManagement.FileFlags.FILE_FLAG_BACKUP_SEMANTICS))
            {
                // This will give back the NT path (\Device\HarddiskVolumen...)
                string name = NativeMethods.Handles.GetObjectName(directory);

                // Skip past the C:\
                Paths.AddTrailingSeparator(name).Should().EndWith(tempPath.Substring(3));
            }
        }

        [Fact]
        public void CanCreateHandleToMountPointManager()
        {
            try
            {
                using (var mountPointManager = NativeMethods.FileManagement.CreateFile(
                    @"\\.\MountPointManager", 0, FileShare.ReadWrite, FileMode.Open, FileManagement.FileAttributes.FILE_ATTRIBUTE_NORMAL))
                {
                    mountPointManager.IsInvalid.Should().BeFalse();
                }
            }
            catch (UnauthorizedAccessException)
            {
                Utility.Environment.IsWindowsStoreApplication().Should().BeTrue();
            }
        }

#if DESKTOP
        [Fact]
        public void QueryDosVolumePathBasic()
        {
            string tempPath = NativeMethods.FileManagement.GetTempPath();
            using (var directory = NativeMethods.FileManagement.CreateFile(tempPath, FileAccess.Read, FileShare.ReadWrite, FileMode.Open,
                FileManagement.FileAttributes.NONE, FileManagement.FileFlags.FILE_FLAG_BACKUP_SEMANTICS))
            {
                // This will give back the NT path (\Device\HarddiskVolumen...)
                string fullName = NativeMethods.Handles.GetObjectName(directory);
                string fileName = NativeMethods.FileManagement.GetFileNameByHandle(directory);
                string deviceName = fullName.Substring(0, fullName.Length - fileName.Length);

                string dosVolumePath = NativeMethods.DeviceManagement.Desktop.QueryDosVolumePath(deviceName);

                tempPath.Should().StartWith(dosVolumePath);
                tempPath.Should().Be(dosVolumePath + fileName + @"\");
            }
        }
#endif
    }
}
