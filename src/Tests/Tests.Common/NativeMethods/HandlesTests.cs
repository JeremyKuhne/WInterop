// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using FluentAssertions;
using System.Collections.Generic;
using WInterop.FileManagement;
using WInterop.Tests.Support;
using WInterop.Utility;
using Xunit;

namespace WInterop.Tests.NativeMethodsTests
{
    public class HandlesTests
    {
#if DESKTOP
        [Fact]
        public void GetHandleTypeBasic()
        {
            string tempPath = FileManagement.NativeMethods.GetTempPath();
            using (var directory = FileManagement.NativeMethods.CreateFile(tempPath, DesiredAccess.GENERIC_READ, ShareMode.ReadWrite, CreationDisposition.OPEN_EXISTING,
                FileAttributes.NONE, FileManagement.FileFlags.FILE_FLAG_BACKUP_SEMANTICS))
            {
                string name = Handles.Desktop.NativeMethods.GetObjectType(directory);
                name.Should().Be("File");
            }
        }

        [Fact]
        public void GetHandleNameBasic()
        {
            string tempPath = FileManagement.NativeMethods.GetTempPath();
            using (var directory = FileManagement.NativeMethods.CreateFile(tempPath, DesiredAccess.GENERIC_READ, ShareMode.ReadWrite, CreationDisposition.OPEN_EXISTING,
                FileAttributes.NONE, FileManagement.FileFlags.FILE_FLAG_BACKUP_SEMANTICS))
            {
                // This will give back the NT path (\Device\HarddiskVolumen...)
                string name = Handles.Desktop.NativeMethods.GetObjectName(directory);

                // Skip past the C:\
                Paths.AddTrailingSeparator(name).Should().EndWith(tempPath.Substring(3));
            }
        }

        [Fact]
        public void GetFileNameBasic()
        {
            string tempPath = FileManagement.NativeMethods.GetTempPath();
            using (var directory = FileManagement.NativeMethods.CreateFile(tempPath, DesiredAccess.GENERIC_READ, ShareMode.ReadWrite, CreationDisposition.OPEN_EXISTING,
                FileAttributes.NONE, FileManagement.FileFlags.FILE_FLAG_BACKUP_SEMANTICS))
            {
                // This will give back the local path (minus the device, eg \Users\... or \Server\Share\...)
                string name = Handles.Desktop.NativeMethods.GetFileName(directory);

                tempPath.Should().EndWith(Paths.AddTrailingSeparator(name));
            }
        }

        [Fact]
        public void GetVolumeNameBasic()
        {
            string tempPath = FileManagement.NativeMethods.GetTempPath();
            using (var directory = FileManagement.NativeMethods.CreateFile(tempPath, DesiredAccess.GENERIC_READ, ShareMode.ReadWrite, CreationDisposition.OPEN_EXISTING,
                FileAttributes.NONE, FileManagement.FileFlags.FILE_FLAG_BACKUP_SEMANTICS))
            {
                // This will give back the NT volume path (\Device\HarddiskVolumen\)
                string name = Handles.Desktop.NativeMethods.GetVolumeName(directory);

                name.Should().StartWith(@"\Device\");
            }
        }

        [Fact]
        public void GetShortNameBasic()
        {
            string tempPath = FileManagement.NativeMethods.GetTempPath();
            using (var directory = FileManagement.NativeMethods.CreateFile(tempPath, DesiredAccess.GENERIC_READ, ShareMode.ReadWrite, CreationDisposition.OPEN_EXISTING,
                FileAttributes.NONE, FileManagement.FileFlags.FILE_FLAG_BACKUP_SEMANTICS))
            {
                // This will give back the NT volume path (\Device\HarddiskVolumen\)
                string directoryName = Handles.Desktop.NativeMethods.GetShortName(directory);
                directoryName.Should().Be("Temp");

                string tempFileName = "ExtraLongName" + System.IO.Path.GetRandomFileName();
                string tempFilePath = System.IO.Path.Combine(tempPath, tempFileName);
                try
                {
                    using (var file = FileManagement.NativeMethods.CreateFile(tempFilePath, DesiredAccess.GENERIC_READ, ShareMode.ReadWrite, CreationDisposition.CREATE_NEW))
                    {
                        string fileName = Handles.Desktop.NativeMethods.GetShortName(file);
                        fileName.Length.Should().BeLessOrEqualTo(12);
                    }
                }
                finally
                {
                    FileManagement.NativeMethods.DeleteFile(tempFilePath);
                }
            }
        }

        [Fact]
        public void OpenDosDeviceDirectory()
        {
            using (var directory = Handles.Desktop.NativeMethods.OpenDirectoryObject(@"\??"))
            {
                directory.IsInvalid.Should().BeFalse();
            }
        }

        [Fact]
        public void OpenGlobalDosDeviceDirectory()
        {
            using (var directory = Handles.Desktop.NativeMethods.OpenDirectoryObject(@"\Global??"))
            {
                directory.IsInvalid.Should().BeFalse();
            }
        }

        [Fact]
        public void OpenRootDirectory()
        {
            using (var directory = Handles.Desktop.NativeMethods.OpenDirectoryObject(@"\"))
            {
                directory.IsInvalid.Should().BeFalse();
            }
        }

        [Fact]
        public void GetRootDirectoryEntries()
        {
            using (var directory = Handles.Desktop.NativeMethods.OpenDirectoryObject(@"\"))
            {
                IEnumerable<Handles.Desktop.ObjectInformation> objects = Handles.Desktop.NativeMethods.GetDirectoryEntries(directory);
                objects.Should().NotBeEmpty();
                objects.Should().Contain(new Handles.Desktop.ObjectInformation { Name = "Device", TypeName = "Directory" });
            }
        }

        [Fact]
        public void OpenCDriveSymbolicLink()
        {
            using (var link = Handles.Desktop.NativeMethods.OpenSymbolicLinkObject(@"\??\C:"))
            {
                link.IsInvalid.Should().BeFalse();
            }
        }

        [Fact]
        public void GetTargetForCDriveSymbolicLink()
        {
            using (var link = Handles.Desktop.NativeMethods.OpenSymbolicLinkObject(@"\??\C:"))
            {
                string target = Handles.Desktop.NativeMethods.GetSymbolicLinkTarget(link);
                target.Should().StartWith(@"\Device\");
            }
        }
#endif

        [Fact]
        public void CanCreateHandleToMountPointManager()
        {
            StoreHelper.ValidateStoreGetsUnauthorizedAccess(() =>
            {
                using (var mountPointManager = FileManagement.NativeMethods.CreateFile(
                    @"\\.\MountPointManager", 0, ShareMode.ReadWrite, CreationDisposition.OPEN_EXISTING, FileManagement.FileAttributes.FILE_ATTRIBUTE_NORMAL))
                {
                    mountPointManager.IsInvalid.Should().BeFalse();
                }
            });
        }

#if DESKTOP
        [Fact]
        public void QueryDosVolumePathBasic()
        {
            string tempPath = FileManagement.NativeMethods.GetTempPath();
            using (var directory = FileManagement.NativeMethods.CreateFile(tempPath, DesiredAccess.GENERIC_READ, ShareMode.ReadWrite, CreationDisposition.OPEN_EXISTING,
                FileAttributes.NONE, FileManagement.FileFlags.FILE_FLAG_BACKUP_SEMANTICS))
            {
                // This will give back the NT path (\Device\HarddiskVolumen...)
                string fullName = Handles.Desktop.NativeMethods.GetObjectName(directory);
                string fileName = FileManagement.NativeMethods.GetFileNameByHandle(directory);
                string deviceName = fullName.Substring(0, fullName.Length - fileName.Length);

                string dosVolumePath = DeviceManagement.Desktop.NativeMethods.QueryDosVolumePath(deviceName);

                tempPath.Should().StartWith(dosVolumePath);
                tempPath.Should().Be(dosVolumePath + fileName + @"\");
            }
        }
#endif
    }
}
