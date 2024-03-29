﻿// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Tests.Support;
using WInterop.Security;
using WInterop.Errors;
using WInterop.Storage;
using WInterop.Support;

namespace StorageTests;

public class SymbolicLinks
{
    private static bool CanCreateSymbolicLinks()
    {
        // Assuming that the current thread can replicate rights from the process
        using var processToken = Security.OpenProcessToken(AccessTokenRights.Query | AccessTokenRights.Read);
        return processToken.HasPrivilege(Privilege.CreateSymbolicLink);
    }

    [Fact]
    public void FinalPathNameForLinks()
    {
        if (!CanCreateSymbolicLinks()) return;

        // GetFinalPathName always points to the linked file unless you specifically open the reparse point
        using var cleaner = new TestFileCleaner();
        string filePath = Path.Join(cleaner.TempFolder, "Target");
        string extendedPath = @"\\?\" + filePath;

        FileHelper.WriteAllText(filePath, "CreateSymbolicLinkToFile");

        string symbolicLink = Path.Join(cleaner.TempFolder, "Link");
        string extendedLink = @"\\?\" + symbolicLink;
        Storage.CreateSymbolicLink(symbolicLink, filePath);
        Storage.FileExists(symbolicLink).Should().BeTrue("symbolic link should exist");

        // GetFinalPathName should normalize the casing, pushing ToUpper to validate
        using (var handle = Storage.CreateFile(symbolicLink.ToUpperInvariant(), CreationDisposition.OpenExisting, DesiredAccess.GenericRead))
        {
            handle.IsInvalid.Should().BeFalse();
            Storage.GetFinalPathNameByHandle(handle, GetFinalPathNameByHandleFlags.FileNameNormalized)
                .Should().Be(extendedPath);
            Storage.GetFinalPathNameByHandle(handle, GetFinalPathNameByHandleFlags.FileNameOpened)
                .Should().Be(extendedPath);
        }

        using (var handle = Storage.CreateFile(symbolicLink.ToUpperInvariant(), CreationDisposition.OpenExisting, DesiredAccess.GenericRead,
            ShareModes.ReadWrite, AllFileAttributes.None, FileFlags.OpenReparsePoint))
        {
            handle.IsInvalid.Should().BeFalse();
            Storage.GetFinalPathNameByHandle(handle, GetFinalPathNameByHandleFlags.FileNameNormalized)
                .Should().Be(extendedLink);
            Storage.GetFinalPathNameByHandle(handle, GetFinalPathNameByHandleFlags.FileNameOpened)
                .Should().Be(extendedLink);
        }
    }

    [Fact]
    public void CreateSymbolicLinkToFile()
    {
        using var cleaner = new TestFileCleaner();
        string filePath = cleaner.CreateTestFile("CreateSymbolicLinkToFile");
        string symbolicLink = cleaner.GetTestPath();
        Action action = () => Storage.CreateSymbolicLink(symbolicLink, filePath);

        if (CanCreateSymbolicLinks())
        {
            action();
            var attributes = Storage.GetFileAttributes(symbolicLink);
            attributes.Should().HaveFlag(FileAttributes.ReparsePoint);

            using var handle = Storage.CreateFile(symbolicLink, CreationDisposition.OpenExisting, DesiredAccess.ReadExtendedAttributes,
                ShareModes.All, fileFlags: FileFlags.OpenReparsePoint);
            handle.IsInvalid.Should().BeFalse();
            var (printName, substituteName, tag) = WInterop.Devices.Devices.GetReparsePointNames(handle);
            tag.Should().Be(ReparseTag.SymbolicLink);
            printName.Should().Be(filePath);
            substituteName.Should().Be(@"\??\" + filePath);
        }
        else
        {
            // Can't create links unless you have admin rights SE_CREATE_SYMBOLIC_LINK_NAME SeCreateSymbolicLinkPrivilege
            action.Should().Throw<System.IO.IOException>().And.HResult.Should().Be((int)WindowsError.ERROR_PRIVILEGE_NOT_HELD.ToHResult());
        }
    }

    [Fact]
    public void CreateRelativeSymbolicLinkToFile()
    {
        using var cleaner = new TestFileCleaner();
        string filePath = cleaner.CreateTestFile("CreateRelativeSymbolicLinkToFile");
        string fileName = Paths.GetLastSegment(filePath);
        string symbolicLink = cleaner.GetTestPath();
        Action action = () => Storage.CreateSymbolicLink(symbolicLink, fileName);

        if (CanCreateSymbolicLinks())
        {
            action();
            var attributes = Storage.GetFileAttributes(symbolicLink);
            attributes.Should().HaveFlag(FileAttributes.ReparsePoint);

            using var handle = Storage.CreateFile(symbolicLink, CreationDisposition.OpenExisting, DesiredAccess.ReadExtendedAttributes,
                ShareModes.All, fileFlags: FileFlags.OpenReparsePoint);
            handle.IsInvalid.Should().BeFalse();
            var (printName, substituteName, tag) = WInterop.Devices.Devices.GetReparsePointNames(handle);
            tag.Should().Be(ReparseTag.SymbolicLink);
            printName.Should().Be(fileName);
            substituteName.Should().Be(fileName);
        }
        else
        {
            // Can't create links unless you have admin rights SE_CREATE_SYMBOLIC_LINK_NAME SeCreateSymbolicLinkPrivilege
            action.Should().Throw<System.IO.IOException>().And.HResult.Should().Be((int)WindowsError.ERROR_PRIVILEGE_NOT_HELD.ToHResult());
        }
    }

    [Fact]
    public void CreateSymbolicLinkToLongPathFile()
    {
        using var cleaner = new TestFileCleaner();
        string longPath = @"\\?\" + PathGenerator.CreatePathOfLength(cleaner.TempFolder, 500);
        FileHelper.CreateDirectoryRecursive(longPath);
        string filePath = cleaner.CreateTestFile("CreateSymbolicLinkToLongPathFile", longPath);

        string symbolicLink = cleaner.GetTestPath();
        Action action = () => Storage.CreateSymbolicLink(symbolicLink, filePath);

        if (CanCreateSymbolicLinks())
        {
            action();
            var attributes = Storage.GetFileAttributes(symbolicLink);
            attributes.Should().HaveFlag(FileAttributes.ReparsePoint);
        }
        else
        {
            action.Should().Throw<System.IO.IOException>().And.HResult.Should().Be((int)WindowsError.ERROR_PRIVILEGE_NOT_HELD.ToHResult());
        }
    }
}
