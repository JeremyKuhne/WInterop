// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using FluentAssertions;
using System;
using Tests.Support;
using WInterop.DirectoryManagement;
using WInterop.FileManagement;
using WInterop.FileManagement.DataTypes;
using Xunit;

namespace Tests.DirectoryManagement
{
    public class DirectoryManagementTests
    {
        [Fact]
        public void GetCurrentDirectoryBasic()
        {
            string currentDirectory = DirectoryMethods.GetCurrentDirectory();
            currentDirectory.Should().NotBeNullOrWhiteSpace();
        }

        [Fact]
        public void SetCurrentDirectoryBasic()
        {
            string currentDirectory = DirectoryMethods.GetCurrentDirectory();

            StoreHelper.ValidateStoreGetsUnauthorizedAccess(() =>
            {
                DirectoryMethods.SetCurrentDirectory(@"C:\");
                DirectoryMethods.GetCurrentDirectory().Should().Be(@"C:\");
            });
            DirectoryMethods.SetCurrentDirectory(currentDirectory);
            DirectoryMethods.GetCurrentDirectory().Should().Be(currentDirectory);
        }

        [Fact]
        public void SetCurrentDirectoryToNonExistant()
        {
            using (var cleaner = new TestFileCleaner())
            {
                string directoryPath = cleaner.GetTestPath();
                Action action = () => DirectoryMethods.SetCurrentDirectory(directoryPath);
                action.ShouldThrow<System.IO.FileNotFoundException>();
            }
        }

        [Fact]
        public void CreateDirectoryBasic()
        {
            using (var temp = new TestFileCleaner())
            {
                string directoryPath = temp.GetTestPath();
                DirectoryMethods.CreateDirectory(directoryPath);
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
                DirectoryMethods.CreateDirectory(directoryPath);
                using (var directory = FileMethods.CreateFile(directoryPath, 0, ShareMode.FILE_SHARE_READWRITE, CreationDisposition.OPEN_EXISTING,
                    FileAttributes.FILE_ATTRIBUTE_DIRECTORY, FileFlags.FILE_FLAG_BACKUP_SEMANTICS))
                {
                    directory.IsInvalid.Should().BeFalse();
                }

                DirectoryMethods.RemoveDirectory(directoryPath);

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
