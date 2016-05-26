// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using FluentAssertions;
using System;
using WInterop.Authorization;
using WInterop.Authorization.DataTypes;
using WInterop.ErrorHandling;
using WInterop.FileManagement;
using WInterop.FileManagement.DataTypes;
using Tests.Support;
using WInterop.Support;
using Xunit;
using WInterop.ErrorHandling.DataTypes;

namespace DesktopTests.FileManagementTests
{
    public class Methods
    {
        [Fact]
        public void GetShortPathBasic()
        {
            string tempPath = FileMethods.GetTempPath();
            FileDesktopMethods.GetShortPathName(tempPath).Should().NotBeNullOrWhiteSpace();
        }

        [Fact]
        public void GetLongPathBasic()
        {
            string tempPath = FileMethods.GetTempPath();
            FileDesktopMethods.GetLongPathName(tempPath).Should().NotBeNullOrWhiteSpace();
        }

        [Fact]
        public void FinalPathNameVolumeNameBehavior()
        {
            // This test is asserting that the original volume name has nothing to do with the volume GetFinalPathNameByHandle returns
            using (var cleaner = new TestFileCleaner())
            {
                string filePath = cleaner.CreateTestFile("FinalPathNameVolumeNameBehavior");

                using (var handle = FileMethods.CreateFile(filePath.ToLower(),
                     DesiredAccess.GENERIC_READ, ShareMode.FILE_SHARE_READWRITE, CreationDisposition.OPEN_EXISTING))
                {
                    handle.IsInvalid.Should().BeFalse();

                    string extendedPath = @"\\?\" + filePath;
                    FileDesktopMethods.GetFinalPathNameByHandle(handle, GetFinalPathNameByHandleFlags.FILE_NAME_NORMALIZED)
                        .Should().Be(extendedPath);
                    FileDesktopMethods.GetFinalPathNameByHandle(handle, GetFinalPathNameByHandleFlags.FILE_NAME_OPENED)
                        .Should().Be(extendedPath);
                    FileDesktopMethods.GetFinalPathNameByHandle(handle, GetFinalPathNameByHandleFlags.VOLUME_NAME_DOS)
                        .Should().Be(extendedPath);
                    FileDesktopMethods.GetFinalPathNameByHandle(handle, GetFinalPathNameByHandleFlags.VOLUME_NAME_GUID)
                        .Should().StartWith(@"\\?\Volume");
                    FileDesktopMethods.GetFinalPathNameByHandle(handle, GetFinalPathNameByHandleFlags.VOLUME_NAME_NT)
                        .Should().StartWith(@"\Device\");
                    FileDesktopMethods.GetFinalPathNameByHandle(handle, GetFinalPathNameByHandleFlags.VOLUME_NAME_NONE)
                        .Should().Be(filePath.Substring(2));
                }
            }
        }

        private static bool CanCreateSymbolicLinks()
        {
            // Assuming that the current thread can replicate rights from the process
            using (var processToken = AuthorizationDesktopMethods.OpenProcessToken(TokenRights.TOKEN_QUERY | TokenRights.TOKEN_READ))
            {
                return AuthorizationDesktopMethods.HasPrivilege(processToken, Privileges.SeCreateSymbolicLinkPrivilege);
            }
        }

        [Fact]
        public void FinalPathNameForLinks()
        {
            if (!CanCreateSymbolicLinks()) return;

            // GetFinalPathName always points to the linked file unless you specifically open the reparse point
            using (var cleaner = new TestFileCleaner())
            {
                string filePath = Paths.Combine(cleaner.TempFolder, "Target");
                string extendedPath = @"\\?\" + filePath;

                FileHelper.WriteAllText(filePath, "CreateSymbolicLinkToFile");

                string symbolicLink = Paths.Combine(cleaner.TempFolder, "Link");
                string extendedLink = @"\\?\" + symbolicLink;
                FileDesktopMethods.CreateSymbolicLink(symbolicLink, filePath);
                FileMethods.FileExists(symbolicLink).Should().BeTrue("symbolic link should exist");

                // GetFinalPathName should normalize the casing, pushing ToUpper to validate
                using (var handle = FileMethods.CreateFile(symbolicLink.ToUpperInvariant(), DesiredAccess.FILE_GENERIC_READ, ShareMode.FILE_SHARE_READWRITE, CreationDisposition.OPEN_EXISTING))
                {
                    handle.IsInvalid.Should().BeFalse();
                    FileDesktopMethods.GetFinalPathNameByHandle(handle, GetFinalPathNameByHandleFlags.FILE_NAME_NORMALIZED)
                        .Should().Be(extendedPath);
                    FileDesktopMethods.GetFinalPathNameByHandle(handle, GetFinalPathNameByHandleFlags.FILE_NAME_OPENED)
                        .Should().Be(extendedPath);
                }

                using (var handle = FileMethods.CreateFile(symbolicLink.ToUpperInvariant(), DesiredAccess.FILE_GENERIC_READ,
                    ShareMode.FILE_SHARE_READWRITE, CreationDisposition.OPEN_EXISTING, FileAttributes.NONE, FileFlags.FILE_FLAG_OPEN_REPARSE_POINT))
                {
                    handle.IsInvalid.Should().BeFalse();
                    FileDesktopMethods.GetFinalPathNameByHandle(handle, GetFinalPathNameByHandleFlags.FILE_NAME_NORMALIZED)
                        .Should().Be(extendedLink);
                    FileDesktopMethods.GetFinalPathNameByHandle(handle, GetFinalPathNameByHandleFlags.FILE_NAME_OPENED)
                        .Should().Be(extendedLink);
                }
            }
        }

        [Fact]
        public void FinalPathNameBasic()
        {
            using (var cleaner = new TestFileCleaner())
            {
                string filePath = cleaner.CreateTestFile("FinalPathNameBehavior");

                using (var handle = FileMethods.CreateFile(filePath.ToLower(),
                    DesiredAccess.GENERIC_READ, ShareMode.FILE_SHARE_READWRITE, CreationDisposition.OPEN_EXISTING))
                {
                    handle.IsInvalid.Should().BeFalse();

                    string extendedPath = @"\\?\" + filePath;
                    FileDesktopMethods.GetFinalPathNameByHandle(handle, GetFinalPathNameByHandleFlags.FILE_NAME_NORMALIZED)
                        .Should().Be(extendedPath);
                    FileDesktopMethods.GetFinalPathNameByHandle(handle, GetFinalPathNameByHandleFlags.FILE_NAME_OPENED)
                        .Should().Be(extendedPath);
                    FileDesktopMethods.GetFinalPathNameByHandle(handle, GetFinalPathNameByHandleFlags.VOLUME_NAME_DOS)
                        .Should().Be(extendedPath);
                    FileDesktopMethods.GetFinalPathNameByHandle(handle, GetFinalPathNameByHandleFlags.VOLUME_NAME_GUID)
                        .Should().StartWith(@"\\?\Volume");
                    FileDesktopMethods.GetFinalPathNameByHandle(handle, GetFinalPathNameByHandleFlags.VOLUME_NAME_NT)
                        .Should().StartWith(@"\Device\");
                    FileDesktopMethods.GetFinalPathNameByHandle(handle, GetFinalPathNameByHandleFlags.VOLUME_NAME_NONE)
                        .Should().Be(filePath.Substring(2));
                }
            }
        }

        [Fact]
        public void FinalPathNameLongPath()
        {
            using (var cleaner = new TestFileCleaner())
            {
                string longPath = @"\\?\" + PathGenerator.CreatePathOfLength(cleaner.TempFolder, 500);
                string filePath = Paths.Combine(longPath, System.IO.Path.GetRandomFileName());

                FileHelper.CreateDirectoryRecursive(longPath);
                FileHelper.WriteAllText(filePath, "FinalPathNameLongPathPrefixRoundTripBehavior");

                using (var handle = FileMethods.CreateFile(filePath,
                    DesiredAccess.GENERIC_READ, ShareMode.FILE_SHARE_READWRITE, CreationDisposition.OPEN_EXISTING))
                {
                    handle.IsInvalid.Should().BeFalse();

                    FileDesktopMethods.GetFinalPathNameByHandle(handle, GetFinalPathNameByHandleFlags.FILE_NAME_NORMALIZED)
                        .Should().Be(filePath);
                    FileDesktopMethods.GetFinalPathNameByHandle(handle, GetFinalPathNameByHandleFlags.FILE_NAME_OPENED)
                        .Should().Be(filePath);
                    FileDesktopMethods.GetFinalPathNameByHandle(handle, GetFinalPathNameByHandleFlags.VOLUME_NAME_DOS)
                        .Should().Be(filePath);
                    FileDesktopMethods.GetFinalPathNameByHandle(handle, GetFinalPathNameByHandleFlags.VOLUME_NAME_GUID)
                        .Should().StartWith(@"\\?\Volume");
                    FileDesktopMethods.GetFinalPathNameByHandle(handle, GetFinalPathNameByHandleFlags.VOLUME_NAME_NT)
                        .Should().StartWith(@"\Device\");
                    FileDesktopMethods.GetFinalPathNameByHandle(handle, GetFinalPathNameByHandleFlags.VOLUME_NAME_NONE)
                        .Should().Be(filePath.Substring(6));
                }
            }
        }

        [Fact]
        public void FinalPathNameFromPath()
        {
            string tempPath = FileMethods.GetTempPath();
            string lowerTempPath = tempPath.ToLowerInvariant();
            tempPath.Should().NotBe(lowerTempPath);
            FileDesktopMethods.GetFinalPathName(lowerTempPath, 0, false).Should().Be(@"\\?\" + Paths.RemoveTrailingSeparators(tempPath));
        }

        [Fact]
        public void CreateSymbolicLinkToFile()
        {
            using (var cleaner = new TestFileCleaner())
            {
                string filePath = cleaner.CreateTestFile("CreateSymbolicLinkToFile");
                string symbolicLink = cleaner.GetTestPath();
                Action action = () => FileDesktopMethods.CreateSymbolicLink(symbolicLink, filePath);

                if (CanCreateSymbolicLinks())
                {
                    action();
                    var attributes = FileDesktopMethods.GetFileAttributes(symbolicLink);
                    attributes.Should().HaveFlag(FileAttributes.FILE_ATTRIBUTE_REPARSE_POINT);
                }
                else
                {
                    // Can't create links unless you have admin rights SE_CREATE_SYMBOLIC_LINK_NAME SeCreateSymbolicLinkPrivilege
                    action.ShouldThrow<System.IO.IOException>().And.HResult.Should().Be(ErrorMacros.HRESULT_FROM_WIN32(WindowsError.ERROR_PRIVILEGE_NOT_HELD));
                }
            }
        }

        [Fact]
        public void CreateSymbolicLinkToLongPathFile()
        {
            using (var cleaner = new TestFileCleaner())
            {
                string longPath = @"\\?\" + PathGenerator.CreatePathOfLength(cleaner.TempFolder, 500);
                FileHelper.CreateDirectoryRecursive(longPath);
                string filePath = cleaner.CreateTestFile("CreateSymbolicLinkToLongPathFile", longPath);

                string symbolicLink = cleaner.GetTestPath();
                Action action = () => FileDesktopMethods.CreateSymbolicLink(symbolicLink, filePath);

                if (CanCreateSymbolicLinks())
                {
                    action();
                    var attributes = FileDesktopMethods.GetFileAttributes(symbolicLink);
                    attributes.Should().HaveFlag(FileAttributes.FILE_ATTRIBUTE_REPARSE_POINT);
                }
                else
                {
                    action.ShouldThrow<System.IO.IOException>().And.HResult.Should().Be(ErrorMacros.HRESULT_FROM_WIN32(WindowsError.ERROR_PRIVILEGE_NOT_HELD));
                }
            }
        }

        [Fact]
        public void GetFileAttributesLongPath()
        {
            string longPath = @"\\?\" + PathGenerator.CreatePathOfLength(@"C:\", 300);
            Action action = () => FileDesktopMethods.GetFileAttributes(longPath);
            action.ShouldThrow<System.IO.DirectoryNotFoundException>();
        }

        [Fact]
        public void CreateFileRawParition()
        {
            // You can't open with read/write access unless running elevated
            using (var file = FileMethods.CreateFile(@"\\?\GLOBALROOT\Device\Harddisk0\Partition0",
                DesiredAccess.NONE, ShareMode.FILE_SHARE_READWRITE, CreationDisposition.OPEN_EXISTING))
            {
                file.IsInvalid.Should().BeFalse();
                FileMethods.GetFileType(file).Should().Be(FileType.FILE_TYPE_DISK);
            }
        }

        [Fact]
        public void CreateFileHarddiskVolume()
        {
            using (var file = FileMethods.CreateFile(@"\\?\GLOBALROOT\Device\HarddiskVolume1",
                DesiredAccess.GENERIC_READ, ShareMode.FILE_SHARE_READWRITE, CreationDisposition.OPEN_EXISTING))
            {
                file.IsInvalid.Should().BeFalse();
                FileMethods.GetFileType(file).Should().Be(FileType.FILE_TYPE_DISK);
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
                string name = FileDesktopMethods.GetFileName(directory);

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
                    string name = FileDesktopMethods.GetVolumeName(directory);
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
                string directoryName = FileDesktopMethods.GetShortName(directory);
                directoryName.Should().Be("Temp");

                string tempFileName = "ExtraLongName" + System.IO.Path.GetRandomFileName();
                string tempFilePath = System.IO.Path.Combine(tempPath, tempFileName);
                try
                {
                    using (var file = FileMethods.CreateFile(tempFilePath, DesiredAccess.GENERIC_READ, ShareMode.FILE_SHARE_READWRITE, CreationDisposition.CREATE_NEW))
                    {
                        string fileName = FileDesktopMethods.GetShortName(file);
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
        public void FileModeSynchronousFile()
        {
            using (var cleaner = new TestFileCleaner())
            {
                string filePath = cleaner.GetTestPath();
                using (var file = FileMethods.CreateFile(filePath, DesiredAccess.FILE_GENERIC_READWRITE, ShareMode.FILE_SHARE_NONE,
                    CreationDisposition.CREATE_NEW))
                {
                    file.IsInvalid.Should().BeFalse();
                    var mode = FileDesktopMethods.GetFileMode(file);
                    mode.Should().HaveFlag(FILE_MODE_INFORMATION.FILE_SYNCHRONOUS_IO_NONALERT);
                }
            }
        }

        [Fact]
        public void FileModeAsynchronousFile()
        {
            using (var cleaner = new TestFileCleaner())
            {
                string filePath = cleaner.GetTestPath();
                using (var file = FileMethods.CreateFile(filePath, DesiredAccess.FILE_GENERIC_READWRITE, ShareMode.FILE_SHARE_NONE,
                    CreationDisposition.CREATE_NEW, FileAttributes.NONE, FileFlags.FILE_FLAG_OVERLAPPED))
                {
                    file.IsInvalid.Should().BeFalse();
                    var mode = FileDesktopMethods.GetFileMode(file);
                    mode.Should().NotHaveFlag(FILE_MODE_INFORMATION.FILE_SYNCHRONOUS_IO_NONALERT);
                    mode.Should().NotHaveFlag(FILE_MODE_INFORMATION.FILE_SYNCHRONOUS_IO_ALERT);
                }
            }
        }
    }
}
