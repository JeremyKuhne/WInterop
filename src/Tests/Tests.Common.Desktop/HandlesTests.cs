// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using FluentAssertions;
using System;
using System.Collections.Generic;
using WInterop.DeviceManagement;
using WInterop.FileManagement;
using WInterop.Handles;
using WInterop.Tests.Support;
using WInterop.Utility;
using Xunit;

namespace WInterop.DesktopTests
{
    public partial class HandlesTests
    {
        [Fact]
        public void GetHandleTypeBasic()
        {
            string tempPath = FileMethods.GetTempPath();
            using (var directory = FileMethods.CreateFile(tempPath, DesiredAccess.GENERIC_READ, ShareMode.FILE_SHARE_READWRITE, CreationDisposition.OPEN_EXISTING,
                FileAttributes.NONE, FileFlags.FILE_FLAG_BACKUP_SEMANTICS))
            {
                string name = Handles.HandleDesktopMethods.GetObjectType(directory);
                name.Should().Be("File");
            }
        }

        [Fact]
        public void GetHandleNameBasic()
        {
            string tempPath = FileMethods.GetTempPath();
            using (var directory = FileMethods.CreateFile(tempPath, DesiredAccess.GENERIC_READ, ShareMode.FILE_SHARE_READWRITE, CreationDisposition.OPEN_EXISTING,
                FileAttributes.NONE, FileFlags.FILE_FLAG_BACKUP_SEMANTICS))
            {
                // This will give back the NT path (\Device\HarddiskVolumen...)
                string name = Handles.HandleDesktopMethods.GetObjectName(directory);

                // Skip past the C:\
                Paths.AddTrailingSeparator(name).Should().EndWith(tempPath.Substring(3));
            }
        }

        [Fact]
        public void GetFileNameBasic()
        {
            string tempPath = FileMethods.GetTempPath();
            using (var directory = FileMethods.CreateFile(tempPath, DesiredAccess.GENERIC_READ, ShareMode.FILE_SHARE_READWRITE, CreationDisposition.OPEN_EXISTING,
                FileAttributes.NONE, FileFlags.FILE_FLAG_BACKUP_SEMANTICS))
            {
                // This will give back the local path (minus the device, eg \Users\... or \Server\Share\...)
                string name = Handles.HandleDesktopMethods.GetFileName(directory);

                tempPath.Should().EndWith(Paths.AddTrailingSeparator(name));
            }
        }

        [Fact]
        public void GetVolumeNameBasic()
        {
            string tempPath = FileMethods.GetTempPath();
            using (var directory = FileMethods.CreateFile(tempPath, DesiredAccess.GENERIC_READ, ShareMode.FILE_SHARE_READWRITE, CreationDisposition.OPEN_EXISTING,
                FileAttributes.NONE, FileFlags.FILE_FLAG_BACKUP_SEMANTICS))
            {
                // This will give back the NT volume path (\Device\HarddiskVolumen\)
                try
                {
                    string name = Handles.HandleDesktopMethods.GetVolumeName(directory);
                    name.Should().StartWith(@"\Device\");
                }
                catch (NotImplementedException)
                {
                    // Needs Windows 10
                    System.Environment.OSVersion.Version.Major.Should().BeLessThan(10);
                }
            }
        }

        [Fact]
        public void GetShortNameBasic()
        {
            string tempPath = FileMethods.GetTempPath();
            using (var directory = FileMethods.CreateFile(tempPath, DesiredAccess.GENERIC_READ, ShareMode.FILE_SHARE_READWRITE, CreationDisposition.OPEN_EXISTING,
                FileAttributes.NONE, FileFlags.FILE_FLAG_BACKUP_SEMANTICS))
            {
                // This will give back the NT volume path (\Device\HarddiskVolumen\)
                string directoryName = Handles.HandleDesktopMethods.GetShortName(directory);
                directoryName.Should().Be("Temp");

                string tempFileName = "ExtraLongName" + System.IO.Path.GetRandomFileName();
                string tempFilePath = System.IO.Path.Combine(tempPath, tempFileName);
                try
                {
                    using (var file = FileMethods.CreateFile(tempFilePath, DesiredAccess.GENERIC_READ, ShareMode.FILE_SHARE_READWRITE, CreationDisposition.CREATE_NEW))
                    {
                        string fileName = Handles.HandleDesktopMethods.GetShortName(file);
                        fileName.Length.Should().BeLessOrEqualTo(12);
                    }
                }
                finally
                {
                    FileMethods.DeleteFile(tempFilePath);
                }
            }
        }

        [Fact]
        public void OpenDosDeviceDirectory()
        {
            using (var directory = Handles.HandleDesktopMethods.OpenDirectoryObject(@"\??"))
            {
                directory.IsInvalid.Should().BeFalse();
            }
        }

        [Fact]
        public void OpenGlobalDosDeviceDirectory()
        {
            using (var directory = Handles.HandleDesktopMethods.OpenDirectoryObject(@"\Global??"))
            {
                directory.IsInvalid.Should().BeFalse();
            }
        }

        [Fact]
        public void OpenRootDirectory()
        {
            using (var directory = Handles.HandleDesktopMethods.OpenDirectoryObject(@"\"))
            {
                directory.IsInvalid.Should().BeFalse();
            }
        }

        [Fact]
        public void GetRootDirectoryEntries()
        {
            using (var directory = Handles.HandleDesktopMethods.OpenDirectoryObject(@"\"))
            {
                IEnumerable<Handles.Desktop.ObjectInformation> objects = Handles.HandleDesktopMethods.GetDirectoryEntries(directory);
                objects.Should().NotBeEmpty();
                objects.Should().Contain(new Handles.Desktop.ObjectInformation { Name = "Device", TypeName = "Directory" });
            }
        }

        [Fact]
        public void OpenCDriveSymbolicLink()
        {
            using (var link = Handles.HandleDesktopMethods.OpenSymbolicLinkObject(@"\??\C:"))
            {
                link.IsInvalid.Should().BeFalse();
            }
        }

        [Fact]
        public void GetTargetForCDriveSymbolicLink()
        {
            using (var link = Handles.HandleDesktopMethods.OpenSymbolicLinkObject(@"\??\C:"))
            {
                string target = Handles.HandleDesktopMethods.GetSymbolicLinkTarget(link);
                target.Should().StartWith(@"\Device\");
            }
        }

        [Fact]
        public void QueryDosVolumePathBasic()
        {
            string tempPath = FileMethods.GetTempPath();
            using (var directory = FileMethods.CreateFile(tempPath, DesiredAccess.GENERIC_READ, ShareMode.FILE_SHARE_READWRITE, CreationDisposition.OPEN_EXISTING,
                FileAttributes.NONE, FileFlags.FILE_FLAG_BACKUP_SEMANTICS))
            {
                // This will give back the NT path (\Device\HarddiskVolumen...)
                string fullName = Handles.HandleDesktopMethods.GetObjectName(directory);
                string fileName = FileMethods.GetFileNameByHandle(directory);
                string deviceName = fullName.Substring(0, fullName.Length - fileName.Length);

                string dosVolumePath = DeviceDesktopMethods.QueryDosVolumePath(deviceName);

                tempPath.Should().StartWith(dosVolumePath);
                tempPath.Should().Be(dosVolumePath + fileName + @"\");
            }
        }

        [Fact]
        public void GetPipeObjectInfo()
        {
            var fileHandle = FileMethods.CreateFile(
                @"\\.\pipe\",
                0,                  // We don't care about read or write, we're just getting metadata with this handle
                System.IO.FileShare.ReadWrite,
                System.IO.FileMode.Open,
                0,
                FileFlags.FILE_FLAG_OPEN_REPARSE_POINT          // To avoid traversing links
                    | FileFlags.FILE_FLAG_BACKUP_SEMANTICS);    // To open directories

            string name = HandleDesktopMethods.GetObjectName(fileHandle);
            name.Should().Be(@"\Device\NamedPipe\");

            string typeName = HandleDesktopMethods.GetObjectType(fileHandle);
            typeName.Should().Be(@"File");

            string fileName = FileMethods.GetFileNameByHandle(fileHandle);
            fileName.Should().Be(@"\");
        }

        [Fact]
        public void GetPipeObjectInfoNoTrailingSlash()
        {
            var fileHandle = FileMethods.CreateFile(
                @"\\.\pipe",
                0,                  // We don't care about read or write, we're just getting metadata with this handle
                System.IO.FileShare.ReadWrite,
                System.IO.FileMode.Open,
                0,
                FileFlags.FILE_FLAG_OPEN_REPARSE_POINT          // To avoid traversing links
                    | FileFlags.FILE_FLAG_BACKUP_SEMANTICS);    // To open directories

            string name = HandleDesktopMethods.GetObjectName(fileHandle);
            name.Should().Be(@"\Device\NamedPipe");

            string typeName = HandleDesktopMethods.GetObjectType(fileHandle);
            typeName.Should().Be(@"File");

            // Not sure why this is- probably the source of why so many other things go wrong
            Action action = () => FileMethods.GetFileNameByHandle(fileHandle);
            action.ShouldThrow<ArgumentException>().And.HResult.Should().Be(unchecked((int)0x80070057));
        }
    }
}
