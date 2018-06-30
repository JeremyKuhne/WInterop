// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using FluentAssertions;
using System;
using Tests.Support;
using WInterop.Storage;
using Xunit;

namespace Tests.DirectoryManagement
{
    public class DirectoryManagementTests
    {
        [Fact]
        public void GetCurrentDirectoryBasic()
        {
            string currentDirectory = StorageMethods.GetCurrentDirectory();
            currentDirectory.Should().NotBeNullOrWhiteSpace();
        }

        [Fact]
        public void SetCurrentDirectoryBasic()
        {
            string currentDirectory = StorageMethods.GetCurrentDirectory();

            try
            {
                StoreHelper.ValidateStoreGetsUnauthorizedAccess(() =>
                {
                    StorageMethods.SetCurrentDirectory(@"C:\");
                    StorageMethods.GetCurrentDirectory().Should().Be(@"C:\");
                });
            }
            finally
            {
                if (currentDirectory != StorageMethods.GetCurrentDirectory())
                {
                    StorageMethods.SetCurrentDirectory(currentDirectory);
                    StorageMethods.GetCurrentDirectory().Should().Be(currentDirectory);
                }
            }
        }

        [Fact]
        public void SetCurrentDirectoryShortName()
        {
            string currentDirectory = StorageMethods.GetCurrentDirectory();

            try
            {
                StoreHelper.ValidateStoreGetsUnauthorizedAccess(() =>
                {
                    StorageMethods.SetCurrentDirectory(@"C:\PROGRA~1");
                    StorageMethods.GetCurrentDirectory().Should().Be(@"C:\PROGRA~1");
                });
            }
            finally
            {
                if (currentDirectory != StorageMethods.GetCurrentDirectory())
                {
                    StorageMethods.SetCurrentDirectory(currentDirectory);
                    StorageMethods.GetCurrentDirectory().Should().Be(currentDirectory);
                }
            }
        }

        [Fact]
        public void SetCurrentDirectoryToNonExistant()
        {
            using (var cleaner = new TestFileCleaner())
            {
                string directoryPath = cleaner.GetTestPath();
                Action action = () => StorageMethods.SetCurrentDirectory(directoryPath);
                action.Should().Throw<System.IO.FileNotFoundException>();
            }
        }

        [Fact]
        public void CreateDirectoryBasic()
        {
            using (var temp = new TestFileCleaner())
            {
                string directoryPath = temp.GetTestPath();
                StorageMethods.CreateDirectory(directoryPath);
                using (var directory = StorageMethods.CreateDirectoryHandle(directoryPath))
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
                StorageMethods.CreateDirectory(directoryPath);
                using (var directory = StorageMethods.CreateDirectoryHandle(directoryPath))
                {
                    directory.IsInvalid.Should().BeFalse();
                }

                StorageMethods.RemoveDirectory(directoryPath);

                Action action = () =>
                {
                    using (var directory = StorageMethods.CreateDirectoryHandle(directoryPath))
                    {
                    }
                };

                action.Should().Throw<System.IO.FileNotFoundException>();
            }
        }
    }
}
