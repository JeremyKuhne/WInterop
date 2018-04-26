﻿// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using FluentAssertions;
using System;
using Tests.Support;
using WInterop.DirectoryManagement;
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

            try
            {
                StoreHelper.ValidateStoreGetsUnauthorizedAccess(() =>
                {
                    DirectoryMethods.SetCurrentDirectory(@"C:\");
                    DirectoryMethods.GetCurrentDirectory().Should().Be(@"C:\");
                });
            }
            finally
            {
                if (currentDirectory != DirectoryMethods.GetCurrentDirectory())
                {
                    DirectoryMethods.SetCurrentDirectory(currentDirectory);
                    DirectoryMethods.GetCurrentDirectory().Should().Be(currentDirectory);
                }
            }
        }

        [Fact]
        public void SetCurrentDirectoryShortName()
        {
            string currentDirectory = DirectoryMethods.GetCurrentDirectory();

            try
            {
                StoreHelper.ValidateStoreGetsUnauthorizedAccess(() =>
                {
                    DirectoryMethods.SetCurrentDirectory(@"C:\PROGRA~1");
                    DirectoryMethods.GetCurrentDirectory().Should().Be(@"C:\PROGRA~1");
                });
            }
            finally
            {
                if (currentDirectory != DirectoryMethods.GetCurrentDirectory())
                {
                    DirectoryMethods.SetCurrentDirectory(currentDirectory);
                    DirectoryMethods.GetCurrentDirectory().Should().Be(currentDirectory);
                }
            }
        }

        [Fact]
        public void SetCurrentDirectoryToNonExistant()
        {
            using (var cleaner = new TestFileCleaner())
            {
                string directoryPath = cleaner.GetTestPath();
                Action action = () => DirectoryMethods.SetCurrentDirectory(directoryPath);
                action.Should().Throw<System.IO.FileNotFoundException>();
            }
        }

        [Fact]
        public void CreateDirectoryBasic()
        {
            using (var temp = new TestFileCleaner())
            {
                string directoryPath = temp.GetTestPath();
                DirectoryMethods.CreateDirectory(directoryPath);
                using (var directory = DirectoryMethods.CreateDirectoryHandle(directoryPath))
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
                using (var directory = DirectoryMethods.CreateDirectoryHandle(directoryPath))
                {
                    directory.IsInvalid.Should().BeFalse();
                }

                DirectoryMethods.RemoveDirectory(directoryPath);

                Action action = () =>
                {
                    using (var directory = DirectoryMethods.CreateDirectoryHandle(directoryPath))
                    {
                    }
                };

                action.Should().Throw<System.IO.FileNotFoundException>();
            }
        }
    }
}
