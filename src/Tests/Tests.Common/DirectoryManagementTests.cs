// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using FluentAssertions;
using System;
using System.Linq;
using WInterop.FileManagement;
using WInterop.Tests.Support;
using Xunit;

namespace WInterop.Tests
{
    public class DirectoryManagementTests
    {
        [Fact]
        public void GetCurrentDirectoryBasic()
        {
            string currentDirectory = DirectoryManagement.DirectoryMethods.GetCurrentDirectory();
            currentDirectory.Should().NotBeNullOrWhiteSpace();
        }

        [Fact]
        public void SetCurrentDirectoryBasic()
        {
            string currentDirectory = DirectoryManagement.DirectoryMethods.GetCurrentDirectory();

            StoreHelper.ValidateStoreGetsUnauthorizedAccess(() =>
            {
                DirectoryManagement.DirectoryMethods.SetCurrentDirectory(@"C:\");
                DirectoryManagement.DirectoryMethods.GetCurrentDirectory().Should().Be(@"C:\");
            });
            DirectoryManagement.DirectoryMethods.SetCurrentDirectory(currentDirectory);
            DirectoryManagement.DirectoryMethods.GetCurrentDirectory().Should().Be(currentDirectory);
        }

        [Fact]
        public void CreateDirectoryBasic()
        {
            using (var temp = new TestFileCleaner())
            {
                string directoryPath = temp.GetTestPath();
                DirectoryManagement.DirectoryMethods.CreateDirectory(directoryPath);
                using (var directory = FileMethods.CreateFile(directoryPath, 0, ShareMode.FILE_SHARE_READWRITE, CreationDisposition.OPEN_EXISTING,
                    FileAttributes.FILE_ATTRIBUTE_DIRECTORY, FileFlags.FILE_FLAG_BACKUP_SEMANTICS))
                {
                    directory.IsInvalid.Should().BeFalse();
                }
            }
        }

        [Fact]
        public void DeleteDirectoryBasic()
        {
            using (var temp = new TestFileCleaner())
            {
                string directoryPath = temp.GetTestPath();
                DirectoryManagement.DirectoryMethods.CreateDirectory(directoryPath);
                using (var directory = FileMethods.CreateFile(directoryPath, 0, ShareMode.FILE_SHARE_READWRITE, CreationDisposition.OPEN_EXISTING,
                    FileAttributes.FILE_ATTRIBUTE_DIRECTORY, FileFlags.FILE_FLAG_BACKUP_SEMANTICS))
                {
                    directory.IsInvalid.Should().BeFalse();
                }

                DirectoryManagement.DirectoryMethods.RemoveDirectory(directoryPath);

                Action action = () =>
                {
                    using (var directory = FileMethods.CreateFile(directoryPath, 0, ShareMode.FILE_SHARE_READWRITE, CreationDisposition.OPEN_EXISTING,
                        FileAttributes.FILE_ATTRIBUTE_DIRECTORY, FileFlags.FILE_FLAG_BACKUP_SEMANTICS))
                    {

                    }
                };

                action.ShouldThrow<System.IO.FileNotFoundException>();
            }
        }
    }
}
