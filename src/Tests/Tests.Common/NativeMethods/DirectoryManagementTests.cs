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

namespace WInterop.Tests.NativeMethodTests
{
    public class DirectoryManagementTests
    {
        [Fact]
        public void GetCurrentDirectoryBasic()
        {
            string currentDirectory = NativeMethods.DirectoryManagement.GetCurrentDirectory();
            currentDirectory.Should().NotBeNullOrWhiteSpace();
        }

        [Fact]
        public void SetCurrentDirectoryBasic()
        {
            string currentDirectory = NativeMethods.DirectoryManagement.GetCurrentDirectory();

            StoreHelper.ValidateStoreGetsUnauthorizedAccess(() =>
            {
                NativeMethods.DirectoryManagement.SetCurrentDirectory(@"C:\");
                NativeMethods.DirectoryManagement.GetCurrentDirectory().Should().Be(@"C:\");
            });
            NativeMethods.DirectoryManagement.SetCurrentDirectory(currentDirectory);
            NativeMethods.DirectoryManagement.GetCurrentDirectory().Should().Be(currentDirectory);
        }

        [Fact]
        public void CreateDirectoryBasic()
        {
            using (var temp = new TestFileCleaner())
            {
                string directoryPath = temp.GetTestPath();
                NativeMethods.DirectoryManagement.CreateDirectory(directoryPath);
                using (var directory = NativeMethods.FileManagement.CreateFile(directoryPath, 0, ShareMode.ReadWrite, CreationDisposition.OPEN_EXISTING,
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
                NativeMethods.DirectoryManagement.CreateDirectory(directoryPath);
                using (var directory = NativeMethods.FileManagement.CreateFile(directoryPath, 0, ShareMode.ReadWrite, CreationDisposition.OPEN_EXISTING,
                    FileAttributes.FILE_ATTRIBUTE_DIRECTORY, FileFlags.FILE_FLAG_BACKUP_SEMANTICS))
                {
                    directory.IsInvalid.Should().BeFalse();
                }

                NativeMethods.DirectoryManagement.RemoveDirectory(directoryPath);

                Action action = () =>
                {
                    using (var directory = NativeMethods.FileManagement.CreateFile(directoryPath, 0, ShareMode.ReadWrite, CreationDisposition.OPEN_EXISTING,
                        FileAttributes.FILE_ATTRIBUTE_DIRECTORY, FileFlags.FILE_FLAG_BACKUP_SEMANTICS))
                    {

                    }
                };

                action.ShouldThrow<System.IO.FileNotFoundException>();
            }
        }
    }
}
