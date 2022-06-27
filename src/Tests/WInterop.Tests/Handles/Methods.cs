// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using WInterop.Devices;
using WInterop.Storage;
using WInterop.Handles;
using WInterop.Support;

namespace HandlesTests;

public partial class Methods
{
    [Fact]
    public void GetHandleTypeBasic()
    {
        string tempPath = Storage.GetTempPath();
        using var directory = Storage.CreateDirectoryHandle(tempPath);
        string name = Handles.GetObjectType(directory);
        name.Should().Be("File");
    }

    [Fact]
    public void GetHandleNameBasic()
    {
        string tempPath = Storage.GetTempPath();
        using var directory = Storage.CreateDirectoryHandle(tempPath);
        // This will give back the NT path (\Device\HarddiskVolumen...)
        string name = Handles.GetObjectName(directory);

        // Skip past the C:\
        Paths.AddTrailingSeparator(name).Should().EndWith(tempPath[3..]);
    }

    [Fact]
    public void OpenDosDeviceDirectory()
    {
        using var directory = Handles.OpenDirectoryObject(@"\??");
        directory.IsInvalid.Should().BeFalse();
    }

    [Fact]
    public void OpenGlobalDosDeviceDirectory()
    {
        using var directory = Handles.OpenDirectoryObject(@"\Global??");
        directory.IsInvalid.Should().BeFalse();
    }

    [Fact]
    public void OpenRootDirectory()
    {
        using var directory = Handles.OpenDirectoryObject(@"\");
        directory.IsInvalid.Should().BeFalse();
    }

    [Fact]
    public void GetRootDirectoryEntries()
    {
        using var directory = Handles.OpenDirectoryObject(@"\");
        IEnumerable<ObjectInformation> objects = Handles.GetDirectoryEntries(directory);
        objects.Should().NotBeEmpty();
        objects.Should().Contain(new ObjectInformation { Name = "Device", TypeName = "Directory" });
    }

    [Fact]
    public void OpenCDriveSymbolicLink()
    {
        using var link = Handles.OpenSymbolicLinkObject(@"\??\C:");
        link.IsInvalid.Should().BeFalse();
    }

    [Fact]
    public void GetTargetForCDriveSymbolicLink()
    {
        using var link = Handles.OpenSymbolicLinkObject(@"\??\C:");
        string target = Handles.GetSymbolicLinkTarget(link);
        target.Should().StartWith(@"\Device\");
    }

    [Fact]
    public void QueryDosVolumePathBasic()
    {
        string tempPath = Storage.GetTempPath();
        using var directory = Storage.CreateDirectoryHandle(tempPath);
        // This will give back the NT path (\Device\HarddiskVolumen...)
        string fullName = Handles.GetObjectName(directory);
        string fileName = Storage.GetFileName(directory);
        string deviceName = fullName[..^fileName.Length];

        string dosVolumePath = Devices.QueryDosVolumePath(deviceName);

        tempPath.Should().StartWith(dosVolumePath);
        tempPath.Should().Be($@"{dosVolumePath}{fileName}\");
    }

    [Fact]
    public void GetPipeObjectInfo()
    {
        var fileHandle = Storage.CreateFileSystemIo(
            @"\\.\pipe\",
            0,                  // We don't care about read or write, we're just getting metadata with this handle
            System.IO.FileShare.ReadWrite,
            System.IO.FileMode.Open,
            0,
            FileFlags.OpenReparsePoint           // To avoid traversing links
                | FileFlags.BackupSemantics);    // To open directories

        string name = Handles.GetObjectName(fileHandle);
        name.Should().Be(@"\Device\NamedPipe\");

        string typeName = Handles.GetObjectType(fileHandle);
        typeName.Should().Be(@"File");

        string fileName = Storage.GetFileName(fileHandle);
        fileName.Should().Be(@"\");
    }

    [Fact]
    public void GetPipeObjectInfoNoTrailingSlash()
    {
        var fileHandle = Storage.CreateFileSystemIo(
            @"\\.\pipe",
            0,                  // We don't care about read or write, we're just getting metadata with this handle
            System.IO.FileShare.ReadWrite,
            System.IO.FileMode.Open,
            0,
            FileFlags.OpenReparsePoint           // To avoid traversing links
                | FileFlags.BackupSemantics);    // To open directories

        string name = Handles.GetObjectName(fileHandle);
        name.Should().Be(@"\Device\NamedPipe");

        string typeName = Handles.GetObjectType(fileHandle);
        typeName.Should().Be(@"File");

        // Not sure why this is- probably the source of why so many other things go wrong
        Action action = () => Storage.GetFileName(fileHandle);
        action.Should().Throw<ArgumentException>().And.HResult.Should().Be(unchecked((int)0x80070057));
    }
}
