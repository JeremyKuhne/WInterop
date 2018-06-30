// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using FluentAssertions;
using System;
using System.Collections.Generic;
using WInterop.DeviceManagement;
using WInterop.File;
using WInterop.File.Types;
using WInterop.Handles;
using WInterop.Handles.Types;
using WInterop.Support;
using Xunit;

namespace DesktopTests.HandlesTests
{
    public partial class Methods
    {
        [Fact]
        public void GetHandleTypeBasic()
        {
            string tempPath = FileMethods.GetTempPath();
            using (var directory = FileMethods.CreateDirectoryHandle(tempPath))
            {
                string name = HandleMethods.GetObjectType(directory);
                name.Should().Be("File");
            }
        }

        [Fact]
        public void GetHandleNameBasic()
        {
            string tempPath = FileMethods.GetTempPath();
            using (var directory = FileMethods.CreateDirectoryHandle(tempPath))
            {
                // This will give back the NT path (\Device\HarddiskVolumen...)
                string name = HandleMethods.GetObjectName(directory);

                // Skip past the C:\
                Paths.AddTrailingSeparator(name).Should().EndWith(tempPath.Substring(3));
            }
        }

        [Fact]
        public void OpenDosDeviceDirectory()
        {
            using (var directory = HandleMethods.OpenDirectoryObject(@"\??"))
            {
                directory.IsInvalid.Should().BeFalse();
            }
        }

        [Fact]
        public void OpenGlobalDosDeviceDirectory()
        {
            using (var directory = HandleMethods.OpenDirectoryObject(@"\Global??"))
            {
                directory.IsInvalid.Should().BeFalse();
            }
        }

        [Fact]
        public void OpenRootDirectory()
        {
            using (var directory = HandleMethods.OpenDirectoryObject(@"\"))
            {
                directory.IsInvalid.Should().BeFalse();
            }
        }

        [Fact]
        public void GetRootDirectoryEntries()
        {
            using (var directory = HandleMethods.OpenDirectoryObject(@"\"))
            {
                IEnumerable<ObjectInformation> objects = HandleMethods.GetDirectoryEntries(directory);
                objects.Should().NotBeEmpty();
                objects.Should().Contain(new ObjectInformation { Name = "Device", TypeName = "Directory" });
            }
        }

        [Fact]
        public void OpenCDriveSymbolicLink()
        {
            using (var link = HandleMethods.OpenSymbolicLinkObject(@"\??\C:"))
            {
                link.IsInvalid.Should().BeFalse();
            }
        }

        [Fact]
        public void GetTargetForCDriveSymbolicLink()
        {
            using (var link = HandleMethods.OpenSymbolicLinkObject(@"\??\C:"))
            {
                string target = HandleMethods.GetSymbolicLinkTarget(link);
                target.Should().StartWith(@"\Device\");
            }
        }

        [Fact]
        public void QueryDosVolumePathBasic()
        {
            string tempPath = FileMethods.GetTempPath();
            using (var directory = FileMethods.CreateDirectoryHandle(tempPath))
            {
                // This will give back the NT path (\Device\HarddiskVolumen...)
                string fullName = HandleMethods.GetObjectName(directory);
                string fileName = FileMethods.GetFileName(directory);
                string deviceName = fullName.Substring(0, fullName.Length - fileName.Length);

                string dosVolumePath = DeviceMethods.QueryDosVolumePath(deviceName);

                tempPath.Should().StartWith(dosVolumePath);
                tempPath.Should().Be(dosVolumePath + fileName + @"\");
            }
        }

        [Fact]
        public void GetPipeObjectInfo()
        {
            var fileHandle = FileMethods.CreateFileSystemIo(
                @"\\.\pipe\",
                0,                  // We don't care about read or write, we're just getting metadata with this handle
                System.IO.FileShare.ReadWrite,
                System.IO.FileMode.Open,
                0,
                FileFlags.OpenReparsePoint           // To avoid traversing links
                    | FileFlags.BackupSemantics);    // To open directories

            string name = HandleMethods.GetObjectName(fileHandle);
            name.Should().Be(@"\Device\NamedPipe\");

            string typeName = HandleMethods.GetObjectType(fileHandle);
            typeName.Should().Be(@"File");

            string fileName = FileMethods.GetFileName(fileHandle);
            fileName.Should().Be(@"\");
        }

        [Fact]
        public void GetPipeObjectInfoNoTrailingSlash()
        {
            var fileHandle = FileMethods.CreateFileSystemIo(
                @"\\.\pipe",
                0,                  // We don't care about read or write, we're just getting metadata with this handle
                System.IO.FileShare.ReadWrite,
                System.IO.FileMode.Open,
                0,
                FileFlags.OpenReparsePoint           // To avoid traversing links
                    | FileFlags.BackupSemantics);    // To open directories

            string name = HandleMethods.GetObjectName(fileHandle);
            name.Should().Be(@"\Device\NamedPipe");

            string typeName = HandleMethods.GetObjectType(fileHandle);
            typeName.Should().Be(@"File");

            // Not sure why this is- probably the source of why so many other things go wrong
            Action action = () => FileMethods.GetFileName(fileHandle);
            action.Should().Throw<ArgumentException>().And.HResult.Should().Be(unchecked((int)0x80070057));
        }
    }
}
