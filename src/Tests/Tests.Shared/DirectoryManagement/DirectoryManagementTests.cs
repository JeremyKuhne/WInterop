// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using FluentAssertions;
using System;
using Tests.Support;
using WInterop.File;
using Xunit;

namespace Tests.DirectoryManagement
{
    public class DirectoryManagementTests
    {
        [Fact]
        public void GetCurrentDirectoryBasic()
        {
            string currentDirectory = FileMethods.GetCurrentDirectory();
            currentDirectory.Should().NotBeNullOrWhiteSpace();
        }

        [Fact]
        public void SetCurrentDirectoryBasic()
        {
            string currentDirectory = FileMethods.GetCurrentDirectory();

            try
            {
                StoreHelper.ValidateStoreGetsUnauthorizedAccess(() =>
                {
                    FileMethods.SetCurrentDirectory(@"C:\");
                    FileMethods.GetCurrentDirectory().Should().Be(@"C:\");
                });
            }
            finally
            {
                if (currentDirectory != FileMethods.GetCurrentDirectory())
                {
                    FileMethods.SetCurrentDirectory(currentDirectory);
                    FileMethods.GetCurrentDirectory().Should().Be(currentDirectory);
                }
            }
        }

        [Fact]
        public void SetCurrentDirectoryShortName()
        {
            string currentDirectory = FileMethods.GetCurrentDirectory();

            try
            {
                StoreHelper.ValidateStoreGetsUnauthorizedAccess(() =>
                {
                    FileMethods.SetCurrentDirectory(@"C:\PROGRA~1");
                    FileMethods.GetCurrentDirectory().Should().Be(@"C:\PROGRA~1");
                });
            }
            finally
            {
                if (currentDirectory != FileMethods.GetCurrentDirectory())
                {
                    FileMethods.SetCurrentDirectory(currentDirectory);
                    FileMethods.GetCurrentDirectory().Should().Be(currentDirectory);
                }
            }
        }

        [Fact]
        public void SetCurrentDirectoryToNonExistant()
        {
            using (var cleaner = new TestFileCleaner())
            {
                string directoryPath = cleaner.GetTestPath();
                Action action = () => FileMethods.SetCurrentDirectory(directoryPath);
                action.Should().Throw<System.IO.FileNotFoundException>();
            }
        }

        [Fact]
        public void CreateDirectoryBasic()
        {
            using (var temp = new TestFileCleaner())
            {
                string directoryPath = temp.GetTestPath();
                FileMethods.CreateDirectory(directoryPath);
                using (var directory = FileMethods.CreateDirectoryHandle(directoryPath))
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
                FileMethods.CreateDirectory(directoryPath);
                using (var directory = FileMethods.CreateDirectoryHandle(directoryPath))
                {
                    directory.IsInvalid.Should().BeFalse();
                }

                FileMethods.RemoveDirectory(directoryPath);

                Action action = () =>
                {
                    using (var directory = FileMethods.CreateDirectoryHandle(directoryPath))
                    {
                    }
                };

                action.Should().Throw<System.IO.FileNotFoundException>();
            }
        }
    }
}
