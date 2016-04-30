// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using FluentAssertions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using WInterop.Buffers;
using WInterop.Handles;
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
                string name = NativeMethods.Handles.GetObjectType(directory);
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
        public void GetFileNameBasic()
        {
            string tempPath = NativeMethods.FileManagement.GetTempPath();
            using (var directory = NativeMethods.FileManagement.CreateFile(tempPath, FileAccess.Read, FileShare.ReadWrite, FileMode.Open,
                FileManagement.FileAttributes.NONE, FileManagement.FileFlags.FILE_FLAG_BACKUP_SEMANTICS))
            {
                // This will give back the local path (minus the device, eg \Users\... or \Server\Share\...)
                string name = NativeMethods.Handles.GetFileName(directory);

                tempPath.Should().EndWith(Paths.AddTrailingSeparator(name));
            }
        }

        [Fact]
        public void GetVolumeNameBasic()
        {
            string tempPath = NativeMethods.FileManagement.GetTempPath();
            using (var directory = NativeMethods.FileManagement.CreateFile(tempPath, FileAccess.Read, FileShare.ReadWrite, FileMode.Open,
                FileManagement.FileAttributes.NONE, FileManagement.FileFlags.FILE_FLAG_BACKUP_SEMANTICS))
            {
                // This will give back the NT volume path (\Device\HarddiskVolumen\)
                string name = NativeMethods.Handles.GetVolumeName(directory);

                name.Should().StartWith(@"\Device\");
            }
        }

        [Fact]
        public void GetShortNameBasic()
        {
            string tempPath = NativeMethods.FileManagement.GetTempPath();
            using (var directory = NativeMethods.FileManagement.CreateFile(tempPath, FileAccess.Read, FileShare.ReadWrite, FileMode.Open,
                FileManagement.FileAttributes.NONE, FileManagement.FileFlags.FILE_FLAG_BACKUP_SEMANTICS))
            {
                // This will give back the NT volume path (\Device\HarddiskVolumen\)
                string directoryName = NativeMethods.Handles.GetShortName(directory);
                directoryName.Should().Be("Temp");

                string tempFileName = "ExtraLongName" + Path.GetRandomFileName();
                string tempFilePath = Path.Combine(tempPath, tempFileName);
                try
                {
                    using (var file = NativeMethods.FileManagement.CreateFile(tempFilePath, FileAccess.Read, FileShare.ReadWrite, FileMode.Create))
                    {
                        string fileName = NativeMethods.Handles.GetShortName(file);
                        fileName.Length.Should().BeLessOrEqualTo(12);
                    }
                }
                finally
                {
                    NativeMethods.FileManagement.DeleteFile(tempFilePath);
                }
            }
        }

        [Fact]
        public void OpenDosDeviceDirectory()
        {
            StoreUnauthorizedAccess(() =>
            {
                using (var directory = NativeMethods.Handles.OpenDirectoryObject(@"\??"))
                {
                    directory.IsInvalid.Should().BeFalse();
                }
            });
        }

        [Fact]
        public void OpenGlobalDosDeviceDirectory()
        {
            StoreUnauthorizedAccess(() =>
            {
                using (var directory = NativeMethods.Handles.OpenDirectoryObject(@"\Global??"))
                {
                    directory.IsInvalid.Should().BeFalse();
                }
            });
        }

        [Fact]
        public void OpenRootDirectory()
        {
            StoreUnauthorizedAccess(() =>
            {
                using (var directory = NativeMethods.Handles.OpenDirectoryObject(@"\"))
                {
                    directory.IsInvalid.Should().BeFalse();
                }
            });
        }

        [Fact]
        public void GetRootDirectoryEntries()
        {
            StoreUnauthorizedAccess(() =>
            {
                using (var directory = NativeMethods.Handles.OpenDirectoryObject(@"\"))
                {
                    IEnumerable<ObjectInformation> objects = NativeMethods.Handles.GetDirectoryEntries(directory);
                    objects.Should().NotBeEmpty();
                    objects.Should().Contain(new ObjectInformation { Name = "Device", TypeName = "Directory" });
                }
            });
        }


        [Fact]
        public void OpenCDriveSymbolicLink()
        {
            StoreUnauthorizedAccess(() =>
            {
                using (var link = NativeMethods.Handles.OpenSymbolicLinkObject(@"\??\C:"))
                {
                    link.IsInvalid.Should().BeFalse();
                }
            });
        }

        [Fact]
        public void GetTargetForCDriveSymbolicLink()
        {
            StoreUnauthorizedAccess(() =>
            {
                using (var link = NativeMethods.Handles.OpenSymbolicLinkObject(@"\??\C:"))
                {
                    string target = NativeMethods.Handles.GetSymbolicLinkTarget(link);
                    target.Should().StartWith(@"\Device\");
                }
            });
        }

        public static void StoreUnauthorizedAccess(Action action)
        {
            try
            {
                action();
                Utility.Environment.IsWindowsStoreApplication().Should().BeFalse();
            }
            catch (UnauthorizedAccessException)
            {
                Utility.Environment.IsWindowsStoreApplication().Should().BeTrue();
            }
        }

        [Fact]
        public void CanCreateHandleToMountPointManager()
        {
            StoreUnauthorizedAccess(() =>
            {
                using (var mountPointManager = NativeMethods.FileManagement.CreateFile(
                    @"\\.\MountPointManager", 0, FileShare.ReadWrite, FileMode.Open, FileManagement.FileAttributes.FILE_ATTRIBUTE_NORMAL))
                {
                    mountPointManager.IsInvalid.Should().BeFalse();
                }
            });
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
