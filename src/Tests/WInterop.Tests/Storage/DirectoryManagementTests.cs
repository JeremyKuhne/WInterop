// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using FluentAssertions;
using Tests.Support;
using WInterop.Storage;
using Xunit;

namespace StorageTests;

public class DirectoryManagement
{
    [Fact]
    public void GetCurrentDirectoryBasic()
    {
        string currentDirectory = Storage.GetCurrentDirectory();
        currentDirectory.Should().NotBeNullOrWhiteSpace();
    }

    [Fact]
    public void SetCurrentDirectoryBasic()
    {
        string currentDirectory = Storage.GetCurrentDirectory();

        try
        {
            StoreHelper.ValidateStoreGetsUnauthorizedAccess(() =>
            {
                Storage.SetCurrentDirectory(@"C:\");
                Storage.GetCurrentDirectory().Should().Be(@"C:\");
            });
        }
        finally
        {
            if (currentDirectory != Storage.GetCurrentDirectory())
            {
                Storage.SetCurrentDirectory(currentDirectory);
                Storage.GetCurrentDirectory().Should().Be(currentDirectory);
            }
        }
    }

    [Fact]
    public void SetCurrentDirectoryShortName()
    {
        string currentDirectory = Storage.GetCurrentDirectory();

        try
        {
            StoreHelper.ValidateStoreGetsUnauthorizedAccess(() =>
            {
                Storage.SetCurrentDirectory(@"C:\PROGRA~1");
                Storage.GetCurrentDirectory().Should().Be(@"C:\PROGRA~1");
            });
        }
        finally
        {
            if (currentDirectory != Storage.GetCurrentDirectory())
            {
                Storage.SetCurrentDirectory(currentDirectory);
                Storage.GetCurrentDirectory().Should().Be(currentDirectory);
            }
        }
    }

    [Fact]
    public void SetCurrentDirectoryToNonExistant()
    {
        using (var cleaner = new TestFileCleaner())
        {
            string directoryPath = cleaner.GetTestPath();
            Action action = () => Storage.SetCurrentDirectory(directoryPath);
            action.Should().Throw<System.IO.FileNotFoundException>();
        }
    }

    [Fact]
    public void CreateDirectoryBasic()
    {
        using (var temp = new TestFileCleaner())
        {
            string directoryPath = temp.GetTestPath();
            Storage.CreateDirectory(directoryPath);
            using (var directory = Storage.CreateDirectoryHandle(directoryPath))
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
            Storage.CreateDirectory(directoryPath);
            using (var directory = Storage.CreateDirectoryHandle(directoryPath))
            {
                directory.IsInvalid.Should().BeFalse();
            }

            Storage.RemoveDirectory(directoryPath);

            Action action = () =>
            {
                using (var directory = Storage.CreateDirectoryHandle(directoryPath))
                {
                }
            };

            action.Should().Throw<System.IO.FileNotFoundException>();
        }
    }
}
